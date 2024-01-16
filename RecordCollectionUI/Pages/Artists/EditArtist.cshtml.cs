using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RecordCollectionUI.Models;

namespace RecordCollectionUI.Pages.Artists
{
    [Authorize]
    public class EditArtistModel : PageModel
    {
        [BindProperty]
        public Artist ExistingArtist { get; set; } = new Artist();
        public void OnGet(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT * FROM Artist WHERE ArtistID = @artistID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@artistID", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    ExistingArtist.ArtistName = reader["ArtistName"].ToString();
                    ExistingArtist.ArtistBio = reader["ArtistBio"].ToString();
                    ExistingArtist.ArtistImageURL = reader["ArtistImageURL"].ToString();
                    ExistingArtist.ArtistWebsite = reader["ArtistWebsite"].ToString();
                }
            }
        }
        public IActionResult OnPost(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                if (ModelState.IsValid)
                {
                    string sql = "UPDATE Artist SET ArtistName = @artistName, ArtistBio = @artistBio," +
                        "ArtistImageURL = @artistImageURL, ArtistWebsite = @artistWebsite WHERE ArtistId = @artistId";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@artistName", ExistingArtist.ArtistName);
                    cmd.Parameters.AddWithValue("@artistBio", ExistingArtist.ArtistBio);
                    cmd.Parameters.AddWithValue("@artistImageURL", ExistingArtist.ArtistImageURL);
                    cmd.Parameters.AddWithValue("@artistWebsite", ExistingArtist.ArtistWebsite);
                    cmd.Parameters.AddWithValue("@artistID", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return RedirectToPage("Index");
                }
                else
                {
                    return Page();
                }
            }
        }
    }
}
