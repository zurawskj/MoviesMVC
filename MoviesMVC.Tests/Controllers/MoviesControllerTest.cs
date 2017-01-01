using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesMVC.Controllers;
using System.Web.Mvc;
using System.Data.Entity;
using MoviesMVC.Models;
using Movies.Services;
using MoviesMVC.Tests.Mocks;
using System.Threading.Tasks;

namespace MoviesMVC.Tests.Controllers
{
    [TestClass]
    public class MoviesControllerTest
    {
        [TestMethod]
        public async Task Index_IsNotNull()
        {
            //Arrange
            MoviesController controller = new MoviesController(new MockMovieRepository());

            //Act
            ViewResult result = await controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Details()
        {
            //Arrange
            MockMovieRepository movies = new MockMovieRepository();
            await movies.Add(new Movie { ID = 1, Genre = "Horror", Price = 10, ReleaseDate = new DateTime(1980, 5, 23), Title = "The Shining" });
            MoviesController controller = new MoviesController(movies);

            //Act
            ViewResult result = await controller.Details(1) as ViewResult;

            //Assert
            Assert.AreEqual(((MovieViewModel)result.Model).Genre, "Horror");
        }

        [TestMethod]
        public void CreateNoParams_IsNotNull()
        {
            //Arrange
            MoviesController controller = new MoviesController(new MockMovieRepository());

            //Act
            ViewResult result = controller.Create() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task CreateWithParams_MovieIsCreated()
        {
            //Arrange
            MockMovieRepository movies = new MockMovieRepository();
            MoviesController controller = new MoviesController(movies);
            MovieViewModel movie = new MovieViewModel { ID = 1, Genre = "Horror", Price = 10, ReleaseDate = new DateTime(1980, 5, 23), Title = "The Shining" };

            //Act
            ViewResult result = await controller.Create(movie) as ViewResult;

            //Assert
            Assert.AreEqual(movies.GetByIdAsync(1).Result.Title, "The Shining");
        }

        [TestMethod]
        public async Task EditWithId()
        {
            //Arrange
            MockMovieRepository movies = new MockMovieRepository();
            await movies.Add(new Movie { ID = 1, Genre = "Horror", Price = 10, ReleaseDate = new DateTime(1980, 5, 23), Title = "The Shining" });
            MoviesController controller = new MoviesController(movies);

            //Act
            ViewResult result = await controller.Edit(1) as ViewResult;

            //Assert
            Assert.AreEqual(((MovieViewModel)result.Model).Title, "The Shining");
        }

        [TestMethod]
        public async Task EditWithMovie()
        {
            //Arrange
            MockMovieRepository movies = new MockMovieRepository();
            await movies.Add(new Movie { ID = 1, Genre = "Horror", Price = 10, ReleaseDate = new DateTime(1980, 5, 23), Title = "The Shining" });
            MoviesController controller = new MoviesController(movies);

            //Act
            ViewResult result = await controller.Edit(new MovieViewModel { ID = 1, Genre = "Horror", Price = 10, ReleaseDate = new DateTime(1980, 5, 23), Title = "The Dimming" }) as ViewResult;

            //Assert
            Assert.AreEqual(movies.GetByIdAsync(1).Result.Title, "The Dimming");
        }

        [TestMethod]
        public async Task DeleteWithId()
        {
            //Arrange
            MockMovieRepository movies = new MockMovieRepository();
            await movies.Add(new Movie { ID = 1, Genre = "Horror", Price = 10, ReleaseDate = new DateTime(1980, 5, 23), Title = "The Shining" });
            MoviesController controller = new MoviesController(movies);

            //Act
            ViewResult result = await controller.Delete(1) as ViewResult;

            //Assert
            Assert.AreEqual(((MovieViewModel)result.Model).Title, "The Shining");
        }

        [TestMethod]
        public async Task DeleteConfirmed()
        {
            //Arrange
            MockMovieRepository movies = new MockMovieRepository();
            await movies.Add(new Movie { ID = 1, Genre = "Horror", Price = 10, ReleaseDate = new DateTime(1980, 5, 23), Title = "The Shining" });
            MoviesController controller = new MoviesController(movies);

            //Act
            ViewResult result = await controller.DeleteConfirmed(1) as ViewResult;

            //Assert
            Assert.AreEqual(movies.GetAllAsync().Result.Count, 0);
        }
    }
}
