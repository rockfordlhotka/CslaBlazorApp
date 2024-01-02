using Csla.Configuration;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddCsla(opt => opt
    .AddBlazorWebAssembly(o => o.SyncContextWithServer = true));

await builder.Build().RunAsync();
