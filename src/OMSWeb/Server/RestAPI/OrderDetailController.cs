using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMS.Api.Models.OrderDetails;
using OMS.Data.Model;
using OMS.Queries.OrderDetailsQuery;
using OMSWeb.Maps;

namespace OMSWeb.Server.RestAPI
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderDetailController : ControllerBase
	{
        private readonly IOrderDetailsQueryProcessor _query;
        private readonly IAutoMapper _mapper;

        public OrderDetailController(IOrderDetailsQueryProcessor query, IAutoMapper mapper)
        {
            _query = query;
            _mapper = mapper;
        }

        [HttpGet]
        public IQueryable<OrderdetailsModel> Get()
        {
            var result = _query.Get();
            var models = _mapper.Map<OrderDetails, OrderdetailsModel>(result);
            return models;
        }

        [HttpGet("{id}")]
        public OrderdetailsModel Get(int id)
        {
            var item = _query.Get(id);
            var model = _mapper.Map<OrderdetailsModel>(item);
            return model;
        }

        [HttpPost]
        public async Task<OrderdetailsModel> Post([FromBody] CreateOrderDetailsModel requestModel)
        {
            var item = await _query.Create(requestModel);
            var model = _mapper.Map<OrderdetailsModel>(item);
            return model;
        }

        [HttpPut("{id}")]
        public async Task<OrderdetailsModel> Put(int id, [FromBody] UpdateOrderDetailsModel requestModel)
        {
            var item = await _query.Update(id, requestModel);
            var model = _mapper.Map<OrderdetailsModel>(item);
            return model;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _query.Delete(id);
        }
    }
}
