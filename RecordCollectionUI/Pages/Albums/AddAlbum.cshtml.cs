using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using RecordCollectionUI.Models;
using System.Net;

namespace RecordCollectionUI.Pages.Albums
{
    [Authorize]
    public class AddAlbumModel : PageModel
    {
        [BindProperty]
        public Album NewAlbum { get; set; } = new Album();
        [BindProperty]
        public List<SelectListItem> Artist { get; set; } = new List<SelectListItem>();
        [BindProperty]
        public List<SelectListItem> GenreList { get; set; } = new List<SelectListItem>();
        [BindProperty]
        public List<int> GenresSelected { get; set; }
        public void OnGet()
        {
            PopulateArtistList();
            PopulateGenreList();
        }

        private void PopulateGenreList()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sqlGenre = "SELECT GenreId, GenreName FROM Genre Order By GenreName";
                SqlCommand cmd = new SqlCommand(sqlGenre, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var genre = new SelectListItem();
                        genre.Value = reader["GenreId"].ToString();
                        genre.Text = reader["GenreName"].ToString();
                        GenreList.Add(genre);
                    }
                }
            }
        }

        private void PopulateArtistList()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sqlArtist = "SELECT ArtistID, ArtistName FROM Artist Order By ArtistName";
                SqlCommand cmd = new SqlCommand(sqlArtist, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var artist = new SelectListItem();
                        artist.Value = reader["ArtistID"].ToString();
                        artist.Text = reader["ArtistName"].ToString();
                        Artist.Add(artist);
                    }
                }
            }
        }

        public IActionResult OnPost()
        {
   
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
                {
                    string sql = "INSERT INTO Album(AlbumTitle, ArtistID, NumberOfTracks, ReleaseYear, Price, AlbumCoverImageURL)" +
                        "VALUES (@albumTitle, @artistID, @numberOfTracks, @releaseYear, @price, @albumCoverImageURL)";
                    string sqlArtistID = "SELECT @@Identity";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@albumTitle", NewAlbum.AlbumTitle);
                    cmd.Parameters.AddWithValue("@artistID", NewAlbum.ArtistID);
                    cmd.Parameters.AddWithValue("@numberOfTracks", NewAlbum.NumberOfTracks);
                    cmd.Parameters.AddWithValue("@releaseYear", NewAlbum.ReleaseYear);
                    cmd.Parameters.AddWithValue("@price", NewAlbum.Price);
                    cmd.Parameters.AddWithValue("@albumCoverImageURL", NewAlbum.AlbumCoverImageURL);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    /*Microsoft.Data.SqlClient.SqlException: 'Cannot insert the value NULL into column 'ArtistID', 
                     * table 'RecordCollection.dbo.Album'; column does not allow nulls. INSERT fails.
                        The statement has been terminated.'*/
                    cmd.CommandText = sqlArtistID;
                    int albumID = int.Parse(cmd.ExecuteScalar().ToString());

                    string sqlAlbumGenre = "INSERT INTO AlbumGenre(AlbumID, GenreID) VALUES (@albumID, @genreID)";
                    cmd.CommandText = sqlAlbumGenre;

                    for (int i = 0; i < GenresSelected.Count(); i++)
                    {
                        cmd.Parameters.AddWithValue("@albumID", albumID);
                        cmd.Parameters.AddWithValue("@genreID", GenresSelected[i]);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    return RedirectToPage("Index");
                }
            }
            else
            {
                return Page();
            }
        }
    }
}
