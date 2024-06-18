using E_Commerce.Model;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Repository.Interface
{
    public interface ISeller
    {
        #region GET Method
        Task<List<userModel>> GetSellerProfile(int SellerId);
        Task<List<sellerProduct>> GetSellerActiveProducts(int SellerId);
        Task<List<sellerProduct>> GetMyProduct(int SellerId);
        #endregion
        #region POST Method
        Task<string> AddProduct(int SellerId, AddProduct product);
        #endregion
        #region PUT Method
        Task<string> UpdateProduct(int ProductId, ProductUpdate product);
        Task<bool> UpdateSellerProfile(int UserId, userModel user);
        #endregion
        #region DELETE Method
        Task<bool> DeleteProduct(int ProductId);
        #endregion
    }
}
