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
	internal class CategoryMap : IMap
	{
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<Category>()
                .ToTable("Categories")
                .HasKey(x => x.CategoryId);
        }
    }
}
