using Microsoft.EntityFrameworkCore.Query;
using project.Models;
using System.Linq.Expressions;

namespace project.Services
{
    public interface IProductServices
    {
 
        IEnumerable<Product> GetAll();
        Product? Get(Expression<Func<Product, bool>> expression);
        Product Add (Product product, IFormFile file );   
        bool Edit(int id , Product product);
        bool Remove(int id);    
    }
}
