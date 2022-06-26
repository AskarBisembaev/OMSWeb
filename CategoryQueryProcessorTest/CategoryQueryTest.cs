using OMS.Data.Access.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using OMS.Queries.CategoryQuery;
using Xunit;
using FluentAssertions;

public class CategoryQueryProcessorTest
{
	private CategoryQueryProcessor _query;
	private Mock<IUnitOfWork> _uow;

	[Fact]
		public void GetShouldReturnAll()
		{

			var result = _query.Get().ToList();
			result.Count.Should().Be(1);
		}
}
