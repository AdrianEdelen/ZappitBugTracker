using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using ZappitBugTracker.Data;
using ZappitBugTracker.Models;
using ZappitBugTracker.Models.ChartModel;

namespace ZappitBugTracker.Controllers
{
    public class ChartsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public JsonResult GetDemoChartData()
        {
            List<DemoData> result = new List<DemoData>();
            var ticketPriorites = _context.TicketPriority.ToList();
            foreach (var priority in ticketPriorites)
            {
                result.Add(new DemoData
                {
                    Priority = priority.Name,
                    Count = _context.Tickets.Where(t => t.TicketPriorityId == priority.Id).Count()
                });
            }
            return Json(result);
        }


        public List<int> PieChartTypes()
        {
            List<int> tickets = new List<int>();
            var ticketTypes = _context.TicketTypes.ToList();
            foreach (var type in ticketTypes)
            {
                tickets.Add(_context.Tickets.Where(t => t.TicketTypeId == type.Id && t.TicketStatusId != 3).Count());
            }
            return tickets;
        }

        public int[] WeeklyTickets()
        {
            List<LineChartData> result = new List<LineChartData>();
            List<Ticket> allTickets = _context.Tickets.ToList();
            //List<int> tickets = new List<int>();
            int[] tickets = new int[7];

            List<DateTimeOffset> dateList = new List<DateTimeOffset>();


            dateList.Add(DateTimeOffset.Now.AddDays(-1));
            dateList.Add(DateTimeOffset.Now.AddDays(-2));
            dateList.Add(DateTimeOffset.Now.AddDays(-3));
            dateList.Add(DateTimeOffset.Now.AddDays(-4));
            dateList.Add(DateTimeOffset.Now.AddDays(-5));
            dateList.Add(DateTimeOffset.Now.AddDays(-6));
            dateList.Add(DateTimeOffset.Now.AddDays(-7));
            dateList.Add(DateTimeOffset.Now.AddDays(-8));


            // i have a list of ints and a list of dates
            foreach (var tic in allTickets)
            {
                if (tic.Created.Date == dateList[0].Date)
                {
                    tickets[6] += 1;
                }
                else if (tic.Created.Date == dateList[1].Date)
                {
                    tickets[5] += 1;
                }
                else if (tic.Created.Date == dateList[2].Date)
                {
                    tickets[4] += 1;
                }
                else if (tic.Created.Date == dateList[3].Date)
                {
                    tickets[3] += 1;
                }
                else if (tic.Created.Date == dateList[4].Date)
                {
                    tickets[2] += 1;
                }
                else if (tic.Created.Date == dateList[5].Date)
                {
                    tickets[1] += 1;
                }
                else if (tic.Created.Date == dateList[6].Date)
                {
                    tickets[0] += 1;
                }
            }

            return tickets;
        }
    }
}
