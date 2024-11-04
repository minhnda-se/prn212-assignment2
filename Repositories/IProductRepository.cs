using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IProductRepository
    {
        bool SaveProducts(Product product);
        bool DeleteProduct(int id);
        bool UpdateProduct(Product product);
        List<Product> GetAllProducts();
        Product GetProductById(int id);
    }
}
