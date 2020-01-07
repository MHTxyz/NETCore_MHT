using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Entities;

namespace WebApiDemo.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyContext _myContext;

        public ProductRepository(MyContext myContext)
        {
            _myContext = myContext;    
        }

        public Material GetMaterialForProduct(int productId, int materialId)
        {
            return _myContext.Materials.FirstOrDefault(m => m.Id == materialId && m.ProductId == productId);
        }

        public IEnumerable<Material> GetMaterialsForProduct(int productId)
        {
            return _myContext.Materials.Where(m => m.ProductId == productId).ToList();
        }

        public Product GetProduct(int productId, bool includeMaterials)
        {
            if (includeMaterials)
            {
                return _myContext.Products.Include(p => p.Materials).FirstOrDefault(p => p.Id == productId);
            }

            return _myContext.Products.Find(productId);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _myContext.Products.OrderBy(p => p.Name).ToList();
        }

        public bool ProductExist(int productId)
        {
            return _myContext.Products.Any(p => p.Id == productId);
        }

        public void AddProduct(Product product)
        {
            _myContext.Products.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            _myContext.Products.Remove(product);
        }

        public bool Save()
        {
            return _myContext.SaveChanges() >= 0;
        }
    }
}
