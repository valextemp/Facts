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

            //var fact = new Fact() { Content="Fact 2",CreatedBy="Valex"};
            var fact1=new Fact() {Content="fact77", Tags = new List<Tag>() { new Tag() { Name = "tag1" }, new() { Name = "tag2" } } };
            var fact2 = new Fact() { Content = "fact88", Tags = new List<Tag>() { new Tag() { Name = "tag3" }, new() { Name = "tag4" } } };

            context.AddRange(fact1, fact2);
            //context.Facts.Add(fact);
            context.SaveChanges();

            if (context.SaveChangesResult.IsOk)
            {
                tran.Commit();
                return View();
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
