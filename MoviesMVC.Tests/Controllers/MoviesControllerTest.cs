using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesMVC.Controllers;
using System.Web.Mvc;
using MoviesMVC.Models;
using MoviesMVC.Tests.Mocks;
using System.Threading.Tasks;
using Movies.Services.DomainModels;
using AutoMapper;

namespace MoviesMVC.Tests.Controllers
{

    [TestClass]
    public class MoviesControllerTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MovieViewModel, MovieDomainModel>();
                cfg.CreateMap<MovieDomainModel, MovieViewModel>();
            });
            mapper = config.CreateMapper();
        }

        private IMapper mapper;


        [TestMethod]
        public async Task Index_IsNotNull()
        {
            //Arrange
            MoviesController controller = new MoviesController(new MockMovieRepository(), mapper);

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
            await movies.Add(new MovieDomainModel {Genre = "Horror", Price = 10, ReleaseDate = new DateTime(1980, 5, 23), Title = "The Shining" });
            MoviesController controller = new MoviesController(movies, mapper);

            //Act
            ViewResult result = await controller.Details(0) as ViewResult;

            //Assert
            Assert.AreEqual(((MovieViewModel)result.Model).Genre, "Horro");
        }

        [TestMethod]
        public async Task CreateNoParams_IsNotNull()
        {
            //Arrange
            MoviesController controller = new MoviesController(new MockMovieRepository(), mapper);

            //Act
            ViewResult result = await controller.Create() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task CreateWithParams_MovieIsCreated()
        {
            //Arrange
            MockMovieRepository movies = new MockMovieRepository();
            MoviesController controller = new MoviesController(movies, mapper);
            MovieViewModel movie = new MovieViewModel { Genre = "Horror", Price = 10, ReleaseDate = new DateTime(1980, 5, 23), Title = "The Shining" };

            //Act
            ViewResult result = await controller.Create(movie) as ViewResult;

            //Assert
            Assert.AreEqual(movies.GetByIdAsync(0).Result.Title, "The Shining");
        }

        [TestMethod]
        public async Task EditWithId()
        {
            //Arrange
            MockMovieRepository movies = new MockMovieRepository();
            await movies.Add(new MovieDomainModel { Genre = "Horror", Price = 10, ReleaseDate = new DateTime(1980, 5, 23), Title = "The Shining" });
            MoviesController controller = new MoviesController(movies, mapper);

            //Act
            ViewResult result = await controller.Edit(0) as ViewResult;

            //Assert
            Assert.AreEqual(((MovieViewModel)result.Model).Title, "The Shining");
        }

        [TestMethod]
        public async Task EditWithMovie()
        {
            //Arrange
            MockMovieRepository movies = new MockMovieRepository();
            await movies.Add(new MovieDomainModel { Genre = "Horror", Price = 10, ReleaseDate = new DateTime(1980, 5, 23), Title = "The Shining" });
            MoviesController controller = new MoviesController(movies, mapper);

            //Act
            ViewResult result = await controller.Edit(new MovieViewModel { ID = 0, Genre = "Horror", Price = 10, ReleaseDate = new DateTime(1980, 5, 23), Title = "The Dimming" }) as ViewResult;

            //Assert
            Assert.AreEqual(movies.GetByIdAsync(0).Result.Title, "The Dimming");
        }

        [TestMethod]
        public async Task DeleteWithId()
        {
            //Arrange
            MockMovieRepository movies = new MockMovieRepository();
            await movies.Add(new MovieDomainModel { Genre = "Horror", Price = 10, ReleaseDate = new DateTime(1980, 5, 23), Title = "The Shining" });
            MoviesController controller = new MoviesController(movies, mapper);

            //Act
            ViewResult result = await controller.Delete(0) as ViewResult;

            //Assert
            Assert.AreEqual(((MovieViewModel)result.Model).Title, "The Shining");
        }

        [TestMethod]
        public async Task DeleteConfirmed()
        {
            //Arrange
            MockMovieRepository movies = new MockMovieRepository();
            await movies.Add(new MovieDomainModel { Genre = "Horror", Price = 10, ReleaseDate = new DateTime(1980, 5, 23), Title = "The Shining" });
            MoviesController controller = new MoviesController(movies, mapper);

            //Act
            ViewResult result = await controller.DeleteConfirmed(0) as ViewResult;

            //Assert
            Assert.AreEqual(movies.GetAllAsync().Result.Count, 0);
        }
    }
}
