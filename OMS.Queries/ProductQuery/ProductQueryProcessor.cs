using OMS.Api.Models.ProductM;
using OMS.Data.Access.DAL;
using OMS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Queries.ProductQuery
{
	internal class ProductQueryProcessor : IProductQueryProcessor
	{
		private readonly IUnitOfWork _uow;

		public ProductQueryProcessor(IUnitOfWork uow)
		{
			_uow = uow;
		}
		public IQueryable<Product> Get()
		{
			var query = GetQuery();
			return query;
		}


		private IQueryable<Product> GetQuery()
		{
			var c = _uow.Query<Product>();

			return c;
		}


		public Product Get(int id)
		{
			var product = GetQuery().FirstOrDefault(x => x.ProductId == id);
			return product;
		}

		public async Task<Product> Create(CreateProductModel model)
		{
			var product = new Product
			{
				ProductId = model.ProductId,
				ProductName = model.ProductName,
				UnitPrice = model.UnitPrice,
				SupplierId = model.SupplierId,
				CategoryId = model.CategoryId,
			};

			_uow.Add(product);
			return product;
		}

		public async Task<Product> Update(int id, UpdateProductModel model)
		{
			var product = GetQuery().FirstOrDefault(x => x.ProductId == id);

			product.ProductId = model.ProductId;
			product.ProductName = model.ProductName;
			product.UnitPrice = model.UnitPrice;
			product.SupplierId = model.SupplierId;
			product.CategoryId = model.CategoryId;

            return product;
		}

		public async Task Delete(int id)
		{
			var user = GetQuery().FirstOrDefault(u => u.ProductId == id);

			if (user.IsDeleted) return;

			user.IsDeleted = true;
			await _uow.CommitAsync();
		}
	}
}
