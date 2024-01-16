using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RecordCollectionUI.Models;

namespace RecordCollectionUI.Pages.Genres
{
    [Authorize]
    public class EditGenreModel : PageModel
    {
        [BindProperty]
        public Genre ExisitingGenre { get; set; } = new Genre();
        public void OnGet(int id)
        {

            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT * FROM Genre WHERE GenreID = @genreID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@genreID", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    ExisitingGenre.GenreName = reader["GenreName"].ToString();

                }
            }
        }

        public IActionResult OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
                {
                    string sql = "UPDATE Genre SET GenreName = @genreName WHERE GenreID = @genreID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@genreName", ExisitingGenre.GenreName);
                    cmd.Parameters.AddWithValue("@genreID", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return RedirectToPage("Index");
                }
            }
            return Page();

        }
    }
}

