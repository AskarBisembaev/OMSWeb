using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMS.Api.Models.OrderM;
using OMS.Data.Model;
using OMS.Queries.OrderQuery;
using OMSWeb.Maps;

namespace OMSWeb.Server.RestAPI
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
        private readonly IOrderQueryProcessor _query;
        private readonly IAutoMapper _mapper;

        public OrderController(IOrderQueryProcessor query, IAutoMapper mapper)
        {
            _query = query;
            _mapper = mapper;
        }

        [HttpGet]
        public IQueryable<OrderModel> Get()
        {
            var result = _query.Get();
            var models = _mapper.Map<Order, OrderModel>(result);
            return models;
        }

        [HttpGet("{id}")]
        public OrderModel Get(int id)
        {
            var item = _query.Get(id);
            var model = _mapper.Map<OrderModel>(item);
            return model;
        }

        [HttpPost]
        public async Task<OrderModel> Post([FromBody] CreateOrderModel requestModel)
        {
            var item = await _query.Create(requestModel);
            var model = _mapper.Map<OrderModel>(item);
            return model;
        }

        [HttpPut("{id}")]
        public async Task<OrderModel> Put(int id, [FromBody] UpdateOrderModel requestModel)
        {
            var item = await _query.Update(id, requestModel);
            var model = _mapper.Map<OrderModel>(item);
            return model;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _query.Delete(id);
        }
    }
}
