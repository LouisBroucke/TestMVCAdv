using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoWeb.ViewComponents
{
    public class Verwelkoming : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            string verwelkoming = "Welkom ! Meld je aan om te kunnen huren!";

            if (Request.Cookies != null)
            {
                if (Request.Cookies["naamBezoeker"] != null)
                {
                    verwelkoming = "Welkom, " + Request.Cookies["naamBezoeker"] + "!";
                }
            }

            return View((object)verwelkoming);
        }
    }
}
