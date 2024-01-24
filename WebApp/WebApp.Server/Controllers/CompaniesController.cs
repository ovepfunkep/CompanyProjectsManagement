using DataAccess.Models;

using Microsoft.AspNetCore.Mvc;

using WebAPI.Services.Implementations;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController(ILinkedRepository<Company> companyRepository) : ControllerBase
    {
        private readonly ILinkedRepository<Company> _companyRepository = companyRepository;

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _companyRepository.GetAsync();

            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            var company = await _companyRepository.GetAsync(id);

            if (company == null) return NotFound();

            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> AddCompany([FromBody] Company company)
        {
            await _companyRepository.AddAsync(company);
            return CreatedAtAction(nameof(GetCompany), new { id = company.ID }, company);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCompany([FromBody] Company company)
        {
            var existingCompany = await _companyRepository.GetAsync(company.ID);

            if (existingCompany == null) return NotFound();

            existingCompany.CopyProperties(company,
                c => c.Name,
                c => c.Employees,
                c => c.OrderedProjects,
                c => c.MadenProjects);


            try
            {
                await _companyRepository.UpdateAsync(existingCompany);
                return CreatedAtAction(nameof(GetCompany), new { id = company.ID }, company);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to update the company.\n{ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id, [FromQuery] bool cascade = false)
        {
            var existingCompany = await _companyRepository.GetAsync(id);

            if (existingCompany == null) return NotFound();

            try
            {
                if (cascade) await _companyRepository.CascadeDeleteAsync(id);
                else await _companyRepository.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete the company.\n{ex.Message}");
            }
        }
    }
}