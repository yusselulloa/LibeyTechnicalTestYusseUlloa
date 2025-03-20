using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Infrastructure;
using Microsoft.AspNetCore.Mvc;
namespace LibeyTechnicalTestAPI.Controllers.LibeyUser
{
    [ApiController]
    [Route("[controller]")]
    public class LibeyUserController : Controller
    {
        private readonly ILibeyUserAggregate _aggregate;
        public LibeyUserController(ILibeyUserAggregate aggregate)
        {
            _aggregate = aggregate;
        }
        [HttpGet]
        [Route("{documentNumber}")]
        public IActionResult FindResponse(string documentNumber)
        {
            var row = _aggregate.FindResponse(documentNumber);
            return Ok(row);
        }
        [HttpPost]       
        public IActionResult Create(UserUpdateorCreateCommand command)
        {
            if (command == null || string.IsNullOrEmpty(command.DocumentNumber))
            {
                return BadRequest("El número de documento es obligatorio");
            }
            try
            {
                _aggregate.Create(command);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }           
        }
        [HttpGet]     
        public IActionResult GetAll()
        {
            var users = _aggregate.GetAll();
            return Ok(users);
        }

        [HttpDelete]
        [Route("{documentNumber}")]
        public IActionResult Delete(string documentNumber)
        {
            var usuario = _aggregate.Delete(documentNumber);
            if(usuario == null)
                return NotFound();

            return NoContent();
        }

        [HttpPut]
        public IActionResult Update(LibeyUserResponse libeyUser)
        {
            var usuario = _aggregate.Update(libeyUser);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }
    }
}