using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.BookingDto;
using System.Text;

namespace SignalRWebUI.Controllers
{
	public class BookingController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;
		public BookingController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}
		public async Task<IActionResult> Index()
		{

			try
			{
				var client = _httpClientFactory.CreateClient();
				var responseMessage = await client.GetAsync("https://localhost:7068/api/Booking");

				if (responseMessage.IsSuccessStatusCode)
				{
					var jsonData = await responseMessage.Content.ReadAsStringAsync();
					var values = JsonConvert.DeserializeObject<List<ResultBookingDto>>(jsonData);
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
		public IActionResult CreateBooking()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateBooking(CreateBookingDto createBookingDto)
		{
			try
			{
				createBookingDto.Description = "Rezervasyon Alındı";
				var client = _httpClientFactory.CreateClient();
				var jsonData = JsonConvert.SerializeObject(createBookingDto);
				StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
				var responseMessage = await client.PostAsync("https://localhost:7068/api/Booking", stringContent);
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
		public async Task<IActionResult> DeleteBooking(int id)
		{
			try
			{
				var client = _httpClientFactory.CreateClient();
				var responseMessage = await client.DeleteAsync($"https://localhost:7068/api/Booking/{id}");
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
		public async Task<IActionResult> UpdateBooking(int id)
		{
			try
			{
				var client = _httpClientFactory.CreateClient();
				var responseMessage = await client.GetAsync($"https://localhost:7068/api/Booking/{id}");
				if (responseMessage.IsSuccessStatusCode)
				{
					var jsonData = await responseMessage.Content.ReadAsStringAsync();
					var values = JsonConvert.DeserializeObject<UpdateBookingDto>(jsonData);

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
		public async Task<IActionResult> UpdateBooking(UpdateBookingDto updateBookingDto)
		{
			try
			{
				var client = _httpClientFactory.CreateClient();
				var jsonData = JsonConvert.SerializeObject(updateBookingDto);
				StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
				var responseMessage = await client.PutAsync("https://localhost:7068/api/Booking/", content);
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
		public async Task<IActionResult> BookingStatusApproved(int id)
		{
			var client = _httpClientFactory.CreateClient();
			await client.GetAsync($"https://localhost:7068/api/Booking/BookingStatusApproved/{id}");
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> BookingStatusCancelled(int id)
		{
			var client = _httpClientFactory.CreateClient();
			await client.GetAsync($"https://localhost:7068/api/Booking/BookingStatusCancelled/{id}");
			return RedirectToAction("Index");
		}
	}
}
