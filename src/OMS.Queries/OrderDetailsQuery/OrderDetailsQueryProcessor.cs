using OMS.Api.Models.OrderDetails;
using OMS.Data.Access.DAL;
using OMS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Queries.OrderDetailsQuery
{
	public class OrderDetailsQueryProcessor : IOrderDetailsQueryProcessor
	{
        private readonly IUnitOfWork _uow;

        public OrderDetailsQueryProcessor(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IQueryable<OrderDetails> Get()
        {
            var query = GetQuery();
            return query;
        }


        private IQueryable<OrderDetails> GetQuery()
        {
            var c = _uow.Query<OrderDetails>();

            return c;
        }


        public OrderDetails Get(int id)
        {
            var order_details = GetQuery().FirstOrDefault(x => x.OrderId == id);
            return order_details;
        }

        public async Task<OrderDetails> Create(CreateOrderDetailsModel model)
        {
            var order_details = new OrderDetails
            {
               OrderId = model.OrderId,
               ProductId=   model.ProductId,
               UnitPrice = model.UnitPrice,
               Quantity = model.Quantity,
               Order = model.Order,
               Product = model.Product,
            };

            _uow.Add(order_details);
            return order_details;
        }

        public async Task<OrderDetails> Update(int id, UpdateOrderDetailsModel model)
        {
            var order_details = GetQuery().FirstOrDefault(x => x.OrderId == id);

            order_details.ProductId = model.ProductId;
            order_details.UnitPrice = model.UnitPrice;
            order_details.Quantity = model.Quantity;

            return order_details;
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
