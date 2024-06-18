﻿namespace E_Commerce.Model;
//{
//public class orderModel
//{ }
//        public int OrderId { get; set; }
//        public Product Product { get; set; }
//        public int OrderQuantity { get; set; }
//        public DateTime ExpectedDate { get; set; }
//        public DateTime OrderDate { get; set; }
//        public string? DeliveryAddress { get; set; }
//        public Seller? Seller { get; set; }
//        public Customer? Customer { get; set; }
//        public string? OrderStatus { get; set; }
//        public DateTime? DeliveredDate { get; set; }
//        public string? PaymentMethod { get; set; }
//    }

//    public class Product
//    {
//        public int ProductId { get; set; }
//        public string ProductName { get; set; }
//        public string ProductDescription { get; set; }
//    }

//    public class Customer
//    {
//        public int CustomerId { get; set; }
//        public string CustomerName { get; set; }
//        public string CustomerContact { get; set; }
//    }

//    public class Seller
//    {
//        public int SellerId { get; set; }
//        public string SellerName { get; set; }
//        public string SellerContact { get; set; }
//        public string SellerAddress { get; set; }
//    }


public class orderModel
{
    public int OrderId { get; set; }
    public Product? Product { get; set; }
    public int OrderQuantity { get; set; }
    public DateTime ExpectedDate { get; set; }
    public DateTime OrderDate { get; set; }
    public string? DeliveryAddress { get; set; }
    public Seller? Seller { get; set; }
    public Customer? Customer { get; set; }

}

public class Seller
{
    public int? SellerId { get; set; }
    public string? SellerName { get; set; }
    public string? SellerContact { get; set; }
    public string? SellerAddress { get; set; }
}

public class Customer
{
    public int? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerContact { get; set; }
}

public class Product
{
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductDescription { get; set; }
}


