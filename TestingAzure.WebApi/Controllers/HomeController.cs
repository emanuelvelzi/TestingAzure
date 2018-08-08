﻿using System.Web.Mvc;

namespace TestingAzure.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return new RedirectResult("~/swagger/ui/index");
        }
    }
}
