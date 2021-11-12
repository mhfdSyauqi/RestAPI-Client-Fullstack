using EmployeeAPI.Context;
using EmployeeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Repository.Data
{
	public class EducationRepository : GeneralRepository<MyContext, Education , int>
	{
		public EducationRepository(MyContext myContext) : base(myContext)
		{

		}
	}
}
