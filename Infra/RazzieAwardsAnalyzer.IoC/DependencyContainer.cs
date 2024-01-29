using Microsoft.Extensions.DependencyInjection;
using RazzieAwardsAnalyzer.Application.Interfaces;
using RazzieAwardsAnalyzer.Application.Services;
using RazzieAwardsAnalyzer.Domain.Interfaces.Repositories;
using RazzieAwardsAnalyzer.Data.FileRepositories;
using RazzieAwardsAnalyzer.Data.DataRepositories;

namespace RazzieAwardsAnalyzer.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IFileRepository, FileRepository>();
            services.AddScoped<IImportFileService, ImportFileService>();
            services.AddScoped<IRazzieAwardRepository, RazzieAwardRepository>();
            services.AddScoped<IAwardIntervalService, AwardIntervalService>();
        }
    }
}