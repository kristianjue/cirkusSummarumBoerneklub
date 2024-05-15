using Api.Interfaces;
using Api.Repositories;
using MongoDB.Driver;
using Core;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient("mongodb+srv://eaa23krju:PwJX0IIg5tK6TFBgbrJ6@cirkussummarum.to00ch9.mongodb.net/"));
builder.Services.AddSingleton<ILoginRepository, LoginRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("policy",
                      policy =>
                      {
                          policy.AllowAnyOrigin();
                          policy.AllowAnyMethod();
                          policy.AllowAnyHeader();
                      });
});

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("policy");

app.MapControllers();

app.Run();

app.MapGet("/", () => "Hello World!");

app.Run();