using OMS.Api.Models.OrderDetails;
using OMS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Queries.OrderDetailsQuery
{
	public interface IOrderDetailsQueryProccessor
	{
		IQueryable<OrderDetails> Get();
		OrderDetails Get(int id);
		Task<OrderDetails> Create(CreateOrderDetailsModel model);
		Task<OrderDetails> Update(int id, UpdateOrderDetailsModel model);
		Task Delete(int id);
	}
}
