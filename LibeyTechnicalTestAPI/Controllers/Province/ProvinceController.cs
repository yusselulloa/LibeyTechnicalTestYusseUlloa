using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibeyTechnicalTestAPI.Controllers.Province
{
    [ApiController]
    [Route("[controller]")]
    public class ProvinceController : Controller
    {
        private readonly IProvinceAggregate _aggregate;
        public ProvinceController(IProvinceAggregate aggregate)
        {
            _aggregate = aggregate;
        }

        [HttpGet]
        [Route("{regionCode}")]
        public IActionResult GetAll(string regionCode)
        {
            var province = _aggregate.GetAll(regionCode);
            return Ok(province);
        }
    }
}
