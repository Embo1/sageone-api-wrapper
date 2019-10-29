using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using SageOneApi.Interfaces;
using SageOneApi.Models;

namespace SageOneApi.Requests
{
    public class ItemMovementRequest : RequestBase, IItemMovementRequest
    {
        public ItemMovementRequest(IRestClient client, string apiKey, int companyId) : base(client, apiKey, companyId) { }

        public PagingResponse<ItemMovement> Get(string filter = "", int skip = 0)
        {
            var url = $"ItemMovement/Get?apikey={_apiKey}&companyid={_companyId}";

            if (!string.IsNullOrEmpty(filter))
                url = $"ItemMovement/Get?apikey={_apiKey}&companyid={_companyId}&$filter={filter}";

            if (skip > 0)
                url += "&$skip=" + skip;

            var request = new RestRequest(url, Method.GET);
            request.RequestFormat = DataFormat.Json;

            var response = _client.Execute(request);
            JsonDeserializer deserializer = new JsonDeserializer();

            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return deserializer.Deserialize<PagingResponse<ItemMovement>>(response);
        }
    }
}