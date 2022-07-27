using Microsoft.EntityFrameworkCore;
using OMS.Data.Access.Maps.Common;
using OMS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Data.Access.Maps.Main
{
	public class OrderDetailsMap : IMap
	{
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<OrderDetails>()
                .ToTable("OrderDetils")
                .HasKey(x => x.OrderId);
        }
    }
}
