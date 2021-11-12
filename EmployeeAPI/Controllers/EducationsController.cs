using Microsoft.AspNetCore.Http;
using EmployeeAPI.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAPI.Repository.Data;
using EmployeeAPI.Models;

namespace EmployeeAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EducationsController : BaseController<Education, EducationRepository, int>
	{
		public EducationsController(EducationRepository educationRepository) : base(educationRepository)
		{

		}
	}
}
