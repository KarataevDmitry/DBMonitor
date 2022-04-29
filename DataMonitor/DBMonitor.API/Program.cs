
using DBMonitor.API.Validators;
using DBMonitor.BLL;
using DBMonitor.DAL;
using DBMonitor.DAL.Interfaces;
using DBMonitor.DAL.Services;

using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CorsPolicy",
//        builder => builder
//        .AllowAnyOrigin()
//        .AllowAnyHeader()
//        .AllowAnyMethod()
//        );
//});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocument(
    config =>
    {
        config.PostProcess = document =>
        {
            document.Info.Version = "v1";
            document.Info.Title = "DB Monitoring API";
            document.Info.Description = "API for rule-based database monitoring";
            document.Info.TermsOfService = "None";
            document.Info.Contact = new NSwag.OpenApiContact
            {
                Name = "Dmitry Karataev",
                Email = "dkarataev1990@gmail.com",
                ExtensionData = new Dictionary<string, object>()
                {
                    { "Telegram", "https://t.me/Krawler" },
                    {"Skype", "https://join.skype.com/invite/nLLqAtfgOM3d" },
                    {"Discord", "https://discordapp.com/users/LonelySoul#7246" }
                }
            };

        };
    });
builder.Services.AddDbContext<ApplicationDbContext>();
//builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
builder.Services.AddScoped<IDBService<LaunchHistory>, LaunchHistoryDBService>();
builder.Services.AddScoped<IDBService<Rule>, RuleDBService>();
builder.Services.AddScoped<IValidator<Rule>, RuleValidator>();

//builder.Services.AddAuthorization(options =>
//{
//    // By default, all incoming requests will be authorized according to the default policy.
//    options.FallbackPolicy = options.DefaultPolicy;
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{

//}
app.UseOpenApi();
app.UseSwaggerUi3();
app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();
