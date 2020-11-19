using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZappitBugTracker.Data;
using ZappitBugTracker.Models;

namespace ZappitBugTracker.services
{
    public class BTProjectService : IBTProjectService
    {
        //private member field
        private readonly ApplicationDbContext _context;

        //constructor
        public BTProjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsUserOnProject(string userId, int projectId)
        {
            Project project = await _context.Projects
                .Include(u => u.ProjectUsers)
                .ThenInclude(u => u.User)
                .FirstOrDefaultAsync(u => u.Id == projectId);
            bool result = project.ProjectUsers
                .Any(u => u.UserId == userId);
            return result;
        }

        public async Task<List<Project>> ListUserProjectsAsync(string userId)
        {
            BTUser user = await _context.Users
                .Include(p => p.ProjectUsers)
                .ThenInclude(p => p.Project)
                .ThenInclude(p => p.Tickets)
                .FirstOrDefaultAsync(p => p.Id == userId);
            List<Project> projects = user.ProjectUsers.Select(p => p.Project).ToList();
            return projects;
        }
        public async Task AddUserToProject(string userId, int projectId)
        {
            if (!await IsUserOnProject(userId, projectId))
            {
                try
                {
                    await _context.ProjectUsers.AddAsync(new ProjectUser { ProjectId = projectId, UserId = userId });
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"*** ERROR *** -Error Adding user to porject. --> {ex.Message}");
                }
            }

        }

        public async Task RemoveUserFromProject(string userId, int projectId)
        {
            if (await IsUserOnProject(userId, projectId))
            {
                try
                {
                    ProjectUser projectUser = new ProjectUser()
                    {
                        ProjectId = projectId,
                        UserId = userId
                    };
                    _context.projectUsers.Remove(projectUser);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"*** ERROR *** - Error Removing user from project. --> {ex.Message}");
                }
            }
        }

        public async Task<ICollection<BTUser>> UsersNotOnProject(int projectId)
        {
            return await _context.Users.Where(u => IsUserOnProject(u.Id, projectId).Result == false).ToListAsync();
        }

        public async Task<ICollection<BTUser>> UsersOnProject(int projectId)
        {
            Project project = await _context.Projects
                .Include(u => u.ProjectUsers)
                .ThenInclude(u => u.User)
                .FirstOrDefaultAsync(u => u.Id == projectId);

            List<BTUser> projectusers = project.ProjectUsers.Select(p => p.User).ToList();
            return projectusers;
        }


    }
}
