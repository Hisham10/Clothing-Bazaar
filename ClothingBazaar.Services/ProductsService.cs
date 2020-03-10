using ClothingBazaar.Database;
using ClothingBazaar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity; 

namespace ClothingBazaar.Services
{
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
        
        
        public Product GetProduct(int ID)
        {

            using (CBContext db = new CBContext()) { 
            return db.Products.Where(x => x.ID == ID).Include(x => x.Category).FirstOrDefault();
            }
        }

        public List<Product> GetProducts(List<int> IDs)
        {
            using (CBContext db = new CBContext())
            {

                return db.Products.Where(product => IDs.Contains(product.ID)).ToList();
            }
        }

        public List<Product> GetProducts()
        {
            using (CBContext db = new CBContext())
            {
                return db.Products.Include(p => p.Category).ToList();
            }
            
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
            using(CBContext db = new CBContext()) { 
                db.Entry(Product.Category).State = System.Data.Entity.EntityState.Unchanged;
                db.Products.Add(Product);
                db.SaveChanges();
            }
        }

        public void UpdateProduct(Product Product)
        {
            using (CBContext db1 = new CBContext())
            {
                db1.Entry(Product.Category).State = System.Data.Entity.EntityState.Unchanged;
                db1.Entry(Product).State = System.Data.Entity.EntityState.Modified;
                db1.SaveChanges();
            }
            
        }

        public void DeleteProduct(int ID)
        {
            using(CBContext db = new CBContext()) { 
                var product = db.Products.Find(ID);
                db.Products.Remove(product);
                db.SaveChanges();
            }
        }
    }
}
