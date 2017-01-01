using Movies.Services;
using MoviesMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MoviesMVC.Controllers
{
    public class HomeController : Controller
    {
        private IMovieRepository _movieRepository;

        public HomeController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<ActionResult> Index()
        {
            List<Movie> movies = await _movieRepository.GetAllAsync();
            List<MovieViewModel> movieViewModels = movies.ConvertAll(m => m.ToViewModel());

            return View(movieViewModels);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}