using System.Web.Mvc;
using Donor.Controllers;
using Donor.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Donor.Testes.Controllers {
    [TestClass]
    public class TipoContaSelecaoControllerTest {

        [TestMethod]
        public void Index() {
            // Arrange
            var controller = new TipoContaSelecaoController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RedirecionaTipoConta(){
            // Arrange
            var controller = new TipoContaSelecaoController();

            var mockModel = new TipoContaSelecaoViewModel{ TipoConta = "U" };

            Assert.IsNotNull(mockModel);
            Assert.IsTrue(mockModel.TipoConta == "U");

            // Act
            var result = (RedirectToRouteResult) controller.RedirecionaTipoConta(mockModel);

            //Assert 
            Assert.IsNotNull(result);
            Assert.IsTrue(result.RouteValues["controller"].ToString() == "Usuario");
            Assert.IsTrue(result.RouteValues["action"].ToString() == "Create");
        }

        [TestMethod]
        public void RedirecionaTipoContaPontoDoacao() {
            // Arrange
            var controller = new TipoContaSelecaoController();

            var mockModel = new TipoContaSelecaoViewModel{ TipoConta = "P" };

            Assert.IsNotNull(mockModel);
            Assert.IsTrue(mockModel.TipoConta == "P");

            // Act
            var result = (RedirectToRouteResult)controller.RedirecionaTipoConta(mockModel);

            //Assert 
            Assert.IsNotNull(result);
            Assert.IsTrue(result.RouteValues["controller"].ToString() == "PontoDeDoacao");
            Assert.IsTrue(result.RouteValues["action"].ToString() == "Create");
        }
    }
}
