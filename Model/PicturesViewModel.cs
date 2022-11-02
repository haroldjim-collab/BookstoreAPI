using System.ComponentModel.DataAnnotations;

namespace BookstoreAPI.Model
{
    public class PicturesViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }

        public string? FilenameExtension { get; set; }

        public string? ContentType { get; set; }

        public string? URLLocation { get; set; }
    }
}
