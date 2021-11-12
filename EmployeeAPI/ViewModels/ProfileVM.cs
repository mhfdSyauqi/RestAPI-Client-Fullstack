using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.ViewModels
{
	public class ProfileVM
	{
		public string NIK { get; set; }
		public string Fullname { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public DateTime BirthDate { get; set; }
		public string Degree { get; set; }
		public string GPA { get; set; }
		public string UniversityName { get; set; }
		public int AccRole { get; set; }
	}
}
