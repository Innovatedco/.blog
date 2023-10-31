using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blazor.Blog.Models;

namespace Blazor.Blog.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        [BindProperty]
        public InputModel? Creds { get; set; }
        public string? ReturnUrl { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="signInManager"></param>
        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        /// <summary>
        /// Processes the query string.
        /// </summary>
        /// <returns>Page</returns>
        public ActionResult OnGetAsync()
        {
            ReturnUrl = SetReturnUrl();
            return Page();
        }

        /// <summary>
        /// Validates the login model then signs in, if successful the user is redirected to the return url
        /// or home page
        /// </summary>
        /// <returns>Page</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            ReturnUrl = SetReturnUrl();
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Creds!.Email, Creds!.Password, false, lockoutOnFailure: false);
                if (result.Succeeded) return LocalRedirect(ReturnUrl);
            }
            return Page();
        }

        /// <summary>
        /// Sets the return URL based on the incoming query string
        /// </summary>
        /// <returns>String returnUrl</returns>
        private string SetReturnUrl()
        {
            var queryStringVariable = Request.Query["returnUrl"];
            if (queryStringVariable != String.Empty)
            {
                return Url.Content($"~/{queryStringVariable}");
            }
            return ReturnUrl = Url.Content("~/");
        }
    }
}
