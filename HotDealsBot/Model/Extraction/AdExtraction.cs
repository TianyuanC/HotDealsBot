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
    }
}