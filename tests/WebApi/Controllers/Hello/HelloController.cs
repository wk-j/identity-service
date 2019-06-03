using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Hello {
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class HelloController : ControllerBase {
        [HttpGet]
        public string Hello() {
            return "Hi";
        }
    }
}