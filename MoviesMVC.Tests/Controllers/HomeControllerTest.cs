using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesMVC.Controllers;
using Movies.Services;
using MoviesMVC.Tests.Mocks;
using System.Threading.Tasks;

namespace MoviesMVC.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public async Task Index()
        {
            // Arrange
            HomeController controller = new HomeController(new MockMovieRepository());

            // Act
            ViewResult result = await controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController(new MockMovieRepository());

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController(new MockMovieRepository());

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
