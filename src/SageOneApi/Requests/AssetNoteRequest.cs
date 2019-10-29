using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using RestSharp.Serializers;
using SageOneApi.Interfaces;
using SageOneApi.Models;

namespace SageOneApi.Requests
{
	public class AssetNoteRequest : RequestBase, IAssetNoteRequest
	{
		public AssetNoteRequest(IRestClient client, string apiKey, int companyId) : base(client, apiKey, companyId) { }

		public AssetNote Get(int id)
		{
			var response = _client.Execute<AssetNote>(new RestRequest(
                $"AssetNote/Get/{id}?apikey={_apiKey}&companyid={_companyId}", Method.GET));
			return response.Data;
		}

		public PagingResponse<AssetNote> Get(string filter = "", int skip = 0)
		{
			var url = $"AssetNote/Get?apikey={_apiKey}&companyid={_companyId}";

			if (!string.IsNullOrEmpty(filter))
				url = $"AssetNote/Get?apikey={_apiKey}&companyid={_companyId}&$filter={filter}";

			if (skip > 0)
				url += "&$skip=" + skip;

			var request = new RestRequest(url, Method.GET);
			request.RequestFormat = DataFormat.Json;

			var response = _client.Execute(request);
			JsonDeserializer deserializer = new JsonDeserializer();

			return deserializer.Deserialize<PagingResponse<AssetNote>>(response);
		}

		public AssetNote Save(AssetNote assetNote)
		{
			var url = $"AssetNote/Save?apikey={_apiKey}&companyid={_companyId}";
			var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
			request.RequestFormat = DataFormat.Json;
			request.AddBody(assetNote);
			var response = _client.Execute<AssetNote>(request);
			return response.Data;
		}

		public bool Delete(int id)
		{
			var url = $"AssetNote/Delete/{id}?apikey={_apiKey}&companyid={_companyId}";
			var response = _client.Execute<AssetNote>(new RestRequest(url, Method.DELETE));
			return response.ResponseStatus == ResponseStatus.Completed;
		}
	}
}