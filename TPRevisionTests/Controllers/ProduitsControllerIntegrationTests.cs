using API.Controller;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using TPRevision;
using TPRevision.Entities;

namespace API.Controller.Tests
{
    [TestClass()]
    public class ProduitsControllerIntegrationTests
    {
        private const string _connectionString =
            "Host=localhost;Port=5432;Database=ProduitsTestDB;Username=postgres;Password=postgres";

        private DbProduitsContext _context;
        private ProduitRepository _repository;
        private ProductController _controller;

        [TestInitialize]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<DbProduitsContext>()
                .UseNpgsql(_connectionString)
                .Options;

            _context = new DbProduitsContext(options);
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();

            _repository = new ProduitRepository(_context);
            _controller = new ProductController(_repository);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.DisposeAsync();
        }

        [TestMethod()]
        public async Task PostProduit_ThenGetProduit_ReturnsCreatedAndPersistedProduit()
        {
            // Given : Marque et TypeProduit valides insérées en base
            var marque = new Marque { NomMarque = "Apple" };
            var typeProduit = new TypeProduit { NomTypeProduit = "Smartphone" };

            _context.Marques.Add(marque);
            _context.TypeProduits.Add(typeProduit);
            await _context.SaveChangesAsync();

            var nouveauProduit = new Produit
            {
                NomProduit = "iPhone 17",
                Description = "Smartphone Apple dernière génération",
                NomPhoto = "iphone17.jpg",
                UriPhoto = "/images/iphone17.jpg",
                IdMarque = marque.IdMarque,
                IdTypeProduit = typeProduit.IdTypeProduit,
                stockReel = 10,
                stockMin = 5,
                stockMax = 50
            };

            // When : création via le contrôleur
            var postResult = await _controller.PostProduct(nouveauProduit);

            // Then : la création renvoie 201 CreatedAtAction
            var createdResult = postResult.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);

            var produitCree = createdResult.Value as Produit;
            Assert.IsNotNull(produitCree);
            Assert.IsTrue(produitCree.IdProduit > 0);

            // When : relecture via le contrôleur
            var getResult = await _controller.GetProductById(produitCree.IdProduit);

            // Then : le produit persisté est bien retourné avec 200 OK
            var okResult = getResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var produitRelu = okResult.Value as Produit;
            Assert.IsNotNull(produitRelu);
            Assert.AreEqual(produitCree.IdProduit, produitRelu.IdProduit);
            Assert.AreEqual("iPhone 17", produitRelu.NomProduit);
            Assert.AreEqual(marque.IdMarque, produitRelu.IdMarque);
            Assert.AreEqual(typeProduit.IdTypeProduit, produitRelu.IdTypeProduit);
        }
    }
}