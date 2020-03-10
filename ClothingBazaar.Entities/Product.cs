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

