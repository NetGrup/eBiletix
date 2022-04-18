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
    }
}
