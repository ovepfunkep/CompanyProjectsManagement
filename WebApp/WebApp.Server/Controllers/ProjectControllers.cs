﻿using DataAccess.Models;

using Microsoft.AspNetCore.Mvc;

using WebAPI.Services.Implementations;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(IGenericRepository<Project> projectRepository) : ControllerBase
    {
        private readonly IGenericRepository<Project> _projectRepository = projectRepository;

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _projectRepository.GetAsync();

            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await _projectRepository.GetAsync(id);

            if (project == null) return NotFound();

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> AddProject([FromBody] Project project)
        {
            await _projectRepository.AddAsync(project);
            return CreatedAtAction(nameof(GetProject), new { id = project.ID }, project);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProject([FromBody] Project project)
        {
            var existingProject = await _projectRepository.GetAsync(project.ID);

            if (existingProject == null) return NotFound();

            existingProject.CopyProperties(project,
                p => p.Name,
                p => p.DateStarted,
                p => p.DateEnded,
                p => p.Priority,
                p => p.ManagerID,
                p => p.CustomerCompanyID,
                p => p.ContractorCompanyID,
                p => p.Employees);

            try
            {
                await _projectRepository.UpdateAsync(existingProject);
                return CreatedAtAction(nameof(GetProject), new { id = project.ID }, project);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to update the project.\n{ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var existingProject = await _projectRepository.GetAsync(id);

            if (existingProject == null) return NotFound();

            try
            {
                await _projectRepository.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete the project.\n{ex.Message}");
            }
        }
    }
}