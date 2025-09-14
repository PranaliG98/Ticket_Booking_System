using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Entities;
using TicketBooking.Infrastructure;
using TicketBooking.Repositories.Interfaces;

namespace TicketBooking.Repositories.Implementations
{
    public class DbInitial : IDbInitial
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;

        public DbInitial(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        public Task DataSeed()
        {
            try
            {
                if(_context.Database.GetPendingMigrations().Count()>0)
                {
                    _context.Database.Migrate();
                }
                if(!_roleManager.RoleExistsAsync(GlobalConfiguration.Admin_Role).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(GlobalConfiguration.Admin_Role)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(GlobalConfiguration.Customer_Role)).GetAwaiter().GetResult();

                    var AppUser = new ApplicationUser
                    {
                        UserName = "admin@gmail.com",
                        Email = "admin@gmail.com",
                        EmailConfirmed=true
                    };
                    _userManager.CreateAsync(AppUser, "Admin@1234").GetAwaiter().GetResult();
                    var user = _userManager.Users.Where(x => x.Email == "admin@gmail.com").FirstOrDefault();
                    _userManager.AddToRoleAsync(user, GlobalConfiguration.Admin_Role).GetAwaiter().GetResult();
                }
                return Task.CompletedTask;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
