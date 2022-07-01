using OMS.Data.Access.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using OMS.Data.Model;
using FluentAssertions;
using OMS.Api.Common.Exceptions;
using OMS.Api.Models.OrderDetails;
using OMS.Queries.OrderDetailsQuery;

namespace OrderDetailQueryTest
{
	public class OrderDetailTest
	{
		private Mock<IUnitOfWork> _uow;
		private List<OrderDetails> _orderDList;
		private IOrderDetailsQueryProcessor _query;
		private OrderDetails _currentOrderD;

		public OrderDetailTest()
		{
			_uow = new Mock<IUnitOfWork>();

			_orderDList = new List<OrderDetails>();
			_uow.Setup(x => x.Query<OrderDetails>()).Returns(() => _orderDList.AsQueryable());
			_currentOrderD = new OrderDetails { OrderId = 1, ProductId = 2, UnitPrice = 322 };

			_query = new OrderDetailsQueryProcessor(_uow.Object);
		}

		[Fact]
		public void GetAll()
		{
			_orderDList.Add(new OrderDetails { OrderId = _currentOrderD.OrderId });
			var result = _query.Get().ToList();
			result.Count.Should().Be(1);
		}

		[Fact]
		public void GetById()
		{
			var orderD = new OrderDetails { OrderId = _currentOrderD.OrderId };
			_orderDList.Add(orderD);

			var result = _query.Get(orderD.OrderId);
			result.Should().Be(orderD);
		}

		[Fact]
		public async Task AsDeleted()
		{
			var orderD = new OrderDetails { OrderId = _currentOrderD.OrderId };
			_orderDList.Add(orderD);

			await _query.Delete(orderD.OrderId);

			orderD.IsDeleted.Should().BeTrue();

			_uow.Verify(x => x.CommitAsync());
		}

		[Fact]
		public async Task CreateOrderDetails()
		{
			var model = new CreateOrderDetailsModel
			{
				OrderId = _currentOrderD.OrderId,
				ProductId = _currentOrderD.ProductId,
				UnitPrice = _currentOrderD.UnitPrice,
			};

			var result = await _query.Create(model);

			result.OrderId.Should().Be(model.OrderId);
			result.ProductId.Should().Be(model.ProductId);
			result.UnitPrice.Should().Be(model.UnitPrice);
			_uow.Verify(x => x.Add(result));
		}

		[Fact]
		public async Task UpdateOrderDetails()
		{
			var order = new OrderDetails { OrderId = _currentOrderD.OrderId };
			_orderDList.Add(order);

			var model = new UpdateOrderDetailsModel
			{
				ProductId = _currentOrderD.ProductId,
				UnitPrice = _currentOrderD.UnitPrice,
			};

			var result = await _query.Update(order.OrderId, model);

			result.Should().Be(order);
			result.ProductId.Should().Be(model.ProductId);
			result.UnitPrice.Should().Be(model.UnitPrice);
		}
	}
}