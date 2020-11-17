using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ZappitBugTracker.Data;
using ZappitBugTracker.Models;
using ZappitBugTracker.Models.Extensions;

namespace ZappitBugTracker.services
{
    public class BTHistoryService : IBTHistoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<BTUser> _userManager;
        private readonly IEmailSender _emailSender;
        
        public BTHistoryService(ApplicationDbContext context, UserManager<BTUser> userManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task AddHistory(Ticket oldTicket, Ticket newTicket, string userId)
        {
            var diffs = GenerateTicketHistories(oldTicket, newTicket);
            foreach (var val in diffs)
            {
                val.TicketId = oldTicket.Id;
                val.UserId = userId;
                val.Created = DateTime.Now;
                await _context.TicketHistories.AddAsync(val);
                await _context.SaveChangesAsync();
            }
            Notification notification = new Notification
            {
                TicketId = newTicket.Id,
                Description = "You Have A New Ticket",
                Created = DateTimeOffset.Now,
                SenderId = userId,
                RecipientId = newTicket.DeveloperUserId
            };
            await _context.Notifications.AddAsync(notification);

            //send email
            string devEmail = newTicket.DeveloperUser.Email;
            string subject = "New Ticket Assignment";
            string message = $"You have a new ticket for project: {newTicket.Project.Name}";
            await _emailSender.SendEmailAsync(devEmail, subject, message);
        }

        private void SendEmailAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TicketHistory> GenerateTicketHistories(Ticket oldTicket, Ticket newTicket)
        {
            Dictionary<string, dynamic> srcDict = oldTicket.GetPropertiesForCompare();
            Dictionary<string, dynamic> targetDict = newTicket.GetPropertiesForCompare();
            IEnumerable<TicketHistory> ticketHistories = GenerateTicketHistoriesFromDictionaries(srcDict, targetDict);
            return ticketHistories;
        }

        private IEnumerable<TicketHistory> GenerateTicketHistoriesFromDictionaries(Dictionary<string, dynamic> src, Dictionary<string, dynamic> target)
        {
            Dictionary<string, dynamic> diffs = target.GetDifferences(src);
            foreach (var kvp in diffs)
            {
                yield return new TicketHistory()
                {
                    Property = kvp.Key.ToString(),
                    OldValue = src[kvp.Key].ToString(),
                    NewValue = kvp.Value.ToString(),
                };
            }
        }
    }
}

//var test = _configuration["TicketHistoryCheck"];


//List<PropertyInfo> changes = new List<PropertyInfo>();
//foreach (PropertyInfo prop in oldTicket.GetType().GetProperties())
//{

//    //if (test.Contains(prop))
//    //{

//    //}
//    object prop1 = prop.GetValue(oldTicket);
//    object prop2 = prop.GetValue(newTicket);
//    if (!prop1.Equals(prop2))
//    {
//        changes.Add(prop);
//    }
//}
//return changes;

//if (oldTicket.Title != newTicket.Title)
//{
//    TicketHistory history = new TicketHistory
//    {
//        TicketId = newTicket.Id,
//        Property = "Title",
//        OldValue = oldTicket.Title,
//        NewValue = newTicket.Title,
//        Created = DateTimeOffset.Now,
//        UserId = userId
//    };
//    await _context.TicketHistories.AddAsync(history);
//}
//if (oldTicket.Description != newTicket.Description)
//{
//    TicketHistory history = new TicketHistory
//    {
//        TicketId = newTicket.Id,
//        Property = "Description",
//        OldValue = oldTicket.Description,
//        NewValue = newTicket.Description,
//        Created = DateTimeOffset.Now,
//        UserId = userId
//    };
//    await _context.TicketHistories.AddAsync(history);
//}
//if (oldTicket.TicketPriorityId != newTicket.TicketPriorityId)
//{
//    TicketHistory history = new TicketHistory
//    {
//        TicketId = newTicket.Id,
//        Property = "Ticket Priority",
//        OldValue = oldTicket.TicketPriority.Name,
//        NewValue = newTicket.TicketPriority.Name,
//        Created = DateTimeOffset.Now,
//        UserId = userId
//    };
//    await _context.TicketHistories.AddAsync(history);
//}
//if (oldTicket.DeveloperUserId != newTicket.DeveloperUserId)
//{
//    TicketHistory history = new TicketHistory
//    {
//        TicketId = newTicket.Id,
//        Property = "Developer User",
//        OldValue = oldTicket.DeveloperUser.FullName,
//        NewValue = newTicket.DeveloperUser.FullName,
//        Created = DateTimeOffset.Now,
//        UserId = userId
//    };
//    await _context.TicketHistories.AddAsync(history);
//}
//if (oldTicket.TicketStatusId != newTicket.TicketStatusId)
//{
//    TicketHistory history = new TicketHistory
//    {
//        TicketId = newTicket.Id,
//        Property = "Ticket Status",
//        OldValue = oldTicket.TicketStatus.Name,
//        NewValue = newTicket.TicketStatus.Name,
//        Created = DateTimeOffset.Now,
//        UserId = userId
//    };
//    await _context.TicketHistories.AddAsync(history);
//}
//if (oldTicket.TicketTypeId != newTicket.TicketTypeId)
//{
//    TicketHistory history = new TicketHistory
//    {
//        TicketId = newTicket.Id,
//        Property = "Ticket Type",
//        OldValue = oldTicket.TicketType.Name,
//        NewValue = newTicket.TicketType.Name,
//        Created = DateTimeOffset.Now,
//        UserId = userId
//    };
//    await _context.TicketHistories.AddAsync(history);
//}