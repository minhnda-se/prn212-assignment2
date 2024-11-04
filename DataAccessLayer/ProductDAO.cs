using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ProductDAO
    {
        private static List<Product> listProducts = new List<Product>();
        public ProductDAO()
        {
            Product chai = new Product(1, "Chai", 3, 12, 18);
            Product chang = new Product(2, "Chang", 1, 23, 19);
            Product aniseed = new Product(3, "Aniseed Syrup", 2, 23, 10);
            listProducts.AddRange(new[] { chai, chang, aniseed });
           
        }
        public Product GetProduct(int id)
        {
            
            foreach (Product p in listProducts)
            {
                if (p.ProductId == id) return p;
            }
            return null;
        }
        public List<Product> GetProducts()
        {
            foreach (Product p in listProducts)
            {
                p.Category = CategoryDAO.GetCategory(p.CategoryId);
            }
            return listProducts;
        }
        public bool SaveProduct(Product p)
        {
            bool result = false;
            try
            {
                listProducts.Add(p);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        public bool UpdateProduct(Product p)
        {
            bool result = false;
            var product = GetProduct(p.ProductId);
            if (product != null)
            {
                try
                {
                    product.ProductId = p.ProductId;
                    product.ProductName = p.ProductName;
                    product.UnitPrice = p.UnitPrice;
                    product.UnitsInStock = p.UnitsInStock;
                    product.CategoryId = p.CategoryId;
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }
            return result;
        }
        public bool DeleteProduct(int id)
        {
            bool result = false;
            var product = GetProduct(id);
            if (product != null)
            {
                try
                {
                    listProducts.Remove(product);
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }
            return result;
        }
        
    }
}
