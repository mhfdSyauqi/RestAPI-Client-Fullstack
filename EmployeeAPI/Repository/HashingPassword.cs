using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Repository
{
	public class HashingPassword
	{
		public static string GenereateSalt()
		{
			return BCrypt.Net.BCrypt.GenerateSalt(12);
		}
		public static string HashPassword(string pass)
		{
			return BCrypt.Net.BCrypt.HashPassword(pass, GenereateSalt());
		}
		public static bool VerifyPassword(string inputPass, string dbPass)
		{
			return BCrypt.Net.BCrypt.Verify(inputPass, dbPass);
		}

	}
}
