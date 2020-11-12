using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZappitBugTracker.Data;
using ZappitBugTracker.Models;
using ZappitBugTracker.Models.ViewModels;
using ZappitBugTracker.services;


//TODO
//Add users to projects
//add a controller, add a viewmodel, add a view

namespace ZappitBugTracker.Controllers
{
    public class ProjectUsers : Controller
    {

        #region Private Members
        private readonly ApplicationDbContext _context;
        private readonly IBTProjectService _projectService;
        private readonly UserManager<BTUser> _userManager;
        #endregion
        #region constructors
        public ProjectUsers(ApplicationDbContext context, IBTProjectService projectService, UserManager<BTUser> userManager)
        {
            _context = context;
            _projectService = projectService;
            _userManager = userManager;

        }
        #endregion
        [Authorize(Roles = "Admin,ProjectManager")]
        public IActionResult Index()
        {
            return View();
        }

        #region GET/POST AddProjectUsers
        [Authorize(Roles = "Admin,ProjectManager")]
        public async Task<IActionResult> AddProjectUsers(int id)
        {
            var model = new ManageProjectUsersViewModels();
            var project = _context.Projects.Find(id);

            model.Project = project;
            List<BTUser> users = await _context.Users.ToListAsync();
            List<BTUser> members = (List<BTUser>)await _projectService.UsersOnProject(id);
            model.Users = new MultiSelectList(users, "Id", "FullName", members);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,ProjectManager")]
        public async Task<IActionResult> AddProjectUsers(ManageProjectUsersViewModels btuser)
        {
            
            return View();
        }
        #endregion
    }
}
