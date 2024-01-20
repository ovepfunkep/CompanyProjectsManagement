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
            var addedEmployee = await _employeeRepository.AddAsync(employee);
            return CreatedAtAction(nameof(GetEmployee), new { id = addedEmployee.ID }, addedEmployee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            var existingEmployee = await _employeeRepository.GetAsync(id);

            if (existingEmployee == null) return NotFound();

            existingEmployee.CopyProperties(employee,
                e => e.Name,
                e => e.Surname,
                e => e.Patronymic,
                e => e.Email,
                e => e.ParticipatedProjects,
                e => e.ManagedProjects);

            var updated = await _employeeRepository.UpdateAsync(existingEmployee);

            if (updated) return Ok(); 

            return BadRequest("Failed to update the employee");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id, [FromQuery] bool cascade = false)
        {
            var existingEmployee = await _employeeRepository.GetAsync(id);

            if (existingEmployee == null) return NotFound(); 

            bool deleted;

            if (cascade) deleted = await _employeeRepository.CascadeDeleteAsync(existingEmployee);
            else deleted = await _employeeRepository.DeleteAsync(existingEmployee);

            if (deleted) return Ok();

            return BadRequest("Failed to delete the employee");
        }
    }
}