using Microsoft.AspNetCore.Mvc.Rendering;

namespace ZappitBugTracker.Models.ViewModels
{
    public class ManageUserRolesViewModel
    {
        public BTUser User { get; set; }
        public MultiSelectList Roles { get; set; }
        public string[] SelectedRoles { get; set; }
    }
}
