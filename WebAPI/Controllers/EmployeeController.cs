using DataAccess.Models;

using Microsoft.AspNetCore.Mvc;

using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController(IGenericService<Employee> employeeRepository) : ControllerBase
    {
        private readonly IGenericService<Employee> _employeeRepository = employeeRepository;

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

            if (employee == null)
            {
                return NotFound();
            }

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

            if (existingEmployee == null)
            {
                return NotFound();
            }

            existingEmployee.UpdateProperties(employee,
                e => e.Name,
                e => e.Surname,
                e => e.Patronymic,
                e => e.Email,
                e => e.ParticipatedProjects,
                e => e.ManagedProjects);

            var updated = await _employeeRepository.UpdateAsync(existingEmployee);

            if (updated)
            {
                return NoContent();
            }

            return BadRequest("Failed to update the employee");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var existingEmployee = await _employeeRepository.GetAsync(id);

            if (existingEmployee == null)
            {
                return NotFound();
            }

            var deleted = await _employeeRepository.DeleteAsync(existingEmployee);

            if (deleted)
            {
                return NoContent();
            }

            return BadRequest("Failed to delete the employee");
        }
    }
}