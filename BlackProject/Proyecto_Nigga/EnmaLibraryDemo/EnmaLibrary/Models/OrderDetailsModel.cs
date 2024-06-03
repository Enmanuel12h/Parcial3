namespace EnmaLibrary.Models
{
    public class OrderDetailsModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }

        // Navigation properties
        public OrdersModel? Order { get; set; }
        public BooksModel? Book { get; set; }
    }

}
