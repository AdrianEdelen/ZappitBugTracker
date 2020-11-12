using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ZappitBugTracker.Data;

namespace ZappitBugTracker.services
{
    public class BTDisplayService : IBTDisplayService
    {
        private readonly ApplicationDbContext _context;

        public BTDisplayService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CanInteractProject(string userId, int projectId, string roleName)
        {
            switch (roleName)
            {
                case "Admin":
                    return true;
                case "ProjectManager":
                    if (await _context.ProjectUsers.Where(pu => pu.UserId == userId && pu.ProjectId == projectId).AnyAsync())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }
        public async Task<bool> CanInteractTicket(string userId, int projectId, string roleName)
        {
            switch (roleName)
            {
                case "Admin":
                    return true;
                case "ProjectManager":
                    if (await _context.ProjectUsers.Where(pu => pu.UserId == userId && pu.ProjectId == projectId).AnyAsync())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }
        
    }
}
