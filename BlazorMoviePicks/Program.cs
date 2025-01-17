using Blazored.LocalStorage;
using BlazorMoviePicks;
using BlazorMoviePicks.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ITmdbClient, TmdbClient>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IMovieStateService, MovieStateService>();

await builder.Build().RunAsync();
