using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using RestSharp.Serializers;
using SageOneApi.Interfaces;
using SageOneApi.Models;

namespace SageOneApi.Requests
{
    public class AdditionalPriceListRequest : RequestBase, IAdditionalPriceListRequest
    {
        public AdditionalPriceListRequest(IRestClient client, string apiKey, int companyId) : base(client, apiKey, companyId) { }

        public AdditionalPriceList Get(int id)
        {
            var response = _client.Execute<AdditionalPriceList>(new RestRequest(
                $"AdditionalPriceList/Get/{id}?apikey={_apiKey}&companyid={_companyId}", Method.GET));
            return response.Data;
        }

        public PagingResponse<AdditionalPriceList> Get(string filter = "", int skip = 0)
        {
            var url = $"AdditionalPriceList/Get?apikey={_apiKey}&companyid={_companyId}";

            if (skip > 0)
                url += "&$skip=" + skip;

            var request = new RestRequest(url, Method.GET);
            request.RequestFormat = DataFormat.Json;

            var response = _client.Execute(request);
            JsonDeserializer deserializer = new JsonDeserializer();

            return deserializer.Deserialize<PagingResponse<AdditionalPriceList>>(response);
        }

        public AdditionalPriceList Save(AdditionalPriceList pricelist)
        {
            var url = $"AdditionalPriceList/Save?apikey={_apiKey}&companyid={_companyId}";
            var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(pricelist);
            var response = _client.Execute<AdditionalPriceList>(request);
            return response.Data;
        }

        public bool Delete(int id)
        {
            var url = $"AdditionalPriceList/Delete/{id}?apikey={_apiKey}&companyid={_companyId}";
            var response = _client.Execute<AdditionalPriceList>(new RestRequest(url, Method.DELETE));
            return response.ResponseStatus == ResponseStatus.Completed;
        }
    }
}
