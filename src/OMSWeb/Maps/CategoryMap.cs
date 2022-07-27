using AutoMapper;
using OMS.Api.Models.Category;
using OMS.Data.Model;

namespace OMSWeb.Maps
{
	public class CategoryMap : IAutoMapperTypeConfigurator
	{
		public void Configure(IMapperConfigurationExpression configuration)
		{
			var map = configuration.CreateMap<Category, CategoryModel>();
			map.ForMember(x => x.CategoryName, x => x.MapFrom(y => y.CategoryName));
		}
	}
}
