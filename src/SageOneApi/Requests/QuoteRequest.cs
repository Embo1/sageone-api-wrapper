using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using RestSharp.Serializers;
using SageOneApi.Interfaces;
using SageOneApi.Models;

namespace SageOneApi.Requests
{
    public class QuoteRequest:RequestBase, IQuoteRequest
    {
        public QuoteRequest(IRestClient client, string apiKey, int companyId) : base(client, apiKey, companyId) { }

        public Quote Get(int id)
        {
            var response = _client.Execute<Quote>(new RestRequest(
                $"Quote/Get/{id}?apikey={_apiKey}&companyid={_companyId}", Method.GET));
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.Data;
        }

        public PagingResponse<Quote> Get(bool includeDetail = false, bool includeCustomerDetails = false, string filter = "", int skip = 0)
        {
            var url =
                $"Quote/Get?companyid={_companyId}&includeDetail={includeDetail.ToString().ToLower()}&includeCustomerDetails={includeCustomerDetails.ToString().ToLower()}&apikey={_apiKey}";

            if (!string.IsNullOrEmpty(filter))
                url =
                    $"Quote/Get?includeDetail={includeDetail}&includeCustomerDetails={includeCustomerDetails}?apikey={_apiKey}&companyid={_companyId}&$filter={filter}";

            if (skip > 0)
                url += "&$skip=" + skip;

            var request = new RestRequest(url, Method.GET);
            request.RequestFormat = DataFormat.Json;

            var response = _client.Execute(request);
            JsonDeserializer deserializer = new JsonDeserializer();

            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return deserializer.Deserialize<PagingResponse<Quote>>(response);
        }

        public Quote Save(Quote quote)
        {
            var url = $"Quote/Save?apikey={_apiKey}&companyid={_companyId}";
            var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(quote);
            var response = _client.Execute<Quote>(request);
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.Data;
        }

        public Quote Calculate(Quote quote)
        {
            var url = $"Quote/Calculate?apikey={_apiKey}&companyid={_companyId}";
            var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
            request.RequestFormat = DataFormat.Json;
            request.RequestFormat = DataFormat.Json;
            request.AddBody(quote);
            var response = _client.Execute<Quote>(request);
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.Data;
        }

        public bool Email(EmailRequest email)
        {
            var url = $"Quote/Email?apikey={_apiKey}&companyid={_companyId}";
            var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(email);
            var response = _client.Execute<EmailRequest>(request);
            return response.ResponseStatus == ResponseStatus.Completed;
        }
    }

}