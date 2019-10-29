using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using RestSharp.Serializers;
using SageOneApi.Interfaces;
using SageOneApi.Models;

namespace SageOneApi.Requests
{
	public class SalesRepresentativeRequest : RequestBase, ISalesRepresentativeRequest
	{
		public SalesRepresentativeRequest(IRestClient client, string apiKey, int companyId) : base(client, apiKey, companyId) { }

		public SalesRepresentative Get(int id)
		{
			var response = _client.Execute<SalesRepresentative>(new RestRequest(
                $"SalesRepresentative/Get/{id}?apikey={_apiKey}&companyid={_companyId}", Method.GET));
			return response.Data;
		}

		public PagingResponse<SalesRepresentative> Get(string filter = "", int skip = 0)
		{
			var url = $"SalesRepresentative/Get?apikey={_apiKey}&companyid={_companyId}";

			if (!string.IsNullOrEmpty(filter))
				url = $"SalesRepresentative/Get?apikey={_apiKey}&companyid={_companyId}&$filter={filter}";

			if (skip > 0)
				url += "&$skip=" + skip;

			var request = new RestRequest(url, Method.GET);
			request.RequestFormat = DataFormat.Json;

			var response = _client.Execute(request);
			JsonDeserializer deserializer = new JsonDeserializer();

			return deserializer.Deserialize<PagingResponse<SalesRepresentative>>(response);
		}

		public SalesRepresentative Save(SalesRepresentative customer)
		{
			var url = $"SalesRepresentative/Save?apikey={_apiKey}&companyid={_companyId}";
			var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
			request.RequestFormat = DataFormat.Json;
			request.AddBody(customer);
			var response = _client.Execute<SalesRepresentative>(request);
			return response.Data;
		}

		public bool Delete(int id)
		{
			var url = $"SalesRepresentative/Delete/{id}?apikey={_apiKey}&companyid={_companyId}";
			var response = _client.Execute<SalesRepresentative>(new RestRequest(url, Method.DELETE));
			return response.ResponseStatus == ResponseStatus.Completed;
		}

		public bool HasActivity(int id)
		{
			var url = $"SalesRepresentative/HasActivity/{id}?apikey={_apiKey}&companyid={_companyId}";
			var response = _client.Execute<bool>(new RestRequest(url, Method.GET));
			return response.Data;
		}
	}
}