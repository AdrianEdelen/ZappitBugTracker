using Microsoft.AspNetCore.Mvc.Rendering;

namespace ZappitBugTracker.Models.ViewModels
{
    public class ManageProjectUsersViewModels
    {
        public Project Project { get; set; }
        public MultiSelectList Users { get; set; }
        public string[] SelectedUsers { get; set; }

    }
}
