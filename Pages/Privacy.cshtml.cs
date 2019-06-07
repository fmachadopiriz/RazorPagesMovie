using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel;
using RazorPagesMovie.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace RazorPagesMovie.Pages
{
    public class PrivacyModel : PageModel
    {
        public string Message { get; set; } = "Initial request";

        private readonly UserManager<ApplicationUser> _userManager;

        public PrivacyModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public void OnGet()
        {
            if (HttpContext != null && HttpContext.User != null && HttpContext.User.Identity!= null && HttpContext.User.Identity.Name != null)
            {
                Message = HttpContext.User.Identity.Name;
            }
        }

        async public void OnPostAction1()
        {
            Message = "Action 1 handler";
            if (HttpContext.User != null)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (user != null)
                {
                    Message = user.Name;
                }
            }
        }

        public void OnPostAction2()
        {
            Message = "Action 2 handler";
        }

        public void OnPostAction3()
        {
            Message = "Action 3 handler";
        }
    }
}