using API.Controller;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPRevision.Entities;

namespace API.Controller.Tests
{
    [TestClass()]
    public class ProductControllerTests
    {
        private ProductController _controller;
        private Moq.Mock<IProduitRepository> _repositoryMock;

        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Moq.Mock<IProduitRepository>();
            _controller = new ProductController(_repositoryMock.Object);
        }


        [TestMethod()]
        public async Task ShowIdGetAllProducts()
        {
            // Given
            var produits = new List<Produit>
            {
                new Produit { IdProduit = 1, nomProduit = "Produit 1", stockMin = 5, stockMax = 50 },
                new Produit { IdProduit = 2, nomProduit = "Produit 2", stockMin = 10, stockMax = 30 },
                new Produit { IdProduit = 3, nomProduit = "Produit 3", stockMin = 2, stockMax = 20 }
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
    }
}