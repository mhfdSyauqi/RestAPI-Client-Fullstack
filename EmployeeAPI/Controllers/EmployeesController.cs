using EmployeeAPI.Base;
using EmployeeAPI.Models;
using EmployeeAPI.Repository.Data;
using EmployeeAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace EmployeeAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
	{
		private readonly EmployeeRepository employee;
		public IConfiguration _configuration;
		public EmployeesController(EmployeeRepository employeeRepository, IConfiguration configuration) : base (employeeRepository)
		{
			this.employee = employeeRepository;
			this._configuration = configuration;
		}
		[Route("Register")]
		[HttpPost]
		public ActionResult Register(RegisterVM entity)
		{
			var result = employee.Register(entity);
			if (result == 4)
			{
				return BadRequest("NIK dan NoHanphone Sudah Dipakai");
			} else if (result == 3)
			{
				return BadRequest("NIK sudah dipakai");
			}
			else if (result == 2)
			{
				return BadRequest("No Handphone Sudah Dipakai");
			}
			return Ok("Berhasil Registrasi");
		}

		//[Authorize(Roles = "Direktur,Manager")]
		[Route("Profile")]
		[HttpGet]
		public ActionResult GetProfile()
		{
			var result = employee.GetProfile();
			return Ok(result);
		}

		//[Authorize(Roles = "Direktur,Manager,Employee")]
		[Route("Profile/{NIK}")]
		[HttpGet]
		public ActionResult GetProfile(string NIK)
		{
			var result = employee.GetProfile(NIK);
			return Ok(result);
		}

		[Route("Login")]
		[HttpPost]
		public ActionResult Login(LoginVM loginVM)
		{
			var result = employee.Login(loginVM);
			if (result == 1)
			{
				return NotFound("Email Belum Terdaftar");
			} else if (result == 2)
			{
				return BadRequest(new JWTokenVM {Message= "Password salah", Token = null });
			}
			// Implement JWT
			var data = new LoginDataVM
			{
				Email = loginVM.Email,
				Roles = employee.GetRole(loginVM)
			};
			var claims = new List<Claim>
			{
				new Claim("email", data.Email)
			};
			foreach (var item in data.Roles)
			{
				claims.Add(new Claim ("roles", item.ToString()));
			}

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(
					_configuration["Jwt:Issuer"],
					_configuration["Jwt:Audience"],
					claims,
					expires : DateTime.UtcNow.AddMinutes(60),
					signingCredentials : signIn
				);
			var idToken = new JwtSecurityTokenHandler().WriteToken(token);
			claims.Add(new Claim("TokenSecurity", idToken.ToString()));
			return Ok(new JWTokenVM { Message = "Login Sukses", Token = idToken });
		}

		[Authorize(Roles = "Direktur")]
		[Route("SignManager")]
		[HttpPost]
		public ActionResult SignManager(AccountRole accountRole)
		{
			var result = employee.SignManager(accountRole);
			return Ok("Berhasil Menambahkan Manager");
		}

		[Route("Chart")]
		[HttpGet]
		public ActionResult Chart()
		{
			var result = employee.ByDegree();
			return Ok(result);
		}
	}
}
