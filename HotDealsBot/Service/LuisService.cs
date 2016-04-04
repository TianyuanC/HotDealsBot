using HotDealsBot.Model;
using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace HotDealsBot.Service
{
    public class LuisService
    {
        public async Task<string> Listen(string query)
        {
            var result = new LuisResponse();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.projectoxford.ai/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = 
                    await client.GetAsync($"luis/v1/application?id=ae65a5e4-8027-45d7-bc31-89eb3a49dd4f&subscription-key=6e44912365eb48f79169d42b72e0e435&q={HttpUtility.UrlEncode(query)}");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<LuisResponse>();
                }
            }
            if (result.intents.Length <= 0 && result.intents[0].intent != "HotDeal" && result.entities.Length <= 0)
            {
                return null;
            }
            var refinements = string.Empty;
            foreach (var entity in result.entities)
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                var val = textInfo.ToTitleCase(entity.entity);
                refinements += $"&{entity.type}={HttpUtility.UrlEncode(val)}";
            }
            return refinements;
        }
    }
}