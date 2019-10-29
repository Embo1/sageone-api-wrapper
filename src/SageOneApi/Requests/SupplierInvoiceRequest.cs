using System.Threading.Tasks;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using RestSharp.Serializers;
using SageOneApi.Interfaces;
using SageOneApi.Models;

namespace SageOneApi.Requests
{
    public class SupplierInvoiceRequest: RequestBase, ISupplierInvoiceRequest
    {
        public SupplierInvoiceRequest(IRestClient client, string apiKey, int companyId) : base(client, apiKey, companyId) { }

        public SupplierInvoice Get(int id)
        {
            var response = _client.Execute<SupplierInvoice>(new RestRequest(
                $"SupplierInvoice/Get/{id}?apikey={_apiKey}&companyid={_companyId}", Method.GET));
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.Data;
        }

        public PagingResponse<SupplierInvoice> Get(bool includeDetail = false, bool includeSupplierDetails = false, string filter = "", int skip = 0)
        {
            var url =
                $"SupplierInvoice/Get?companyid={_companyId}&includeDetail={includeDetail.ToString().ToLower()}&includeSupplierDetails={includeSupplierDetails.ToString().ToLower()}&apikey={_apiKey}";

            if (!string.IsNullOrEmpty(filter))
                url =
                    $"SupplierInvoice/Get?includeDetail={includeDetail}&includeSupplierDetails={includeSupplierDetails}?apikey={_apiKey}&companyid={_companyId}&$filter={filter}";

            if (skip > 0)
                url += "&$skip=" + skip;

            var request = new RestRequest(url, Method.GET);
            request.RequestFormat = DataFormat.Json;

            var response = _client.Execute(request);
            JsonDeserializer deserializer = new JsonDeserializer();

            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return deserializer.Deserialize<PagingResponse<SupplierInvoice>>(response);
        }

        public SupplierInvoice Save(SupplierInvoice invoice)
        {
            var url = $"SupplierInvoice/Save?apikey={_apiKey}&companyid={_companyId}";
            var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(invoice);
            var response = _client.Execute<SupplierInvoice>(request);
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.Data;
        }

        public async Task<SupplierInvoice> SaveAsync(SupplierInvoice invoice)
        {
            var url = $"SupplierInvoice/Save?apikey={_apiKey}&companyid={_companyId}";
            var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(invoice);
            var response = await _client.ExecuteTaskAsync<SupplierInvoice>(request);
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.Data;
        }

        public SupplierInvoice Calculate(SupplierInvoice invoice)
        {
            var url = $"SupplierInvoice/Calculate?apikey={_apiKey}&companyid={_companyId}";
            var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(invoice);
            var response = _client.Execute<SupplierInvoice>(request);
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.Data;
        }

        public bool Email(EmailRequest email)
        {
            var url = $"SupplierInvoice/Email?apikey={_apiKey}&companyid={_companyId}";
            var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(email);
            var response = _client.Execute<EmailRequest>(request);
            return response.ResponseStatus == ResponseStatus.Completed;
        }
    }
}