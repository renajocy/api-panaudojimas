using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace serpapi_test_web.Pages
{
    public class OraiModel : PageModel
    {
        public JObject WeatherInfo { get; private set; }
        public bool NoResults { get; private set; }

        public async Task OnGetAsync(string location, string date)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                NoResults = true; // Show a message if no location is provided
                return;
            }

            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiUrl;
                    if (string.IsNullOrWhiteSpace(date))
                    {
                        // If date is empty, search for current weather
                        apiUrl = $"http://api.weatherapi.com/v1/current.json?key=30a91e3e74694a54a16175907231412&q={location}";
                    }
                    else
                    {
                        // If date is provided, search for weather forecast
                        apiUrl = $"http://api.weatherapi.com/v1/forecast.json?key=30a91e3e74694a54a16175907231412&q={location}&days=1&dt={date}";
                    }

                    var response = await httpClient.GetStringAsync(apiUrl);

                    // Parse the JSON response
                    WeatherInfo = JObject.Parse(response);

                    // Check if there are no search results
                    if (WeatherInfo == null || WeatherInfo["error"] != null)
                    {
                        NoResults = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:");
                Console.WriteLine(ex.ToString());
                // Handle the exception as needed
            }
        }
    }
}
