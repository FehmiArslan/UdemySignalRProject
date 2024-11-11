using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.CategoryDto;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SignalRWebUI.Controllers
{
	public class CategoryController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;
		public CategoryController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}
		public async Task<IActionResult> Index()
		{

			try
			{
				var client = _httpClientFactory.CreateClient();
				var responseMessage = await client.GetAsync("https://localhost:7068/api/Category");

				if (responseMessage.IsSuccessStatusCode)
				{
					var jsonData = await responseMessage.Content.ReadAsStringAsync();
					var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);// Listelemek için
					return View(values);
				}

				return View("Veriler gelmedi");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return View("Big Dick");
				//return View(new List<ResultCategoryDto>());
			}
		}
		[HttpGet]
		public IActionResult CreateCategory()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
		{
			createCategoryDto.Status = true;
			try
			{
				var client = _httpClientFactory.CreateClient();
				var jsonData = JsonConvert.SerializeObject(createCategoryDto);
				StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
				var responseMessage = await client.PostAsync("https://localhost:7068/api/Category", stringContent);
				if (responseMessage.IsSuccessStatusCode)
				{
					return RedirectToAction("Index");
				}
				return View();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return View("Big Dick2");
				throw;
			}
		}
		public async Task<IActionResult>DeleteCategory(int id)
		{
			try
			{
				var client = _httpClientFactory.CreateClient();
				var responseMessage = await client.DeleteAsync($"https://localhost:7068/api/Category/{id}");
				if (responseMessage.IsSuccessStatusCode)
				{
					return RedirectToAction("Index");
				}
				return View();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return View("Big Dick3");
				throw;
			}
		}
		[HttpGet]
		public async Task<IActionResult> UpdateCategory(int id)
		{
			try
			{
				var client = _httpClientFactory.CreateClient();
				var responseMessage = await client.GetAsync($"https://localhost:7068/api/Category/{id}");
				if (responseMessage.IsSuccessStatusCode)
				{
					var jsonData = await responseMessage.Content.ReadAsStringAsync();
					var values = JsonConvert.DeserializeObject<UpdateCategoryDto>(jsonData);

					return View(values);
				}
				return View();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return View("Big Dick4");
				throw;
			}
		}
		[HttpPost]
		public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
		{
			try
			{
				var client = _httpClientFactory.CreateClient();
				var jsonData = JsonConvert.SerializeObject(updateCategoryDto);
				StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
				var responseMessage = await client.PutAsync("https://localhost:7068/api/Category/",content);
				if (responseMessage.IsSuccessStatusCode)
				{
					return RedirectToAction("Index");
				}

				return View();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return View("Big Dick5");
				throw;
			}
		}
	}
}