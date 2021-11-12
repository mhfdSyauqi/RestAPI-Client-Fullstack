using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace EmployeeAPI.Models
{
	[Table("TB_T_Account")]
	public class Account
	{
		[Key]
		public string NIK { get; set; }
		public string Password { get; set; }
		[JsonIgnore]
		public virtual Employee Employee { get; set; }
		[JsonIgnore]
		public virtual Profiling Profiling { get; set; }
		[JsonIgnore]
		[ForeignKey("NIK")]
		public virtual ICollection<AccountRole> AccountRole { get; set; }
	}
}
