using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RecordCollectionUI.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            await HttpContext.SignOutAsync("MusicCookie");
            //async methods require putting await in front, and the method it is in
            //must be "public async Task<IActionResult> MethodNameAsync()"
            return RedirectToPage("/Index");
        }
    }
}
