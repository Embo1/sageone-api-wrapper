using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using RestSharp.Serializers;
using SageOneApi.Interfaces;
using SageOneApi.Models;

namespace SageOneApi.Requests
{
    public class PurchaseOrderRequest : RequestBase, IPurchaseOrderRequest
    {
        public PurchaseOrderRequest(IRestClient client, string apiKey, int companyId) : base(client, apiKey, companyId) { }

        public PurchaseOrder Get(int id)
        {
            var response = _client.Execute<PurchaseOrder>(new RestRequest(
                $"PurchaseOrder/Get/{id}?apikey={_apiKey}&companyid={_companyId}", Method.GET));
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.Data;
        }

        public PagingResponse<PurchaseOrder> Get(bool includeDetail = false, bool includeSupplierDetails = false, string filter = "", int skip = 0)
        {
            var url =
                $"PurchaseOrder/Get?companyid={_companyId}&includeDetail={includeDetail.ToString().ToLower()}&includeSupplierDetails={includeSupplierDetails.ToString().ToLower()}&apikey={_apiKey}";

            if (!string.IsNullOrEmpty(filter))
                url =
                    $"PurchaseOrder/Get?includeDetail={includeDetail}&includeSupplierDetails={includeSupplierDetails}?apikey={_apiKey}&companyid={_companyId}&$filter={filter}";

            if (skip > 0)
                url += "&$skip=" + skip;

            var request = new RestRequest(url, Method.GET);
            request.RequestFormat = DataFormat.Json;

            var response = _client.Execute(request);
            JsonDeserializer deserializer = new JsonDeserializer();

            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return deserializer.Deserialize<PagingResponse<PurchaseOrder>>(response);
        }

        public PurchaseOrder Save(PurchaseOrder purchaseOrder)
        {
            var url = $"PurchaseOrder/Save?apikey={_apiKey}&companyid={_companyId}";
            var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(purchaseOrder);
            var response = _client.Execute<PurchaseOrder>(request);
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.Data;
        }

        public PurchaseOrder Calculate(PurchaseOrder purchaseOrder)
        {
            var url = $"PurchaseOrder/Calculate?apikey={_apiKey}&companyid={_companyId}";
            var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(purchaseOrder);
            var response = _client.Execute<PurchaseOrder>(request);
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.Data;
        }

        public bool Delete(int id)
        {
            var url = $"PurchaseOrder/Delete/{id}?apikey={_apiKey}&companyid={_companyId}";
            var response = _client.Execute<PurchaseOrder>(new RestRequest(url, Method.DELETE));
            return response.ResponseStatus == ResponseStatus.Completed;
        }
    }
}