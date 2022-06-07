using OMS.Api.Models.ProductM;
using OMS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Queries.ProductQuery
{
	public interface IProductQueryProcessor
	{
		IQueryable<Product> Get();
		Product Get(int id);
		Task<Product> Create(CreateProductModel model);
		Task<Product> Update(int id, UpdateProductModel model);
		Task Delete(int id);
	}
}
