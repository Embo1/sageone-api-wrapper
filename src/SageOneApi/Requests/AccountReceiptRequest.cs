using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using RestSharp.Serializers;
using SageOneApi.Interfaces;
using SageOneApi.Models;

namespace SageOneApi.Requests
{
    public class AccountReceiptRequest: RequestBase, IAccountReceipt
    {
        public AccountReceiptRequest(IRestClient client, string apiKey, int companyId) : base(client, apiKey, companyId) { }

        public PagingResponse<AccountReceipt> Get(int skip = 0)
        {
            var url = $"AccountReceipt/Get?apikey={_apiKey}&companyid={_companyId}";

            if (skip > 0)
                url += "&$skip=" + skip;

            var request = new RestRequest(url, Method.GET);
            request.RequestFormat = DataFormat.Json;

            var response = _client.Execute(request);
            JsonDeserializer deserializer = new JsonDeserializer();

            return deserializer.Deserialize<PagingResponse<AccountReceipt>>(response);
        }

        public Models.AccountReceipt Save(AccountReceipt accountReceipt)
        {
            var url = $"AccountReceipt/Save?apikey={_apiKey}&companyid={_companyId}";
            var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(accountReceipt);
            var response = _client.Execute<AccountReceipt>(request);
            return response.Data;
        }
    }
}