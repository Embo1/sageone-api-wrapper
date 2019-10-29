using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using RestSharp.Serializers;
using SageOneApi.Interfaces;
using SageOneApi.Models;

namespace SageOneApi.Requests
{
	public class AccountNoteRequest : RequestBase, IAccountNoteRequest
	{
		public AccountNoteRequest(IRestClient client, string apiKey, int companyId) : base(client, apiKey, companyId) { }

		public AccountNote Get(int id)
		{
			var response = _client.Execute<AccountNote>(new RestRequest(
                $"AccountNote/Get/{id}?apikey={_apiKey}&companyid={_companyId}", Method.GET));
			return response.Data;
		}

		public PagingResponse<AccountNote> Get(string filter = "", int skip = 0)
		{
			var url = $"AccountNote/Get?apikey={_apiKey}&companyid={_companyId}";

			if (!string.IsNullOrEmpty(filter))
				url = $"AccountNote/Get?apikey={_apiKey}&companyid={_companyId}&$filter={filter}";

			if (skip > 0)
				url += "&$skip=" + skip;

			var request = new RestRequest(url, Method.GET);
			request.RequestFormat = DataFormat.Json;

			var response = _client.Execute(request);
			JsonDeserializer deserializer = new JsonDeserializer();

			return deserializer.Deserialize<PagingResponse<AccountNote>>(response);
		}

		public AccountNote Save(AccountNote accountNote)
		{
			var url = $"AccountNote/Save?apikey={_apiKey}&companyid={_companyId}";
			var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
			request.RequestFormat = DataFormat.Json;
			request.AddBody(accountNote);
			var response = _client.Execute<AccountNote>(request);
			return response.Data;
		}

		public bool Delete(int id)
		{
			var url = $"AccountNote/Delete/{id}?apikey={_apiKey}&companyid={_companyId}";
			var response = _client.Execute<AccountNote>(new RestRequest(url, Method.DELETE));
			return response.ResponseStatus == ResponseStatus.Completed;
		}
	}
}