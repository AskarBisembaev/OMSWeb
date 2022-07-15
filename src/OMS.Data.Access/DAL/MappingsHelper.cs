using OMS.Data.Access.Maps.Common;
using OMS.Data.Access.Maps.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Data.Access.DAL
{
	public static class MappingsHelper
	{
        public static IEnumerable<IMap> GetMainMappings()
        {
			var assemblyTypes = typeof(ProductMap).GetTypeInfo().Assembly.DefinedTypes;
			var mappings = assemblyTypes
				// ReSharper disable once AssignNullToNotNullAttribute
				.Where(t => t.Namespace != null && t.Namespace.Contains(typeof(ProductMap).Namespace))
				.Where(t => typeof(IMap).GetTypeInfo().IsAssignableFrom(t));
            mappings = mappings.Where(x => !x.IsAbstract);
            return mappings.Select(m => (IMap)Activator.CreateInstance(m.AsType())).ToArray();
        }
    }
}
