using System;

namespace BusinessLogic.Filters
{
	public interface IBaseFilter<T>
    {
		 Func<T, bool> FilterExpression { get; }
    }
}
