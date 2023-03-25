using static cotacao_api.General.ViaCepClient;
using System.Net.Http;

namespace cotacao_api.General
{
    public class ViaCepClient
    {

        #region Private fields

        private const string _baseUrl = "https://viacep.com.br";

        private readonly HttpClient _httpClient;

        #endregion

        #region ~Ctors

        public ViaCepClient()
        {
            _httpClient = HttpClientFactory.Create();
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

       
        public ViaCepClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #endregion

        #region Public methods

        public ViaCepResult Search(string zipCode)
        {
            return SearchAsync(zipCode, CancellationToken.None).Result;
        }

        public async Task<ViaCepResult> SearchAsync(string zipCode, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"/ws/{zipCode}/json", cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<ViaCepResult>(cancellationToken).ConfigureAwait(false);
        }

        #endregion
    }
}

