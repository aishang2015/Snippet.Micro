using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Snippet.Micro.Identity.Models;
using System.Diagnostics;

namespace Snippet.Micro.Identity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy([FromServices] ConfigurationDbContext configurationDb,
            [FromServices] PersistedGrantDbContext persistedGrantDb)
        {
            configurationDb.Database.EnsureCreated();
            persistedGrantDb.Database.EnsureCreated();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}