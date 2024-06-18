namespace E_Commerce.Model
{
    public class sellerModel
    {
    }

    public class sellerProduct
    {
            public int ProductId { get; set; }
            public string? ProductName { get; set; }
            public string? ProductDescription { get; set; }
            public decimal Price { get; set; }
            public int ProductQuantity { get; set; }
            public string? CategoryName { get; set; }
            public bool IsActive { get; set; }
        
    }

    public class AddProduct
    {
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal Price { get; set; }
        public int ProductQuantity { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
    }
}
