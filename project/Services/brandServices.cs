using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using project.Context;
using project.Models;

namespace project.Services
{
    public class brandServices : IBrandServices
    {
        ApplicationDbContext _context;
        public brandServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public Brand Add(Brand brand)
        {

            _context.Brands.Add(brand);
            _context.SaveChanges();

            return brand;
        }

        public bool Edit(int id, Brand brand)
        {
            var brandInDb = _context.Brands.AsNoTracking().FirstOrDefault(c => c.Id == id);
            if (brandInDb == null) return false;
            brand.Id = brandInDb.Id;
            _context.Brands.Update(brand);
            _context.SaveChanges();
            return true;

        }

        public Brand? Get(Expression<Func<Brand, bool>> expression)
        {
            var brand = _context.Brands.FirstOrDefault(expression);

            return brand;
        }

        public IEnumerable<Brand> GetAll()
        {
            var brands = _context.Brands;
            return brands.ToList();
        }

        public bool Remove(int id)
        {
            Brand? brand = _context.Brands.Find(id);
            if (brand == null) return false;
            _context.Brands.Remove(brand);
            _context.SaveChanges();
            return true;
        }
    }
}
