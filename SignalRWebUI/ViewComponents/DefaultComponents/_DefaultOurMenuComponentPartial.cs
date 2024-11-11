using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.ProductDto;
using SignalRWebUI.Dtos.SliderDto;

namespace SignalRWebUI.ViewComponents.DefaultComponents
{
	public class _DefaultOurMenuComponentPartial:ViewComponent
	{
        private readonly IHttpClientFactory _httpClientFactory;
        public _DefaultOurMenuComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync("https://localhost:7068/api/Product/GetLast9Products");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);// Listelemek için
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
    }
}
