using Client.Base;
using Client.Repositories.Data;
using EmployeeAPI.Models;
using EmployeeAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Controllers
{
	public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
	{
		private readonly EmployeeRepository employee;

		public EmployeesController(EmployeeRepository repository) : base(repository)
		{
			this.employee = repository;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<JsonResult> GetProfile()
		{
			var result = await employee.GetProfile();
			return Json(result);
		}

		public async Task<JsonResult> Profile(string id)
		{
			var result = await employee.Profile(id);
			return Json(result);
		}

		//[HttpPost("Register")]
		public JsonResult Register(RegisterVM entity)
		{
			var result = employee.Register(entity);
			return Json(result);
		}

		//[ValidateAntiForgeryToken]
		//[HttpPost("Auth/")]
		public async Task<IActionResult> Auth(LoginVM login)
		{
			var jwtToken = await employee.Auth(login);
			var token = jwtToken.Token;

			if (token == null)
			{
				return RedirectToAction("index");
			}

			HttpContext.Session.SetString("JWToken", token);
			//HttpContext.Session.SetString("Name", jwtHandler.GetName(token));
			HttpContext.Session.SetString("ProfilePicture", "assets/img/theme/user.png");

			return RedirectToAction("employeeAPI", "home");
		}


	}
}
