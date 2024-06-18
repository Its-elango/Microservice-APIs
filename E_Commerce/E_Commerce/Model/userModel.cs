namespace E_Commerce.Model
{
    public class userModel
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Gender { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
    
    public class OrderPlace
    {
        public int ProductId { get; set; }
        public int OrderQuantity { get; set; }
        public int CustomerId { get; set; }
        public string? PaymentMethod { get; set; }
        public string? DeliveryAddress { get; set; }

        public string? Email { get; set; }
        public decimal Amount { get; set; }
    }

    public class OrderResponse
    {
        public string ResultMessage { get; set; }
        public int OrderId { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}
