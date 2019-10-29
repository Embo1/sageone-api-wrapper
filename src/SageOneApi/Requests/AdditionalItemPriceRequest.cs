using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using RestSharp.Serializers;
using SageOneApi.Interfaces;
using SageOneApi.Models;

namespace SageOneApi.Requests
{
    public class AdditionalItemPriceRequest : RequestBase, IAdditionalItemPriceRequest
    {
        public AdditionalItemPriceRequest(IRestClient client, string apiKey, int companyId) : base(client, apiKey, companyId) { }

        public AdditionalItemPrice Get(int id)
        {
            var response = _client.Execute<AdditionalItemPrice>(new RestRequest(
                $"AdditionalItemPrice/Get/{id}?apikey={_apiKey}&companyid={_companyId}", Method.GET));
            return response.Data;
        }

        public PagingResponse<AdditionalItemPrice> Get(string filter = "", int skip = 0)
        {
            var url = $"AdditionalItemPrice/Get?apikey={_apiKey}&companyid={_companyId}";

            if (!string.IsNullOrEmpty(filter))
                url = $"AdditionalItemPrice/Get?apikey={_apiKey}&companyid={_companyId}&$filter={filter}";

            if (skip > 0)
                url += "&$skip=" + skip;

            var request = new RestRequest(url, Method.GET) { RequestFormat = DataFormat.Json };
            var response = _client.Execute(request);
            JsonDeserializer deserializer = new JsonDeserializer();

            return deserializer.Deserialize<PagingResponse<AdditionalItemPrice>>(response);
        }

        public AdditionalItemPrice Save(AdditionalItemPrice additionalItemPrice)
        {
            var url = $"AdditionalItemPrice/Save?apikey={_apiKey}&companyid={_companyId}";
            var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(additionalItemPrice);
            var response = _client.Execute<AdditionalItemPrice>(request);
            return response.Data;
        }

        public bool Delete(int id)
        {
            var url = $"AdditionalItemPrice/Delete/{id}?apikey={_apiKey}&companyid={_companyId}";
            var response = _client.Execute<AdditionalItemPrice>(new RestRequest(url, Method.DELETE));
            return response.ResponseStatus == ResponseStatus.Completed;
        }
    }
}