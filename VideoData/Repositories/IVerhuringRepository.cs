using System;
using System.Collections.Generic;
using System.Text;
using VideoData.Models;

namespace VideoData.Repositories
{
    public interface IVerhuringRepository
    {
        void Add(Verhuring verhuring);
    }
}
