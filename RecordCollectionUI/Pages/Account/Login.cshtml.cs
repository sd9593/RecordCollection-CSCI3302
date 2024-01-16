using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace RecordCollectionUI.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential LoginInfo { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //verify login info
            if (ModelState.IsValid)
            {
                if (LoginInfo.Email == "admin@hotmail.com" && LoginInfo.Password == "Password")
                {
                    var claims = new List<Claim>{
                        new Claim(ClaimTypes.Email, LoginInfo.Email),
                        new Claim(ClaimTypes.Name, "Admin"),
                        new Claim("username", "Admin")
                    };
                    var identity = new ClaimsIdentity(claims, "MusicCookie");
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync("MusicCookie", principal);
                    //async methods require putting await in front, and the method it is in
                    //must be "public async Task<IActionResult> MethodNameAsync()"
                    return RedirectToPage("/Index");
                    //create security context
                }
                return Page();
            }
            return Page();
        }
    }

    public class Credential
    {
        //creates model to bind to properties
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
