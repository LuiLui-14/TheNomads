using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Working_With_API.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _config;

        public AccountController(ILogger<AccountController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult AccountPage()
        {
            return View();
        }
    }
}

