using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMS.Api.Models.ProductM;
using OMS.Data.Model;
using OMS.Queries.ProductQuery;
using OMSWeb.Maps;

namespace OMSWeb.Server.RestAPI
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
        private readonly IProductQueryProcessor _query;
        private readonly IAutoMapper _mapper;

        public ProductController(IProductQueryProcessor query, IAutoMapper mapper)
        {
            _query = query;
            _mapper = mapper;
        }

        [HttpGet]
        public IQueryable<ProductModel> Get()
        {
            var result = _query.Get();
            var models = _mapper.Map<Product, ProductModel>(result);
            return models;
        }

        [HttpGet("{id}")]
        public ProductModel Get(int id)
        {
            var item = _query.Get(id);
            var model = _mapper.Map<ProductModel>(item);
            return model;
        }

        [HttpPost]
        public async Task<ProductModel> Post([FromBody] CreateProductModel requestModel)
        {
            var item = await _query.Create(requestModel);
            var model = _mapper.Map<ProductModel>(item);
            return model;
        }

        [HttpPut("{id}")]
        public async Task<ProductModel> Put(int id, [FromBody] UpdateProductModel requestModel)
        {
            var item = await _query.Update(id, requestModel);
            var model = _mapper.Map<ProductModel>(item);
            return model;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _query.Delete(id);
        }
    }
}
