using System;
using System.Collections.Generic;
using System.Text;
using VideoData.Models;

namespace VideoData.Repositories
{
    public interface IFilmRepository
    {
        Film GetFilm(int id);
        IEnumerable<Film> GetFilmsVoorGenre(int genreID);
        void PasVoorraadAan(Film film);
    }
}
