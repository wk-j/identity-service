using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Hello {

    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class HelloController : ControllerBase {
        [HttpGet]
        public string Hello() {
            return "Hello";
        }
    }

    [Route("api/[controller]/[action]")]
    [Authorize()]
    public class AController : ControllerBase {
        public string A() => "A";
    }
}