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
    public class TypeProduitControllerTests
    {
        private TypeProduitController _controller;
        private Mock<ITypeProduitRepository> _repositoryMock;
        private Mock<IQueryService<TypeProduitDto, TypeProduitDto>> _queryServiceMock;

        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Mock<ITypeProduitRepository>();
            _queryServiceMock = new Mock<IQueryService<TypeProduitDto, TypeProduitDto>>();
            _controller = new TypeProduitController(_repositoryMock.Object, _queryServiceMock.Object);
        }

        [TestMethod()]
        public async Task GetAllProduct_ReturnsAllTypesWithNbProduits()
        {
            // Given
            var types = new List<TypeProduitDto>
            {
                new TypeProduitDto { IdTypeProduit = 1, NomTypeProduit = "Smartphone", NbProduits = 3 },
                new TypeProduitDto { IdTypeProduit = 2, NomTypeProduit = "Console", NbProduits = 2 }
            };

            _queryServiceMock.Setup(qs => qs.GetAllDtoAsync())
                              .ReturnsAsync(types);

            // When
            var actionResult = await _controller.GetAllProduct();

            // Then
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var returnedTypes = okResult.Value as IEnumerable<TypeProduitDto>;
            Assert.IsNotNull(returnedTypes);
            Assert.AreEqual(2, returnedTypes.Count());
            Assert.AreEqual(3, returnedTypes.First().NbProduits);
        }

        [TestMethod()]
        public async Task GetProductById_ReturnsType_WhenIdExists()
        {
            // Given
            var type = new TypeProduitDto { IdTypeProduit = 1, NomTypeProduit = "Smartphone", NbProduits = 3 };

            _queryServiceMock.Setup(qs => qs.GetByIdDtoAsync(1))
                              .ReturnsAsync(type);

            // When
            var actionResult = await _controller.GetProductById(1);

            // Then
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var returnedType = okResult.Value as TypeProduitDto;
            Assert.IsNotNull(returnedType);
            Assert.AreEqual("Smartphone", returnedType.NomTypeProduit);
        }

        [TestMethod()]
        public async Task GetProductById_ReturnsNotFound_WhenIdDoesNotExist()
        {
            // Given
            _queryServiceMock.Setup(qs => qs.GetByIdDtoAsync(999))
                              .ReturnsAsync((TypeProduitDto)null);

            // When
            var actionResult = await _controller.GetProductById(999);

            // Then
            var notFoundResult = actionResult.Result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }
    }
}