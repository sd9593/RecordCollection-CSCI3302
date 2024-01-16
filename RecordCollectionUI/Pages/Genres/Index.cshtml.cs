using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RecordCollectionUI.Models;

namespace RecordCollectionUI.Pages.Genres
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<Genre> Genres { get; set; } = new List<Genre>();
        public void OnGet()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT * FROM Genre";
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Genre genre = new Genre();
                        genre.GenreID = int.Parse(reader["GenreID"].ToString());
                        genre.GenreName = reader["GenreName"].ToString();
                        Genres.Add(genre);
                    }
                }

            }
        }
    }
}
