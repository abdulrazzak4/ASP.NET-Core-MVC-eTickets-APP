using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webApp.Data.Base;
using webApp.Data.ViewModels;
using webApp.Models;

namespace webApp.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>,IMoviesService
    {
        public readonly AppDbContext _context;
        public MoviesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewMovieAsync(NewMovieVM data)
        {
            var newMovie =new Movie(){
                Name =data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                CinemaId = data.CinemaId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId
            };
            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            //add movie actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie(){
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var moviedetails =await _context.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);
            return moviedetails;    
        }

        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
        {
            var respons =new NewMovieDropdownsVM(){
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(), 
                Producers = await _context.Producers.OrderBy( n => n.FullName).ToListAsync()
            };
            return respons;
        }

        public async Task UpdateMovieAsync(NewMovieVM data)
        {
            var dbmovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);

            if(dbmovie !=null){
                dbmovie.Name =data.Name;
                dbmovie.Description = data.Description;
                dbmovie.Price = data.Price;
                dbmovie.ImageURL = data.ImageURL;
                dbmovie.CinemaId = data.CinemaId;
                dbmovie.StartDate = data.StartDate;
                dbmovie.EndDate = data.EndDate;
                dbmovie.MovieCategory = data.MovieCategory;
                dbmovie.ProducerId = data.ProducerId;
                await _context.SaveChangesAsync();

            }
            //REMOVE existing actors'
            var existingActorsDb = _context.Actors_Movies.Where(n => n.MovieId == data.Id).ToList();
            _context.Actors_Movies.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();
            
            //add movie actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie(){
                    MovieId = data.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }
    }
}