namespace BookstoreAPI.Model
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public int BooksId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public BooksViewModel Books { get; set; }
    }
}
