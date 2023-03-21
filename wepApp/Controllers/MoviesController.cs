using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using webApp.Data;
using webApp.Data.Services;
using webApp.Models;

namespace webApp.Controllers
{
    //[Route("[controller]")]
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;
        public MoviesController(IMoviesService service)
        {
            _service = service ;
        }
        public async Task<IActionResult> Index()
        {
            var listMovie =await  _service.GetAllAsync(n => n.Cinema);
            return View(listMovie);
        }
        public async Task<IActionResult> Filter(string searchString)
        {
            var listMovie =await  _service.GetAllAsync(n => n.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filterResult = listMovie.Where(n=> n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
                return View("Index",filterResult);
            }
            return View("Index",listMovie);
        }
        //GET: movies/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var MovieDetails =await  _service.GetMovieByIdAsync(id);
            return View(MovieDetails);
        }
        //GET: movies/Create
        public async Task<IActionResult> Create(int id)
        {
            var MovieDropdowns = await _service.GetNewMovieDropdownsValues();
            ViewBag.Cinemas = new SelectList(MovieDropdowns.Cinemas,"Id","Name");
            ViewBag.Producers = new SelectList(MovieDropdowns.Producers,"Id","FullName");
            ViewBag.Actors = new SelectList(MovieDropdowns.Actors,"Id","FullName");

            return View();
        }     
        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if(!ModelState.IsValid){
                var MovieDropdowns = await _service.GetNewMovieDropdownsValues();
                ViewBag.Cinemas = new SelectList(MovieDropdowns.Cinemas,"Id","Name");
                ViewBag.Producers = new SelectList(MovieDropdowns.Producers,"Id","FullName");
                ViewBag.Actors = new SelectList(MovieDropdowns.Actors,"Id","FullName");
                return View();
            }
            await _service.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        //GET: movies/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var moviedetails = await _service.GetMovieByIdAsync(id);
            if (moviedetails == null) return View("NotFound");

            var respons = new NewMovieVM()
            {
                Id = moviedetails.Id,
                Name = moviedetails.Name,
                Description = moviedetails.Description,
                Price = moviedetails.Price,
                StartDate = moviedetails.StartDate,
                EndDate = moviedetails.EndDate,
                ImageURL = moviedetails.ImageURL,
                MovieCategory = moviedetails.MovieCategory,
                CinemaId = moviedetails.CinemaId,
                ProducerId= moviedetails.ProducerId,
                ActorIds = moviedetails.Actors_Movies.Select(n => n.ActorId).ToList(),
            };

            var MovieDropdowns = await _service.GetNewMovieDropdownsValues();
            
            ViewBag.Cinemas = new SelectList(MovieDropdowns.Cinemas,"Id","Name");
            ViewBag.Producers = new SelectList(MovieDropdowns.Producers,"Id","FullName");
            ViewBag.Actors = new SelectList(MovieDropdowns.Actors,"Id","FullName");

            return View(respons);
        }     
        [HttpPost]
        public async Task<IActionResult> Edit(int id,NewMovieVM movie)
        {
            if(id != movie.Id) return View("NotFound");

            if(!ModelState.IsValid){
                var MovieDropdowns = await _service.GetNewMovieDropdownsValues();
                ViewBag.Cinemas = new SelectList(MovieDropdowns.Cinemas,"Id","Name");
                ViewBag.Producers = new SelectList(MovieDropdowns.Producers,"Id","FullName");
                ViewBag.Actors = new SelectList(MovieDropdowns.Actors,"Id","FullName");
                return View();
            }
            await _service.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }     
    }
}