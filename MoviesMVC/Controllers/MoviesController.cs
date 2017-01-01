using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using MoviesMVC.Models;
using Movies.Services;
using System.Collections.Generic;

namespace MoviesMVC.Controllers
{
    public class MoviesController : Controller
    {
        private IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        // GET: Movies
        public async Task<ActionResult> Index()
        {
            List<Movie> movies = await _movieRepository.GetAllAsync();
            List<MovieViewModel> movieViewModels = movies.ConvertAll(m => m.ToViewModel());
            return View(movieViewModels);
        }

        // GET: Movies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = await _movieRepository.GetByIdAsync(id);
            MovieViewModel movieViewModel = movie.ToViewModel();
            if (movieViewModel == null)
            {
                return HttpNotFound();
            }
            return View(movieViewModel);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Title,ReleaseDate,Genre,Price")] MovieViewModel movieViewModel)
        {
            if (ModelState.IsValid)
            {
                await _movieRepository.Add(movieViewModel.ToMovie());
                return RedirectToAction("Index");
            }

            return View(movieViewModel);
        }

        // GET: Movies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = await _movieRepository.GetByIdAsync(id);
            MovieViewModel movieViewModel = movie.ToViewModel();
            if (movieViewModel == null)
            {
                return HttpNotFound();
            }
            return View(movieViewModel);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Title,ReleaseDate,Genre,Price")] MovieViewModel movieViewModel)
        {
            if (ModelState.IsValid)
            {
                await _movieRepository.Edit(movieViewModel.ToMovie());
                return RedirectToAction("Index");
            }
            return View(movieViewModel);
        }

        // GET: Movies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = await _movieRepository.GetByIdAsync(id);
            MovieViewModel movieViewModel = movie.ToViewModel();
            if (movieViewModel == null)
            {
                return HttpNotFound();
            }
            return View(movieViewModel);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Movie movie = await _movieRepository.GetByIdAsync(id);
            await _movieRepository.Delete(movie);
            return RedirectToAction("Index");
        }
    }
}
