using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGW.Portal.Models;

namespace SGW.Portal.Controllers
{
    public class ConfigurationController : Controller
    {
        //
        // GET: /Configuration/

        public ActionResult Index()
        {
			ViewBag.Message = "Preferencias, Workflows e outros.";
            return View();
        }


		public ActionResult Profile()
		{
			ViewBag.Message = "Meus Dados e Preferencias.";
			return View();
		}
	}
}
