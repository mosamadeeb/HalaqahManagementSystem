using System.Globalization;
using HalaqahBlazorApp.Components;
using MudBlazor.Services;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLocalization();
builder.Services.Configure<RequestLocalizationOptions>(
    options =>
    {
        List<CultureInfo> supportedCultures =
        [
            new CultureInfo("ar-SA"),
            new CultureInfo("en-US"),
        ];

        options.DefaultRequestCulture = new RequestCulture("ar-SA");

        // Formatting numbers, dates, etc.
        options.SupportedCultures = supportedCultures;

        // UI string 
        options.SupportedUICultures = supportedCultures;
    });

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Mudblazor
builder.Services.AddMudServices();

builder.Services.AddHttpClient("API", client => client.BaseAddress = new Uri("http://localhost:5285/"));
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("API"));

// Blazorise Bootstrap 5
// builder.Services
//     .AddBlazorise( options =>
//     {
//         options.Immediate = true;
//     } )
//     .AddBootstrap5Providers()
//     .AddFontAwesomeIcons();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseRequestLocalization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();