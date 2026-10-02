using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TPRevision.Entities;
using TPRevision.Models.Model.Dtos;

namespace API.Controller.Tests
{
    [TestClass()]
    public class MappingProfileTests
    {
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();
        }

        [TestMethod()]
        public void Configuration_IsValid()
        {
            // Vérifie que tous les CreateMap du profil sont cohérents
            // (aucune propriété destination non mappée sans configuration explicite)
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            config.AssertConfigurationIsValid();
        }

        [TestMethod()]
        public void Map_ProduitToProduitDto_UsesNavigationNames()
        {
            // Given
            var marque = new Marque { IdMarque = 1, NomMarque = "Apple" };
            var type = new TypeProduit { IdTypeProduit = 1, NomTypeProduit = "Smartphone" };
            var produit = new Produit
            {
                IdProduit = 1,
                NomProduit = "iPhone 17",
                MarqueNavigation = marque,
                TypeProduitNavigation = type
            };

            // When
            var dto = _mapper.Map<ProduitDto>(produit);

            // Then
            Assert.AreEqual(1, dto.Id);
            Assert.AreEqual("iPhone 17", dto.Nom);
            Assert.AreEqual("Apple", dto.Marque);
            Assert.AreEqual("Smartphone", dto.Type);
        }

        [TestMethod()]
        public void Map_ProduitToProduitDetailDto_EnReappro_TrueWhenStockBelowMin()
        {
            // Given
            var produit = new Produit
            {
                IdProduit = 1,
                NomProduit = "Produit test",
                MarqueNavigation = new Marque { NomMarque = "Apple" },
                TypeProduitNavigation = new TypeProduit { NomTypeProduit = "Smartphone" },
                stockReel = 2,
                stockMin = 5
            };

            // When
            var dto = _mapper.Map<ProduitDetailDto>(produit);

            // Then
            Assert.IsTrue(dto.EnReappro);
        }

        [TestMethod()]
        public void Map_ProduitToProduitDetailDto_EnReappro_FalseWhenStockAboveMin()
        {
            // Given
            var produit = new Produit
            {
                IdProduit = 1,
                NomProduit = "Produit test",
                MarqueNavigation = new Marque { NomMarque = "Apple" },
                TypeProduitNavigation = new TypeProduit { NomTypeProduit = "Smartphone" },
                stockReel = 20,
                stockMin = 5
            };

            // When
            var dto = _mapper.Map<ProduitDetailDto>(produit);

            // Then
            Assert.IsFalse(dto.EnReappro);
        }

        [TestMethod()]
        public void Map_ProduitToProduitDetailDto_EnReappro_FalseWhenStockIsNull()
        {
            // Given : produit sans stock renseigné (cas du seed, produit 18)
            var produit = new Produit
            {
                IdProduit = 1,
                NomProduit = "Produit sans stock",
                MarqueNavigation = new Marque { NomMarque = "Apple" },
                TypeProduitNavigation = new TypeProduit { NomTypeProduit = "Smartphone" },
                stockReel = null,
                stockMin = null
            };

            // When
            var dto = _mapper.Map<ProduitDetailDto>(produit);

            // Then : comparaison avec null => false, pas d'exception
            Assert.IsFalse(dto.EnReappro);
        }

        [TestMethod()]
        public void Map_MarqueToMarqueDto_CountsProduits()
        {
            // Given
            var marque = new Marque
            {
                IdMarque = 1,
                NomMarque = "Apple",
                produits = new List<Produit>
                {
                    new Produit { IdProduit = 1, NomProduit = "iPhone 17" },
                    new Produit { IdProduit = 2, NomProduit = "MacBook Air" }
                }
            };

            // When
            var dto = _mapper.Map<MarqueDto>(marque);

            // Then
            Assert.AreEqual(2, dto.NbProduits);
        }

        [TestMethod()]
        public void Map_MarqueToMarqueDto_ZeroProduitsWhenEmpty()
        {
            // Given
            var marque = new Marque { IdMarque = 1, NomMarque = "Nouvelle Marque", produits = new List<Produit>() };

            // When
            var dto = _mapper.Map<MarqueDto>(marque);

            // Then
            Assert.AreEqual(0, dto.NbProduits);
        }

        [TestMethod()]
        public void Map_TypeProduitToTypeProduitDto_CountsProduits()
        {
            // Given
            var type = new TypeProduit
            {
                IdTypeProduit = 1,
                NomTypeProduit = "Smartphone",
                produits = new List<Produit>
                {
                    new Produit { IdProduit = 1 },
                    new Produit { IdProduit = 2 },
                    new Produit { IdProduit = 3 }
                }
            };

            // When
            var dto = _mapper.Map<TypeProduitDto>(type);

            // Then
            Assert.AreEqual(3, dto.NbProduits);
        }
    }
}