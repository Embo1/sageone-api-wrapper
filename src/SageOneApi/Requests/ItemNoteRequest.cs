using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using RestSharp.Serializers;
using SageOneApi.Interfaces;
using SageOneApi.Models;

namespace SageOneApi.Requests
{
    public class ItemNoteRequest:RequestBase, IItemNoteRequest
    {
        public ItemNoteRequest(IRestClient client, string apiKey, int companyId) : base(client, apiKey, companyId) { }

        public ItemNote Get(int id)
        {
            var response = _client.Execute<ItemNote>(new RestRequest(
                $"ItemNote/Get/{id}?apikey={_apiKey}&companyid={_companyId}", Method.GET));
            return response.Data;
        }

        public PagingResponse<ItemNote> Get(string filter = "", int skip = 0)
        {
            var url = $"ItemNote/Get?apikey={_apiKey}&companyid={_companyId}";

            if (!string.IsNullOrEmpty(filter))
                url = $"ItemNote/Get?apikey={_apiKey}&companyid={_companyId}&$filter={filter}";

            if (skip > 0)
                url += "&$skip=" + skip;

            var request = new RestRequest(url, Method.GET);
            request.RequestFormat = DataFormat.Json;

            var response = _client.Execute(request);
            JsonDeserializer deserializer = new JsonDeserializer();

            return deserializer.Deserialize<PagingResponse<ItemNote>>(response);
        }

        public ItemNote Save(ItemNote itemNote)
        {
            var url = $"ItemNote/Save?apikey={_apiKey}&companyid={_companyId}";
            var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(itemNote);
            var response = _client.Execute<ItemNote>(request);
            return response.Data;
        }

        public bool Delete(int id)
        {
            var url = $"ItemNote/Delete/{id}?apikey={_apiKey}&companyid={_companyId}";
            var response = _client.Execute<ItemNote>(new RestRequest(url, Method.DELETE));
            return response.ResponseStatus == ResponseStatus.Completed;
        }
    }
}