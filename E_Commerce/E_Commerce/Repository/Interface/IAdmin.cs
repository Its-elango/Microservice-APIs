using E_Commerce.Model;

namespace E_Commerce.Repository.Interface
{
    public interface IAdmin
    {

        #region GET Methods
        Task<List<userModel>> GetAllCustomer(int? UserId);
        Task<List<userModel>> GetAllSeller(int? UserId);
        Task<List<userModel>> GetAllAdmin(int? UserId);
        Task<List<AllProduct>> GetAllProducts();
        Task<List<AllProduct>> GetAllActiveProducts(int? ProductId);
        #endregion
        #region PUT Methods
        Task<bool> UpdateOrderStatus(updateOrderStatus orderStatus);
        Task<bool> UpdateProductActiveState(int ProductId, bool isActive);
        Task<bool> UpdateAdminProfile(int UserId, userModel user);

        #endregion
        #region POST Methods
        Task<string> AddNewUser(userModel user);
        #endregion
    }
}
