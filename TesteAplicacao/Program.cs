using System.Net;
using System.Net.Mime;
using Cuca_Api.Infraestructure.Context;
using Cuca_Api.Infraestructure.Middleware;
using Cuca_Api.Infraestructure.Repository;
using Cuca_Api.Infraestrutra.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using PD_Api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApiVersioning(
    o =>
    {
        o.ApiVersionReader = new HeaderApiVersionReader("api-version");
        o.AssumeDefaultVersionWhenUnspecified = true;
        o.DefaultApiVersion = new ApiVersion(1, 0);
        o.ReportApiVersions = true;
    }
);

builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true
).ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var result = new ValidationFailedResult(context.ModelState);
        result.ContentTypes.Add(MediaTypeNames.Application.Json);
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
        return result;
    };
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#pragma warning disable ASP0011 // Suggest using builder.Logging over Host.ConfigureLogging or WebHost.ConfigureLogging
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});
#pragma warning restore ASP0011 // Suggest using builder.Logging over Host.ConfigureLogging or WebHost.ConfigureLogging

#pragma warning disable ASP0013 // Suggest switching from using Configure methods to WebApplicationBuilder.Configuration

//builder.WebHost.ConfigureAppConfiguration(
//    c =>
//    {
//        c.AddSystemsManager(source =>
//        {
//            source.Path = "/pdi_api/";
//            source.AwsOptions = new Amazon.Extensions.NETCore.Setup.AWSOptions()
//            {
//                Profile = "pdi"
//            };
//        });
//    }
//);

#pragma warning restore ASP0013 // Suggest switching from using Configure methods to WebApplicationBuilder.Configuration

builder.WebHost.UseKestrel();

#if !DEBUG
if (ParameterStore.Ambiente(builder.Configuration) != "Test")
{
    SentrySdk.Init(o =>
    {
        o.Dsn = "https://d088e476c12324f98cbba1e8bfff53f4@o4509322451156992.ingest.us.sentry.io/4509328837246976";
        // When configuring for the first time, to see what the SDK is doing:
        o.Debug = false;
        // Set TracesSampleRate to 1.0 to capture 100%
        // of transactions for tracing.
        // We recommend adjusting this value in production
        o.TracesSampleRate = 1.0;

        // This option is recommended. It enables Sentry's "Release Health" feature.
        o.AutoSessionTracking = true;

        // Enabling this option is recommended for client applications only. It ensures all threads use the same global scope.
        o.IsGlobalModeEnabled = false;
    });
}
#endif

// Carrega as configurações do Cognito a partir do appsettings.json
//builder.Services.Configure<CognitoSettings>(builder.Configuration.GetSection("CognitoSettings"));

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
{
    //options.Events = new JwtBearerEvents
    //{
    //    OnAuthenticationFailed = context =>
    //    {
    //        Console.WriteLine("=== Authentication Failed ===");
    //        Console.WriteLine(context.Exception.Message);
    //        Console.WriteLine(context.Exception.ToString());
    //        return Task.CompletedTask;
    //    },
    //    OnTokenValidated = context =>
    //    {
    //        Console.WriteLine("=== Token Validated Successfully ===");
    //        return Task.CompletedTask;
    //    }
    //};

    //    options.Authority = $"https://cognito-idp.{builder.Configuration["CognitoSettings:Region"]}.amazonaws.com/{builder.Configuration["CognitoSettings:UserPoolId"]}";
    //    options.Audience = builder.Configuration["CognitoSettings:ClientId"];
    //    options.TokenValidationParameters = new TokenValidationParameters
    //    {
    //        ValidateIssuer = true,
    //        ValidIssuer = $"https://cognito-idp.{builder.Configuration["CognitoSettings:Region"]}.amazonaws.com/{builder.Configuration["CognitoSettings:UserPoolId"]}",
    //        ValidateAudience = false,
    //        //ValidAudience = builder.Configuration["CognitoSettings:ClientId"],
    //        ValidateLifetime = true,
    //        ClockSkew = TimeSpan.Zero, // Evitar que pequenas diferenças de tempo causem falha na validação
    //        ValidateIssuerSigningKey = true
    //        //ValidTypes = ["at+jwt"]
    //    };
    //});


    builder.Services.AddAuthorization(options =>
    {
        options.FallbackPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
    });



    builder.Services.AddDbContext<MainDBContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")!)
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine, LogLevel.Information);
    });

    builder.Services.AddHttpClient();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddHealthChecks();
    builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();
    //builder.Services.RegisterHttpClient();
    //builder.Services.RegisterMappings();
    builder.Services.RegisterServices();
    builder.Services.RegisterRepositories();

    builder.Services.AddCors(options =>
                    options.AddPolicy("CorsPolicy",
                        builder => builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                        //.AllowCredentials()
                        )
                );


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("CorsPolicy");

    app.UseRouting();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

    /*
    private IDisposable _sentrySdk;
    _sentrySdk = SentrySdk.Init(o =>
    {
        // We store the DSN inside Web.config
        o.Dsn = ConfigurationManager.AppSettings["SentryDsn"];
        // Add the EntityFramework integration
        o.AddEntityFramework();
    });
    */
#pragma warning disable ASP0014 // Suggest using top level route registrations
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapHealthChecks("/health").AllowAnonymous();
        //endpoints.Map("/sentry", _ =>
        //{
        //    var sentryId = SentrySdk.CaptureMessage("Hello Sentry");
        //    return Task.Run(() => sentryId);
        //});
        //endpoints.Map("/exception", _ => throw new Exception());
    });
#pragma warning restore ASP0014 // Suggest using top level route registrations

    app.Run();
}