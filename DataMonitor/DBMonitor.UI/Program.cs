using DBMonitor.APIClient;

using Microsoft.AspNetCore.Authentication.Negotiate;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddHttpClient();
var handler = new HttpClientHandler()
{
    UseDefaultCredentials = true
};

builder.Services.AddHttpClient("windowsAuthClient", c => { })
    .ConfigurePrimaryHttpMessageHandler(() => handler);
builder.Services.AddTransient<IDataMonitorAPIClient, DataMonitorAPIClient>();
builder.Services.AddTransient<IRuleAPIService, RuleAPIService>();
builder.Services.AddTransient<ILaunchHistoryAPIService, LaunchHistoryAPIService>();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}
app.MapGet("/debug/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
        string.Join("\n", endpointSources.SelectMany(source => source.Endpoints.Select(x => x.Metadata))));
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseHttpMethodOverride();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllers();


app.Run();
