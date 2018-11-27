using System.ComponentModel.DataAnnotations;

namespace WebAssignment3.Models
{
    public class ESImage
    {
        public long ESImageId { get; set; }
        [MaxLength(128)]
        [Required]
        public string ImageMimeType { get; set; }
        public byte[] Thumbnail { get; set; }
        public byte[] ImageData { get; set; }
    }
}