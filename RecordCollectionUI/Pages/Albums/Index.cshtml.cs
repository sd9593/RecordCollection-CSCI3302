using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RecordCollectionUI.Models;

namespace RecordCollectionUI.Pages.Albums
{
    public class IndexModel : PageModel
    {
        public Album NewAlbum = new Album();
        [BindProperty]
        public List<Album> AlbumList { get; set; } = new List<Album>();
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        //global variable
        public void OnGet()
        {
            searchAlbumTitle(SearchTerm);

        }

        public void searchAlbumTitle(string SearchTerm)
        {
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
                {
                    //step 2
                    string sql = "SELECT * FROM Album WHERE AlbumTitle LIKE '%" + SearchTerm + "%' ORDER BY AlbumTitle";
                    //step 3
                    //SqlCommand cmd = new SqlCommand();
                    //cmd.CommandText = sql;
                    //cmd.Connection = conn;
                    //Above 3 lines is the same as the one below line
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    //step 4
                    conn.Open();
                    //step 5
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
                            AlbumList.Add(album);
                        }
                    }
                    //ExecuteNonQuery executes without caring about result
                    //Insert and delete usually use these
                    //ExecuteReader
                    //Select uses this
                }

            }
            else
            {
                allAlbums();
            }

        }

        public void allAlbums()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                //step 2
                string sql = "SELECT * FROM Album ORDER BY AlbumTitle";
                //step 3
                //SqlCommand cmd = new SqlCommand();
                //cmd.CommandText = sql;
                //cmd.Connection = conn;
                //Above 3 lines is the same as the one below line
                SqlCommand cmd = new SqlCommand(sql, conn);
                //step 4
                conn.Open();
                //step 5
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
                        AlbumList.Add(album);
                    }
                }
                //ExecuteNonQuery executes without caring about result
                //Insert and delete usually use these
                //ExecuteReader
                //Select uses this
            }
        }
    }
}
