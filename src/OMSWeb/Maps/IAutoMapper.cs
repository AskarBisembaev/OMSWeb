﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace OMSWeb.Maps
{
	public interface IAutoMapper
	{
		AutoMapper.IConfigurationProvider Configuration { get; }

		T Map<T>(object objectToMap);

		void Map<TSource, TDestination>(TSource source, TDestination destination);

		TResult[] Map<TSource, TResult>(IEnumerable<TSource> sourceQuery);
		IQueryable<TResult> Map<TSource, TResult>(IQueryable<TSource> sourceQuery);
	}
}
