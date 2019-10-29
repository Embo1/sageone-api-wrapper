using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using SageOneApi.Interfaces;
using SageOneApi.Models;

namespace SageOneApi.Requests
{
	public class CompanyRequest : RequestBase, ICompanyRequest
	{
		public CompanyRequest(IRestClient client, string apiKey) : base(client, apiKey) { }

		public PagingResponse<Company> Current()
		{
			var url = $"Company/Get?apikey={_apiKey}";
			var request = new RestRequest(url, Method.GET);
			request.RequestFormat = DataFormat.Json;

			var response = _client.Execute(request);
			JsonDeserializer deserializer = new JsonDeserializer();
			return deserializer.Deserialize<PagingResponse<Company>>(response);
		}

	    public PagingResponse<Company> Get(string filter = "", int skip = 0)
	    {
            var url = $"Company/Get?apikey={_apiKey}";

            if (!string.IsNullOrEmpty(filter))
                url = $"Company/Get?apikey={_apiKey}&$filter={filter}";

            if (skip > 0)
                url += "&$skip=" + skip;

            var request = new RestRequest(url, Method.GET);
            request.RequestFormat = DataFormat.Json;

            var response = _client.Execute(request);
            JsonDeserializer deserializer = new JsonDeserializer();

            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return deserializer.Deserialize<PagingResponse<Company>>(response);
        }

	    public Company Get(int id)
	    {
            var response = _client.Execute<Company>(new RestRequest($"Company/Get/{id}?apikey={_apiKey}", Method.GET));
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.Data;
        }
	}
}