using OMS.Data.Access.DAL;
using OMS.Queries.ProductQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using OMS.Data.Model;
using FluentAssertions;
using OMS.Api.Common.Exceptions;
using OMS.Api.Models.ProductM;

namespace ProductQueryTest
{
	public class ProductTest
	{
		private Mock<IUnitOfWork> _uow;
		private List<Product> _productList;
		private IProductQueryProcessor _query;
		private Random _random;
		private Product _currentProduct;

		public ProductTest()
		{
			_uow = new Mock<IUnitOfWork>();

			_productList = new List<Product>();
			_uow.Setup(x => x.Query<Product>()).Returns(() => _productList.AsQueryable());
			_currentProduct = new Product { ProductId = 123, ProductName = "water", CategoryId = 25};


		_query = new ProductQueryProcessor(_uow.Object);
		}

		[Fact]
		public void GetAll()
		{
			_productList.Add(new Product { ProductId = _currentProduct.ProductId });
			var result = _query.Get().ToList();
			result.Count.Should().Be(1);
		}

		[Fact]
		public void GetById()
		{
			var product = new Product { ProductId = _currentProduct.ProductId };
			_productList.Add(product);

			var result = _query.Get(product.ProductId);
			result.Should().Be(product);
		}

		[Fact]
		public async Task AsDeleted()
		{
			var product = new Product() { ProductId = _currentProduct.ProductId };
			_productList.Add(product);

			await _query.Delete(product.ProductId);

			product.IsDeleted.Should().BeTrue();

			_uow.Verify(x => x.CommitAsync());
		}

		[Fact]
		public async Task CreateProduct()
		{
			var model = new CreateProductModel
			{
				ProductName = _currentProduct.ProductName,
				CategoryId = _currentProduct.CategoryId,
			};

			var result = await _query.Create(model);

			result.ProductName.Should().Be(model.ProductName);
			result.CategoryId.Should().Be(model.CategoryId);
			result.ProductId.Should().Be(model.ProductId);
			_uow.Verify(x => x.Add(result));
		}

		[Fact]
		public async Task UpdateProduct()
		{
			var product = new Product() { ProductId = _currentProduct.ProductId }; ;
			_productList.Add(product);

			var model = new UpdateProductModel
			{
				ProductName = _currentProduct.ProductName,
			};

			var result = await _query.Update(product.ProductId, model);

			result.Should().Be(product);

		}
	}
}