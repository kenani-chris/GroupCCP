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
    public class AssigningTask : BackgroundService
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public AssigningTask(IServiceProvider serviceProvider)
        {
            IServiceScope serviceScope = serviceProvider.GetService<IServiceScopeFactory>().CreateScope();
            _context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
        }
 
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            while (!stoppingToken.IsCancellationRequested)
            {
                TasksFunctions taskFunctions = new(_context);
                await Task.Delay(30000, stoppingToken);
                try
                {
                    taskFunctions.AssignLogs();
                }
                catch (Exception ex)
                {
                    taskFunctions.WriteLog("Notification task failed with error \"" + ex.Message + "\"");
                }
            }
        }
    }
}
