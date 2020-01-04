using Microsoft.AspNetCore.Mvc;

namespace PersonalFinance
{
    [ApiController]
    [Route("[controller]")]
    public class InfoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index() => Ok("Personal Finance API");
    }
}