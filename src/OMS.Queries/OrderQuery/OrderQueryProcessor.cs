using OMS.Api.Models.OrderM;
using OMS.Data.Access.DAL;
using OMS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Queries.OrderQuery
{
	public class OrderQueryProcessor : IOrderQueryProcessor
	{
        private readonly IUnitOfWork _uow;

        public OrderQueryProcessor(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IQueryable<Order> Get()
        {
            var query = GetQuery();
            return query;
        }


        private IQueryable<Order> GetQuery()
        {
            var c = _uow.Query<Order>();

            return c;
        }


        public Order Get(int id)
        {
            var order = GetQuery().FirstOrDefault(x => x.OrderId == id);
            return order;
        }

        public async Task<Order> Create(CreateOrderModel model)
        {
            var order = new Order
            {
                OrderId = model.OrderId,
                CustomerId = model.CustomerId,
                EmployeeId = model.EmployeeId,
                OrderDate = model.OrderDate,
                ShipAddress = model.ShipAddress,
            };

            _uow.Add(order);
            return order;
        }

        public async Task<Order> Update(int id, UpdateOrderModel model)
        {
            var order = GetQuery().FirstOrDefault(x => x.OrderId == id);

            order.EmployeeId = model.EmployeeId;
            order.OrderDate = model.OrderDate;
            order.ShipAddress = model.ShipAddress;

            return order;
        }

        public async Task Delete(int id)
        {
            var user = GetQuery().FirstOrDefault(u => u.OrderId == id);

            if (user.IsDeleted) return;

            user.IsDeleted = true;
            await _uow.CommitAsync();
        }
    }
}
