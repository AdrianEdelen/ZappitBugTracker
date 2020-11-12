using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ZappitBugTracker.Models.ViewModels
{
    public class TicketHistoriesIdToNameViewModel
    {
       
        public string OldValLabel { get; set; }
        public string NewValLabel { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string Property { get; set; }

        public string UserId { get; set; }
    }
}
