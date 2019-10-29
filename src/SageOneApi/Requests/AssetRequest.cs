using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using RestSharp.Serializers;
using SageOneApi.Interfaces;
using SageOneApi.Models;

namespace SageOneApi.Requests
{
	public class AssetRequest : RequestBase, IAssetRequest
	{
		public AssetRequest(IRestClient client, string apiKey, int companyId) : base(client, apiKey, companyId) { }

		public Asset Get(int id)
		{
			var response = _client.Execute<Asset>(new RestRequest(
                $"Asset/Get/{id}?apikey={_apiKey}&companyid={_companyId}", Method.GET));
			return response.Data;
		}

		public PagingResponse<Asset> Get(string filter = "", int skip = 0)
		{
			var url = $"Asset/Get?apikey={_apiKey}&companyid={_companyId}";

			if (!string.IsNullOrEmpty(filter))
				url = $"Asset/Get?apikey={_apiKey}&companyid={_companyId}&$filter={filter}";

			if (skip > 0)
				url += "&$skip=" + skip;

			var request = new RestRequest(url, Method.GET);
			request.RequestFormat = DataFormat.Json;

			var response = _client.Execute(request);
			JsonDeserializer deserializer = new JsonDeserializer();

			return deserializer.Deserialize<PagingResponse<Asset>>(response);
		}

		public Asset Save(Asset asset)
		{
			var url = $"Asset/Save?apikey={_apiKey}&companyid={_companyId}";
			var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
			request.RequestFormat = DataFormat.Json;
			request.AddBody(asset);
			var response = _client.Execute<Asset>(request);
			return response.Data;
		}

		public bool Delete(int id)
		{
			var url = $"Asset/Delete/{id}?apikey={_apiKey}&companyid={_companyId}";
			var response = _client.Execute<Asset>(new RestRequest(url, Method.DELETE));
			return response.ResponseStatus == ResponseStatus.Completed;
		}
	}
}