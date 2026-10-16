using API.Controller;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using TPRevision;
using TPRevision.Entities;
using TPRevision.Models.Model.Dtos;

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

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

            // ProduitRepository implémente à la fois IProduitRepository et IQueryService<ProduitDto, ProduitDetailDto>,
            // la même instance sert donc aux deux rôles ici.
            _repository = new ProduitRepository(_context, mapperConfig);
            _controller = new ProductController(_repository, _repository);
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
            var postResult = await _controller.Post(nouveauProduit);

            // Then : la création renvoie 201 CreatedAtAction
            var createdResult = postResult.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);

            var produitCree = createdResult.Value as Produit;
            Assert.IsNotNull(produitCree);
            Assert.IsTrue(produitCree.IdProduit > 0);

            // When : relecture via le contrôleur (renvoie un ProduitDetailDto)
            var getResult = await _controller.GetById(produitCree.IdProduit);

            // Then : le produit persisté est bien retourné avec 200 OK
            var okResult = getResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var produitRelu = okResult.Value as ProduitDetailDto;
            Assert.IsNotNull(produitRelu);
            Assert.AreEqual(produitCree.IdProduit, produitRelu.Id);
            Assert.AreEqual("iPhone 17", produitRelu.Nom);
            Assert.AreEqual("Apple", produitRelu.Marque);
            Assert.AreEqual("Smartphone", produitRelu.Type);
            Assert.AreEqual(10, produitRelu.Stock);
            Assert.IsFalse(produitRelu.EnReappro);
        }
    

        [TestMethod()]
        public async Task GetById_ProduitSansStock_RenvoieEnReapproFalse()
        {
            // Given : un produit sans stock renseigné (StockReel et StockMin à NULL en base)
            var marque = new Marque { NomMarque = "Asus" };
            var typeProduit = new TypeProduit { NomTypeProduit = "Écran" };
            _context.Marques.Add(marque);
            _context.TypeProduits.Add(typeProduit);
            await _context.SaveChangesAsync();

            var produit = new Produit
            {
                NomProduit = "ProArt PA278QV",
                Description = "Ecran 27 pouces",
                NomPhoto = "pa278qv.jpg",
                UriPhoto = "/images/pa278qv.jpg",
                IdMarque = marque.IdMarque,
                IdTypeProduit = typeProduit.IdTypeProduit
            };
            _context.Produits.Add(produit);
            await _context.SaveChangesAsync();

            // When : la projection ProjectTo est exécutée en SQL
            var result = await _controller.GetById(produit.IdProduit);

            // Then : pas d'exception, EnReappro vaut false et Stock reste null
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var dto = okResult.Value as ProduitDetailDto;
            Assert.IsNotNull(dto);
            Assert.IsFalse(dto.EnReappro);
            Assert.IsNull(dto.Stock);
        }
    }
}
