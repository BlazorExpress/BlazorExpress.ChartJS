// Since .NET 10, a NavLink with Match="All" ignores the query string and fragment. The Samples menu links differ only by
// ?category=, so without this switch every one of them shows as selected. NavLink reads the switch once, so set it first.
AppContext.SetSwitch("Microsoft.AspNetCore.Components.Routing.NavLink.EnableMatchAllForQueryStringAndFragment", true);

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
