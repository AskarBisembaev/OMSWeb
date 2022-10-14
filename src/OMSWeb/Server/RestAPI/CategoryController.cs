using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMS.Api.Models.Category;
using OMS.Data.Model;
using OMS.Queries.CategoryQuery;
using OMSWeb.Maps;

namespace OMSWeb.Server.RestAPI
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
        private readonly ICategoryQueryProcessor _query;
        private readonly IAutoMapper _mapper;

        public CategoryController(ICategoryQueryProcessor query, IAutoMapper mapper)
        {
            _query = query;
            _mapper = mapper;
        }

        [HttpGet]
        public IQueryable<CategoryModel> GetCategory()
        {
            var result = _query.Get();
            var models = _mapper.Map<Category, CategoryModel>(result);
            return models;
        }

        [HttpGet("{id}")]
        public CategoryModel Get(int id)
        {
            var item = _query.Get(id);
            var model = _mapper.Map<CategoryModel>(item);
            return model;
        }

        [HttpPost]
        public async Task<CategoryModel> Post([FromBody] CreateCategoryModel requestModel)
        {
            var item = await _query.Create(requestModel);
            var model = _mapper.Map<CategoryModel>(item);
            return model;
        }

        [HttpPut("{id}")]
        public async Task<CategoryModel> Put(int id, [FromBody] UpdateCategoryModel requestModel)
        {
            var item = await _query.Update(id, requestModel);
            var model = _mapper.Map<CategoryModel>(item);
            return model;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _query.Delete(id);
        }
    }
}
