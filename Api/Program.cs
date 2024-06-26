using Api.Interfaces;
using Api.Logic;
using Api.Repositories;
using Api.Repository;
using Core;
using MongoDB.Driver;
using QuestPDF.Infrastructure;


namespace ServerAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient("mongodb+srv://system:system@cirkussummarum.to00ch9.mongodb.net/"));
        builder.Services.AddSingleton<ILoginRepository, LoginRepository>();
        builder.Services.AddSingleton<IAdminRepository, AdminRespository>();
        builder.Services.AddSingleton<IApplicationRepository, ApplicationRepository>();
        builder.Services.AddSingleton<ISignatureRepository, SignatureRepository>();
        builder.Services.AddSingleton<ICityRepository, CityRepository>();
        QuestPDF.Settings.License = LicenseType.Community;
        
        builder.Services.AddScoped<PdfForSignature>();
        builder.Services.AddScoped<Email>();

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

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseCors("policy");

        app.MapControllers();

        app.Run();
    }
   
}