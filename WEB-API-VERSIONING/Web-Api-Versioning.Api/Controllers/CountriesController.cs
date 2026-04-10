using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_Api_Versioning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        [HttpGet]

        public IActionResult GetV1()
        {
            var countriesdomain=
        }

    }
}
