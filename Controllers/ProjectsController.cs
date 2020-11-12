using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        #endregion
        #region constructors
        public ProjectsController(ApplicationDbContext context, IBTProjectService projectService, IBTAccessService accessService, UserManager<BTUser> userManager, IBTRolesService rolesService)
        {
            _context = context;
            _projectService = projectService;
            _accessService = accessService;
            _userManager = userManager;
            _rolesService = rolesService;
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
            var user = await _userManager.GetUserAsync(User);

            if (await _rolesService.IsUserInRole(user, "Admin"))
            {
                return View(await _context.Projects.ToListAsync());
            };
            if (await _rolesService.IsUserInRole(user, "ProjectManager"))
            {
                return View(await _context.Projects.ToListAsync());
            };

            return View(await _projectService.ListUserProjectsAsync(user.Id));
        }
        #endregion
        #region GET details
        // GET: Identity/Projects/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
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
        public async Task<IActionResult> Create([Bind("Id,Name,ImagePath,ImageData")] Project project)
        {
            if (ModelState.IsValid)
            {
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
        #region GET/POST delete
        // GET: Identity/Projects/Delete/5
        [Authorize(Roles = "Admin,ProjectManager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }
        // POST: Identity/Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ProjectManager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
