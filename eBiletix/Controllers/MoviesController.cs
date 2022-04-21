using eBiletix.Data;
using eBiletix.Data.Services;
using eBiletix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBiletix.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;

        public MoviesController(IMoviesService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync(n => n.Cinema);
            return View(data);
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allMovies.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                return View("Index", filteredResult);
            }

            return View("Index", allMovies);
        }

        public async Task<IActionResult> Details(int id)
        {
            var detail = await _service.GetMovieByIdAsync(id);
            return View(detail);
        }

        public async Task<IActionResult> Create()
        {
            var moviesDropDownsData = await _service.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(moviesDropDownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(moviesDropDownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(moviesDropDownsData.Actors, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var moviesDropDownsData = await _service.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(moviesDropDownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(moviesDropDownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(moviesDropDownsData.Actors, "Id", "FullName");

                return View(movie);

            }


            await _service.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
   
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            if (movieDetails == null) return View("NotFound");

            var response = new NewMovieVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                ImageURL = movieDetails.ImageURL,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList()

            };

            var moviesDropDownsData = await _service.GetNewMovieDropdownsValues();
            ViewBag.Cinemas = new SelectList(moviesDropDownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(moviesDropDownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(moviesDropDownsData.Actors, "Id", "FullName");
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM movie)
        {
            if (id != movie.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var moviesDropDownsData = await _service.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(moviesDropDownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(moviesDropDownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(moviesDropDownsData.Actors, "Id", "FullName");

                return View(movie);

            }


            await _service.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
    }
}
