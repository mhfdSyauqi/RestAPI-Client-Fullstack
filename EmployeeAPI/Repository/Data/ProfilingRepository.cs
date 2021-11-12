using EmployeeAPI.Context;
using EmployeeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Repository.Data
{
	public class ProfilingRepository : GeneralRepository<MyContext, Profiling, string>
	{
		public ProfilingRepository(MyContext myContext) : base(myContext)
		{
		}
	}
}
