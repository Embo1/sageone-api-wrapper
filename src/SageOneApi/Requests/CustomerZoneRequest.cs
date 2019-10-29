using RestSharp;
using RestSharp.Serialization.Json;
using RestSharp.Serializers;
using SageOneApi.Interfaces;

namespace SageOneApi.Requests
{
    public class CustomerZoneRequest : RequestBase, ICustomerZoneRequest
    {
        public CustomerZoneRequest(IRestClient client, string apiKey, int companyId) : base(client, apiKey, companyId) { }

        public string GetCustomerZoneInvoiceUrl(int invoiceId, int customerId)
        {
            var url = $"CustomerZone/GetCustomerZoneInvoiceUrl/?apikey={_apiKey}&companyid={_companyId}";
            var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new CustomerZoneRequestModel() { CustomerId = customerId, DocumentHeaderId = invoiceId });
            var response = _client.Execute(request);
            return response.Content;
        }

        public string GetCustomerZoneQuoteUrl(int quoteId, int customerId)
        {
            var url = $"CustomerZone/GetCustomerZoneQuoteUrl/?apikey={_apiKey}&companyid={_companyId}";
            var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new CustomerZoneRequestModel() { CustomerId = customerId, DocumentHeaderId = quoteId });
            var response = _client.Execute(request);
            return response.Content;
        }
    }

    public class CustomerZoneRequestModel
    {
        public int DocumentHeaderId { get; set; }
        public int CustomerId { get; set; }
    }

}