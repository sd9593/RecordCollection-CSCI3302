using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RecordCollectionUI.Models;

namespace RecordCollectionUI.Pages.Artists
{
    [Authorize]
    public class AddArtistModel : PageModel
    {
        [BindProperty]

        public Artist NewArtist { get; set; } = new Artist();
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            //Capture user input and add a new artist to the database
            /*
             * 1. Create a connection to the database server
             * using the connection string stored in appsettings.json
             *    Create an SQL connection object
             * 2. Write the SQL query to insert data
             * INSERT INTO Artist(ArtistName, ArtistBio, ArtistImageURL, ArtistWebsite)
             * VALUES (@artistName, @artistBio, @artistImageURL, @artistWebsite)
             * 3. Create a command to use the query
             *    Command includes query string and the relevant connections
             * 4. Open the connection
             * 5. Execute the command object
             * 6. Close the connection
             */
            //step 1
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
                {
                    //step 2
                    string sql = "INSERT INTO Artist(ArtistName, ArtistBio, ArtistImageURL, ArtistWebsite)" +
                        "VALUES (@artistName, @artistBio, @artistImageURL, @artistWebsite)";
                    //step 3
                    //SqlCommand cmd = new SqlCommand();
                    //cmd.CommandText = sql;
                    //cmd.Connection = conn;
                    //Above 3 lines is the same as the one below line
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@artistName", NewArtist.ArtistName);
                    cmd.Parameters.AddWithValue("@artistBio", NewArtist.ArtistBio);
                    cmd.Parameters.AddWithValue("@artistImageURL", NewArtist.ArtistImageURL);
                    cmd.Parameters.AddWithValue("@artistWebsite", NewArtist.ArtistWebsite);

                    //step 4
                    conn.Open();
                    //step 5
                    cmd.ExecuteNonQuery();
                    //ExecuteNonQuery executes without caring about result
                    //Insert and delete usually use these
                    //ExecuteReader
                    //Select uses this
                }//using this block you don't need conn.Close() since it will close automatically
                return RedirectToPage("Index"); //redirect to Artists/Index.cshtml
            }
            return Page();
            //else statement
        }
    }
}


