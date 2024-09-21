using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UescColcicAPI.Services.BD.Interfaces;
using UescColcicAPI.Core;
using UescColcicAPI.Services.ViewModels; 
using System.Collections.Generic;
using System;

namespace UescColcicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorsCRUD _professorsCRUD;

        public ProfessorController(IProfessorsCRUD professorsCRUD)
        {
            _professorsCRUD = professorsCRUD;
        }
        [HttpGet]
        public IEnumerable<Professor> Get()
        {
            return _professorsCRUD.ReadAll();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok($"Get professor with ID {id}");
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok("Create a new professor");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            return Ok($"Update professor with ID {id}");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Delete professor with ID {id}");
        }
    }
}
