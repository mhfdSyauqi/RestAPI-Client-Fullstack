using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAPI.Models
{
	[Table("TB_M_Employee")]
	public class Employee
	{
		[Key]
		public string NIK { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public int Salary { get; set; }
		public DateTime BirthDate { get; set; }
		[JsonIgnore]
		public virtual Account Acc { get; set; }
	}
}
