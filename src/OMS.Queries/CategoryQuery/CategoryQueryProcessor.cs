using OMS.Api.Models.Category;
using OMS.Data.Access.DAL;
using OMS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Queries.CategoryQuery
{
    public class CategoryQueryProcessor : ICategoryQueryProcessor
    {
        private readonly IUnitOfWork _uow;

        public CategoryQueryProcessor(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IQueryable<Category> Get()
        {
            var query = GetQuery();
            return query;
        }


        private IQueryable<Category> GetQuery()
        {
            var c = _uow.Query<Category>();

            return c;
        }


        public Category Get(int id)
        {
            var category = GetQuery().FirstOrDefault(x => x.CategoryId == id);
            return category;
        }

        public async Task<Category> Create(CreateCategoryModel model)
        {
            var category = new Category
            {
                CategoryId = model.CategoryId,
                CategoryName = model.CategoryName,
                Description = model.Description,
            };

            _uow.Add(category);
            return category;
        }

        public async Task<Category> Update(int id, UpdateCategoryModel model)
        {
            var category = GetQuery().FirstOrDefault(x => x.CategoryId == id);

            category.CategoryName = model.CategoryName;
            category.Description = model.Description;

            return category;
        }

        public async Task Delete(int id)
        {
            var user = GetQuery().FirstOrDefault(u => u.CategoryId == id);

            if (user.IsDeleted) return;

            user.IsDeleted = true;
            await _uow.CommitAsync();
        }

		
	}
}
