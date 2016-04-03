﻿using HotDealsBot.Model;
using HotDealsBot.Model.Extraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HotDealsBot.Service
{
    public class HotDealService
    {
        public async Task<string> Get()
        {
            var ads = new List<Ad>();
            var result = new SearchResponse();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://search.autotrader.ca/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/ad/?v=2&sps=true&c=1&d=1&t=3&haspr=true");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<SearchResponse>();
                    //decode embeded xml
                    foreach (var hit in result.Hits)
                    {
                        var serializer = new XmlSerializer(typeof(Ad));
                        StringReader reader = new StringReader(hit.DocumentXml);
                        var ad = (Ad)serializer.Deserialize(reader);
                        ads.Add(ad);
                    }   
                }
            }
            var sample = ads[0];
            return $"~~${sample.MsrpPrice()}~~ **${sample.InternetPrice()}** \n ![{sample.Cymm()}]({sample.MainImageUrl()}) ";
        }
    }
}