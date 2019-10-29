using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using RestSharp.Serializers;
using SageOneApi.Interfaces;
using SageOneApi.Models;

namespace SageOneApi.Requests
{
    public class CustomerNoteRequest:RequestBase, ICustomerNoteRequest
    {
        public CustomerNoteRequest(IRestClient client, string apiKey, int companyId) : base(client, apiKey, companyId) { }

        public CustomerNote Get(int id)
        {
            var response = _client.Execute<CustomerNote>(new RestRequest(
                $"CustomerNote/Get/{id}?apikey={_apiKey}&companyid={_companyId}", Method.GET));
            return response.Data;
        }

        public PagingResponse<CustomerNote> Get(string filter = "", int skip = 0)
        {
            var url = $"CustomerNote/Get?apikey={_apiKey}&companyid={_companyId}";

            if (!string.IsNullOrEmpty(filter))
                url = $"CustomerNote/Get?apikey={_apiKey}&companyid={_companyId}&$filter={filter}";

            if (skip > 0)
                url += "&$skip=" + skip;

            var request = new RestRequest(url, Method.GET);
            request.RequestFormat = DataFormat.Json;

            var response = _client.Execute(request);
            JsonDeserializer deserializer = new JsonDeserializer();

            return deserializer.Deserialize<PagingResponse<CustomerNote>>(response);
        }

        public CustomerNote Save(CustomerNote customerNote)
        {
            var url = $"CustomerNote/Save?apikey={_apiKey}&companyid={_companyId}";
            var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(customerNote);
            var response = _client.Execute<CustomerNote>(request);
            return response.Data;
        }

        public bool Delete(int id)
        {
            var url = $"CustomerNote/Delete/{id}?apikey={_apiKey}&companyid={_companyId}";
            var response = _client.Execute<CustomerNote>(new RestRequest(url, Method.DELETE));
            return response.ResponseStatus == ResponseStatus.Completed;
        }
    }
}