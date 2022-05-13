using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Data.Model
{
	public class Category
	{
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; } = null!;
        public byte[] Picture { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
