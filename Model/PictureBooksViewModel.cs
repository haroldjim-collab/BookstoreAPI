namespace BookstoreAPI.Model
{
    public class PictureBooksViewModel
    {
        public int Id { get; set; }
        public int PicturesId { get; set; }
        public int BooksId { get; set; }
        public int DisplayOrderBy { get; set; } = 1;

        public PicturesViewModel Pictures { get; set; }

    }
}
