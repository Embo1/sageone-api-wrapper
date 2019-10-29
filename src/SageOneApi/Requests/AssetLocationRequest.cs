using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using RestSharp.Serializers;
using SageOneApi.Interfaces;
using SageOneApi.Models;

namespace SageOneApi.Requests
{
	public class AssetLocationRequest : RequestBase, IAssetLocationLocationRequest
	{
		public AssetLocationRequest(IRestClient client, string apiKey, int companyId) : base(client, apiKey, companyId) { }

		public AssetLocation Get(int id)
		{
			var response = _client.Execute<AssetLocation>(new RestRequest(
                $"AssetLocation/Get/{id}?apikey={_apiKey}&companyid={_companyId}", Method.GET));
			return response.Data;
		}

		public PagingResponse<AssetLocation> Get(string filter = "", int skip = 0)
		{
			var url = $"AssetLocation/Get?apikey={_apiKey}&companyid={_companyId}";

			if (!string.IsNullOrEmpty(filter))
				url = $"AssetLocation/Get?apikey={_apiKey}&companyid={_companyId}&$filter={filter}";

			if (skip > 0)
				url += "&$skip=" + skip;

			var request = new RestRequest(url, Method.GET);
			request.RequestFormat = DataFormat.Json;

			var response = _client.Execute(request);
			JsonDeserializer deserializer = new JsonDeserializer();

			return deserializer.Deserialize<PagingResponse<AssetLocation>>(response);
		}

		public AssetLocation Save(AssetLocation assetLocation)
		{
			var url = $"AssetLocation/Save?apikey={_apiKey}&companyid={_companyId}";
			var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
			request.RequestFormat = DataFormat.Json;
			request.AddBody(assetLocation);
			var response = _client.Execute<AssetLocation>(request);
			return response.Data;
		}

		public bool Delete(int id)
		{
			var url = $"AssetLocation/Delete/{id}?apikey={_apiKey}&companyid={_companyId}";
			var response = _client.Execute<AssetLocation>(new RestRequest(url, Method.DELETE));
			return response.ResponseStatus == ResponseStatus.Completed;
		}
	}
}