using System;
using System.Linq;

namespace HotDealsBot.Model.Extraction
{
    public static class AdExtraction
    {
        public static string Year(this Ad ad) {
            return ad.Structures.FirstOrDefault(x => x.Type == "Year")?.En;
        }

        public static string Make(this Ad ad)
        {
            return ad.Structures.FirstOrDefault(x => x.Type == "Make")?.En;
        }

        public static string Model(this Ad ad)
        {
            return ad.Structures.FirstOrDefault(x => x.Type == "Model")?.En;
        }

        public static string Color(this Ad ad)
        {
            return ad.Aspects.FirstOrDefault(x => x.Type == "ExtColorCd")?.Value;
        }

        public static string Cymm(this Ad ad)
        {
            return $"{ad.Color()} {ad.Year()} {ad.Make()} {ad.Model()}";
        }

        public static decimal? MsrpPrice(this Ad ad)
        {
            return ad.Prices.FirstOrDefault(x => x.Type == "Msrp")?.Value;
        }

        public static decimal? InternetPrice(this Ad ad)
        {
            return ad.Prices.FirstOrDefault(x => x.Type == "AskingPrice")?.Value;
        }

        public static string MainImageUrl(this Ad ad)
        {
            var route = ad.Resources.FirstOrDefault(x=>x.Type=="MainPhoto")?.SourceUrl;
            var baseUrl = "http://azr.cdnmedia.autotrader.ca/5";
            return $"{baseUrl}{route}";
        }
    }
}