using E_Commerce.Model;

namespace E_Commerce.Repository.Interface
{
    public interface ICustomer
    {
        #region GET Method
        Task<List<userModel>> GetMyProfile(int UserId);
        Task<orderModel> GetOrderDetails(int orderId);
        Task<List<AllProduct>> GetAllActiveProducts();
        Task<List<AllProduct>> GetAllActiveProductsById(int ProductId);
        Task<List<AllProduct>> GetByCategory(string? CategoryName);
        #endregion
        #region PUT Method
        Task<bool> UpdateCustomerProfile(int UserId, userModel user);
        #endregion
        #region POST METHOD
        Task<OrderResponse> PlaceOrder(OrderPlace order);
        #endregion
    }
}
