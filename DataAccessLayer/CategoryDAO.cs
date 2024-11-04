using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CategoryDAO
    {
        private static List<string> catName;
        public CategoryDAO()
        {
            catName = new List<string> { "Beverages", "Condiments", "Confection", "Dairy Products", "Grains/Cereals", "Meat/Poultry", "Produce", "Seafood" };
        }
        public static List<Category> GetCategories()
        {
            
            var listCategories = new List<Category>();
            try
            {
                for (int i = 0; i < catName.Count; i++)
                {
                    listCategories.Add(new Category(i + 1, catName[i]));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return listCategories;
        }
        public static Category GetCategory(int? id)
        { 
            if (id == null)
            {
                return null;
            }
            else
            {
                return GetCategories()[id.Value-1];
            }
           
        }
    }
}
