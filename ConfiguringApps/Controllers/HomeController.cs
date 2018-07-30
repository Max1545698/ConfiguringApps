using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfiguringApps.Infrastructure;

namespace ConfiguringApps.Controllers
{
    public class HomeController : Controller
    {
        UptimeService uptime;
        public HomeController(UptimeService up)
        {
            uptime = up;
        }
        public ViewResult Index(bool thowException = false)
        {
            if (thowException)
            {
                throw new NullReferenceException();
            }
            return View(new Dictionary<string, string>
            {
                ["Message"] = "This is the Index action",
                ["Uptime"] = $"{uptime.Uptime}ms"
            });
        }

        public ViewResult Error() => View(nameof(Index), new Dictionary<string, string>
        {
            ["Message"] = "This is error action"
        });
    }

    //public ViewResult Index() => View(new Dictionary<string, string>
    //{
    //    ["Message"] = "This is the Index action",
    //    ["Uptime"] = $"{uptime.Uptime}ms"
    //});
}

