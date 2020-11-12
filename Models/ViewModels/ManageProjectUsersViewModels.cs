using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZappitBugTracker.Models.ViewModels
{
    public class ManageProjectUsersViewModels
    {
        public Project Project { get; set; }
        public MultiSelectList Users { get; set; }
        public string[] SelectedUsers { get; set; }

    }
}
