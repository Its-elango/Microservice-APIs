using Logistics_Management_System.Models;
using LogisticsManagementSystem.Models;

namespace Logistics_Management_System.Respository
{
    public interface ILogistics
    {
        #region GET Methods
        Task<IEnumerable<Shipper>> GetShippers();

        Task<IEnumerable<Shipper>> GetShipper(int shipperId);
        Task<IEnumerable<Order>> GetShipperOrders(int shipperId);
        Task<IEnumerable<Order>> GetOrders();
        Task<IEnumerable<Order>> GetOrder(int orderId);
        Task<IEnumerable<Shipment>> GetShipments();
        Task<IEnumerable<Shipment>> GetShipment(int shipmentId);
        Task<string> CheckOrderStatus(int orderId);

        #endregion End of GET Methods

        #region POST Methods
        Task<string> AddOrder(Order order);
        Task<string> AddShipper(Shipper shipper);

        Task<LoginResponse> Login(User user);
        //Task<bool> AddShipment(Shipment shipment);

        #endregion POST Methods

        #region PUT Methods
        Task<string> UpdateShipper(Shipper shipper);
        Task<string> TakeOrder(int shipperId, int orderId);
        Task<string> ApproveShipper(ShipperApprovel shipperApprovel);
        Task<string> UpdateShipment(Shipment shipment);
        Task<string> UpdateDeliveryStatus(UpdateDeliveryStatus deliveryStatus);

        #endregion PUT Methods
    }
}
