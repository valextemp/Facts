using Calabonga.Facts.Web.Data;
using Calabonga.Facts.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Calabonga.Facts.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index([FromServices]ApplicationDbContext context)
        {
            using var tran=context.Database.BeginTransaction();

            var fact = new Fact() { Content="Fact 2",CreatedBy="Valex"};
            context.Facts.Add(fact);
            context.SaveChanges();

            if (context.SaveChangesResult.IsOk)
            {
                tran.Commit();
                return View(fact);
            }

            tran.Rollback();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
