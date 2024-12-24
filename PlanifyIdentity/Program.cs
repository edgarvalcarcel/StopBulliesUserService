using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using PlanifyIdentity.Database;
using PlanifyIdentity.Domain.Entities;
using PlanifyIdentity.Extensions;
using PlanifyIdentity.Infrastructure;

using Swashbuckle.AspNetCore.Filters;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
//  Entity Framework Core Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

// Authentication & Authorization Configuration
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(IdentityConstants.BearerScheme).AddBearerToken();
builder.Services.AddAuthorizationBuilder();
// Configuraci�n de Identity Core
builder.Services.AddIdentityApiEndpoints<User>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
/* Authorization Button on Swagger */
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please type token : Bearer {token}",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

/* comment for inmigration */
builder.Services.AddScoped<ApplicationDbContextInitializer>();
/* Uncomment for inmigration */

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
});
builder.Services.AddTransient<IEmailSender, MailKitEmailSender>();
builder.Services.AddTransient<MailDesign>();

builder.Services.Configure<MailDesign>(options => 
    options.HtmlDesign = builder.Configuration["MailDesign:HtmlDesign"] ?? "");

builder.Services.Configure<MailKitEmailSenderOptions>(options =>
{
    options.Host_Address = builder.Configuration["ExternalProviders:MailKit:SMTP:Address"];
    options.Host_Port = Convert.ToInt32(builder.Configuration["ExternalProviders:MailKit:SMTP:Port"]);
    options.Host_Username = builder.Configuration["ExternalProviders:MailKit:SMTP:Account"] ?? "";
    options.Host_Password = builder.Configuration["ExternalProviders:MailKit:SMTP:Password"] ?? "";
    options.Sender_EMail = builder.Configuration["ExternalProviders:MailKit:SMTP:SenderEmail"] ?? "";
    options.Sender_Name = builder.Configuration["ExternalProviders:MailKit:SMTP:SenderName"] ?? "";
});
WebApplication app = builder.Build();

using IServiceScope scope = app.Services.CreateScope();
ApplicationDbContextInitializer initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
if (builder.Configuration.GetValue<bool>("SeedingDatabase"))
{
    await initializer.TrySeedAsync();
}

if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
    app.ApplyMigrations();
}
////app.UseSerilogRequestLogging();

app.MapGet("User/me", async (ClaimsPrincipal claims, ApplicationDbContext context) =>
{
    string userId = claims.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

    return await context.Users.FindAsync(userId);
})
    .WithOpenApi(o => { o.Tags[0].Name = "Users"; return o; })
    .WithSummary("Data of the user logged in").RequireAuthorization();

app.MapGet("/Hi", (ClaimsPrincipal user) => $"Hello {user.Identity!.Name}")
    .WithOpenApi(o => { o.Tags[0].Name = "Users"; return o; })
    .WithSummary("Hi User").RequireAuthorization();

app.MapGet("/Weather", async () =>
{
    var client = new HttpClient();
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    string longitude = "-74.063644";
    string latitude = "4.624335";
    string url = $"https://weatherbit-v1-mashape.p.rapidapi.com/current?lon={longitude}&lat={latitude}&units=metric&lang=en";
    var request = new HttpRequestMessage
    {
        Method = HttpMethod.Get,
        RequestUri = new Uri(url),
        Headers =
        {
            { "x-rapidapi-key", "e5d7c26cc2mshfcd70a329e479c7p1b3d94jsnac356a890845" },
            { "x-rapidapi-host", "weatherbit-v1-mashape.p.rapidapi.com" },
        },
    };
    using HttpResponseMessage response = await client.SendAsync(request);
    _ = response.EnsureSuccessStatusCode();

    string responseString = await response.Content.ReadAsStringAsync();
    WeatherResponse myDeserializedClass = JsonConvert.DeserializeObject<WeatherResponse>(responseString);

    return myDeserializedClass;
}
    ).WithOpenApi(o => { o.Tags[0].Name = "Weather"; return o; })
     .WithSummary("City Weather").RequireAuthorization();

app.MapGroup("/Identity")
    .WithOpenApi(o => { o.Tags[0].Name = "Identity"; return o; })
    .MapIdentityApi<User>();

app.MapGroup("/Identity")
    .WithOpenApi(o => { o.Tags[0].Name = "Identity"; return o; })
    .MapPost("/logout", async (SignInManager<User> signInManager) => await signInManager.SignOutAsync()
    .ConfigureAwait(false)).RequireAuthorization();
////app.MapGet("/secrets", () => secrets.OutgoingServer);
await app.RunAsync();
