using GroupCCP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GroupCCP
{
    public class NotificationTask : BackgroundService
    {
        private readonly ILogger<NotificationTask> Logger;
        private readonly TasksFunctions windowsService;
        private readonly GroupCCP.Data.ApplicationDbContext _context;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public NotificationTask(ILogger<NotificationTask> logger, IServiceProvider serviceProvider)
        {
            Logger = logger;
            IServiceScope serviceScope = serviceProvider.GetService<IServiceScopeFactory>().CreateScope();

            _context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
        }
 
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000);
                

                try
                {
                    TasksFunctions windowsService = new(_context);
                    windowsService.SaySomething();
                    LogInformation("I say we got sth good going");
                }
                catch (Exception ex)
                {
                    LogErrors(ex.Message);
                }

            }
        }
        public void LogErrors(string error)
        {
            Logger.LogError(error);
        }

        public void LogInformation(string information)
        {
            Logger.LogInformation(information);
        }
    }
}
