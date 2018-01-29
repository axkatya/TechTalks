using System.Collections.Generic;

namespace DataAccess.Services.Interfaces
{
	public interface IServiceBase<T> where T: class
	{
		IEnumerable<T> GetAll();
		T GetById(int id);
		T Create(T item);
		void Update(T item);

		void Delete(T item);

        void DeleteById(int id);

    }
}
