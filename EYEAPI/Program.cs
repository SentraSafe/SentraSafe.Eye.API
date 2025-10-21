using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using EYEAPI.BackgroundServices;
using EYEAPI.Repositories;
using EYEAPI.Contexts;
using EYEAPI.Models;
using MQTTnet.Formatter;
using MQTTnet.Protocol;
using MQTTnet;
using MongoDB.Driver;
using Microsoft.Extensions.Azure;
using Microsoft.IdentityModel.Tokens;
using EYEAPI.Services.MqttService;
using EYEAPI.Services.MachineService;
using EYEAPI.Services.LocationService;
using EYEAPI.Services.AlarmService;
using EYEAPI.Services.SublocationService;
using EYEAPI.Services.LogService;
using EYEAPI.Controllers;
using EYEAPI.Hubs;
using EYEAPI.Models.Dtos.LocationDtos;
using EYEAPI.Models.Dtos.MachineDtos;
using EYEAPI.Models.Entities;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://login.microsoftonline.com/2dfd1f89-3b0a-454b-9ec5-778b2f3140d5/v2.0";
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuers = ["https://sts.windows.net/2dfd1f89-3b0a-454b-9ec5-778b2f3140d5/",
                "https://login.microsoftonline.com/2dfd1f89-3b0a-454b-9ec5-778b2f3140d5/v2.0"]
        };
        options.Audience = "api://32ca31d5-86a3-4177-a755-80c827cc93f0";
    });
builder.Services.AddAuthorizationBuilder();

builder.Services.AddOptions<AppSettings>().Bind(builder.Configuration);
builder.Services.AddSingleton<MongoClient>(s =>
{
    IOptions<AppSettings> options = s.GetRequiredService<IOptions<AppSettings>>();
    return new MongoClient(options.Value.ConnectionStrings.MongoDbConnectionString);
});

builder.Services.AddAutoMapper((serviceProvider, expression) =>
{
    IOptions<AppSettings> options = serviceProvider.GetRequiredService<IOptions<AppSettings>>();
    expression.AddMaps(typeof(Program).Assembly);
    expression.LicenseKey = options.Value.AutoMapper.LicenseKey;
    expression.CreateMap<Machine, MachineDto>()
        .ForMember(x => x.Location, configurationExpression => configurationExpression.MapFrom(machine => machine.Sublocation.Location.Name))
        .ForMember(x => x.Sublocation, configurationExpression => configurationExpression.MapFrom(machine => machine.Sublocation.Name));
}, typeof(Program).Assembly);
builder.Services.AddSignalR();

builder.Services.AddSingleton<MqttClientFactory>();
builder.Services.AddSingleton<MqttClientOptionsBuilder>(serviceProvider =>
{
    IOptions<AppSettings> options = serviceProvider.GetRequiredService<IOptions<AppSettings>>();
    return new MqttClientOptionsBuilder()
        .WithTcpServer(options.Value.MqttBroker.Host)
        .WithTlsOptions(x => x.UseTls())
        .WithProtocolVersion(MqttProtocolVersion.V311)
        .WithWillTopic("health")
        .WithWillPayload("dead")
        .WithWillQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
        .WithWillRetain();
});
builder.Services.AddDbContext<EyeContext>(x => x.UseSqlServer("Name=Eye"));

//Custom Services
builder.Services.AddSingleton<IMqttService, MqttService>();
builder.Services.AddScoped<IMachineService, MachineService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IAlarmService, AlarmService>();
builder.Services.AddScoped<ISublocationService, SublocationService>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IEyeRepository, EyeRepository>();
builder.Services.AddHostedService<SensorWorkerService>();
builder.Services.AddHostedService<WebSocketWorkerService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

// app.UseHttpsRedirection();
app.MapHub<MachineHub>($"/{nameof(MachineHub)}").RequireCors("AllowClient");
app.MapHub<AlarmHub>($"/{nameof(AlarmHub)}").RequireCors("AllowClient");
app.MapControllers();

app.UseCors(policyBuilder =>
{
    policyBuilder.AllowAnyOrigin();
    policyBuilder.AllowAnyMethod();
    policyBuilder.AllowAnyHeader();
});

app.Run();
