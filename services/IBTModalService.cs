using Microsoft.AspNetCore.Mvc.Rendering;

namespace ZappitBugTracker.services
{
    public interface IBTModalService
    {
        public SelectList PriorityDropDown();
        public SelectList ProjectDropDown();
        public SelectList TypeDropDown();
        public SelectList DevUserDropDown();
        public SelectList OwnerUserDropDown();
        public SelectList StatusDropDown();

    }
}
