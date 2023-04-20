using RestSharp;
using Newtonsoft.Json.Linq;

namespace KanbanizeTesting
{
    public class Testing
    {
        private RestClient client;
        private RestRequest request;
        private const string _apikey = "A0MfKhN4c8wyBzOidaSTEW9Ba8tiyiRfyZHT6EYW";
        //do not have the technical time to import this sensitive info in variables.json file and .gitignore it :(
        //may appear in next patches
        [SetUp]
        public void Setup()
        {
            client = new RestClient("https://maplemediaeood.kanbanize.com/api/v2/cards");
            client.AddDefaultHeader("apikey", _apikey);
        }

        [TearDown]
        public void TearDown()
        {
            client.Dispose();
        }

        [Test]
        public void Test_Card_CanBeCreated_ValidData()
        {
            request = new RestRequest();
            var payload = new JObject
            {
                { "title", "testest" },
                { "column_id", "22" },
                { "lane_id", 5 },
                { "position", 5 },
                { "priority", 250 }
            };
            request.AddStringBody(payload.ToString(), DataFormat.Json);
            var result = client.ExecutePost(request);
        }
    }
}