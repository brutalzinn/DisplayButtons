using System;
using Microsoft.AspNetCore.Mvc;

namespace MyWinFormsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            string text = "testando o abacatão!";
          
            return text;
        }

        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
          
            return Ok();
        }
    }
}