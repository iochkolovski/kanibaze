using RestSharp;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text.Json.Nodes;

namespace KanbanizeTesting
{
    public class Testing
    {
        private Dictionary<string, string> tempDictionary;
        private RestClient client;
        private RestRequest request;
        private const string _apikey = "A0MfKhN4c8wyBzOidaSTEW9Ba8tiyiRfyZHT6EYW";
        //do not have the technical time to import this sensitive info in variables.json file and .gitignore it :(
        //may appear in next patches
        [SetUp]
        public void Setup()
        {
            tempDictionary = new Dictionary<string, string>();//savetes
            client = new RestClient("https://maplemediaeood.kanbanize.com/api/v2/cards");
            client.AddDefaultHeader("apikey", _apikey);
        }

        [TearDown]
        public void TearDown()
        {
            client.Dispose();
        }

        [Test, Order(0)]
        public void Validate_CardCreation_Successful()
        {
            request = new RestRequest();
            var payload = new JObject
            {
                { "title", "testest" },
                { "column_id", "22" },//i've created test workspace and got 
                { "lane_id", 5 },
                { "position", 2 },
                { "priority", 250 },
                { "color", "050505" }
            };
            request.AddStringBody(payload.ToString(), DataFormat.Json);
            var result = client.ExecutePost(request);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var resultContent = JObject.Parse(result.Content);
            var createdCardId = resultContent["card_id"];
        }
    }
}