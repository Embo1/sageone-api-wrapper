using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using RestSharp.Serializers;
using SageOneApi.Interfaces;
using SageOneApi.Models;

namespace SageOneApi.Requests
{
    public class JournalEntryRequest : RequestBase, IJournalEntryRequest
    {
        public JournalEntryRequest(IRestClient client, string apiKey, int companyId) : base(client, apiKey, companyId) { }

        public JournalEntry Get(int id)
        {
            var response = _client.Execute<JournalEntry>(new RestRequest(
                $"JournalEntry/Get/{id}?apikey={_apiKey}&companyid={_companyId}", Method.GET));
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.Data;
        }

        public PagingResponse<JournalEntry> Get(bool includeDetail = false, bool includeSupplierDetails = false, string filter = "", int skip = 0)
        {
            var url =
                $"JournalEntry/Get?companyid={_companyId}&includeDetail={includeDetail.ToString().ToLower()}&includeSupplierDetails={includeSupplierDetails.ToString().ToLower()}&apikey={_apiKey}";

            if (!string.IsNullOrEmpty(filter))
                url += $"&$filter={filter}";

            if (skip > 0)
                url += "&$skip=" + skip;

            var request = new RestRequest(url, Method.GET) {RequestFormat = DataFormat.Json};

            var response = _client.Execute(request);
            JsonDeserializer deserializer = new JsonDeserializer();

            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return deserializer.Deserialize<PagingResponse<JournalEntry>>(response);
        }

        public JournalEntry Save(JournalEntry journalEntry)
        {
            var url = $"JournalEntry/Save?apikey={_apiKey}&companyid={_companyId}";
            var request = new RestRequest(url, Method.POST)
            {
                JsonSerializer = new JsonSerializer(), RequestFormat = DataFormat.Json
            };
            request.AddBody(journalEntry);
            var response = _client.Execute<JournalEntry>(request);
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.Data;
        }

        public JournalEntry Calculate(JournalEntry journalEntry)
        {
            var url = $"JournalEntry/Calculate?apikey={_apiKey}&companyid={_companyId}";
            var request = new RestRequest(url, Method.POST)
            {
                JsonSerializer = new JsonSerializer(), RequestFormat = DataFormat.Json
            };
            request.AddBody(journalEntry);
            var response = _client.Execute<JournalEntry>(request);
            StatusDescription = response.StatusDescription;
            StatusCode = response.StatusCode;
            return response.Data;
        }

        public bool Delete(int id)
        {
            var url = $"JournalEntry/Delete/{id}?apikey={_apiKey}&companyid={_companyId}";
            var response = _client.Execute<JournalEntry>(new RestRequest(url, Method.DELETE));
            return response.ResponseStatus == ResponseStatus.Completed;
        }

        //TODO: Implement SaveBatch Method
    }
}