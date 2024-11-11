using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.AboutDto;
using System.Text;

namespace SignalRWebUI.Controllers
{
	public class AboutController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;
		public AboutController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}
		public async Task<IActionResult> Index()
		{

			try
			{
				var client = _httpClientFactory.CreateClient();
				var responseMessage = await client.GetAsync("https://localhost:7068/api/About");

				if (responseMessage.IsSuccessStatusCode)
				{
					var jsonData = await responseMessage.Content.ReadAsStringAsync();
					var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);// Listelemek için
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
		public IActionResult CreateAbout()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
		{
			try
			{
				var client = _httpClientFactory.CreateClient();
				var jsonData = JsonConvert.SerializeObject(createAboutDto);
				StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
				var responseMessage = await client.PostAsync("https://localhost:7068/api/About", stringContent);
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
		public async Task<IActionResult> DeleteAbout(int id)
		{
			try
			{
				var client = _httpClientFactory.CreateClient();
				var responseMessage = await client.DeleteAsync($"https://localhost:7068/api/About/{id}");
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
		public async Task<IActionResult> UpdateAbout(int id)
		{
			try
			{
				var client = _httpClientFactory.CreateClient();
				var responseMessage = await client.GetAsync($"https://localhost:7068/api/About/{id}");
				if (responseMessage.IsSuccessStatusCode)
				{
					var jsonData = await responseMessage.Content.ReadAsStringAsync();
					var values = JsonConvert.DeserializeObject<UpdateAboutDto>(jsonData);

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
		public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
		{
			try
			{
				var client = _httpClientFactory.CreateClient();
				var jsonData = JsonConvert.SerializeObject(updateAboutDto);
				StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
				var responseMessage = await client.PutAsync("https://localhost:7068/api/About/", content);
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
