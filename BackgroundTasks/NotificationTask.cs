using GroupCCP.Data;
using GroupCCP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GroupCCP
{
    public class NotificationTask : BackgroundService
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public NotificationTask(IServiceProvider serviceProvider)
        {
            IServiceScope serviceScope = serviceProvider.GetService<IServiceScopeFactory>().CreateScope();
            _context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
        }
 
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            TasksFunctions taskFunctions = new(_context);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(30000, stoppingToken);                
                try
                {                    
                    // Email Notifications
                    var Notifications = await _context.Notification
                        .Include(c => c.Account).ThenInclude(c => c.User)
                        .Where(c => c.IsSent == false)
                        .ToListAsync(cancellationToken: stoppingToken);
                    foreach (var Notification in Notifications)
                    {
                        taskFunctions.EmailStaff(Notification.Account.User.Email, "GroupCCP Notification", Notification.Message);
                        Notification.IsSent = true;
                        _context.SaveChanges();
                    }

                    // Email Reminders
                    var Reminders = await _context.OverdueReminder
                        .Include(c => c.StaffAccount).ThenInclude(c => c.User)
                        .Where(c => c.IsSent == false)
                        .ToListAsync(cancellationToken: stoppingToken);

                    foreach(var Reminder in Reminders)
                    {
                        taskFunctions.EmailStaff(Reminder.StaffAccount.User.Email, "GroupCCP Reminder Log" + Reminder.LogId + " -" + Reminder.MessageType, Reminder.Reminder);
                        Reminder.IsSent = true;
                        _context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    taskFunctions.WriteLog("Notification task failed with error \"" + ex.Message + "\"");
                }

            }
        }
    }
}
