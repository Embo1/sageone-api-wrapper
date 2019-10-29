using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using RestSharp.Serializers;
using SageOneApi.Interfaces;
using SageOneApi.Models;

namespace SageOneApi.Requests
{
	public class CustomerRequest : RequestBase, ICustomerRequest
	{
		public CustomerRequest(IRestClient client, string apiKey, int companyId) : base(client, apiKey, companyId) { }

		public Customer Get(int id)
		{
			var response = _client.Execute<Customer>(new RestRequest(
                $"Customer/Get/{id}?apikey={_apiKey}&companyid={_companyId}", Method.GET));
			return response.Data;
		}

		public PagingResponse<Customer> Get(string filter = "", int skip = 0)
		{
			var url = $"Customer/Get?apikey={_apiKey}&companyid={_companyId}";

			if (!string.IsNullOrEmpty(filter))
				url = $"Customer/Get?apikey={_apiKey}&companyid={_companyId}&$filter={filter}";

			if (skip > 0)
				url += "&$skip=" + skip;

			var request = new RestRequest(url, Method.GET);
			request.RequestFormat = DataFormat.Json;

			var response = _client.Execute(request);
			JsonDeserializer deserializer = new JsonDeserializer();

			return deserializer.Deserialize<PagingResponse<Customer>>(response);
		}

		public Customer Save(Customer customer)
		{
			var url = $"Customer/Save?apikey={_apiKey}&companyid={_companyId}";
			var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
			request.RequestFormat = DataFormat.Json;
			request.AddBody(customer);
			var response = _client.Execute<Customer>(request);
			return response.Data;
		}

		public bool Delete(int id)
		{
			var url = $"Customer/Delete/{id}?apikey={_apiKey}&companyid={_companyId}";
			var response = _client.Execute<Customer>(new RestRequest(url, Method.DELETE));
			return response.ResponseStatus == ResponseStatus.Completed;
		}
	}
}