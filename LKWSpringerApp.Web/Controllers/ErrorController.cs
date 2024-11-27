using Microsoft.AspNetCore.Mvc;

namespace LKWSpringerApp.Web.Controllers
{
    [Route("Error")]
    public class ErrorController : Controller
    {
        [Route("{statusCode}")]
        public IActionResult HandleErrorCode(int statusCode)
        {
            if (statusCode == 404)
            {
                return View("NotFound");
            }
            return View("Error");
        }

        [Route("500")]
        public IActionResult ServerError()
        {
            return View("ServerError");
        }
    }
}

