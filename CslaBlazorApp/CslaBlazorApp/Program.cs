using Csla.Configuration;
using CslaBlazorApp.Client.Pages;
using CslaBlazorApp.Components;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string BlazorClientPolicy = "AllowAllOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(BlazorClientPolicy,
      builder =>
      {
          builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
      });
});

builder.Services.AddControllers();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddScoped(typeof(Csla.Core.IContextManager), typeof(Csla.AspNetCore.Blazor.ApplicationContextManagerBlazor));
builder.Services.AddHttpContextAccessor();
builder.Services.AddCsla(opt => opt
    .AddAspNetCore()
    .AddServerSideBlazor());

// If using Kestrel:
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

// If using IIS:
builder.Services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

app.Run();
