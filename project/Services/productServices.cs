using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using project.Context;
using project.DTOs.Requests;
using project.Migrations;
using project.Models;

namespace project.Services
{
    public class productServices : IProductServices
    {
        ApplicationDbContext _context;
        public productServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public Product Add(Product product, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
                product.mainImg = fileName;
                _context.Products.Add(product);
                _context.SaveChanges();

                return product;
            }
            return null;
        }

        public bool Edit(int id, Product product)
        {
            var productInDb = _context.Products.AsNoTracking().FirstOrDefault(c => c.Id == id);
            if (productInDb == null) return false;
            product.Id = productInDb.Id;
            _context.Products.Update(product);
            _context.SaveChanges();
            return true;

        }

        public Product? Get(Expression<Func<Product, bool>> expression)
        {
            var product = _context.Products.FirstOrDefault(expression);

            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            var products = _context.Products;
            return products.ToList();
        }

        public bool Remove(int id)
        {

            Product? product = _context.Products.Find(id);

            if (product == null) return false;
            var file = Path.Combine(Directory.GetCurrentDirectory(), "images", product.mainImg);
            if (System.IO.File.Exists(file))
            {
                System.IO.File.Delete(file);
            }
            _context.Products.Remove(product);
            _context.SaveChanges();

            return true;
        }
    }
}
