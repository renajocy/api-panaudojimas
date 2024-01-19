using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using SerpApi;
using System.Collections;

namespace serpapi_test_web.Pages
{
    public class VietosModel : PageModel
    {
        public JObject data { get; private set; }
        public bool NoResults { get; private set; }

        public async Task OnGetAsync(string location)
        {
            string apiKey = "bb18b589af8518d3b212cd2f1f5e89e42a939035b8e6f8edcf2f0c64c5b6ce26";


            if (string.IsNullOrWhiteSpace(location))
            {
                NoResults = true;
                return;
            }
            Hashtable ht = new Hashtable();
            ht.Add("q", location);
            ht.Add("engine", "google_images");
            //ht.Add("hl", "lt");
            //ht.Add("gl", "lt");
            //ht.Add("lr", "lang_lt");
            ht.Add("ijn", "0");

            try
            {
                GoogleSearch search = new GoogleSearch(ht, apiKey);
                data = search.GetJson();
                var images_results = data["images_results"];

                if (data["images_results"] == null || !data["images_results"].Any())
                {
                    NoResults = true;
                }
                else
                {
                    NoResults = false;
                }
            }
            catch (SerpApiSearchException ex)
            {
                Console.WriteLine("Exception:");
                Console.WriteLine(ex.ToString());
            }
        //    Hashtable ht = new Hashtable
        //{
        //    { "engine", "google" },
        //    { "q", $"Top sights in {location}" },
        //};

        //    try
        //    {
        //        GoogleSearch search = new GoogleSearch(ht, apiKey);
        //        SearchResults = search.GetJson();

        //        if (SearchResults["top_sights"]?["sights"] == null || !SearchResults["top_sights"]["sights"].Any())
        //        {
        //            NoResults = true;
        //        }
        //    }
        //    catch (SerpApiSearchException ex)
        //    {
        //        Console.WriteLine("Exception:");
        //        Console.WriteLine(ex.ToString());
        //    }
        }
    }
}
