using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoData.Models;

namespace VideoData.Repositories
{
    public class SQLVideoRepository : IFilmRepository, IGenreRepository, IKlantRepository, IVerhuringRepository
    {
        private VideoDBContext videoDB;

        public SQLVideoRepository(VideoDBContext context)
        {
            videoDB = context;
        }

        public void Add(Verhuring verhuring)
        {
            videoDB.Verhuringen.Add(verhuring);
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            return videoDB.Genres;
        }

        public Film GetFilm(int id)
        {
            return videoDB.Films.Find(id);
        }

        public IEnumerable<Film> GetFilmsVoorGenre(int genreID)
        {
            return videoDB.Films
                .Where(f => f.GenreID == genreID);
        }

        public Genre GetGenre(int id)
        {
            return videoDB.Genres.Find(id);
        }

        public Klant GetKlant(string voornaam, int postcode)
        {
            return videoDB.Klanten.FirstOrDefault(
                k => k.Voornaam == voornaam && k.Postcode == postcode);
        }
    }
}
