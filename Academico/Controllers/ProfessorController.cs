using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Professor.DAOs;
using Professor.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Professor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        // GET: api/<ProfessorController>
        [HttpGet]
        public IEnumerable<ProfessorClass> Get()
        {
            return (new DAOprofessor()).SelectAll();
        }

        // GET api/<ProfessorController>/5
        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            return new ObjectResult((new DAOprofessor()).SelectId(id));
        }

        // GET api/<ProfessorController>/nome=A
        [HttpGet("nome={nome}")]
        public IEnumerable<ProfessorClass> GetNome(string nome)
        {
            return (new DAOprofessor()).SelectNome(nome);
        }

        // POST api/<ProfessorController>
        [HttpPost]
        public IActionResult Post([FromBody] ProfessorClass obj)
        {
            try
            {
                (new DAOprofessor()).Insert(obj);
                return CreatedAtAction(nameof(GetId), new { id = obj.Id }, obj);
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT api/<ProfessorController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProfessorClass obj)
        {
            var dao = new DAOprofessor();
            try
            {
                dao.Update(obj);
                return NoContent();
            }
            catch
            {
                if (dao.SelectId(id) == null)
                    return NotFound();

                return BadRequest();
            }
        }

        // DELETE api/<ProfessorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                (new DAOprofessor()).Delete(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
