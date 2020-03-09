using ClothingBazaar.Database;
using ClothingBazaar.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClothingBazaar.Services
{
    public class CategoriesService
    {
        #region Singleton
        private static CategoriesService instance { get; set; }

        public static CategoriesService Instance
        {
            get
            {
                if (instance == null) instance = new CategoriesService();
                return instance;
            }
        }

        private CategoriesService()
        {
        }
        #endregion

        public Category GetCategory(int id)
        {
            using(CBContext db = new CBContext()) { 
            return db.Categories.Find(id);
            }
            //using (var context = new CBContext()) //We have to install Entity Framework in NUGet pakages in services as well.
            //{
            //    return context.Categories.Find(id); 
            //}
        }

        public int GetCategoriesCount(string search)
        {
            using (CBContext db = new CBContext())
            {
                if (string.IsNullOrEmpty(search) == false)
                {
                    return db.Categories
                            .Where(category => category.Name != null &&
                            category.Name.ToLower().Contains(search.ToLower())).Count();
                }
                else
                {
                    return db.Categories.Count();
                } 
            }
        }

        public List<Category> GetAllCategories() //Returns a list of all categories
        {
            using (CBContext db = new CBContext())
            {
                return db.Categories.Include(cat => cat.Products).ToList();
            }
            //Here we used the include method to include all the products related to that category.

            //using (var context = new CBContext()) //We have to install Entity Framework in NUGet pakages in services as well.
            //{
            //    return context.Categories.ToList(); 
            //}
        }

        public List<Category> GetCategories(string search, int pageNo) //Returns a list of categories
        {
            int pageSize = 5;
            using (CBContext db = new CBContext())
            {
                if (string.IsNullOrEmpty(search) == false)
                {
                    return db.Categories
                            .Where(category=>category.Name!=null && category.Name.ToLower().Contains(search.ToLower()))
                            .OrderBy(x => x.ID)
                            .Skip((pageNo - 1) * pageSize)
                            .Take(pageSize)
                            .Include(cat => cat.Products)
                            .ToList();
                }
                else
                {
                    return db.Categories
                            .OrderBy(x => x.ID)
                            .Skip((pageNo - 1) * pageSize)
                            .Take(pageSize)
                            .Include(cat => cat.Products)
                            .ToList();
                }
            }
        }

        public List<Category> GetFeaturedCategories()
        {
            using (CBContext db = new CBContext())
            {
                return db.Categories.Where(x => x.isFeatured).ToList();
            }
        }

        public void SaveCategory(Category category)
        {
            using (CBContext db = new CBContext())
            {
                db.Categories.Add(category);
                db.SaveChanges();
            }
            //using(var context = new CBContext()) //We have to install Entity Framework in NUGet pakages in services as well.
            //{
            //    context.Categories.Add(category);
            //    context.SaveChanges();
            //}
        }

        public void UpdateCategory(Category category)
        {
            using (CBContext db = new CBContext())
            {
                db.Entry(category).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            //using (var context = new CBContext()) //We have to install Entity Framework in NUGet pakages in services as well.
            //{
            //    //We just need to inform the entity framework that we have made any modifications to the data
            //    context.Entry(category).State = System.Data.Entity.EntityState.Modified;
            //    context.SaveChanges();
            //}
        }

        public void DeleteCategory(int ID)
        {
            using (CBContext db = new CBContext())
            {
                var category = db.Categories.Find(ID);
                db.Categories.Remove(category);
                db.SaveChanges();
            }
            //using (var context = new CBContext())
            //{
            //    var category = context.Categories.Find(ID);

            //    //context.Entry(category).State = System.Data.Entity.EntityState.Deleted;
            //    context.Categories.Remove(category); //This and the above method is same.

            //    context.SaveChanges();
            //}
        }
    }
}
