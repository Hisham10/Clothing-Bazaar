using ClothingBazaar.Database;
using ClothingBazaar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity; //Using this allows us to remove an error created when writing db.Products.Include(p => p.Category).ToList();

namespace ClothingBazaar.Services
{

    //SINGLETON: by using this we will not have to make objects again and again like we do in our controllers.
    //We will only assign one object in the constructor itself and use it everywhere we want. This allows us to 
    //Use the service without making any object.
    public class ProductsService
    {
        #region Singleton
        private static ProductsService instance { get; set; }

        public static ProductsService Instance
        {
            get
            {
                if (instance == null) instance = new ProductsService();
                return instance;
            }
        }

        private ProductsService()
        {
        }
        #endregion

        //by using the 'Using' methods below, along with the inheritance of IDisposable, it allows us to dispose off the context created right after its work is done.
        
        public Product GetProduct(int ID)
        {

            using (CBContext db = new CBContext()) { 
            return db.Products.Where(x => x.ID == ID).Include(x => x.Category).FirstOrDefault();
            }
            //return db.Products.Find(id);  -- THiS was used here before

            //using (var context = new CBContext()) //We have to install Entity Framework in NUGet pakages in services as well.
            //{
            //    return context.Categories.Find(id); 
            //}
        }

        public List<Product> GetProducts(List<int> IDs)
        {
            using (CBContext db = new CBContext())
            {

                return db.Products.Where(product => IDs.Contains(product.ID)).ToList();
            }


            //using (var context = new CBContext()) //We have to install Entity Framework in NUGet pakages in services as well.
            //{
            //    return context.Categories.Find(id); 
            //}
        }

        public List<Product> GetProducts()
        {
            using (CBContext db = new CBContext())
            {
                return db.Products.Include(p => p.Category).ToList();
            }

            //using (var context = new CBContext()) //We have to install Entity Framework in NUGet pakages in services as well.
            //{
            //    return context.Categories.ToList(); 
            //}
        }

        public List<Product> GetLatestProducts()
        {
            using (CBContext db = new CBContext())
            {
                return db.Products.Where(p => p.AddedOn != null).OrderByDescending(p => p.AddedOn).Take(4).ToList();
            }
        }

        public void SaveProduct(Product Product)
        {
            ////We write this unchanged function when double category issue arises: like when selecting a category from create products page, the category was updating
            ////itself in the dropdown as well.
            //db.Entry(Product.Category).State = System.Data.Entity.EntityState.Unchanged; 
            //Now that we have the categoryID property and we have directly sent an ID to productcontroller, we do not need this anymore.
            using(CBContext db = new CBContext()) { 
                db.Entry(Product.Category).State = System.Data.Entity.EntityState.Unchanged;
                db.Products.Add(Product);
                db.SaveChanges();
            }
            //using(var context = new CBContext()) //We have to install Entity Framework in NUGet pakages in services as well.
            //{
            //    context.Categories.Add(category);
            //    context.SaveChanges();
            //}
        }

        public void UpdateProduct(Product Product)
        {
            using (CBContext db1 = new CBContext())
            {
                db1.Entry(Product.Category).State = System.Data.Entity.EntityState.Unchanged; //Issue with the category being added when editing the product
                //The reason for this is because the entity framework thinks we have added the category as well
                //so we need to tell EF explicitly that we are not adding a category.
                db1.Entry(Product).State = System.Data.Entity.EntityState.Modified;
                db1.SaveChanges();
            }

            //using (var context = new CBContext()) //We have to install Entity Framework in NUGet pakages in services as well.
            //{
            //    //We just need to inform the entity framework that we have made any modifications to the data
            //    context.Entry(category).State = System.Data.Entity.EntityState.Modified;
            //    context.SaveChanges();
            //}
        }

        public void DeleteProduct(int ID)
        {
            using(CBContext db = new CBContext()) { 
                var product = db.Products.Find(ID);
                db.Products.Remove(product);
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
