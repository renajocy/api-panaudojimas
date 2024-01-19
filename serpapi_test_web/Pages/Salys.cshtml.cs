using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace serpapi_test_web.Pages
{
    public class SalysModel : PageModel
    {
        public JObject CountryInfo { get; private set; }
        public bool NoResults { get; private set; }
        public int countryCount { get; private set; }

        public async Task OnGetAsync(string country)
        {
            if (string.IsNullOrWhiteSpace(country))
            {
                NoResults = true;
                return;
            }

            try
            {
                using (var httpClient = new HttpClient())
                {
                    var apiUrl = $"https://restcountries.com/v3.1/name/{country}";
                    var response = await httpClient.GetStringAsync(apiUrl);

                    var countryArray = JArray.Parse(response);
                    countryCount = countryArray.Count;

                    if (countryCount == 0)
                    {
                        NoResults = true;
                    }
                    else
                    {
                        CountryInfo = countryArray[0] as JObject;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:");
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
