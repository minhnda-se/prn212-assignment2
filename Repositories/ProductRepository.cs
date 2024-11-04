using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDAO productDAO;
        public ProductRepository()
        {
             productDAO = new ProductDAO();
        }
        public bool DeleteProduct(int id)
        {
            return productDAO.DeleteProduct(id);
        }

        public List<Product> GetAllProducts()
        {
            return productDAO.GetProducts();
        }
        public Product GetProductById(int id)
        {
            return productDAO.GetProduct(id);
        }

        public bool SaveProducts(Product product)
        {
            return productDAO.SaveProduct(product);
        }

        public bool UpdateProduct(Product product)
        {
            return productDAO.UpdateProduct(product);
        }
    }
}
