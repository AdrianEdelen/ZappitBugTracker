using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZappitBugTracker.Data;
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
    }
}
