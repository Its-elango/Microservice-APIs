using System.Data;
using System;
using System.Data.SqlClient;
using E_Commerce.Model;
using E_Commerce.Repository.Interface;

namespace E_Commerce.Repository
{
    public class adminRepository:IAdmin
    {
        private readonly IConfiguration _configuration;
        private SqlConnection _connect;

        public adminRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Connection()
        {
            string connects = _configuration.GetConnectionString("connect");
            _connect = new SqlConnection(connects);
        }

        #region GetMethods
        public async Task<List<userModel>> GetAllCustomer(int? UserId)
        {
            try
            {
                Connection();
                 _connect.Open();
                List<userModel> customerList = new List<userModel>();
                SqlCommand command = new SqlCommand("SP_GetByCustomer", _connect);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("UserId", UserId);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow list in dt.Rows)
                {
                    customerList.Add(
                        new userModel
                        {
                            UserId = Convert.ToInt32(list["UserId"]),
                            Username = Convert.ToString(list["Username"]),
                            Password = Convert.ToString(list["Password"]),
                            Gender = Convert.ToString(list["Gender"]),
                            Age = Convert.ToInt32(list["Age"]),
                            Email = Convert.ToString(list["Email"]),
                            Role = Convert.ToString(list["Role"]),
                            State = Convert.ToString(list["State"]),
                            City = Convert.ToString(list["City"]),
                            Address = Convert.ToString(list["Address"]),
                            PhoneNumber = Convert.ToString(list["PhoneNumber"]),
                        }
                    );
               

                }
             return customerList;
            }
            finally
            {
                 _connect.Close();
            }
        }

