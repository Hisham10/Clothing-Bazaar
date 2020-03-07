using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingBazaar.Entities
{
    public class Product : BaseEntity
    {
        //ID, Name and description is being inherited from the base entity.

        public int? CategoryID { get; set; }
        public virtual Category Category { get; set; } //One product will belong to only one major category. -- no need of list here
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTime AddedOn { get; set; }
    }
}

//Next step after this is to install entity framework in the database project
//Create a context class -- DBset for each entity and set the base class as dbcontext
//Modify app.config by copying the connection string line div from the web.config file from the web project
//You can change the name of the connection by changing the value of initial catalogue.
//Make sure to Override the base constructor with the string of the connection name.
//The next step is to enable migrations.
//For migrations step check the cbcontext.cs file.
