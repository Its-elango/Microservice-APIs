namespace LogisticsManagementSystem.Models
{
    public class Shipper
    {
        public int ShipperId { get; set; }
        public string? ShipperName { get; set; }
        public string? ShipperContact { get; set; }
        public string? ShipperAddress { get; set; }
        public string? WorkLocation { get; set; }
        public DateTime JoinDate { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }


    public class ShipperApprovel
    {
        public int ShipperId { get; set; }
        public bool IsShipperApproved { get; set; }
    }

    public class Shipment
    {
        public int? ShipmentId { get; set; }
        public int? OrderId { get; set; }
        public int? ShipperId { get; set; }
        public bool HasDelivered { get; set; }
        public string? Comments { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }


}
