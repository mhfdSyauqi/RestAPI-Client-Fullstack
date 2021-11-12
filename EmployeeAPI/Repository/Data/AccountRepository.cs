using EmployeeAPI.Context;
using EmployeeAPI.Models;
using System;
using System.Collections.Generic;

namespace EmployeeAPI.Repository.Data
{
	public class AccountRepository : GeneralRepository<MyContext, Account, string>
	{
		public AccountRepository(MyContext myContext) : base(myContext)
		{
		}

	}
}
