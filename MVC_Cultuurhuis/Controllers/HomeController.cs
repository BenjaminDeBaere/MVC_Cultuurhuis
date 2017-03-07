using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data;
using System.Web.Mvc;
using MVC_Cultuurhuis.Models;
using MVC_Cultuurhuis.Services;

namespace MVC_Cultuurhuis.Controllers
{
    public class HomeController : Controller
    {
        private CultuurService db = new CultuurService();
        private List<VoorstellingReservatie> mandje = new List<VoorstellingReservatie>();               
        public ActionResult Index(int? id)
        {
            VoorstellingViewModel vm = new VoorstellingViewModel();
            vm.Genres = db.GetAllGenres();
            vm.Genre = db.GetGenre(id);
            vm.Voorstellingen = db.GetVoorstellingenPerGenre(id);
            return View(vm);           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult ReserveerForm(int id)
        {
            var aantalReserveringen = 1;
            if (Request.Cookies != null)
            {
                if(Request.Cookies["AantalGereserveerdePlaatsen"]==null)
                {
                    var userCookie = new HttpCookie("AantalGereserveerdePlaatsen", aantalReserveringen.ToString());
                    Response.Cookies.Add(userCookie);
                }
                aantalReserveringen = Convert.ToInt32(Request.Cookies["AantalGereserveerdePlaatsen"].Value);
                
            }
            ViewBag.aantalReserveringen = aantalReserveringen;
            var gevraagdeVoorstelling = db.GetVoorstellingById(id);
            return View(gevraagdeVoorstelling);
        }

        [HttpPost]
        public ActionResult ReservatieBevestigen(int id)
        {
            if (this.Session["mandje"] == null)
            {
                Session["mandje"] = mandje;
            }
            mandje = (List<VoorstellingReservatie>)Session["mandje"];
            int AantalPlaatsen = int.Parse(Request["AantalGereserveerdePlaatsen"]);
            var GevraagdeVoorstelling = db.GetVoorstellingById(id);
            var ReservatieItem = new VoorstellingReservatie(GevraagdeVoorstelling.VoorstellingsNr, GevraagdeVoorstelling.Titel, GevraagdeVoorstelling.Uitvoerders, GevraagdeVoorstelling.Datum, GevraagdeVoorstelling.Prijs, AantalPlaatsen);
            mandje.Add(ReservatieItem);
            Session["mandje"] = mandje;
            return View(mandje);

        }

        [HttpPost]
        public ActionResult ReservatieVerwijderen(int id)
        {
            mandje = (List<VoorstellingReservatie>)Session["mandje"];
            foreach (var item in mandje)
            {
                if (item.TeVerwijderen == true)
                {
                    mandje.Remove(item);
                    if (mandje.Count() == 0)
                    {
                        Session["mandje"] = null;
                    }
                    else
                    {
                        Session["mandje"] = mandje;
                    }
                }
            }
            return View("ReservatieBevestigen", mandje);
        }
        
  
    }
}