using ClothingBazaar.Database;
using ClothingBazaar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingBazaar.Services
{
    public class ConfigurationsService
    {
        //private static ConfigurationsService privateInMemoryObject { get; set; }

        //public static ConfigurationsService ClassObject
        //{
        //    get
        //    {
        //        if (privateInMemoryObject == null) privateInMemoryObject = new ConfigurationsService();
        //        return privateInMemoryObject;
        //    }
        //}

        //private ConfigurationsService()
        //{
        //}

        public Config GetConfig (string Key)
        {
            using(var context = new CBContext())
            {
                return context.Configurations.Where(x => x.Key == Key).FirstOrDefault();
            }
        }

        public void SaveConfig (Config configuration)
        {
            using (var db = new CBContext())
            {
                db.Configurations.Add(configuration);
                db.SaveChanges();
            }
        }

        public void UpdateConfig(Config configuration)
        {
            using (var db = new CBContext())
            {
                db.Entry(configuration).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteConfig(string Key)
        {
            using (var db = new CBContext())
            {
                var configuration = db.Configurations.Find("Key");
                db.Configurations.Remove(configuration);
                db.SaveChanges();
            }
        }
    }
}
