using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using RestSharp.Serializers;
using SageOneApi.Interfaces;
using SageOneApi.Models;

namespace SageOneApi.Requests
{
    public class BankTransactionRequest : RequestBase, IBankTransactionRequest
    {
        public BankTransactionRequest(IRestClient client, string apiKey, int companyId) : base(client, apiKey, companyId) { }

        public BankTransaction Get(int id)
        {
            var response = _client.Execute<BankTransaction>(new RestRequest(
                $"BankTransaction/Get/{id}?apikey={_apiKey}&companyid={_companyId}", Method.GET));
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.Data;
        }

        public PagingResponse<BankTransaction> Get(string filter = "", int skip = 0)
        {
            var url = $"BankTransaction/Get?apikey={_apiKey}&companyid={_companyId}";

            if (!string.IsNullOrEmpty(filter))
                url = $"BankTransaction/Get?apikey={_apiKey}&companyid={_companyId}&$filter={filter}";

            if (skip > 0)
                url += "&$skip=" + skip;

            var request = new RestRequest(url, Method.GET) { RequestFormat = DataFormat.Json };
            var response = _client.Execute(request);
            JsonDeserializer deserializer = new JsonDeserializer();
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return deserializer.Deserialize<PagingResponse<BankTransaction>>(response);
        }

        public BankTransaction Save(BankTransaction bankTransaction)
        {
            var url = $"BankTransaction/Save?apikey={_apiKey}&companyid={_companyId}";
            var request = new RestRequest(url, Method.POST)
            {
                JsonSerializer = new JsonSerializer(),
                RequestFormat = DataFormat.Json
            };
            request.AddBody(bankTransaction);
            var response = _client.Execute<BankTransaction>(request);
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.Data;
        }
    }
}