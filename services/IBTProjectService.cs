using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZappitBugTracker.Models;
using Project = ZappitBugTracker.Models.Project;

namespace ZappitBugTracker.services
{
    public interface IBTProjectService
    {
        public Task<bool> IsUserOnProject(string userId, int projectId);
        public Task<ICollection<Project>> ListUserProjectsAsync(string userId);
        public Task AddUserToProject(string userId, int projectId);
        public Task RemoveUserFromProject(string userId, int projectId);
        public Task<ICollection<BTUser>> UsersOnProject(int projectId);
        public Task<ICollection<BTUser>> UsersNotOnProject(int projectId);
    }
}
