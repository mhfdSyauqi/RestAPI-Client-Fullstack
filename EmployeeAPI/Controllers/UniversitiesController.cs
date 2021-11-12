using EmployeeAPI.Base;
using EmployeeAPI.Models;
using EmployeeAPI.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UniversitiesController : BaseController<University, UniversityRepository, int>
	{
		public UniversitiesController(UniversityRepository universityRepositories) : base (universityRepositories)
		{

		}
	}
}
