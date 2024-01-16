using Microsoft.AspNetCore.Mvc;

namespace RecordCollectionUI.Models
{
    public class Artist
    {
        public int ArtistID { get; set; }
        public string ArtistName { get; set;}
        public string ArtistBio { get; set;}
        public string ArtistImageURL { get; set;}
        public string ArtistWebsite { get; set;}

    }
}
