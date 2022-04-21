using eBiletix.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBiletix.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Actor_Movie>().HasKey(ao => new 
            {
                ao.ActorId,
                ao.MovieId
            });

            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Movie).WithMany(ao => ao.Actors_Movies).HasForeignKey(m => m.MovieId);


            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Actor).WithMany(ao => ao.Actors_Movies).HasForeignKey(m => m.ActorId);

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor_Movie> Actors_Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Producer> Producers { get; set; }


        //order related tables

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
