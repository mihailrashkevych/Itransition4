using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project1.Data;
using Project1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Project1.Areas.Identity.Pages.Account;

namespace Project1.Controllers
{
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        ApplicationDbContext context;
        UserManager<ApplicationUser> userManager;

        public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        [Route("[controller]")]
        [HttpGet]
        public IEnumerable<ApplicationUser> Get()
        {
            var users = context.Users.ToList();
            return users;
        }
        [Route("[controller]/lock")]
        [HttpPost]
        public async Task<IdentityResult> Lock([FromBody] string[] ids)
        {
            foreach (var id in ids)
            {
                var user = await userManager.FindByIdAsync(id);
                await userManager.SetLockoutEndDateAsync(user, DateTime.Now + TimeSpan.FromMinutes(10));
                await userManager.UpdateSecurityStampAsync(user);
            }
            await context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        [Route("[controller]/unlock")]
        [HttpPost]
        public async Task<IdentityResult> UnLock([FromBody] string[] ids)
        {
            foreach (var id in ids)
            {
                var user = await userManager.FindByIdAsync(id);
                await userManager.SetLockoutEndDateAsync(user, DateTime.Now);
                await userManager.UpdateSecurityStampAsync(user);
            }
            await context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        [Route("[controller]")]
        [HttpDelete]
        public async Task<IdentityResult> Delete([FromBody] string[] ids)
        {
            foreach (var id in ids)
            {
                var user = await userManager.FindByIdAsync(id);
                await userManager.UpdateSecurityStampAsync(user);
                await userManager.DeleteAsync(user);
            }
            await context.SaveChangesAsync();
            return IdentityResult.Success;
        }
    }
}
