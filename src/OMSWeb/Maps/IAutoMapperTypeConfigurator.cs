using AutoMapper;

namespace OMSWeb.Maps
{
	public interface IAutoMapperTypeConfigurator
	{
		void Configure(IMapperConfigurationExpression configuration);
	}
}
