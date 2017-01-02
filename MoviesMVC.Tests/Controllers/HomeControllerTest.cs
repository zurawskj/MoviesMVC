using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesMVC.Controllers;
using Movies.Services;
using MoviesMVC.Tests.Mocks;
using System.Threading.Tasks;
using AutoMapper;
using MoviesMVC.Models;
using Movies.Services.DomainModels;

namespace MoviesMVC.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MovieViewModel, MovieDomainModel>();
                cfg.CreateMap<MovieDomainModel, MovieViewModel>();
            });
            _mapper = config.CreateMapper();
        }

        private IMapper _mapper;

        [TestMethod]
        public async Task Index()
        {
            // Arrange
            HomeController controller = new HomeController(new MockMovieRepository(), _mapper);

            // Act
            ViewResult result = await controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController(new MockMovieRepository(), _mapper);

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController(new MockMovieRepository(), _mapper);

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
