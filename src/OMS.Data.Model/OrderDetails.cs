using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Data.Model
{
	public class OrderDetails
	{
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public Single Discount { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public bool IsDeleted { get; set; }
    }
}
