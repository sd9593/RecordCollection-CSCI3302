using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RecordCollectionUI.Models;

namespace RecordCollectionUI.Pages.Artists
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<Artist> ArtistList { get; set; } = new List<Artist>();
        [BindProperty(SupportsGet =true)]
        public string? SearchTerm { get; set; }
        //global variable
        public void OnGet()
        {
            searchArtists(SearchTerm);
        }

        private void searchArtists(string SearchTerm)
        {

            if (string.IsNullOrEmpty(SearchTerm))
            {
                allArtists();
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
                {
                    //step 2
                    string sql = "SELECT * FROM Artist WHERE ArtistName LIKE '%" + SearchTerm + "%' ORDER BY ArtistName" ;
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
                            Artist artist = new Artist();
                            artist.ArtistName = reader["ArtistName"].ToString();
                            artist.ArtistBio = reader["ArtistBio"].ToString();
                            artist.ArtistImageURL = reader["ArtistImageURL"].ToString();
                            artist.ArtistWebsite = reader["ArtistWebsite"].ToString();
                            artist.ArtistID = int.Parse(reader["ArtistID"].ToString());
                            ArtistList.Add(artist);
                        }
                    }
                    //ExecuteNonQuery executes without caring about result
                    //Insert and delete usually use these
                    //ExecuteReader
                    //Select uses this



                }
            }

           
        }

        public void allArtists()
        {
            //Grab list of artists and store in container
            /*
             * 1. Create a connection to the database server
             * using the connection string stored in appsettings.json
             *    Create an SQL connection object
             * 2. Write the SQL query to select data
             * 3. Create a command to use the query
             *    Command includes query string and the relevant connections
             * 4. Open the connection
             * 5. Execute the command object
             * 6. Close the connection
             */
            //step 1
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                //step 2
                string sql = "SELECT * FROM Artist ORDER BY ArtistName";
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
                        Artist artist = new Artist();
                        artist.ArtistName = reader["ArtistName"].ToString();
                        artist.ArtistBio = reader["ArtistBio"].ToString();
                        artist.ArtistImageURL = reader["ArtistImageURL"].ToString();
                        artist.ArtistWebsite = reader["ArtistWebsite"].ToString();
                        artist.ArtistID = int.Parse(reader["ArtistID"].ToString());
                        ArtistList.Add(artist);
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