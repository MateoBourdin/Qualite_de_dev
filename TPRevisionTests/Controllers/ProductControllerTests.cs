using Microsoft.VisualStudio.TestTools.UnitTesting;
using API.Controller;
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
        [TestMethod()]
        public void GetProductTest()
        {
            List<Produit> produits = new List<Produit>
            {
                new Produit { IdProduit = 1, nomProduit = "Produit 1" },
                new Produit { IdProduit = 2, nomProduit = "Produit 2" },
                new Produit { IdProduit = 3, nomProduit = "Produit 3" }
            };

            Moq.Mock<ProduitRepository> mockRepo = new Moq.Mock<ProduitRepository>();

            ProductController controller = new ProductController(mockRepo);

            var result = controller.GetProduct();

            Assert.IsNotNull(result);
            Assert.AreEqual(produits.Count, result.Result.Value.Count());

        }
    }
}