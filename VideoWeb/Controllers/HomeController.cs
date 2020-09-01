using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VideoData.Repositories;
using VideoWeb.Models;

namespace VideoWeb.Controllers
{
    public class HomeController : Controller
    {
        private SQLVideoRepository repository;
        private WinkelmandModel winkelmand = new WinkelmandModel();

        public HomeController(SQLVideoRepository repository)
        {
            this.repository = repository;
        }

        //Index
        public IActionResult Index()
        {
            Response.Cookies.Delete("naamBezoeker");

            return View(new KlantViewModel());
        }

        //Aanmelden en doorverwijzen naar genre
        public IActionResult Aanmelden(KlantViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                var klant = repository.GetKlant(viewModel.Naam, viewModel.Postcode);

                Response.Cookies.Append("naamBezoeker", klant.Voornaam + " " + klant.Naam);

                return RedirectToAction("Genre");
            }
            else
            {
                return View("Index", viewModel);
            }
        }

        //Kies genre
        public IActionResult Genre()
        {
            return View(repository.GetAllGenres().ToList());
        }

        //Kies film
        public IActionResult KiezenFilm(int id)
        {
            var genre = repository.GetGenre(id);

            ViewBag.GenreNaam = genre.GenreNaam;

            return View(repository.GetFilmsVoorGenre(id));
        }

        //Toevoegen film
        public IActionResult ToevoegenFilm(int? id)
        {
            if (id != null)
            {
                var film = repository.GetFilm((int)id);

                if (!winkelmand.Films.ContainsKey(film.Titel))
                {
                    winkelmand.Films.Add(film.Titel, film);
                }
            } 

            return View(winkelmand);
        }

        //Verwijder film
        public IActionResult VerwijderFilm(string titel)
        {
            return View((object)titel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
