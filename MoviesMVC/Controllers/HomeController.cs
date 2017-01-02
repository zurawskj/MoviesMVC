using AutoMapper;
using Movies.Services;
using MoviesMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MoviesMVC.Controllers
{
    public class HomeController : Controller
    {
        private IMovieService _movieRepository;
        private IMapper _mapper;

        public HomeController(IMovieService movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            return View(_mapper.Map<List<MovieViewModel>>(await _movieRepository.GetAllAsync()));
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