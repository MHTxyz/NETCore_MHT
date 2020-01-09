using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.Controllers
{
    [Route("api/[controller]")]
    public class ConnectController : Controller
    {
        [HttpGet]
        public IActionResult token()
        {
            return Ok(1);
        }
    }
}