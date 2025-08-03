using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Core;
using System.Configuration;
using System.Text;
//using Technofair.BGService;
using Technofair.Data;
using TFSMS.Admin.Model.Common;
using Technofair.SMS;
using TFSMS.Admin.Helper;
using Technofair.Utiity.Helper;
using Technofair.Utiity.Log;
//using Technofair.Utiity.Logger;



var builder = WebApplication.CreateBuilder(args);

//New: 26-05-2025
string webRootPath = builder.Environment.WebRootPath;
string path = Path.Combine(webRootPath, "Config.txt");
var encryptedConnStr = File.ReadAllText(path);
var connectionString = Technofair.Utiity.Security.AES.GetPlainText(encryptedConnStr);

ProgramConfigaration.WebRootPath = webRootPath;
ProgramConfigaration.ConnectionString = connectionString;
//End

builder.Host.UseSerilog((hostingContext, loggerConfig) =>
                    loggerConfig.ReadFrom.Configuration(hostingContext.Configuration)
                );

var services = builder.Services;

// Get the WebRootPath as a string
//Old: Commented 25052025
//string webRootPath = builder.Environment.WebRootPath;

builder.Services.AddSingleton<ITFLogger>(provider => new TFLogger(webRootPath));


//Added On 07.01.2024
services.AddHttpContextAccessor();

var origins = builder.Configuration.GetValue<string>("CORSURL:ClientUrlURl").Split(',');
services.AddCors(options =>
{
    //AllowSpecificOrigin
    options.AddPolicy(name: "AllowAll",
        builder =>
        {
            builder.WithOrigins(origins)
                   .AllowAnyHeader()
                   .AllowAnyMethod();

        });
});

services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


services.AddControllers();
services.AddMvc();
services.AddControllersWithViews();

//it's for background service run
//services.AddHostedService<TimedHostedService>();


// configure strongly typed settings object
services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

//Asad:New

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = true,
          ValidIssuer = "http://localhost:5269/",
          ValidateAudience = true,
          ValidAudience = "http://localhost:5269/",
          ValidateLifetime = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKeyHereYourSecretKeyHere"))
      };
  });

services.AddAuthorization(options =>
{
    // Policy 1: Client App
    options.AddPolicy("Authenticated", policy =>
    policy.RequireClaim("UserId"));

    // Policy 2: TF Admin
    options.AddPolicy("Admin", policy =>
        policy.RequireClaim("UserId"));

    // Policy 3: AternateDeliveryChannel
    options.AddPolicy("ADC", policy =>
        policy.RequireClaim("UserId"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

//New: 25052025
//var webRootPath = app.Environment.WebRootPath;
//End

//Added- 26052025
if (app.Environment.IsProduction())
{
    ProgramConfigaration.IsProduction = true;
}
if (app.Environment.IsDevelopment())
{
    ProgramConfigaration.IsProduction = false;
}
//End


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
//app.UseMiddleware<JwtMiddleware>();


DefaultFilesOptions options = new DefaultFilesOptions();
options.DefaultFileNames.Clear();
options.DefaultFileNames.Add("/index.html");
app.UseDefaultFiles(options);
app.UseStaticFiles();
app.UseFileServer(enableDirectoryBrowsing: false);
app.UseRouting();
//Asad Commented On 05.09.2024
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowAll");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Use the service in your web application
//app.MapGet("/", (TFLogger service) =>
//{
//    var wwwRootPath = service.GetWwwRootPath();
//    return wwwRootPath;
//});


app.Run();
//app.Run("http://localhost:4000");
