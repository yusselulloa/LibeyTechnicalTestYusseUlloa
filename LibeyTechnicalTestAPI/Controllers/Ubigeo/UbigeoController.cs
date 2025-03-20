using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibeyTechnicalTestAPI.Controllers.Ubigeo
{
    [ApiController]
    [Route("[controller]")]
    public class UbigeoController : Controller
    {
        private readonly IUbigeoAggregate _aggregate;
        public UbigeoController(IUbigeoAggregate aggregate)
        {
            _aggregate = aggregate;
        }

        [HttpGet]
        [Route("{provinceCode}")]
        public IActionResult GetAll(string provinceCode)
        {
            var ubigeo = _aggregate.GetAll(provinceCode);
            return Ok(ubigeo);
        }
    }
}
