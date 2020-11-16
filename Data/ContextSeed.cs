using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        NewUser
    }
    public static class ContextSeed
    {
        //seed roles
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.ProjectManager.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Developer.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Submitter.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.NewUser.ToString()));

        }
        //seed Users
        public static async Task SeedDefaultUsersAsync(UserManager<BTUser> userManager)
        {
            var errorMsg = "******* ERROR SEEDING DEFAULT USER(S) *******";
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
        }
        //public static async Task SeedDefaultprojectsTicketsComments(ApplicationDbContext context)
        //{

        //}
        public static async Task SeedDefaultTicketPriority(ApplicationDbContext context)
        {
            var defaultSeedNew = new TicketPriority
            {
                Name = "New"
            };
            var defaultSeedLow = new TicketPriority
            {
                Name = "Low"
            };
            var defaultSeedHigh = new TicketPriority
            {
                Name = "High"
            };
            var defaultSeedUrgent = new TicketPriority
            {
                Name = "Urgent"
            };
            //new
            try
            {
                var ticketPriority = await context.TicketPriority.Where(t => t.Name == "New").FirstOrDefaultAsync();
                if (ticketPriority == null)
                {
                    await context.TicketPriority.AddAsync(defaultSeedNew);
                    await context.SaveChangesAsync();
                }
            } 
            catch (Exception ex)
            {
                Debug.WriteLine("**** Error Adding Default Ticket Priority New ****");
                Debug.WriteLine(ex.Message);
            }
            //low
            try
            {
                var ticketPriority = await context.TicketPriority.Where(t => t.Name == "Low").FirstOrDefaultAsync();
                if (ticketPriority == null)
                {
                    await context.TicketPriority.AddAsync(defaultSeedLow);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("**** Error Adding Default Ticket Priority Low ****");
                Debug.WriteLine(ex.Message);
            }
            //high
            try
            {
                var ticketPriority = await context.TicketPriority.Where(t => t.Name == "High").FirstOrDefaultAsync();
                if (ticketPriority == null)
                {
                    await context.TicketPriority.AddAsync(defaultSeedHigh);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("**** Error Adding Default Ticket Priority High ****");
                Debug.WriteLine(ex.Message);
            }
            //urgent
            try
            {
                var ticketPriority = await context.TicketPriority.Where(t => t.Name == "Urgent").FirstOrDefaultAsync();
                if (ticketPriority == null)
                {
                    await context.TicketPriority.AddAsync(defaultSeedUrgent);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("**** Error Adding Default Ticket Priority Urgent ****");
                Debug.WriteLine(ex.Message);
            }

        }
        public static async Task seedDefaultTicketType(ApplicationDbContext context)
        {
            var defaultSeedBug = new TicketType
            {
                Name = "Bug"
            };
            var defaultSeedFeature = new TicketType
            {
                Name = "Feature"
            };
            try
            {
                var ticketType = await context.TicketTypes.Where(t => t.Name == "Bug").FirstOrDefaultAsync();
                if (ticketType == null)
                {
                    await context.TicketTypes.AddAsync(defaultSeedBug);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("**** Error Adding Default Ticket type Bug ****");
                Debug.WriteLine(ex.Message);
            }
            try
            {
                var ticketType = await context.TicketTypes.Where(t => t.Name == "Feature").FirstOrDefaultAsync();
                if (ticketType == null)
                {
                    await context.TicketTypes.AddAsync(defaultSeedFeature);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("**** Error Adding Default Ticket type Feature ****");
                Debug.WriteLine(ex.Message);
            }
        }
    }


}


