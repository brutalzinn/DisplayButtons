﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

using Microsoft.AspNetCore.Mvc;
using _666.Pages;

namespace myApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndexController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}