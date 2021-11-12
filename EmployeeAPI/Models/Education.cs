using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EmployeeAPI.Models
{
	[Table("TB_M_Education")]
	public class Education
	{
		[Key]
		public int Id { get; set; }
		public string Degree { get; set; }
		public string GPA { get; set; }
		public int University_Id { get; set; }
		[JsonIgnore]
		public virtual ICollection<Profiling> Profiling { get; set; }
		[JsonIgnore]
		[ForeignKey("University_Id")]
		public virtual University University { get; set; }
	}
}
