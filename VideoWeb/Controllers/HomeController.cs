using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using VideoData.Models;
using VideoData.Repositories;
using VideoWeb.Models;

namespace VideoWeb.Controllers
{
    public class HomeController : Controller
    {
        private SQLVideoRepository repository;

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

                Response.Cookies.Append("klant", JsonConvert.SerializeObject(klant));

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

            Response.Cookies.Append("genreID", id.ToString());

            ViewBag.GenreNaam = genre.GenreNaam;

            return View(repository.GetFilmsVoorGenre(id));
        }
        
        //Toevoegen film aan winkelmand
        public IActionResult ToevoegenFilmAanWinkelmand(int id)
        {
            var film = repository.GetFilm(id);

            if (Request.Cookies["winkelmand"] == null)
            {
                Dictionary<string, Film> films = new Dictionary<string, Film>();

                films.Add(film.Titel ,film);

                Response.Cookies.Append("winkelmand", JsonConvert.SerializeObject(films));
            }
            else
            {
                var films = JsonConvert.DeserializeObject<Dictionary<string, Film>>(
                    Request.Cookies["winkelmand"]);

                if (!films.ContainsKey(film.Titel))
                {
                    films.Add(film.Titel, film);
                }

                Response.Cookies.Append("winkelmand", JsonConvert.SerializeObject(films));
            }

            return RedirectToAction("Winkelmand");
        }

        //Winkelmandje
        public IActionResult Winkelmand()
        {
            var films = JsonConvert.DeserializeObject<Dictionary<string, Film>>(
                    Request.Cookies["winkelmand"]);

            ViewBag.GenreID = Request.Cookies["genreID"];

            return View(films);
        }

        //Film verwijderen uit winkelmandje
        public IActionResult FilmVerwijderen(int id)
        {
            var film = repository.GetFilm(id);

            return View(film);
        }

        //Verwijdering doorvoeren
        public IActionResult VerwijderingDoorvoeren(int id)
        {
            var titel = repository.GetFilm(id).Titel;

            var films = JsonConvert.DeserializeObject<Dictionary<string, Film>>(
                    Request.Cookies["winkelmand"]);

            films.Remove(titel);

            Response.Cookies.Append("winkelmand", JsonConvert.SerializeObject(films));

            return RedirectToAction("Winkelmand");
        }

        //Afrekenen
        public IActionResult Afrekenen()
        {
            decimal totaal = 0m;

            var films = JsonConvert.DeserializeObject<Dictionary<string, Film>>(
                    Request.Cookies["winkelmand"]);            

            var klant = JsonConvert.DeserializeObject<Klant>(
                    Request.Cookies["klant"]);

            foreach (var film in films.Values)
            {
                Verhuring verhuring = new Verhuring
                {
                    FilmID = film.FilmID,
                    KlantID = klant.KlantID,
                    VerhuurDatum = DateTime.Today
                };

                totaal += film.Prijs;

                repository.Add(verhuring);
                repository.PasVoorraadAan(film);
            }

            ViewBag.Naam = klant.Naam;
            ViewBag.Straat = klant.Straat_Nr;
            ViewBag.Plaats = klant.Gemeente;
            ViewBag.TotaalPrijs = totaal;

            return View(films);
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
