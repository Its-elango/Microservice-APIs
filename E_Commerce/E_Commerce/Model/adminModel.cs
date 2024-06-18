namespace E_Commerce.Model
{
    public class adminModel
    { 
        
    }

    public class AllUser
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int Age { get; set; }
        public string? Role { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }


    public class AllProduct
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal Price { get; set; }
        public int ProductQuantity { get; set; }
        public string? CategoryName { get; set; }
        public bool IsActive { get; set; }
        public int SellerId { get; set; }
        public string? CategoryDescription { get; set; }
    }


    public class ProductUpdate
    {
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal Price { get; set; }
        public int ProductQuantity { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }

    }

    public class updateOrderStatus
    {
        public int OrderId { get; set; }
        public string? Comments { get; set; }
        public DateTime DeliveryDate { get; set; }
    }



}
