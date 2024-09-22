using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UescColcicAPI.Core;
using UescColcicAPI.Services.BD.Interfaces;
using System;
using UescColcicAPI.Services.ViewModel;

namespace UescColcicAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsCRUD _projectsCRUD;

        public ProjectsController(IProjectsCRUD projectsCRUD)
        {
            _projectsCRUD = projectsCRUD;
        }

        [HttpGet(Name = "GetProjects")]
        public IEnumerable<Project> Get()
        {
            return _projectsCRUD.ReadAll();
        }

        [HttpGet("{id}", Name = "GetProject")]
        public ActionResult<Project> Get(int id)
        {
            try
            {
                var project = _projectsCRUD.ReadById(id);
                if (project == null)
                {
                    return NotFound($"Project with ID {id} not found.");
                }
                return Ok(project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost(Name = "CreateProject")]
        public ActionResult<Project> Post([FromBody] ProjectViewModel projectViewModel)
        {
            try
            {
                var project = new Project
                {
                    Title = projectViewModel.Title,
                    Description = projectViewModel.Description,
                    Type = projectViewModel.Type,
                    StartDate = projectViewModel.StartDate,
                    EndDate = projectViewModel.EndDate
                };

                _projectsCRUD.Create(project);
                return CreatedAtRoute("GetProject", new { id = project.ProjectId }, project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        

        [HttpPut("{id}", Name = "UpdateProject")]
        public ActionResult Update(int id, [FromBody] ProjectViewModel projectViewModel)
        {
            try
            {
                var existingProject = _projectsCRUD.ReadById(id);
                if (existingProject == null)
                {
                    return NotFound($"Project with ID {id} not found.");
                }

                // Atualiza os campos
                existingProject.Title = projectViewModel.Title;
                existingProject.Description = projectViewModel.Description;
                existingProject.Type = projectViewModel.Type;
                existingProject.StartDate = projectViewModel.StartDate;
                existingProject.EndDate = projectViewModel.EndDate;

                _projectsCRUD.Update(existingProject);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}", Name = "DeleteProject")]
        public ActionResult Delete(int id)
        {
            try
            {
                var project = _projectsCRUD.ReadById(id);
                if (project == null)
                {
                    return NotFound($"Project with ID {id} not found.");
                }

                _projectsCRUD.Delete(project);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
