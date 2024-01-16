using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RecordCollectionUI.Models;

namespace RecordCollectionUI.Pages.Artists
{
    public class ResultAlbumsModel : PageModel
    {
        [BindProperty]
        public List<Album> Albums { get; set; } = new List<Album>();
        [BindProperty]
        //public List<Genre> Genres { get; set; } = new List<Genre>();
        public Artist CurrentArtist { get; set; } = new Artist();


        public void OnGet(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT DISTINCT * FROM Album JOIN Artist ON Album.ArtistID = Artist.ArtistID WHERE Album.ArtistID = @artistID ORDER BY AlbumTitle";
                SqlCommand cmd = conn.CreateCommand();
                cmd.Parameters.AddWithValue("@artistID", id);
                cmd.CommandText = sql;
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Album album = new Album();
                        album.AlbumTitle = reader["AlbumTitle"].ToString();
                        album.NumberOfTracks = int.Parse(reader["NumberOfTracks"].ToString());
                        album.ReleaseYear = int.Parse(reader["ReleaseYear"].ToString());
                        album.Price = decimal.Parse(reader["Price"].ToString());
                        album.AlbumCoverImageURL = reader["AlbumCoverImageURL"].ToString();
                        album.AlbumID = int.Parse(reader["AlbumID"].ToString());
                        Albums.Add(album);
                        CurrentArtist.ArtistName = reader["ArtistName"].ToString();
                    }
                }

            }
        }


    }
}

