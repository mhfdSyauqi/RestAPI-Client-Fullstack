using System;
using System.Collections.Generic;

namespace EmployeeAPI.Repository.Interface
{
	public interface IRepository<Entity, Key> where Entity : class
	{
		IEnumerable<Entity> Get();
		Entity Get(Key key);
		int Insert(Entity entity);
		int Update(Entity entity, Key key);
		int Delete(Key key);
	}
}
