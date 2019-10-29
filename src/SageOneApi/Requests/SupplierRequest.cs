using System.Threading.Tasks;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using RestSharp.Serializers;
using SageOneApi.Interfaces;
using SageOneApi.Models;

namespace SageOneApi.Requests
{
	public class SupplierRequest : RequestBase, ISupplierRequest
	{
		public SupplierRequest(IRestClient client, string apiKey, int companyId) : base(client, apiKey, companyId) { }

		public Supplier Get(int id)
		{
			var response = _client.Execute<Supplier>(new RestRequest(
                $"Supplier/Get/{id}?apikey={_apiKey}&companyid={_companyId}", Method.GET));
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.Data;
		}

		public PagingResponse<Supplier> Get(string filter = "", int skip = 0)
		{
			var url = $"Supplier/Get?apikey={_apiKey}&companyid={_companyId}";

			if (!string.IsNullOrEmpty(filter))
				url = $"Supplier/Get?apikey={_apiKey}&companyid={_companyId}&$filter={filter}";

			if (skip > 0)
				url += "&$skip=" + skip;

			var request = new RestRequest(url, Method.GET);
			request.RequestFormat = DataFormat.Json;

			var response = _client.Execute(request);
			JsonDeserializer deserializer = new JsonDeserializer();

            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return deserializer.Deserialize<PagingResponse<Supplier>>(response);
		}

	    public async Task<PagingResponse<Supplier>> GetAsync(string filter = "", int skip = 0)
	    {
	        var url = $"Supplier/Get?apikey={_apiKey}&companyid={_companyId}";

			if (!string.IsNullOrEmpty(filter))
				url = $"Supplier/Get?apikey={_apiKey}&companyid={_companyId}&$filter={filter}";

			if (skip > 0)
				url += "&$skip=" + skip;

			var request = new RestRequest(url, Method.GET);
			request.RequestFormat = DataFormat.Json;

			var response = await _client.ExecuteTaskAsync(request);
			JsonDeserializer deserializer = new JsonDeserializer();
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return deserializer.Deserialize<PagingResponse<Supplier>>(response);
	    }

        public Supplier Save(Supplier supplier)
		{
			var url = $"Supplier/Save?apikey={_apiKey}&companyid={_companyId}";
			var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
			request.RequestFormat = DataFormat.Json;
			request.AddBody(supplier);
			var response = _client.Execute<Supplier>(request);
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.Data;
		}

	    public async Task<Supplier> SaveAsync(Supplier supplier)
	    {
            var url = $"Supplier/Save?apikey={_apiKey}&companyid={_companyId}";
            var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(supplier);
            var response = await _client.ExecuteTaskAsync<Supplier>(request);
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.Data;
        }

	    public bool Delete(int id)
		{
			var url = $"Supplier/Delete/{id}?apikey={_apiKey}&companyid={_companyId}";
			var response = _client.Execute<Company>(new RestRequest(url, Method.DELETE));
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.ResponseStatus == ResponseStatus.Completed;
		}
	}
}