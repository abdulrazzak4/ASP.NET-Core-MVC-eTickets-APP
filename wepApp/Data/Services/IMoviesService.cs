using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApp.Models;
using webApp.Data.Base;
using webApp.Data.ViewModels;

namespace webApp.Data.Services
{
    public interface IMoviesService:IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues();
        Task UpdateMovieAsync(NewMovieVM data);
        
        Task AddNewMovieAsync(NewMovieVM data);
    }
}