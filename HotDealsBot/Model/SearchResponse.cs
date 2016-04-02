/// <summary>
/// Auto Generated, DNT
/// </summary>
namespace HotDealsBot.Model
{
    public class SearchResponse
    {
        public object Facets { get; set; }
        public Hit[] Hits { get; set; }
        public object Debug { get; set; }
        public int Count { get; set; }
    }

    public class Hit
    {
        public int Id { get; set; }
        public int SourceId { get; set; }
        public string ForeignId { get; set; }
        public string DocumentXml { get; set; }
    }
}