        public async Task<List<userModel>> GetAllSeller(int? UserId)
        {
            try
            {
                Connection();
                _connect.Open();
                List<userModel> sellerList = new List<userModel>();
                SqlCommand command = new SqlCommand("SP_GetBySeller", _connect);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("UserId", UserId);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow list in dt.Rows)
                {
                    sellerList.Add(
                        new userModel
                        {
                            UserId = Convert.ToInt32(list["UserId"]),
                            Username = Convert.ToString(list["Username"]),
                            Password = Convert.ToString(list["Password"]),
                            Gender = Convert.ToString(list["Gender"]),
                            Age = Convert.ToInt32(list["Age"]),
                            Email = Convert.ToString(list["Email"]),
                            Role = Convert.ToString(list["Role"]),
                            State = Convert.ToString(list["State"]),
                            City = Convert.ToString(list["City"]),
                            Address = Convert.ToString(list["Address"]),
                            PhoneNumber = Convert.ToString(list["PhoneNumber"]),
                        });
                }
                return sellerList;
            }
            finally
            {
                _connect.Close();
            }
        }

        public async Task<List<userModel>> GetAllAdmin(int? UserId)
        {
            try
            {
                Connection();
                _connect.Open();
                List<userModel> adminList = new List<userModel>();
                SqlCommand command = new SqlCommand("SP_GetBySeller", _connect);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("UserId", UserId);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow list in dt.Rows)
                {
                    adminList.Add(
                        new userModel
                        {
                            UserId = Convert.ToInt32(list["UserId"]),
                            Username = Convert.ToString(list["Username"]),
                            Password = Convert.ToString(list["Password"]),
                            Gender = Convert.ToString(list["Gender"]),
                            Age = Convert.ToInt32(list["Age"]),
                            Email = Convert.ToString(list["Email"]),
                            Role = Convert.ToString(list["Role"]),
                            State = Convert.ToString(list["State"]),
                            City = Convert.ToString(list["City"]),
                            Address = Convert.ToString(list["Address"]),
                            PhoneNumber = Convert.ToString(list["PhoneNumber"]),
                        });
                }
                return adminList;
            }
            finally
            {
                _connect.Close();
            }
        }


        public async Task<List<AllProduct>> GetAllProducts()
        {
            try
            {
                Connection();
                _connect.Open();
                List<AllProduct> productList = new List<AllProduct>();
                SqlCommand command = new SqlCommand("SP_GetAllProductsAndCategories", _connect);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow list in dt.Rows)
                {
                    productList.Add(new AllProduct
                    {   
                        ProductId = Convert.ToInt32(list["ProductId"]),
                        ProductName = Convert.ToString(list["ProductName"]),
                        ProductDescription = Convert.ToString(list["ProductDescription"]),
                        Price = Convert.ToDecimal(list["Price"]),
                        ProductQuantity = Convert.ToInt32(list["ProductQuantity"]),
                        CategoryName = Convert.ToString(list["CategoryName"]),
                        IsActive = Convert.ToBoolean(list["IsActive"]),
                        SellerId = Convert.ToInt32(list["SellerId"]),
                        CategoryDescription = Convert.ToString(list["CategoryDescription"])
                    });
                }
                return productList;
            }
            finally
            {
                _connect.Close();
            }
        }


        public async Task<List<AllProduct>> GetAllActiveProducts(int? ProductId)
        {
            try
            {
                Connection();
                _connect.Open();
                List<AllProduct> productList = new List<AllProduct>();
                SqlCommand command = new SqlCommand("SP_ValidGetProductById", _connect);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("ProductId", ProductId);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow list in dt.Rows)
                {
                    productList.Add(new AllProduct
                    {
                        ProductId = Convert.ToInt32(list["ProductId"]),
                        ProductName = Convert.ToString(list["ProductName"]),
                        ProductDescription = Convert.ToString(list["ProductDescription"]),
                        Price = Convert.ToDecimal(list["Price"]),
                        ProductQuantity = Convert.ToInt32(list["ProductQuantity"]),
                        CategoryName = Convert.ToString(list["CategoryName"]),
                        IsActive = Convert.ToBoolean(list["IsActive"]),
                        SellerId = Convert.ToInt32(list["SellerId"]),
                        CategoryDescription = Convert.ToString(list["CategoryDescription"])
                    });
                }
                return productList;
            }
            finally
            {
                _connect.Close();
            }
        }

        #endregion

        


        public async Task<bool> UpdateOrderStatus(updateOrderStatus orderStatus)
        {
            try
            {
                Connection();
                _connect.Open();
                SqlCommand command = new SqlCommand("SP_UpdateOrderStatus", _connect);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OrderId", orderStatus.OrderId);
                command.Parameters.AddWithValue("@OrderStatus", orderStatus.Comments);
                command.Parameters.AddWithValue("@DeliveredDate", orderStatus.DeliveryDate);
                
                int i = await command.ExecuteNonQueryAsync();
                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            finally
            {
                _connect.Close();
            }
        }

        public async Task<bool> UpdateProductActiveState(int ProductId, bool isActive)
        {
            try
            {
                Connection();
                _connect.Open();
                SqlCommand command = new SqlCommand("SP_UpdateActiveState", _connect);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductId", ProductId);
                command.Parameters.AddWithValue("@isActive", isActive);

                int i = await command.ExecuteNonQueryAsync();
                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            finally
            {
                _connect.Close();
            }
        }

        public async Task<bool> UpdateAdminProfile(int UserId, userModel user)
        {
            try
            {
                Connection();
                _connect.Open();
                SqlCommand command = new SqlCommand("SP_UpdateUserDetails", _connect);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", UserId);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Gender", user.Gender);
                command.Parameters.AddWithValue("@Age", user.Age);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@State", user.State);
                command.Parameters.AddWithValue("@City", user.City);
                command.Parameters.AddWithValue("@Address", user.Address);
                command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);

                int i = await command.ExecuteNonQueryAsync();
                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            finally
            {
                _connect.Close();
            }
        }



        public async Task<string> AddNewUser(userModel user)
        {
            try
            {
                Connection();
                _connect.Open();
                SqlCommand command = new SqlCommand("SP_AddNewUser", _connect);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Gender", user.Gender);
                command.Parameters.AddWithValue("@Age", user.Age);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Role", user.Role);
                command.Parameters.AddWithValue("@State", user.State);
                command.Parameters.AddWithValue("@City", user.City);
                command.Parameters.AddWithValue("@Address", user.Address);
                command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                var outputMessage = new SqlParameter("@ResultMessage", SqlDbType.NVarChar, 250)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputMessage);

                await command.ExecuteNonQueryAsync();

                return outputMessage.Value.ToString();

            }
            finally
            {
                _connect.Close();
            }
        }






    }
}
