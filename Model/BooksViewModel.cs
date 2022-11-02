using System.ComponentModel.DataAnnotations;

namespace BookstoreAPI.Model
{
    public class BooksViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string? MetaKeywords { get; set; }

        public string? MetaTitle { get; set; }

        public string? MetaDescription { get; set; }

        public string? Sku { get; set; }

        public string? ISBN { get; set; }

        public string FullDescription { get; set; }
        public decimal? Price { get; set; }
        public decimal? OldPrice { get; set; }
        public int? StockQuantity { get; set; }

        public int? AuthorsId { get; set; }
        public int LanguagesId { get; set; }
        public int? ClassTypesId { get; set; }

        public AuthorsViewModel Authors { get; set; }
        public LanguagesViewModel Languages { get; set; }
        public ClassTypesViewModel ClassTypes { get; set; }
        public List<PictureBooksViewModel> PictureBooks { get; set; }

    }
}
