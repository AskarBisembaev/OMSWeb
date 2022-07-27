﻿using Microsoft.EntityFrameworkCore;
using OMS.Data.Access.Maps.Common;
using OMS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Data.Access.Maps.Main
{
	public class OrderMap : IMap
	{
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<Order>()
                .ToTable("Orders")
                .HasKey(x => x.OrderId);
        }
    }
}
