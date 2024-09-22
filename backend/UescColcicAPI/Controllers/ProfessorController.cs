using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UescColcicAPI.Services.BD.Interfaces;
using UescColcicAPI.Core;
using UescColcicAPI.Services.ViewModels; 
using System.Collections.Generic;
using System;
using UescColcicAPI.Services.ViewModel;

namespace UescColcicAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorsController : ControllerBase
    {
        private readonly IProfessorsCRUD _professorsCRUD;

        public ProfessorsController(IProfessorsCRUD professorsCRUD)
        {
            _professorsCRUD = professorsCRUD;
        }

        [HttpGet(Name = "GetProfessors")]
        public IEnumerable<Professor> Get()
        {
            return _professorsCRUD.ReadAll();
        }

        [HttpGet("{id}", Name = "GetProfessor")]
        public ActionResult<Professor> Get(int id)
        {
            try
            {
                var professor = _professorsCRUD.ReadById(id);
                if (professor == null)
                {
                    return NotFound($"Professor with ID {id} not found.");
                }
                return Ok(professor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost(Name = "CreateProfessor")]
        public ActionResult<Professor> Post([FromBody] ProfessorViewModel professorViewModel)
        {
            try
            {
                var professor = new Professor
                {
                    Name = professorViewModel.Name,
                    Email = professorViewModel.Email,
                    Department = professorViewModel.Department,
                    Bio = professorViewModel.Bio
                };
                _professorsCRUD.Create(professor);

                return CreatedAtRoute("GetProfessor", new { id = professor.ProfessorId }, professor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}", Name = "UpdateProfessor")]
        public ActionResult Update(int id, [FromBody] ProfessorViewModel professorViewModel)
        {
            try
            {
                var existingProfessor = _professorsCRUD.ReadById(id);
                if (existingProfessor == null)
                {
                    return NotFound($"Professor with ID {id} not found.");
                }

                // Atualiza os campos
                existingProfessor.Name = professorViewModel.Name;
                existingProfessor.Email = professorViewModel.Email;
                existingProfessor.Department = professorViewModel.Department;
                existingProfessor.Bio = professorViewModel.Bio;

                _professorsCRUD.Update(existingProfessor);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}", Name = "DeleteProfessor")]
        public ActionResult Delete(int id)
        {
            try
            {
                var professor = _professorsCRUD.ReadById(id);
                if (professor == null)
                {
                    return NotFound($"Professor with ID {id} not found.");
                }

                _professorsCRUD.Delete(professor);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
