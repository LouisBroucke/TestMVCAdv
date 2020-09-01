using System;
using System.Collections.Generic;
using System.Text;
using VideoData.Models;

namespace VideoData.Repositories
{
    public interface IGenreRepository
    {
        Genre GetGenre(int id);
        IEnumerable<Genre> GetAllGenres();
    }
}
