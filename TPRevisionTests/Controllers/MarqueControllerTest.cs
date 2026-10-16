using API.Controller;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPRevision.Entities;
using TPRevision.Models.Model.Dtos;

namespace API.Controller.Tests
{
    [TestClass()]
    public class MarqueControllerTests
    {
        private MarqueController _controller;
        private Mock<IMarqueRepository> _repositoryMock;
        private Mock<IQueryService<MarqueDto, MarqueDto>> _queryServiceMock;

        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Mock<IMarqueRepository>();
            _queryServiceMock = new Mock<IQueryService<MarqueDto, MarqueDto>>();
            _controller = new MarqueController(_repositoryMock.Object, _queryServiceMock.Object);
        }

        [TestMethod()]
        public async Task GetAllMarque_ReturnsAllMarquesWithNbProduits()
        {
            // Given
            var marques = new List<MarqueDto>
            {
                new MarqueDto { IdMarque = 1, NomMarque = "Apple", NbProduits = 3 },
                new MarqueDto { IdMarque = 2, NomMarque = "Samsung", NbProduits = 2 }
            };

            _queryServiceMock.Setup(qs => qs.GetAllDtoAsync())
                              .ReturnsAsync(marques);

            // When
            var actionResult = await _controller.GetAll();

            // Then
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var returnedMarques = okResult.Value as IEnumerable<MarqueDto>;
            Assert.IsNotNull(returnedMarques);
            Assert.AreEqual(2, returnedMarques.Count());
            Assert.AreEqual(3, returnedMarques.First().NbProduits);
        }

        [TestMethod()]
        public async Task GetMarqueById_ReturnsMarque_WhenIdExists()
        {
            // Given
            var marque = new MarqueDto { IdMarque = 1, NomMarque = "Apple", NbProduits = 3 };

            _queryServiceMock.Setup(qs => qs.GetByIdDtoAsync(1))
                              .ReturnsAsync(marque);

            // When
            var actionResult = await _controller.GetById(1);

            // Then
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var returnedMarque = okResult.Value as MarqueDto;
            Assert.IsNotNull(returnedMarque);
            Assert.AreEqual("Apple", returnedMarque.NomMarque);
        }

        [TestMethod()]
        public async Task GetMarqueById_ReturnsNotFound_WhenIdDoesNotExist()
        {
            // Given
            _queryServiceMock.Setup(qs => qs.GetByIdDtoAsync(999))
                              .ReturnsAsync((MarqueDto)null);

            // When
            var actionResult = await _controller.GetById(999);

            // Then
            var notFoundResult = actionResult.Result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }
    }
}
