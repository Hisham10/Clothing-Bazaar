using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingBazaar.Entities
{
    public class Category : BaseEntity
    {
        public string ImageURL { get; set; }
        //ID, Name and description is being inherited from the base entity.
        public List<Product> Products { get; set; } //One category can have multiple products. -- Connection Between two entities
        public bool isFeatured { get; set; }

        //It's better not to make any changes later on once we have created our business model. Therefore, we create
        //A new view model so that we can make changes from there. This has been implemented when we had to get a category ID
        //From the create view of products (When we were linking products to categories)
    }
}
