using Client.Base.Urls;
using EmployeeAPI.Models;
using EmployeeAPI.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
	public class EmployeeRepository : GeneralRepository<Employee, string>
	{
		private readonly Address address;
		private readonly string request;
		private readonly HttpClient httpClient;

		public EmployeeRepository(Address address, string request = "employees/") : base(address, request)
		{
			this.address = address;
			this.request = request;
			httpClient = new HttpClient
			{
				BaseAddress = new Uri(address.link)
			};
		}

		public async Task<List<ProfileVM>> GetProfile()
		{
			List<ProfileVM> entities = new List<ProfileVM>();

			using (var response = await httpClient.GetAsync(request + "profile/"))
			{
				string apiResponse = await response.Content.ReadAsStringAsync();
				entities = JsonConvert.DeserializeObject<List<ProfileVM>>(apiResponse);
			}
			return entities;
		}

		public async Task<ProfileVM> Profile(string id)
		{
			ProfileVM entity = null;

			using (var response = await httpClient.GetAsync(request + "profile/" + id))
			{
				string apiResponse = await response.Content.ReadAsStringAsync();
				entity = JsonConvert.DeserializeObject<ProfileVM>(apiResponse);
			}
			return entity;
		}

		public HttpStatusCode Register(RegisterVM entity)
		{
			StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
			var result = httpClient.PostAsync (address.link + request + "register/", content).Result;
			return result.StatusCode;
		}

		public async Task<JWTokenVM> Auth(LoginVM login)
		{
			JWTokenVM token = null;

			StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
			var result = await httpClient.PostAsync(request + "login/", content);

			string apiResponse = await result.Content.ReadAsStringAsync();
			token = JsonConvert.DeserializeObject<JWTokenVM>(apiResponse);

			return token;
		}

	}
}
