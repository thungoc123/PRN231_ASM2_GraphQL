using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyDNA.GraphQLClients.BlazorWAS.ThuNTN;
using MyDNA.GraphQLClients.BlazorWAS.ThuNTN.GraphQLClients;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// Ðây là m?t d?ng m?i ðý?c thêm vào
builder.Services.AddScoped<IGraphQLClient>(c => new GraphQLHttpClient(builder.Configuration["GraphQLURI"], new NewtonsoftJsonSerializer()));
builder.Services.AddScoped<GraphQLConsumer>();

await builder.Build().RunAsync();
