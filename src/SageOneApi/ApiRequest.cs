using System;
using RestSharp;
using RestSharp.Authenticators;
using SageOneApi.Requests;

namespace SageOneApi
{
    public class ApiRequest
    {
        private readonly string _apiKey;
        private readonly int _companyId;
        private readonly IRestClient _client;

        public AccountRequest AccountRequest => new AccountRequest(_client, _apiKey, _companyId);
        public AccountReceiptRequest AccountReceiptRequest => new AccountReceiptRequest(_client, _apiKey, _companyId);
        public AccountBalanceRequest AccountBalanceRequest => new AccountBalanceRequest(_client, _apiKey, _companyId);
        public AccountNoteRequest AccountNoteRequest => new AccountNoteRequest(_client, _apiKey, _companyId);
        public AdditionalPriceListRequest AdditionalPriceListRequest => new AdditionalPriceListRequest(_client, _apiKey, _companyId);
        public AdditionalItemPriceRequest AdditionalItemPriceRequest => new AdditionalItemPriceRequest(_client, _apiKey, _companyId);
        public AssetRequest AssetRequest => new AssetRequest(_client, _apiKey, _companyId);
        public AssetLocationRequest AssetLocationRequest => new AssetLocationRequest(_client, _apiKey, _companyId);
        public AssetNoteRequest AssetNoteRequest => new AssetNoteRequest(_client, _apiKey, _companyId);
        public BankAccountRequest BankAccountRequest => new BankAccountRequest(_client, _apiKey, _companyId);
        public BankTransactionRequest BankTransactionRequest => new BankTransactionRequest(_client, _apiKey, _companyId);
        public ItemRequest ItemRequest => new ItemRequest(_client, _apiKey, _companyId);
        public ItemNoteRequest ItemNoteRequest => new ItemNoteRequest(_client, _apiKey, _companyId);
        public ItemMovementRequest ItemMovementRequest => new ItemMovementRequest(_client, _apiKey, _companyId);
        public JournalEntryRequest JournalEntryRequest => new JournalEntryRequest(_client, _apiKey, _companyId);
        public CompanyRequest CompanyRequest => new CompanyRequest(_client, _apiKey);
        public CustomerRequest CustomerRequest => new CustomerRequest(_client, _apiKey, _companyId);
        public CustomerNoteRequest CustomerNoteRequest => new CustomerNoteRequest(_client, _apiKey, _companyId);
        public CustomerZoneRequest CustomerZoneRequest => new CustomerZoneRequest(_client, _apiKey, _companyId);
        public CategoryRequest CategoryRequest => new CategoryRequest(_client, _apiKey, _companyId);
        public PurchaseOrderRequest PurchaseOrderRequest => new PurchaseOrderRequest(_client, _apiKey, _companyId);
        public QuoteRequest QuoteRequest => new QuoteRequest(_client, _apiKey, _companyId);
        public SalesRepresentativeRequest SalesRepresentativeRequest => new SalesRepresentativeRequest(_client, _apiKey, _companyId);
        public SupplierInvoiceRequest SupplierInvoiceRequest => new SupplierInvoiceRequest(_client, _apiKey, _companyId);
        public SupplierRequest SupplierRequest => new SupplierRequest(_client, _apiKey, _companyId);
        public TaxInvoiceRequest TaxInvoiceRequest => new TaxInvoiceRequest(_client, _apiKey, _companyId);
        public TaxTypeRequest TaxTypeRequest => new TaxTypeRequest(_client, _apiKey, _companyId);

        public ApiRequest(string username, string password, string apiKey, int companyId)
        {
            _apiKey = apiKey;
            _companyId = companyId;

            _client = new RestClient
            {
                BaseUrl = new Uri("https://accounting.sageone.co.za/api/2.0.0/"),
                Authenticator = new HttpBasicAuthenticator(username, password)
            };
        }
    }
}
