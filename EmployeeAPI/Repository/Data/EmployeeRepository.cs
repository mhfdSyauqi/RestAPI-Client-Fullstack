using EmployeeAPI.Context;
using EmployeeAPI.Models;
using EmployeeAPI.Repository.Interface;
using EmployeeAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Repository.Data
{

	public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
	{
		private readonly MyContext eContext;
		public EmployeeRepository(MyContext myContext) : base(myContext)
		{
			this.eContext = myContext;
		}
		public int Register(RegisterVM entity)
		{
			var univID = eContext.Universities.Find(entity.UniversityId);
			var empResult = new Employee
			{
				NIK = entity.NIK,
				FirstName = entity.FirstName,
				LastName = entity.LastName,
				Phone = entity.Phone,
				Email = entity.Email,
				Salary = entity.Salary,
				BirthDate = entity.BirthDate,
				Acc = new Account
				{
					NIK = entity.NIK,
					Password = HashingPassword.HashPassword(entity.Password),
					Profiling = new Profiling
					{
						NIK = entity.NIK,
						Education = new Education
						{
							Degree = entity.Degree,
							GPA = entity.GPA,
							University = univID
						}
					}
				}
			};
			if (entity.Role_Id == 0)
			{
				entity.Role_Id = 1;
			}

			var empRole = new AccountRole
			{
				NIK = entity.NIK,
				Role_Id = entity.Role_Id
			};
			eContext.AccountRoles.Add(empRole);
			eContext.Add(empResult);
			var result = eContext.SaveChanges();
			return result;
		}

		public IEnumerable<ProfileVM> GetProfile()
		{
			var profileEmployee = from emp in eContext.Employees
													 join acc in eContext.Accounts on
													 emp.NIK equals acc.NIK
													 join prof in eContext.Profilings on
													 acc.NIK equals prof.NIK
													 join edu in eContext.Educations on
													 prof.Education_Id  equals edu.Id
													 join univ in eContext.Universities on
													 edu.University_Id equals univ.Id
													 join accRole in eContext.AccountRoles on
													 emp.NIK equals accRole.NIK
														select new ProfileVM()
													 {
														 NIK = emp.NIK, 
														 Fullname = emp.FirstName + " " + emp.LastName,
														 Phone = emp.Phone,
														 Email = emp.Email,
														 BirthDate = emp.BirthDate,
														 Degree = edu.Degree,
														 GPA = edu.GPA,
														 UniversityName = univ.Name,
														 AccRole = accRole.Role_Id
													 };

			var result = profileEmployee.ToList();
			return result;

		}
		
		public Object GetProfile(string noInduk)
		{
			var profileEmployee = from emp in eContext.Employees
														join acc in eContext.Accounts on
														emp.NIK equals acc.NIK
														join prof in eContext.Profilings on
														acc.NIK equals prof.NIK
														join edu in eContext.Educations on
														prof.Education_Id equals edu.Id
														join univ in eContext.Universities on
														edu.University_Id equals univ.Id
														where emp.NIK == noInduk
														select new
														{
														 NIK = emp.NIK,
														 Fullname = emp.FirstName + " " + emp.LastName,
														 Phone = emp.Phone,
														 Email = emp.Email,
														 BirthDate = 	emp.BirthDate,
														 Degree =	edu.Degree,
														 GPA = edu.GPA,
														 UniversityName = univ.Name
														};
			var result = profileEmployee.First();
			return result;
		}

		public int Login(LoginVM loginVM)
		{
			var dataExist = eContext.Employees.Where(fn => fn.Email == loginVM.Email).FirstOrDefault();
			if ( dataExist != null)
			{
				var userNIK = dataExist.NIK;
				var userPassword = eContext.Accounts.Find(userNIK).Password;
				bool isVerify = HashingPassword.VerifyPassword(loginVM.Password, userPassword);
					if (isVerify)
					{
						return 0;
					}
					return 2;
			}
			return 1;
		}
		
		public string[] GetRole(LoginVM loginVM) // Get Roles User
		{
			var dataExist = eContext.Employees.Where(fn => fn.Email == loginVM.Email).FirstOrDefault();
			var userNIK = dataExist.NIK;
			var userRole = eContext.AccountRoles.Where(fn => fn.NIK == userNIK).ToList();
			List<string> result = new List<string>();
			foreach (var item in userRole)
			{
				result.Add(eContext.Roles.Where(fn => fn.Role_Id == item.Role_Id).First().Role_Name);
			}

			return result.ToArray();
		}

		public int SignManager(AccountRole accountRole)
		{
			var addManager = new AccountRole
			{
				NIK = accountRole.NIK,
				Role_Id = 2
			};
			eContext.AccountRoles.Add(addManager);
			var result = eContext.SaveChanges();
			return result;
		}

		public IEnumerable ByDegree()
		{
			var degree = from edu in eContext.Educations
							 group edu by edu.Degree into degreeChart
							 select new
							 {
								 Degree = degreeChart.Key,
								 Count = degreeChart.Count()
							 };
			return degree.ToList();
		}

	}
}
