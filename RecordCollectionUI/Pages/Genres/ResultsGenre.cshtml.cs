using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RecordCollectionUI.Models;

namespace RecordCollectionUI.Pages.Genres
{
    public class ResultsGenreModel : PageModel
    {
        [BindProperty]
        public List<Album> AlbumsGenres { get; set; } = new List<Album>();
        [BindProperty]
        //public List<Genre> Genres { get; set; } = new List<Genre>();
        public Genre CurrentGenre { get; set; } = new Genre();


        public void OnGet(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT DISTINCT * FROM Album JOIN AlbumGenre ON Album.AlbumID = AlbumGenre.AlbumID JOIN Genre ON AlbumGenre.GenreID = Genre.GenreID WHERE Genre.GenreID = @genreID ORDER BY AlbumTitle";
                SqlCommand cmd = conn.CreateCommand();
                cmd.Parameters.AddWithValue("@genreID", id);
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
                        AlbumsGenres.Add(album);
                        CurrentGenre.GenreName = reader["GenreName"].ToString();
                    }
                }

            }
        }


    }
}

