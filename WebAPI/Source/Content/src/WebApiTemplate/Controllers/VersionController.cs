using Microsoft.AspNetCore.Mvc;

namespace WebApiTemplate.Controllers
{
    [Route("api")]
    [ApiController]
    public class VersionController : ControllerBase
    {
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
