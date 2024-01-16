using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RecordCollectionUI.Models;

namespace RecordCollectionUI.Pages.Genres
{
    [Authorize]
    public class AddGenreModel : PageModel
    {
        [BindProperty]
        public Genre NewGenre { get; set; } = new Genre();
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
                {
                    string sql = "INSERT INTO Genre(GenreName) VALUES (@genreName)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@genreName", NewGenre.GenreName);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return RedirectToPage("Index");
                }
            }
            return Page();
            
        }
    }
}
