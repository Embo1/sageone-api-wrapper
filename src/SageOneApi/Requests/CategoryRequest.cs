using System;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using RestSharp.Serializers;
using SageOneApi.Interfaces;
using SageOneApi.Models;

namespace SageOneApi.Requests
{
	public class CategoryRequest : RequestBase, ICategoryRequest
	{
		public CategoryRequest(IRestClient client, string apiKey, int companyId) : base(client, apiKey, companyId) { }

		public Category Get(int id, Enums.CategoryType categoryType)
		{
			var response = _client.Execute<Category>(new RestRequest(
                $"{categoryType}Category/Get/{id}?apikey={_apiKey}&companyid={_companyId}", Method.GET));
			return response.Data;
		}

		public PagingResponse<Category> Get(Enums.CategoryType categoryType, string filter = "", int skip = 0)
		{
			var url = $"{categoryType}Category/Get?apikey={_apiKey}&companyid={_companyId}";

			if (!string.IsNullOrEmpty(filter))
				url = $"{categoryType}Category/Get?apikey={_apiKey}&companyid={_companyId}&$filter={filter}";

			if (skip > 0)
				url += "&$skip=" + skip;

			var request = new RestRequest(url, Method.GET);
			request.RequestFormat = DataFormat.Json;

			var response = _client.Execute(request);
			JsonDeserializer deserializer = new JsonDeserializer();

			return deserializer.Deserialize<PagingResponse<Category>>(response);
		}

		public Category Save(Category category, Enums.CategoryType categoryType)
		{
			if (categoryType == Enums.CategoryType.Account)
			{
				throw new NotSupportedException("Account Category Save is not supported by the API.");
			}

			var url = $"{categoryType}Category/Save?apikey={_apiKey}&companyid={_companyId}";
			var request = new RestRequest(url, Method.POST) { JsonSerializer = new JsonSerializer() };
			request.RequestFormat = DataFormat.Json;
			request.AddBody(category);
			var response = _client.Execute<Category>(request);
			return response.Data;
		}

		public bool Delete(int id, Enums.CategoryType categoryType)
		{
			if (categoryType == Enums.CategoryType.Account)
			{
				throw new NotSupportedException("Account Category Delete is not supported by the API.");
			}

			var url = $"{categoryType}Category/Delete/{id}?apikey={_apiKey}&companyid={_companyId}";
			var response = _client.Execute<Category>(new RestRequest(url, Method.DELETE));
			return response.ResponseStatus == ResponseStatus.Completed;
		}
	}
}