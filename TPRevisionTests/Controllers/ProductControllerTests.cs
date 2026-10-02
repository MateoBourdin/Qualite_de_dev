using API.Controller;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPRevision;
using TPRevision.Entities;
using TPRevision.Models.Model.Dtos;


namespace API.Controller.Tests
{
    [TestClass()]
    public class ProductControllerTests
    {
        private ProductController _controller;
        private Mock<IProduitRepository> _repositoryMock;
        private Mock<IQueryService<ProduitDto, ProduitDetailDto>> _queryServiceMock;

        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Mock<IProduitRepository>();
            _queryServiceMock = new Mock<IQueryService<ProduitDto, ProduitDetailDto>>();
            _controller = new ProductController(_repositoryMock.Object, _queryServiceMock.Object);
        }


        [TestMethod()]
        public async Task ShowIdGetAllProducts()
        {
            // Given
            var produits = new List<ProduitDto>
            {
                new ProduitDto { Id = 1, Nom = "Produit 1", Type = "Smartphone", Marque = "Apple" },
                new ProduitDto { Id = 2, Nom = "Produit 2", Type = "Ordinateur portable", Marque = "Dell" },
                new ProduitDto { Id = 3, Nom = "Produit 3", Type = "Casque audio", Marque = "Sony" }
            };

            _queryServiceMock.Setup(qs => qs.GetAllDtoAsync())
                              .ReturnsAsync(produits);

            // When
            var actionResult = await _controller.GetAllProduct();

            // Then
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var returnedProducts = okResult.Value as IEnumerable<ProduitDto>;
            Assert.IsNotNull(returnedProducts);
            Assert.AreEqual(3, returnedProducts.Count());
        }

        [TestMethod()]
        public async Task GetAllProducts_ReturnsEmptyList_WhenNoProductsExist()
        {
            // Given
            var produits = new List<ProduitDto>();

            _queryServiceMock.Setup(qs => qs.GetAllDtoAsync())
                              .ReturnsAsync(produits);

            // When
            var actionResult = await _controller.GetAllProduct();

            // Then
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var returnedProducts = okResult.Value as IEnumerable<ProduitDto>;
            Assert.IsNotNull(returnedProducts);
            Assert.AreEqual(0, returnedProducts.Count());
        }

        [TestMethod()]
        public async Task GetProductById_ReturnsProduct_WhenIdExists()
        {
            // Given
            var produit = new ProduitDetailDto
            {
                Id = 1,
                Nom = "Produit 1",
                Type = "Smartphone",
                Marque = "Apple",
                Description = "Un smartphone",
                Nomphoto = "produit1.jpg",
                Uriphoto = "/images/produit1.jpg",
                Stock = 10,
                EnReappro = false
            };

            _queryServiceMock.Setup(qs => qs.GetByIdDtoAsync(1))
                              .ReturnsAsync(produit);

            // When
            var actionResult = await _controller.GetProductById(1);

            // Then
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var returnedProduct = okResult.Value as ProduitDetailDto;
            Assert.IsNotNull(returnedProduct);
            Assert.AreEqual(1, returnedProduct.Id);
            Assert.AreEqual("Produit 1", returnedProduct.Nom);
        }

        [TestMethod()]
        public async Task GetProductById_ReturnsNotFound_WhenIdDoesNotExist()
        {
            // Given
            _queryServiceMock.Setup(qs => qs.GetByIdDtoAsync(999))
                              .ReturnsAsync((ProduitDetailDto)null);

            // When
            var actionResult = await _controller.GetProductById(999);

            // Then
            var notFoundResult = actionResult.Result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [TestMethod()]
        public async Task GetProductById_ReturnsEnReappro_WhenStockBelowMin()
        {
            // Given
            var produit = new ProduitDetailDto
            {
                Id = 4,
                Nom = "Produit en rupture",
                Type = "Smartphone",
                Marque = "Sony",
                Stock = 2,
                EnReappro = true
            };

            _queryServiceMock.Setup(qs => qs.GetByIdDtoAsync(4))
                              .ReturnsAsync(produit);

            // When
            var actionResult = await _controller.GetProductById(4);

            // Then
            var okResult = actionResult.Result as OkObjectResult;
            var returnedProduct = okResult.Value as ProduitDetailDto;
            Assert.IsNotNull(returnedProduct);
            Assert.IsTrue(returnedProduct.EnReappro);
        }

    }
}