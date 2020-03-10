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
    }
}
