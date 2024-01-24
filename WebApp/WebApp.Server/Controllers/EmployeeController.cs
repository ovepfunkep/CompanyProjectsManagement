using DataAccess.Models;

using Microsoft.AspNetCore.Mvc;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController(ILinkedRepository<Employee> employeeRepository) : ControllerBase
    {
        private readonly ILinkedRepository<Employee> _employeeRepository = employeeRepository;

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeRepository.GetAsync();

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _employeeRepository.GetAsync(id);

            if (employee == null) return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            await _employeeRepository.AddAsync(employee);
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.ID }, employee);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
        {
            var existingEmployee = await _employeeRepository.GetAsync(employee.ID);

            if (existingEmployee == null) return NotFound();

            existingEmployee.CopyProperties(employee,
                e => e.Name,
                e => e.Surname,
                e => e.Patronymic,
                e => e.Email,
                e => e.ParticipatedProjects,
                e => e.ManagedProjects);

            try
            {
                await _employeeRepository.UpdateAsync(existingEmployee);
                return CreatedAtAction(nameof(GetEmployee), new { id = employee.ID }, existingEmployee);
            } catch (Exception ex)
            {
                return BadRequest($"Failed to update the employee.\n{ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id, [FromQuery] bool cascade = false)
        {
            var existingEmployee = await _employeeRepository.GetAsync(id);

            if (existingEmployee == null) return NotFound();

            try
            {
                if (cascade) await _employeeRepository.CascadeDeleteAsync(id);
                else await _employeeRepository.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete the employee.\n{ex.Message}");
            }
        }
    }
}