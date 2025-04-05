using Microsoft.EntityFrameworkCore.Query;
using project.Models;
using System.Linq.Expressions;

namespace project.Services
{
    public interface IBrandServices
    {
 
        IEnumerable<Brand> GetAll();
        Brand? Get(Expression<Func<Brand, bool>> expression);
        Brand Add (Brand brand);   
        bool Edit(int id , Brand brand);
        bool Remove(int id);    
    }
}
