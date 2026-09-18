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


namespace API.Controller.Tests
{
    [TestClass()]
    public class ProductControllerTests
    {
        private ProductController _controller;
        private Mock<IProduitRepository> _repositoryMock;

        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Mock<IProduitRepository>();
            _controller = new ProductController(_repositoryMock.Object);
        }


        [TestMethod()]
        public async Task ShowIdGetAllProducts()
        {
            // Given
            var produits = new List<Produit>
            {
                new Produit { IdProduit = 1, NomProduit = "Produit 1", stockMin = 5, stockMax = 50 },
                new Produit { IdProduit = 2, NomProduit = "Produit 2", stockMin = 10, stockMax = 30 },
                new Produit { IdProduit = 3, NomProduit = "Produit 3", stockMin = 2, stockMax = 20 }
            };

            _repositoryMock.Setup(repo => repo.GetAllAsync())
                            .ReturnsAsync(produits);

            // When
            var actionResult = await _controller.GetAllProduct();

            // Then
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var returnedProducts = okResult.Value as IEnumerable<Produit>;
            Assert.IsNotNull(returnedProducts);
            Assert.AreEqual(3, returnedProducts.Count());
        }

        [TestMethod()]
        public async Task GetAllProducts_ReturnsEmptyList_WhenNoProductsExist()
        {
            // Given
            var produits = new List<Produit>();

            _repositoryMock.Setup(repo => repo.GetAllAsync())
                            .ReturnsAsync(produits);

            // When
            var actionResult = await _controller.GetAllProduct();

            // Then
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var returnedProducts = okResult.Value as IEnumerable<Produit>;
            Assert.IsNotNull(returnedProducts);
            Assert.AreEqual(0, returnedProducts.Count());
        }

        [TestMethod()]
        public async Task GetProductById_ReturnsProduct_WhenIdExists()
        {
            // Given
            var produit = new Produit { IdProduit = 1, NomProduit = "Produit 1", stockMin = 5, stockMax = 50 };

            _repositoryMock.Setup(repo => repo.GetByIdAsync(1))
                            .ReturnsAsync(produit);

            // When
            var actionResult = await _controller.GetProductById(1);

            // Then
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var returnedProduct = okResult.Value as Produit;
            Assert.IsNotNull(returnedProduct);
            Assert.AreEqual(1, returnedProduct.IdProduit);
            Assert.AreEqual("Produit 1", returnedProduct.NomProduit);
        }

        [TestMethod()]
        public async Task GetProductById_ReturnsNotFound_WhenIdDoesNotExist()
        {
            // Given
            _repositoryMock.Setup(repo => repo.GetByIdAsync(999))
                            .ReturnsAsync((Produit)null);

            // When
            var actionResult = await _controller.GetProductById(999);

            // Then
            var notFoundResult = actionResult.Result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

    }
}