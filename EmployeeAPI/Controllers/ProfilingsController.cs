using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAPI.Repository.Data;
using EmployeeAPI.Base;
using EmployeeAPI.Models;

namespace EmployeeAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProfilingsController : BaseController<Profiling, ProfilingRepository, string>
	{
		public ProfilingsController(ProfilingRepository profilingRepository) : base(profilingRepository)
		{

		}
	}
}
