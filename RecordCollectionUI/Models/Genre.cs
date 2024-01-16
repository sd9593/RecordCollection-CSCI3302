using System.ComponentModel.DataAnnotations;

namespace RecordCollectionUI.Models
{
    public class Genre
    {
        [Key]
        public int GenreID { get; set; }
        [Required]
        public string GenreName { get; set; } = string.Empty;
    }
}
