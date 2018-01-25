using System.Collections.Generic;

namespace DataAccess.Repositories.Interfaces
{
	public interface IRepositoryBase<T> where T : class
	{
		T Create(T obj);

		T GetById(int id);

		IEnumerable<T> GetAll();

		void Update(T obj);

		void Delete(T obj);

        void DeleteById(int id);


        void Dispose();
	}
}
