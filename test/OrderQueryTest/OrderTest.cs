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
using OMS.Queries.OrderQuery;
using OMS.Api.Models.OrderM;

namespace OrderQueryTest
{
	public class OrderTest
	{
		private Mock<IUnitOfWork> _uow;
		private List<Order> _orderList;
		private IOrderQueryProcessor _query;
		private Order _currentOrder	;

		public OrderTest()
		{
			_uow = new Mock<IUnitOfWork>();

			_orderList = new List<Order>();
			_uow.Setup(x => x.Query<Order>()).Returns(() => _orderList.AsQueryable());
			_currentOrder = new Order {OrderId = 1, CustomerId = "2", EmployeeId = 3 };

			_query = new OrderQueryProcessor(_uow.Object);
		}

		[Fact]
		public void GetAll()
		{
			_orderList.Add(new Order { OrderId = _currentOrder.OrderId });
			var result = _query.Get().ToList();
			result.Count.Should().Be(1);
		}

		[Fact]
		public void GetById()
		{
			var order = new Order { OrderId = _currentOrder.OrderId };
			_orderList.Add(order);

			var result = _query.Get(order.OrderId);
			result.Should().Be(order);
		}

		[Fact]
		public async Task AsDeleted()
		{
			var order = new Order { OrderId = _currentOrder.OrderId };
			_orderList.Add(order);

			await _query.Delete(order.OrderId);

			order.IsDeleted.Should().BeTrue();

			_uow.Verify(x => x.CommitAsync());
		}

		[Fact]
		public async Task CreateOrder()
		{
			var model = new CreateOrderModel
			{
				OrderId = _currentOrder.OrderId,
				CustomerId = _currentOrder.CustomerId,
				EmployeeId = _currentOrder.EmployeeId,
			};

			var result = await _query.Create(model);

			result.OrderId.Should().Be(model.OrderId);
			result.CustomerId.Should().Be(model.CustomerId);
			result.EmployeeId.Should().Be(model.EmployeeId);
			_uow.Verify(x => x.Add(result));
		}

		[Fact]
		public async Task UpdateOrder()
		{
			var order = new Order { OrderId = _currentOrder.OrderId };
			_orderList.Add(order);

			var model = new UpdateOrderModel
			{
				CustomerId = _currentOrder.CustomerId,
				EmployeeId = _currentOrder.EmployeeId,
			};

			var result = await _query.Update(order.OrderId, model);

			result.Should().Be(order);
			result.CustomerId.Should().Be(model.CustomerId);
			result.EmployeeId.Should().Be(model.EmployeeId);
		}
	}
}