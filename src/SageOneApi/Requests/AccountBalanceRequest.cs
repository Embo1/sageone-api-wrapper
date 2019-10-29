using System;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using RestSharp.Serializers;
using SageOneApi.Interfaces;
using SageOneApi.Models;

namespace SageOneApi.Requests
{
	public class AccountBalanceRequest : RequestBase, IAccountBalanceRequest
	{
		public AccountBalanceRequest(IRestClient client, string apiKey, int companyId) : base(client, apiKey, companyId) { }

		public AccountBalance Get(DateTime fromDate, DateTime toDate)
		{
			var url = $"AccountBalance/Get?apikey={_apiKey}&companyid={_companyId}";
            var request = new RestRequest(url, Method.POST)
            {
                JsonSerializer = new JsonSerializer(), RequestFormat = DataFormat.Json
            };
            var requestObject = new AccountBalanceRequestObject { FromDate = fromDate, ToDate = toDate };
			request.AddBody(requestObject);
			var response = _client.Execute<AccountBalance>(request);
			return response.Data;
		}

		public PagingResponse<AccountBalance> GetAccountBudgetsById(int budgetId, string filter = "", int skip = 0)
		{
			var url =
                $"AccountBalance/GetAccountBudgetsById?budgetId={budgetId}&apikey={_apiKey}&companyid={_companyId}";

			if (!string.IsNullOrEmpty(filter))
				url =
                    $"AccountBalance/GetAccountBudgetsById?budgetId={budgetId}&apikey={_apiKey}&companyid={_companyId}&$filter={filter}";

			if (skip > 0)
				url += "&$skip=" + skip;

            var request = new RestRequest(url, Method.GET) {RequestFormat = DataFormat.Json};

            var response = _client.Execute(request);
			JsonDeserializer deserializer = new JsonDeserializer();

			return deserializer.Deserialize<PagingResponse<AccountBalance>>(response);
		}

		public PagingResponse<AccountBalance> GetAccountBudgets(string filter = "", int skip = 0)
		{
			var url = $"AccountBalance/GetAccountBudgets?apikey={_apiKey}&companyid={_companyId}";

			if (!string.IsNullOrEmpty(filter))
				url = $"AccountBalance/GetAccountBudgets?apikey={_apiKey}&companyid={_companyId}&$filter={filter}";

			if (skip > 0)
				url += "&$skip=" + skip;

            var request = new RestRequest(url, Method.GET) {RequestFormat = DataFormat.Json};

            var response = _client.Execute(request);
			JsonDeserializer deserializer = new JsonDeserializer();

			return deserializer.Deserialize<PagingResponse<AccountBalance>>(response);
		}
	}
}