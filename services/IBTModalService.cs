using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
