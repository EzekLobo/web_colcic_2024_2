using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UescColcicAPI.Services.BD.Interfaces;
using UescColcicAPI.Core;
using UescColcicAPI.Services.ViewModels; // Adiciona a referência para o ViewModel
using System.Collections.Generic;
using System;

namespace UescColcicAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsCRUD _studentsCRUD;

        public StudentsController(IStudentsCRUD studentsCRUD)
        {
            _studentsCRUD = studentsCRUD;
        }

        [HttpGet(Name = "GetStudents")]
        public IEnumerable<Student> Get()
        {
            return _studentsCRUD.ReadAll();
        }

        [HttpGet("{id}", Name = "GetStudent")]
        public ActionResult<Student> Get(int id)
        {
            try
            {
                var student = _studentsCRUD.ReadById(id);
                if (student == null)
                {
                    return NotFound($"Student with ID {id} not found.");
                }
                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Criação de um novo estudante usando a StudentViewModel
        [HttpPost(Name = "CreateStudent")]
        public ActionResult<Student> Post([FromBody] StudentViewModel studentViewModel)
        {
            try
            {
                var student = new Student
                {
                    Name = studentViewModel.Name,
                    Email = studentViewModel.Email
                };
                _studentsCRUD.Create(student); // O ID é gerado automaticamente

                return CreatedAtRoute("GetStudent", new { id = student.StudentId }, student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Atualização de estudante usando a StudentViewModel, sem expor o ID
        [HttpPut("{id}", Name = "UpdateStudent")]
        public ActionResult Update(int id, [FromBody] StudentViewModel studentViewModel)
        {
            try
            {
                var existingStudent = _studentsCRUD.ReadById(id);
                if (existingStudent == null)
                {
                    return NotFound($"Student with ID {id} not found.");
                }

                // Atualiza os campos exceto o ID
                existingStudent.Name = studentViewModel.Name;
                existingStudent.Email = studentViewModel.Email;

                _studentsCRUD.Update(existingStudent);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}", Name = "DeleteStudent")]
        public ActionResult Delete(int id)
        {
            try
            {
                var student = _studentsCRUD.ReadById(id);
                if (student == null)
                {
                    return NotFound($"Student with ID {id} not found.");
                }

                _studentsCRUD.Delete(student);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
