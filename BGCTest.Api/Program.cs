using BGCTest.Api.DependencyInjections;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseNLog()
    .RegisterComponentWithAutofac(builder.Configuration);
builder.Services.RegisterServices();

var app = builder.Build();
app.ConfigurationApplication();
app.Run();
