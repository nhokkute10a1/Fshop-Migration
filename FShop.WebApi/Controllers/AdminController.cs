using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FShop.WebApi.Controllers
{
    [RoutePrefix("AdminDasboard")]
    public class AdminController : Controller
    {
        [Route("IndexAdmin")]
     //   [ActionName("abc")]
        public ActionResult Index()
        {
            return View();
        }
    }
}