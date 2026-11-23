using DummyOrderApp.Api.Services;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<OrderService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpMetrics();
app.MapMetrics("/metrics");

app.MapControllers();
app.Run();
