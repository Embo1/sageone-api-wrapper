using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using RestSharp.Serializers;
using SageOneApi.Interfaces;
using SageOneApi.Models;

namespace SageOneApi.Requests
{
    public class TaxTypeRequest : RequestBase, ITaxTypeRequest
    {
        public TaxTypeRequest(IRestClient client, string apiKey, int companyId) : base(client, apiKey, companyId) { }
        
        public TaxType Get(int id)
        {
            var response = _client.Execute<TaxType>(new RestRequest(
                $"TaxType/Get/{id}?apikey={_apiKey}&companyid={_companyId}", Method.GET));
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.Data;
        }

        public PagingResponse<TaxType> Get(string filter = "", int skip = 0)
        {
            var url = $"TaxType/Get?apikey={_apiKey}&companyid={_companyId}";

            if (!string.IsNullOrEmpty(filter))
                url = $"TaxType/Get?apikey={_apiKey}&companyid={_companyId}&$filter={filter}";

            if (skip > 0)
                url += "&$skip=" + skip;

            var request = new RestRequest(url, Method.GET);
            request.RequestFormat = DataFormat.Json;

            var response = _client.Execute(request);
            JsonDeserializer deserializer = new JsonDeserializer();

            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return deserializer.Deserialize<PagingResponse<TaxType>>(response);
        }

        public TaxType Save(TaxType taxType)
        {
            var url = $"TaxType/Save?apikey={_apiKey}&companyid={_companyId}";
            var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(taxType);
            var response = _client.Execute<TaxType>(request);
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.Data;
        }

        public bool Delete(int id)
        {
            var url = $"TaxType/Delete/{id}?apikey={_apiKey}&companyid={_companyId}";
            var response = _client.Execute<TaxType>(new RestRequest(url, Method.DELETE));
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.ResponseStatus == ResponseStatus.Completed;
        }
    }
}