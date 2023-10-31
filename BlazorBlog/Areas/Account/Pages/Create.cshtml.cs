using Blazor.Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blazor.Blog.Areas.Account.Pages
{
    public class CreateModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        [BindProperty]
        public InputModel? Creds { get; set; }
        public string? ReturnUrl { get; set; }

        public CreateModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        public void OnGet()
        {
            if (_signInManager.IsSignedIn(User))
            {
                ReturnUrl = Url.Content("~/");
            }            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ReturnUrl = Url.Content("~/");
            if (ModelState.IsValid)
            {
                var identity = new IdentityUser { UserName = Creds!.Email, Email = Creds!.Email };
                var result = await _userManager.CreateAsync(identity, Creds!.Password);
                if (result.Succeeded) return LocalRedirect(ReturnUrl);
            }
            return Page();
        }
    }
}
