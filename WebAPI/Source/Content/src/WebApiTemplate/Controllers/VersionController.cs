using Microsoft.AspNetCore.Mvc;

namespace WebApiTemplate.Controllers
{
    [Route("api")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        private readonly IServiceProvider _services;

        public VersionController(IServiceProvider services)
        {
            _services = services;
        }

        /// <summary>
        /// Get Version
        /// </summary>
        /// <returns></returns>
        /// GET api/version
        [HttpGet]
        [Route("version")]
        public ActionResult GetVersion()
        {
            return Ok("1.0.0");
        }
    }
}
