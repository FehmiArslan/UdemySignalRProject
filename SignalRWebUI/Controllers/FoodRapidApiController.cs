﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.RapidApiDto;
using System.Net.Http.Headers;

namespace SignalRWebUI.Controllers
{
    public class FoodRapidApiController : Controller
    {
        public async Task< IActionResult> Index()
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://tasty.p.rapidapi.com/recipes/list?from=0&size=60&tags=under_30_minutes"),
                Headers =
    {
        { "x-rapidapi-key", "63bea777eamsh0c7411caa8d1d93p11c9a8jsn94e000a191e5" },
        { "x-rapidapi-host", "tasty.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                //var values = JsonConvert.DeserializeObject<List<ResultTastyApi>>(body);
                //return View(values.ToList());
                var root = JsonConvert.DeserializeObject<RootTastyApi>(body);
                var values = root.Results;
                return View(values.ToList());
            }
        }
    }
}
