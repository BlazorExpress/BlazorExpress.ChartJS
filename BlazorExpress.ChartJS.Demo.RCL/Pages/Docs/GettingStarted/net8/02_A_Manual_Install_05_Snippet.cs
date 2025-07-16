// Program.cs
using BlazorExpress.Bulma;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
// ... your existing setup

// Register BlazorExpress.Bulma services (if required)
builder.Services.AddBlazorExpressBulma();

await builder.Build().RunAsync();