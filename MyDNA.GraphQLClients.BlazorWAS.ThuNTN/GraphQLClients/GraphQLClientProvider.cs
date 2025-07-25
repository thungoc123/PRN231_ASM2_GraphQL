using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.JSInterop;
using System.Net.Http.Headers;

namespace MyDNA.GraphQLClients.BlazorWAS.ThuNTN.GraphQLClients
{
    public class GraphQLClientProvider
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly IConfiguration _config;

        public GraphQLClientProvider(IJSRuntime jsRuntime, IConfiguration config)
        {
            _jsRuntime = jsRuntime;
            _config = config;
        }

        public async Task<GraphQLHttpClient> GetClientAsync()
        {
            var token = await _jsRuntime.InvokeAsync<string>(
                "eval",
                "document.cookie.match('(^|;)\\s*authToken\\s*=\\s*([^;]+)')?.pop()"
            );

            var httpClient = new HttpClient();
            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var endpoint = _config["GraphQLURI"];
            return new GraphQLHttpClient(new GraphQLHttpClientOptions
            {
                EndPoint = new Uri(endpoint)
            }, new NewtonsoftJsonSerializer(), httpClient);
        }
    }
}
