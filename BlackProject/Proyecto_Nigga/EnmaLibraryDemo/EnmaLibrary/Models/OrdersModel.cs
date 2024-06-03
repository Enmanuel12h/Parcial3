namespace EnmaLibrary.Models
{
    public class OrdersModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }

        // Navigation properties
        public CustomersModel? Customer { get; set; }
    }


}
