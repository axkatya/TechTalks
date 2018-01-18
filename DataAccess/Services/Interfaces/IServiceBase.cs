using System.Collections.Generic;

namespace DataAccess.Services.Interfaces
{
	public interface IServiceBase<T> where T: class
	{
		IEnumerable<T> GetAll();
		T GetById(int id);
		void Create(T item);
		void Update(T item);

		void Delete(T item);
	}
}
