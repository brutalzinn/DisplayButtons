using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

using Microsoft.AspNetCore.Mvc;


namespace myApp.Controllers
{
    [Route("/")]
    [ApiController]
    public class IndexController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}