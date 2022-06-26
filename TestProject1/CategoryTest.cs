using OMS.Data.Access.DAL;
using OMS.Queries.CategoryQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using OMS.Data.Model;
using FluentAssertions;
using OMS.Api.Common.Exceptions;
using OMS.Api.Models.Category;

namespace CategoryQueryTest
{
	public class CategoryTest
    {
		private Mock<IUnitOfWork> _uow;
		private List<Category> _categoryList;	
		private ICategoryQueryProcessor _query;
		private Random _random;
		private Category _currentCategory;

		public CategoryTest()
		{
			_uow = new Mock<IUnitOfWork>();

			_categoryList = new List<Category>();
			_uow.Setup(x => x.Query<Category>()).Returns(() => _categoryList.AsQueryable());
			_currentCategory = new Category { CategoryId = 25 , CategoryName = "milk", Description = "12345"};

			_query = new CategoryQueryProcessor(_uow.Object);
		}

		[Fact]
		public void GetAll()
		{
			_categoryList.Add(new Category {CategoryId = _currentCategory.CategoryId });
			var result = _query.Get().ToList();
			result.Count.Should().Be(1);
		}

		[Fact]
		public void GetById()
		{
			var category = new Category { CategoryId = _currentCategory.CategoryId };
			_categoryList.Add(category);

			var result = _query.Get(category.CategoryId);
			result.Should().Be(category);
		}

		[Fact]
		public async Task AsDeleted()
		{
			var category = new Category() { CategoryId = _currentCategory.CategoryId};
			_categoryList.Add(category);

			await _query.Delete(category.CategoryId);

			category.IsDeleted.Should().BeTrue();

			_uow.Verify(x => x.CommitAsync());
		}

		[Fact]
		public async Task CreateCategory()
		{
			var model = new CreateCategoryModel
			{
				CategoryName = _currentCategory.CategoryName,
				Description = _currentCategory.Description,
			};

			var result = await _query.Create(model);

			result.Description.Should().Be(model.Description);
			result.CategoryName.Should().Be(model.CategoryName);
			result.CategoryId.Should().Be(model.CategoryId);
			_uow.Verify(x => x.Add(result));
		}

		[Fact]
		public async Task UpdateCategory()
		{
			var category = new Category { CategoryId = _currentCategory.CategoryId };
			_categoryList.Add(category);

			var model = new UpdateCategoryModel
			{
				CategoryName = _currentCategory.CategoryName,
				Description = _currentCategory.Description,
			};

			var result = await _query.Update(category.CategoryId, model);

			result.Should().Be(category);
			result.Description.Should().Be(model.Description);

		}
	}
}