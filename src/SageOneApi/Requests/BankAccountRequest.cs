using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using RestSharp.Serializers;
using SageOneApi.Interfaces;
using SageOneApi.Models;

namespace SageOneApi.Requests
{
    public class BankAccountRequest : RequestBase, IBankAccountRequest
    {
        public BankAccountRequest(IRestClient client, string apiKey, int companyId) : base(client, apiKey, companyId) { }

        public BankAccount Get(int id)
        {
            var response = _client.Execute<BankAccount>(new RestRequest(
                $"BankAccount/Get/{id}?apikey={_apiKey}&companyid={_companyId}", Method.GET));
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.Data;
        }

        public PagingResponse<BankAccount> Get(string filter = "", int skip = 0)
        {
            var url = $"BankAccount/Get?apikey={_apiKey}&companyid={_companyId}";

            if (!string.IsNullOrEmpty(filter))
                url = $"BankAccount/Get?apikey={_apiKey}&companyid={_companyId}&$filter={filter}";

            if (skip > 0)
                url += "&$skip=" + skip;

            var request = new RestRequest(url, Method.GET);
            request.RequestFormat = DataFormat.Json;

            var response = _client.Execute(request);
            JsonDeserializer deserializer = new JsonDeserializer();

            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return deserializer.Deserialize<PagingResponse<BankAccount>>(response);
        }

        public BankAccount Save(BankAccount bankAccount)
        {
            var url = $"BankAccount/Save?apikey={_apiKey}&companyid={_companyId}";
            var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(bankAccount);
            var response = _client.Execute<BankAccount>(request);
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.Data;
        }

        public bool Delete(int id)
        {
            var url = $"BankAccount/Delete/{id}?apikey={_apiKey}&companyid={_companyId}";
            var response = _client.Execute<BankAccount>(new RestRequest(url, Method.DELETE));
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.ResponseStatus == ResponseStatus.Completed;
        }
 
    }
}