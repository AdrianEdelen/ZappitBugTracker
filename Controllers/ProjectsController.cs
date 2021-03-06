﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZappitBugTracker.Data;
using ZappitBugTracker.Models;
using ZappitBugTracker.Models.ViewModels;
using ZappitBugTracker.services;

namespace ZappitBugTracker.Controllers
{
    public class ProjectsController : Controller
    {
        #region Private Fields
        private readonly ApplicationDbContext _context;
        private readonly IBTProjectService _projectService;
        private readonly IBTAccessService _accessService;
        private readonly UserManager<BTUser> _userManager;
        private readonly IBTRolesService _rolesService;
        private readonly IBTImageService _imageService;


        #endregion
        #region constructors
        public ProjectsController(ApplicationDbContext context, IBTProjectService projectService, IBTAccessService accessService, UserManager<BTUser> userManager, IBTRolesService rolesService, IBTImageService imageService)
        {
            _context = context;
            _projectService = projectService;
            _accessService = accessService;
            _userManager = userManager;
            _rolesService = rolesService;
            _imageService = imageService;
        }
        #endregion
        #region GET/POST AddProjectUsers
        //GET AddProjectUsers
        [HttpGet]
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
        //POST AddProjectUsers
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ProjectManager")]
        public async Task<IActionResult> AddProjectUsers(ManageProjectUsersViewModels model)
        {
            if (model.SelectedUsers != null)
            {
                var currentMembers = await _context.Projects
               .Include(p => p.ProjectUsers)
               .FirstOrDefaultAsync(p => p.Id == model.Project.Id);
                List<string> memberIds = currentMembers.ProjectUsers.Select(u => u.UserId).ToList();
                foreach (string id in memberIds)
                {
                    await _projectService.RemoveUserFromProject(id, model.Project.Id);
                }
                foreach (string id in model.SelectedUsers)
                {
                    await _projectService.AddUserToProject(id, model.Project.Id);
                }
                return RedirectToAction("Index", "Projects");
            }
            else
            {
                // send an error message back
            }
            return RedirectToAction();
        }
        #endregion
        #region GET Index
        // GET: Identity/Projects
        [Authorize]
        public async Task<IActionResult> Index()
        {

            return View(await _context.Projects.ToListAsync());
        }
        #endregion
        #region GET YourProjects
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> YourProjects()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(await _projectService.ListUserProjectsAsync(user.Id));
        }
        #endregion
        #region GET/POST Create
        // GET: Identity/Projects/Create
        [Authorize(Roles = "Admin,ProjectManager")]
        public IActionResult Create()
        {
            return View();
        }
        // POST: Identity/Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ProjectManager")]
        public async Task<IActionResult> Create([Bind("Id,Name,ImagePath,ImageData")] Project project, IFormFile imageData)
        {
            if (ModelState.IsValid)
            {
                if (imageData != null)
                {
                    project.ImagePath = imageData.FileName;
                    project.ImageData = _imageService.EncodeImage(imageData);
                }
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }
        #endregion
        #region GET/POST edit
        // GET: Identity/Projects/Edit/5
        [Authorize(Roles = "Admin,ProjectManager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }
        // POST: Identity/Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ProjectManager")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImagePath,ImageData")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }
        #endregion
        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
