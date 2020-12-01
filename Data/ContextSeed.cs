using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ZappitBugTracker.Models;

namespace ZappitBugTracker.Data
{
    public enum Roles
    {
        Admin,
        ProjectManager,
        Developer,
        Submitter,
        NewUser,
        Demo
    }
    public static class ContextSeed
    {
        //seed additional users
        //vary users on each project
        //4 projects
        //all users on each project
        // 10 tickets per project
        // 5 comments per ticket
        // histories?
        //randomize titles and descriptions
        //randomize as much as possible

        //ultimate goal, 100+ tickets
        //20+ projects
        //10+ users
        //etc

        //step 7 
        //randomly assign users
        //need to have an equal amount of each role 

        //step 8
        // seed tickets
        //grab issues from github
        //scrape them from github??
        //randomly assign project, priority, type, and status.

        /*
         * 
         * 
         * 
         */
        public static List<BTUser> adminList = new List<BTUser>();
        public static List<BTUser> pMList = new List<BTUser>();
        public static List<BTUser> devList = new List<BTUser>();
        public static List<BTUser> submitterList = new List<BTUser>();
        public static List<Project> projectList = new List<Project>();
        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.ProjectManager.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Developer.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Submitter.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.NewUser.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Demo.ToString()));
        }
        private static async Task SeedDefaultUsersAsync(UserManager<BTUser> userManager)
        {
            var errorMsg = "******* ERROR SEEDING DEFAULT USER(S) *******";
            var demoPassword = "Xyz%987$";
            Random random = new Random();
            var firstNamesList = new List<string>(){
                "Liam",
                "Noah",
                "Oliver",
                "William",
                "Elijah",
                "James",
                "Benjamin",
                "Lucas",
                "Mason",
                "Ethan",
                "Alexander",
                "Henry",
                "Jacob",
                "Michael",
                "Daniel",
                "Logan",
                "Jackson",
                "Sebastian",
                "Jack",
                "Aiden",
                "Owen",
                "Samuel",
                "Matthew",
                "Joseph",
                "Levi",
                "Mateo",
                "David",
                "John",
                "Wyatt",
                "Carter",
                "Julian",
                "Luke",
                "Grayson",
                "Isaac",
                "Jayden",
                "Theodore",
                "Sherlock",
                "Anthony",
                "Dylan",
                "Leo",
                "Lincoln",
                "Jaxon",
                "Asher",
                "Christopher",
                "Josiah",
                "Andrew",
                "Thomas",
                "Gabriel",
                "Ezra",
                "Parker",
                "Watson"
            };
            var lastNamesList = new List<string>()
            {
                "Smith",
                "Johnson",
                "Williams",
                "Brown",
                "Jones",
                "Garcia",
                "Miller",
                "Davis",
                "Rodriguez",
                "Martinez",
                "Hernandez",
                "Lopez",
                "Gonzalez",
                "Wilson",
                "Anderson",
                "Thomas",
                "Taylor",
                "Moore",
                "Jackson",
                "Martin",
                "Lee",
                "Perez",
                "Thompson",
                "White",
                "Harris",
                "Sanchez",
                "Clark",
                "Ramirez",
                "Lewis",
                "Robinson",
                "Walker",
                "Young",
                "Allen",
                "King",
                "Wright",
                "Scott",
                "Torres",
                "Nguyen",
                "Hill",
                "Flores",
                "Green",
                "Adams",
                "Nelson",
                "Baker",
                "Hall",
                "Rivera",
                "Campbell",
                "Mitchell",
                "Carter",
                "Roberts",
                "Allende"
            };
            var mail = "@mailinator.com";
            #region defaultAdmin
            var defaultAdmin = new BTUser
            {
                UserName = "adrianedelen@gmail.com",
                Email = "adrianedelen@gmail.com",
                FirstName = "Adrian",
                LastName = "Edelen",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultAdmin.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultAdmin, "Abc&123!");
                    await userManager.AddToRoleAsync(defaultAdmin, Roles.Admin.ToString());
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(errorMsg);
                Debug.WriteLine(ex);
                throw;
            }
            #endregion
            #region defaultProjectManager
            var defaultProjectManager = new BTUser
            {
                UserName = "araynor@gmail.com",
                Email = "araynor@gmail.com",
                FirstName = "Antonio",
                LastName = "Raynor",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultProjectManager.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultProjectManager, "Abc&123!");
                    await userManager.AddToRoleAsync(defaultProjectManager, Roles.ProjectManager.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(errorMsg);
                Debug.WriteLine(ex);
                throw;
            }
            #endregion
            #region defaultDeveloper
            var defaultDeveloper = new BTUser
            {
                UserName = "drewrussell@gmail.com",
                Email = "drewrussell@gmail.com",
                FirstName = "Andrew",
                LastName = "Russell",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultDeveloper.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultDeveloper, "Abc&123!");
                    await userManager.AddToRoleAsync(defaultDeveloper, Roles.Developer.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(errorMsg);
                Debug.WriteLine(ex);
                throw;
            }
            #endregion
            #region defaultSubmitter
            var defaultSubmitter = new BTUser
            {
                UserName = "annaedelen7@gmail.com",
                Email = "annaedelen7@gmail.com",
                FirstName = "Anna",
                LastName = "Edelen",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultSubmitter.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultSubmitter, "Abc&123!");
                    await userManager.AddToRoleAsync(defaultSubmitter, Roles.Submitter.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(errorMsg);
                Debug.WriteLine(ex);
                throw;
            }
            #endregion
            #region defaultNewUser
            var defaultNewUser = new BTUser
            {
                UserName = "jasontwichell@gmail.com",
                Email = "jasontwichell@gmail.com",
                FirstName = "Jason",
                LastName = "Twichell",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultNewUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultNewUser, "Abc&123!");
                    await userManager.AddToRoleAsync(defaultNewUser, Roles.NewUser.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(errorMsg);
                Debug.WriteLine(ex);
                throw;
            }
            #endregion
            #region demo defaultadmin
            defaultAdmin = new BTUser
            {
                UserName = "demoadmin@mailinator.com",
                Email = "demoadmin@mailinator.com",
                FirstName = "admin",
                LastName = "istrator",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultAdmin.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultAdmin, demoPassword);
                    await userManager.AddToRoleAsync(defaultAdmin, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultAdmin, Roles.Demo.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(errorMsg);
                Debug.WriteLine(ex);
                throw;
            }
            #endregion
            #region demo defaultProjectManager
            defaultProjectManager = new BTUser
            {
                UserName = "demopm@mailinator.com",
                Email = "demopm@mailinator.com",
                FirstName = "Project",
                LastName = "Manager",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultProjectManager.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultProjectManager, demoPassword);
                    await userManager.AddToRoleAsync(defaultProjectManager, Roles.ProjectManager.ToString());
                    await userManager.AddToRoleAsync(defaultProjectManager, Roles.Demo.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(errorMsg);
                Debug.WriteLine(ex);
                throw;
            }
            #endregion
            #region demo defaultDeveloper
            defaultDeveloper = new BTUser
            {
                UserName = "demodev@mailinator.com",
                Email = "demodev@mailinator.com",
                FirstName = "Dev",
                LastName = "Eloper",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultDeveloper.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultDeveloper, demoPassword);
                    await userManager.AddToRoleAsync(defaultDeveloper, Roles.Developer.ToString());
                    await userManager.AddToRoleAsync(defaultDeveloper, Roles.Demo.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(errorMsg);
                Debug.WriteLine(ex);
                throw;
            }
            #endregion
            #region demo defaultSubmitter
            defaultSubmitter = new BTUser
            {
                UserName = "demosubmitter@mailinator.com",
                Email = "demosubmitter@mailinator.com",
                FirstName = "Sub",
                LastName = "Mitter",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultSubmitter.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultSubmitter, demoPassword);
                    await userManager.AddToRoleAsync(defaultSubmitter, Roles.Submitter.ToString());
                    await userManager.AddToRoleAsync(defaultSubmitter, Roles.Demo.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(errorMsg);
                Debug.WriteLine(ex);
                throw;
            }
            #endregion
            #region demo defaultNewUser
            defaultNewUser = new BTUser
            {
                UserName = "demonewuser@mailinator.com",
                Email = "demonewuser@mailinator.com",
                FirstName = "new",
                LastName = "user",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultNewUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultNewUser, demoPassword);
                    await userManager.AddToRoleAsync(defaultNewUser, Roles.NewUser.ToString());
                    await userManager.AddToRoleAsync(defaultNewUser, Roles.Demo.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(errorMsg);
                Debug.WriteLine(ex);
                throw;
            }
            #endregion
            #region gen Admins
            //loop 10 times to create 10 uniqueish admins
            for (var i = 0; i < 10; i++)
            {
                defaultAdmin = new BTUser
                {
                    UserName = firstNamesList[i] + lastNamesList[i] + mail,
                    Email = firstNamesList[i] + lastNamesList[i] + mail,
                    FirstName = firstNamesList[i],
                    LastName = lastNamesList[i],
                    EmailConfirmed = true
                };
                try
                {
                    var user = await userManager.FindByEmailAsync(defaultAdmin.Email);
                    if (user == null)
                    {
                        await userManager.CreateAsync(defaultAdmin, demoPassword);
                        await userManager.AddToRoleAsync(defaultAdmin, Roles.Admin.ToString());
                    }
                    adminList.Add(defaultAdmin);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(errorMsg);
                    Debug.WriteLine(ex);
                    throw;
                }
            }
            #endregion
            #region gen PMs
            // loop over to create Project Managers
            for (var i = 0; i < 10; i++)
            {
                defaultAdmin = new BTUser
                {
                    UserName = firstNamesList[i + 10] + lastNamesList[i + 10] + mail,
                    Email = firstNamesList[i + 10] + lastNamesList[i + 10] + mail,
                    FirstName = firstNamesList[i + 10],
                    LastName = lastNamesList[i + 10],
                    EmailConfirmed = true
                };
                try
                {
                    var user = await userManager.FindByEmailAsync(defaultAdmin.Email);
                    if (user == null)
                    {
                        await userManager.CreateAsync(defaultAdmin, demoPassword);
                        await userManager.AddToRoleAsync(defaultAdmin, Roles.ProjectManager.ToString());
                    }
                    pMList.Add(defaultAdmin);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(errorMsg);
                    Debug.WriteLine(ex);
                    throw;
                }
            }
            #endregion
            #region gen Devs
            // loop over to create Developers
            for (var i = 0; i < 10; i++)
            {
                defaultAdmin = new BTUser
                {
                    UserName = firstNamesList[i + 20] + lastNamesList[i + 20] + mail,
                    Email = firstNamesList[i + 20] + lastNamesList[i + 20] + mail,
                    FirstName = firstNamesList[i + 20],
                    LastName = lastNamesList[i + 20],
                    EmailConfirmed = true
                };
                try
                {
                    var user = await userManager.FindByEmailAsync(defaultAdmin.Email);
                    if (user == null)
                    {
                        await userManager.CreateAsync(defaultAdmin, demoPassword);
                        await userManager.AddToRoleAsync(defaultAdmin, Roles.Developer.ToString());
                    }
                    devList.Add(defaultAdmin);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(errorMsg);
                    Debug.WriteLine(ex);
                    throw;
                }
            }
            #endregion
            #region gen Submitters
            //loop to create submitters
            for (var i = 0; i < 10; i++)
            {
                defaultAdmin = new BTUser
                {
                    UserName = firstNamesList[i + 30] + lastNamesList[i + 30] + mail,
                    Email = firstNamesList[i + 30] + lastNamesList[i + 30] + mail,
                    FirstName = firstNamesList[i + 30],
                    LastName = lastNamesList[i + 30],
                    EmailConfirmed = true
                };
                try
                {
                    var user = await userManager.FindByEmailAsync(defaultAdmin.Email);
                    if (user == null)
                    {
                        await userManager.CreateAsync(defaultAdmin, demoPassword);
                        await userManager.AddToRoleAsync(defaultAdmin, Roles.Submitter.ToString());
                    }
                    submitterList.Add(defaultAdmin);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(errorMsg);
                    Debug.WriteLine(ex);
                    throw;
                }
            }
            #endregion
        }
        private static async Task SeedDefaultTicketPriorityAsync(ApplicationDbContext context)
        {
            var priList = new List<TicketPriority>() {
                new TicketPriority
                {
                    Name = "New"
                },
                new TicketPriority
                {
                    Name = "Low"
                },
                new TicketPriority
                {
                    Name = "High"
                },
                new TicketPriority
                {
                    Name = "Urgent"
                }
            };
            foreach (var pri in priList)
            {
                try
                {
                    var ticketPriority = await context.TicketPriority.Where(t => t.Name == pri.Name).FirstOrDefaultAsync();
                    if (ticketPriority == null)
                    {
                        await context.TicketPriority.AddAsync(pri);
                        await context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"**** Error Adding Default Ticket Priority {pri.Name} ****");
                    Debug.WriteLine(ex.Message);
                }
            }
        }
        private static async Task SeedDefaultTicketTypeAsync(ApplicationDbContext context)
        {
            var typeList = new List<TicketType>() {
                new TicketType
                {
                    Name = "UI"
                },
                new TicketType
                {
                    Name = "Database"
                },
                new TicketType
                {
                    Name = "User Experience"
                },
                new TicketType
                {
                    Name = "Back End"
                },
                new TicketType
                {
                    Name = "Feature Request"
                }
            };
            foreach (var type in typeList)
            {
                try
                {
                    var ticketType = await context.TicketTypes.Where(t => t.Name == type.Name).FirstOrDefaultAsync();
                    if (ticketType == null)
                    {
                        await context.TicketTypes.AddAsync(type);
                        await context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"**** Error Adding Default Ticket type {type.Name} ****");
                    Debug.WriteLine(ex.Message);
                }
            }
        }
        private static async Task SeedDefaultStatusAsync(ApplicationDbContext context)
        {
            var statusList = new List<TicketStatus>() {
                new TicketStatus
                {
                    Name = "New"
                },
                new TicketStatus
                {
                    Name = "Open"
                },
                new TicketStatus
                {
                    Name = "Closed"
                }
            };
            foreach (var status in statusList)
            {
                try
                {
                    var ticketStatus = await context.TicketStatus.Where(t => t.Name == status.Name).FirstOrDefaultAsync();
                    if (ticketStatus == null)
                    {
                        await context.TicketStatus.AddAsync(status);
                        await context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"**** Error Adding Default Ticket Status {status.Name} ****");
                    Debug.WriteLine(ex.Message);
                }
            }    
        }
        private static async Task SeedDefaultProjectsAsync(ApplicationDbContext context)
        {
            var projectNames = new List<Project>()
            {
                new Project {
                    Name = "Financial Management Software"
                },
                new Project
                {
                    Name = "Issue Tracker Software"
                },
                new Project
                {
                    Name = "Blog"
                },
                new Project
                {
                    Name = "Financial Portal API"
                }
            };
            foreach (var project in projectNames)
            {
                try
                {
                    var projectToSave = await context.Projects.Where(p => p.Name == project.Name).FirstOrDefaultAsync();
                    if (projectToSave == null)
                    {
                        await context.Projects.AddAsync(project);
                        await context.SaveChangesAsync();
                    }
                    projectList.Add(projectToSave);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"**** Error Adding Default Project {project.Name} ****");
                    Debug.WriteLine(ex.Message);
                }
            }
        }
        private static async Task SeedProjectUsersAsync(ApplicationDbContext context, UserManager<BTUser> userManager)
        {
            var adminIdList = new List<string>();
            var pMIdList = new List<string>();
            var devIdList = new List<string>();
            var submitterIdList = new List<string>();
            var projectIdList = new List<int>();
            foreach (var admin in adminList)
            {
                
                adminIdList.Add((await userManager.FindByEmailAsync(admin.Email)).Id);
            }
            foreach (var pm in pMList)
            {
                var email = pm.Email;
                var id = (await userManager.FindByEmailAsync(email)).Id;
                pMIdList.Add(id);
            }
            foreach (var dev in devList)
            {
                var email = dev.Email;
                var id = (await userManager.FindByEmailAsync(email)).Id;
                devIdList.Add(id);
            }
            foreach (var submitter in submitterList)
            {
                var email = submitter.Email;
                var id = (await userManager.FindByEmailAsync(email)).Id;
                submitterIdList.Add(id);
            }
            foreach (var project in projectList)
            {
                var projectId = (await context.Projects.FirstOrDefaultAsync(p => p.Name == project.Name)).Id;
                projectIdList.Add(projectId);
            }
            //string adminId = (await userManager.FindByEmailAsync("demoadmin@mailinator.com")).Id;
            //string pMId = (await userManager.FindByEmailAsync("demopm@mailinator.com")).Id;
            //string developerId = (await userManager.FindByEmailAsync("demodev@mailinator.com")).Id;
            //string submiiterId = (await userManager.FindByEmailAsync("demosubmitter@mailinator.com")).Id;
            //int project1Id = (await context.Projects.FirstOrDefaultAsync(p => p.Name == "Financial Management Software")).Id;
            //int project2Id = (await context.Projects.FirstOrDefaultAsync(p => p.Name == "Issue Tracker Software")).Id;
            //int project3Id = (await context.Projects.FirstOrDefaultAsync(p => p.Name == "Blog")).Id;
            //int project4Id = (await context.Projects.FirstOrDefaultAsync(p => p.Name == "Financial Portal API")).Id;

            #region add admin
            ProjectUser adminUser = new ProjectUser();
            ProjectUser pmUser = new ProjectUser();
            ProjectUser devUser = new ProjectUser();
            ProjectUser submitterUser = new ProjectUser();


            //foreach (var projectId in projectIdList)
            //{
            //    var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == adminId && r.ProjectId == project1Id);
            //}
            //try
            //{
            //    var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.)

            //} catch (Exception ex)
            //{

            //}
            //try
            //{
            //    var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == adminId && r.ProjectId == project1Id);
            //    if (record == null)
            //    {
            //        await context.ProjectUsers.AddAsync(projectUser);
            //        await context.SaveChangesAsync();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("**** Error Adding Admin to project 1 ****");
            //    Debug.WriteLine(ex.Message);
            //}
            //projectUser = new ProjectUser
            //{
            //    UserId = adminId,
            //    ProjectId = project2Id
            //};
            //try
            //{
            //    var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == adminId && r.ProjectId == project2Id);
            //    if (record == null)
            //    {
            //        await context.ProjectUsers.AddAsync(projectUser);
            //        await context.SaveChangesAsync();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("**** Error Adding Admin to project 2 ****");
            //    Debug.WriteLine(ex.Message);
            //}
            //projectUser = new ProjectUser
            //{
            //    UserId = adminId,
            //    ProjectId = project3Id
            //};
            //try
            //{
            //    var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == adminId && r.ProjectId == project3Id);
            //    if (record == null)
            //    {
            //        await context.ProjectUsers.AddAsync(projectUser);
            //        await context.SaveChangesAsync();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("**** Error Adding Admin to project 3 ****");
            //    Debug.WriteLine(ex.Message);
            //}
            //projectUser = new ProjectUser
            //{
            //    UserId = adminId,
            //    ProjectId = project4Id
            //};
            //try
            //{
            //    var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == adminId && r.ProjectId == project4Id);
            //    if (record == null)
            //    {
            //        await context.ProjectUsers.AddAsync(projectUser);
            //        await context.SaveChangesAsync();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("**** Error Adding Admin to project 4 ****");
            //    Debug.WriteLine(ex.Message);
            //}
            //#endregion

            //#region add pm
            //projectUser = new ProjectUser
            //{
            //    UserId = pMId,
            //    ProjectId = project1Id
            //};
            //try
            //{
            //    var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == adminId && r.ProjectId == project1Id);
            //    if (record == null)
            //    {
            //        await context.ProjectUsers.AddAsync(projectUser);
            //        await context.SaveChangesAsync();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("**** Error Adding Admin to project 1 ****");
            //    Debug.WriteLine(ex.Message);
            //}
            //projectUser = new ProjectUser
            //{
            //    UserId = pMId,
            //    ProjectId = project2Id
            //};
            //try
            //{
            //    var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == adminId && r.ProjectId == project2Id);
            //    if (record == null)
            //    {
            //        await context.ProjectUsers.AddAsync(projectUser);
            //        await context.SaveChangesAsync();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("**** Error Adding Admin to project 2 ****");
            //    Debug.WriteLine(ex.Message);
            //}
            //projectUser = new ProjectUser
            //{
            //    UserId = pMId,
            //    ProjectId = project3Id
            //};
            //try
            //{
            //    var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == adminId && r.ProjectId == project3Id);
            //    if (record == null)
            //    {
            //        await context.ProjectUsers.AddAsync(projectUser);
            //        await context.SaveChangesAsync();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("**** Error Adding Admin to project 3 ****");
            //    Debug.WriteLine(ex.Message);
            //}
            //projectUser = new ProjectUser
            //{
            //    UserId = pMId,
            //    ProjectId = project4Id
            //};
            //try
            //{
            //    var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == adminId && r.ProjectId == project4Id);
            //    if (record == null)
            //    {
            //        await context.ProjectUsers.AddAsync(projectUser);
            //        await context.SaveChangesAsync();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("**** Error Adding Admin to project 4 ****");
            //    Debug.WriteLine(ex.Message);
            //}
            //#endregion

            //#region add dev
            //projectUser = new ProjectUser
            //{
            //    UserId = developerId,
            //    ProjectId = project1Id
            //};
            //try
            //{
            //    var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == adminId && r.ProjectId == project1Id);
            //    if (record == null)
            //    {
            //        await context.ProjectUsers.AddAsync(projectUser);
            //        await context.SaveChangesAsync();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("**** Error Adding Admin to project 1 ****");
            //    Debug.WriteLine(ex.Message);
            //}
            //projectUser = new ProjectUser
            //{
            //    UserId = developerId,
            //    ProjectId = project2Id
            //};
            //try
            //{
            //    var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == adminId && r.ProjectId == project2Id);
            //    if (record == null)
            //    {
            //        await context.ProjectUsers.AddAsync(projectUser);
            //        await context.SaveChangesAsync();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("**** Error Adding Admin to project 2 ****");
            //    Debug.WriteLine(ex.Message);
            //}
            //projectUser = new ProjectUser
            //{
            //    UserId = developerId,
            //    ProjectId = project3Id
            //};
            //try
            //{
            //    var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == adminId && r.ProjectId == project3Id);
            //    if (record == null)
            //    {
            //        await context.ProjectUsers.AddAsync(projectUser);
            //        await context.SaveChangesAsync();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("**** Error Adding Admin to project 3 ****");
            //    Debug.WriteLine(ex.Message);
            //}
            //projectUser = new ProjectUser
            //{
            //    UserId = developerId,
            //    ProjectId = project4Id
            //};
            //try
            //{
            //    var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == adminId && r.ProjectId == project4Id);
            //    if (record == null)
            //    {
            //        await context.ProjectUsers.AddAsync(projectUser);
            //        await context.SaveChangesAsync();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("**** Error Adding Admin to project 4 ****");
            //    Debug.WriteLine(ex.Message);
            //}
            //#endregion

            //#region add submitter
            //projectUser = new ProjectUser
            //{
            //    UserId = submiiterId,
            //    ProjectId = project1Id
            //};
            //try
            //{
            //    var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == adminId && r.ProjectId == project1Id);
            //    if (record == null)
            //    {
            //        await context.ProjectUsers.AddAsync(projectUser);
            //        await context.SaveChangesAsync();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("**** Error Adding Admin to project 1 ****");
            //    Debug.WriteLine(ex.Message);
            //}
            //projectUser = new ProjectUser
            //{
            //    UserId = submiiterId,
            //    ProjectId = project2Id
            //};
            //try
            //{
            //    var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == adminId && r.ProjectId == project2Id);
            //    if (record == null)
            //    {
            //        await context.ProjectUsers.AddAsync(projectUser);
            //        await context.SaveChangesAsync();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("**** Error Adding Admin to project 2 ****");
            //    Debug.WriteLine(ex.Message);
            //}
            //projectUser = new ProjectUser
            //{
            //    UserId = submiiterId,
            //    ProjectId = project3Id
            //};
            //try
            //{
            //    var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == adminId && r.ProjectId == project3Id);
            //    if (record == null)
            //    {
            //        await context.ProjectUsers.AddAsync(projectUser);
            //        await context.SaveChangesAsync();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("**** Error Adding Admin to project 3 ****");
            //    Debug.WriteLine(ex.Message);
            //}
            //projectUser = new ProjectUser
            //{
            //    UserId = submiiterId,
            //    ProjectId = project4Id
            //};
            //try
            //{
            //    var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == adminId && r.ProjectId == project4Id);
            //    if (record == null)
            //    {
            //        await context.ProjectUsers.AddAsync(projectUser);
            //        await context.SaveChangesAsync();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("**** Error Adding Admin to project 4 ****");
            //    Debug.WriteLine(ex.Message);
            //}
            //#endregion
        }
        private static async Task SeedDefaultTicketsAsync(ApplicationDbContext context, UserManager<BTUser> userManager)
        {
            string developerId = (await userManager.FindByEmailAsync("demodev@mailinator.com")).Id;
            string submiiterId = (await userManager.FindByEmailAsync("demosubmitter@mailinator.com")).Id;
            int project1Id = (await context.Projects.FirstOrDefaultAsync(predicate => predicate.Name == "Financial Management Software")).Id;
            int project2Id = (await context.Projects.FirstOrDefaultAsync(predicate => predicate.Name == "Issue Tracker Software")).Id;
            int project3Id = (await context.Projects.FirstOrDefaultAsync(predicate => predicate.Name == "Blog")).Id;
            int project4Id = (await context.Projects.FirstOrDefaultAsync(predicate => predicate.Name == "Financial Portal API")).Id;
            int statusId = (await context.TicketStatus.FirstOrDefaultAsync(ts => ts.Name == "open")).Id;
            int typeId = (await context.TicketTypes.FirstOrDefaultAsync(ts => ts.Name == "Bug")).Id;
            int priorityId = (await context.TicketPriority.FirstOrDefaultAsync(ts => ts.Name == "Low")).Id;

            Ticket ticket = new Ticket
            {
                Title = "Ticket Title",
                Description = "Description",
                Created = DateTimeOffset.Now.AddDays(-7),
                Updated = DateTimeOffset.Now.AddHours(-30),
                ProjectId = project1Id,
                TicketPriorityId = priorityId,
                TicketStatusId = statusId,
                TicketTypeId = typeId,
                DeveloperUserId = developerId,
                OwnerUserId = submiiterId
            };
            try
            {
                var newTicket = await context.Tickets.FirstOrDefaultAsync(t => t.Title == "Ticket Title");
                if (newTicket == null)
                {
                    await context.Tickets.AddAsync(newTicket);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("**** Error Adding ticket 1  ****");
                Debug.WriteLine(ex.Message);
            }
        }

        public static async Task RunSeedMethodsAsync(RoleManager<IdentityRole> roleManager, UserManager<BTUser> userManager, ApplicationDbContext context)
        {
            await SeedRolesAsync(roleManager);
            await SeedDefaultUsersAsync(userManager);
            await SeedDefaultTicketPriorityAsync(context);
            await SeedDefaultTicketTypeAsync(context);
            await SeedDefaultStatusAsync(context);
            await SeedDefaultProjectsAsync(context);
            await SeedProjectUsersAsync(context, userManager);
            await SeedDefaultTicketsAsync(context, userManager);
        }

        public static async Task CompleteSeedMethod(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var userManager = services.GetRequiredService<UserManager<BTUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await RunSeedMethodsAsync(roleManager, userManager, context);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error ocurred seeding the DB.");
                }
            }
            host.Run();
        }
    }
}

#endregion
