using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace VideoData.Models
{
    public class VideoDBContext : DbContext
    {
        public VideoDBContext()
        {

        }

        public VideoDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Film> Films { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Klant> Klanten { get; set; }
        public DbSet<Verhuring> Verhuringen { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(
                configuration.GetConnectionString("VideoConnection"));
        }
    }
}
