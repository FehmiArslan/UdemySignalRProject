using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.SocialMediaDto;
using System.Text;

namespace SignalRWebUI.Controllers
{
	public class SocialMediaController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;
		public SocialMediaController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}
		public async Task<IActionResult> Index()
		{
			try
			{
				var client = _httpClientFactory.CreateClient();
				var responseMessage = await client.GetAsync("https://localhost:7068/api/SocialMedia");
				if (responseMessage.IsSuccessStatusCode)
				{
					var jsonData = await responseMessage.Content.ReadAsStringAsync();
					var values = JsonConvert.DeserializeObject<List<ResultSocialMediaDto>>(jsonData);// Listelemek için
					return View(values);
				}

				return View("Veriler gelmedi");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return View("Big Dick");
			}
		}
		[HttpGet]
		public IActionResult CreateSocialMedia()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateSocialMedia(CreateSocialMediaDto createSocialMediaDto)
		{
			try
			{
				var client = _httpClientFactory.CreateClient();
				var jsonData = JsonConvert.SerializeObject(createSocialMediaDto);
				StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
				var responseMessage = await client.PostAsync("https://localhost:7068/api/SocialMedia", stringContent);
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
		public async Task<IActionResult> DeleteSocialMedia(int id)
		{
			try
			{
				var client = _httpClientFactory.CreateClient();
				var responseMessage = await client.DeleteAsync($"https://localhost:7068/api/SocialMedia/{id}");
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
		public async Task<IActionResult> UpdateSocialMedia(int id)
		{
			try
			{
				var client = _httpClientFactory.CreateClient();
				var responseMessage = await client.GetAsync($"https://localhost:7068/api/SocialMedia/{id}");
				if (responseMessage.IsSuccessStatusCode)
				{
					var jsonData = await responseMessage.Content.ReadAsStringAsync();
					var values = JsonConvert.DeserializeObject<UpdateSocialMediaDto>(jsonData);

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
		public async Task<IActionResult> UpdateSocialMedia(UpdateSocialMediaDto updateSocialMediaDto)
		{
			try
			{
				var client = _httpClientFactory.CreateClient();
				var jsonData = JsonConvert.SerializeObject(updateSocialMediaDto);
				StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
				var responseMessage = await client.PutAsync("https://localhost:7068/api/SocialMedia/", content);
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
