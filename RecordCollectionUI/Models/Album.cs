using System.ComponentModel.DataAnnotations;

namespace RecordCollectionUI.Models
{
    public class Album
    {
        [Key]
        public int AlbumID { get; set; }
        [Required]
        public string AlbumTitle { get; set; }
        [Required]
        public int NumberOfTracks { get; set; }
        [Required]
        public int ReleaseYear { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string AlbumCoverImageURL { get; set; }
        [Required]
        public int ArtistID { get; set; }
    }
}
