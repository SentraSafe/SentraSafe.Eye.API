using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using EYEAPI.BackgroundServices;
using EYEAPI.Repositories;
using EYEAPI.Contexts;
using EYEAPI.Services;
using EYEAPI.Models;
using MQTTnet.Formatter;
using MQTTnet.Protocol;
using MQTTnet;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("EntraID"));

*/
builder.Services.AddOptions<AppSettings>().Bind(builder.Configuration);
builder.Services.AddSingleton<MqttClientFactory>();
builder.Services.AddSingleton<IMqttClient>(serviceProvider =>
{
    MqttClientFactory mqttClientFactory = serviceProvider.GetRequiredService<MqttClientFactory>();
    IMqttClient mqttClient = mqttClientFactory.CreateMqttClient();
    return mqttClient;
});
builder.Services.AddSingleton<MqttClientOptionsBuilder>(serviceProvider =>
{
    IOptions<AppSettings> options = serviceProvider.GetRequiredService<IOptions<AppSettings>>();
    return new MqttClientOptionsBuilder()
        .WithTcpServer(options.Value.MqttBroker.Host)
        .WithCredentials(options.Value.MqttBroker.Username, options.Value.MqttBroker.Password)
        .WithTlsOptions(x => x.UseTls())
        .WithProtocolVersion(MqttProtocolVersion.V311)
        .WithWillTopic("health")
        .WithWillPayload("dead")
        .WithWillQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
        .WithWillRetain();
});
builder.Services.AddDbContext<MeasurementContext>(x => x.UseSqlite("Name=Measurement"));
builder.Services.AddSingleton<IMqttService, MqttService>();
builder.Services.AddScoped<IMeasurementRepository, MeasurementRepository>();
builder.Services.AddHostedService<SensorWorkerService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
