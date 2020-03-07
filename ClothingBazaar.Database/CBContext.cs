using ClothingBazaar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingBazaar.Database
{
    public class CBContext : DbContext, IDisposable
    {
        public CBContext() : base("ClothingBazaarConnection") //Overriding the constructor of base class by telling the name of the connection string.
        {

        }
        
        //Telling the entity framework that these are the entities that you have to associate the database with.
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Config> Configurations { get; set; }
    }
}
//Now, to add the migration, we have to run 3 command to run in the nuGet package manager console which comes by clicking tools option.
//1. Enable Migrations in database project - So change the default project option in the console and write 'enable-migrations'.
//2. Now 2 entities have already been added and we have to send these entities to the database. For that, we write 'add-migration initialized' .. This initialized is just a name which can be anything
//3. Now, we have created tables with the entities and we have to update the database so we'll send the command 'update-database'.
//Now we can see that the database has been created in the SQL server.
//Also, copy and paste the connection string from app.config to web.config file.

