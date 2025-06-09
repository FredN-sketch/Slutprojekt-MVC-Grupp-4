using Microsoft.AspNetCore.Mvc;

namespace ErrorHandling.Web.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet("error/exception")]
        public IActionResult ServerError()
        {
            return View();
        }

        [HttpGet("error/http/{code}")]
        public IActionResult HTTPError(int code)
        {
            return View(code);
        }
    }
}
