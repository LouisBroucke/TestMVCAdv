using System;
using System.Collections.Generic;
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

        public IEnumerable<Film> GetAllFilms()
        {
            return videoDB.Films;
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            return videoDB.Genres;
        }

        public Film GetFilm(int id)
        {
            return videoDB.Films.Find(id);
        }

        public Genre GetGenre(int id)
        {
            return videoDB.Genres.Find(id);
        }

        public Klant GetKlant(int id)
        {
            return videoDB.Klanten.Find(id);
        }
    }
}
