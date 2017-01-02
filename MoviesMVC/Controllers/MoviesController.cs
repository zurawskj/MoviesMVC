using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using MoviesMVC.Models;
using System.Collections.Generic;
using MoviesMVC.ViewModels;
using Movies.Services;
using Movies.Services.DomainModels;
using AutoMapper;

namespace MoviesMVC.Controllers
{
    public class MoviesController : Controller
    {
        private IMovieService _movieService;
        private IMapper _mapper;

        public MoviesController(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }

        // GET: Movies
        public async Task<ActionResult> Index()
        {
            return View(_mapper.Map<List<MovieViewModel>>(await _movieService.GetAllAsync()));
        }

        // GET: Movies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieViewModel movieViewModel = _mapper.Map<MovieViewModel>(await _movieService.GetByIdAsync(id));
            if (movieViewModel == null)
            {
                return HttpNotFound();
            }
            return View(movieViewModel);
        }

        // GET: Movies/Create
        public async Task<ActionResult> Create()
        {
            CreateMovieViewModel viewModel = new CreateMovieViewModel();
            viewModel.Genres = await _movieService.GetAllGenres();
            return View(viewModel);
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
                int result = await _movieService.Add(_mapper.Map<MovieDomainModel>(movieViewModel));
                if (result == -1)
                    return new HttpStatusCodeResult(500);

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
            MovieViewModel movieViewModel = _mapper.Map<MovieViewModel>(await _movieService.GetByIdAsync(id));
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
                int result = await _movieService.Edit(_mapper.Map<MovieDomainModel>(movieViewModel));
                if (result == -1)
                    return new HttpStatusCodeResult(500);

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
            MovieViewModel movieViewModel = _mapper.Map<MovieViewModel>(await _movieService.GetByIdAsync(id));
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
            MovieDomainModel movie = await _movieService.GetByIdAsync(id);
            int result = await _movieService.Delete(movie);
            if (result == -1)
                return new HttpStatusCodeResult(500);

            return RedirectToAction("Index");
        }
    }
}
