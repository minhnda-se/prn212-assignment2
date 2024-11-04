using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryDAO categoryDAO;
        public CategoryRepository()
        {
            categoryDAO = new CategoryDAO();
        }
        public List<Category> GetCategories()
        {
            return CategoryDAO.GetCategories();
        }

        public Category GetCategory(int? id)
        {
            return CategoryDAO.GetCategory(id);
        }
    }
}
