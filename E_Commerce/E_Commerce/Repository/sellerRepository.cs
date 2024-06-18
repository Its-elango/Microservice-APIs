using E_Commerce.Model;
using E_Commerce.Repository.Interface;
using System.Data;
using System.Data.SqlClient;

namespace E_Commerce.Repository
{
    public class sellerRepository:ISeller
    {

        private readonly IConfiguration _configuration;
        private SqlConnection _connect;

        public sellerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection();
        }

        public void Connection()
        {
            string connects = _configuration.GetConnectionString("connect");
            _connect = new SqlConnection(connects);

        }


        public async Task<List<userModel>> GetSellerProfile(int SellerId)
        {
            try
            {
                Connection();
                _connect.Open();
                List<userModel> myProfile = new List<userModel>();
                SqlCommand command = new SqlCommand("SP_GetBySeller", _connect);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("UserId", SellerId);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow list in dt.Rows)
                {
                    myProfile.Add(
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
                return myProfile;
            }
            finally
            {
                _connect.Close();
            }
        }



        public async Task<List<sellerProduct>> GetSellerActiveProducts(int SellerId)
        {
            try
            {
                Connection();
                _connect.Open();
                List<sellerProduct> ActiverProducts = new List<sellerProduct>();
                SqlCommand command = new SqlCommand("SP_GetBySellerActiveProducts", _connect);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SellerId", SellerId);
                SqlDataAdapter adapter = new    (command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow list in dt.Rows)
                {
                    ActiverProducts.Add(
                        new sellerProduct
                        {
                            ProductId = Convert.ToInt32(list["ProductId"]),
                            ProductName = Convert.ToString(list["ProductName"]),
                            ProductDescription = Convert.ToString(list["ProductDescription"]),
                            Price = Convert.ToDecimal(list["Price"]),
                            ProductQuantity = Convert.ToInt32(list["ProductQuantity"]),
                            CategoryName = Convert.ToString(list["CategoryName"]),
                            IsActive = Convert.ToBoolean(list["IsActive"])
                        });
                }
                return ActiverProducts;
            }
            finally
            {
                _connect.Close();
            }
        }


        public async Task<List<sellerProduct>> GetMyProduct(int SellerId)
        {
            try
            {
                Connection();
                _connect.Open();
                List<sellerProduct> MyProducts = new List<sellerProduct>();
                SqlCommand command = new SqlCommand("SP_GetBySellerProducts", _connect);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SellerId", SellerId);
                SqlDataAdapter adapter = new(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow list in dt.Rows)
                {
                    MyProducts.Add(
                        new sellerProduct
                        {
                            ProductId = Convert.ToInt32(list["ProductId"]),
                            ProductName = Convert.ToString(list["ProductName"]),
                            ProductDescription = Convert.ToString(list["ProductDescription"]),
                            Price = Convert.ToDecimal(list["Price"]),
                            ProductQuantity = Convert.ToInt32(list["ProductQuantity"]),
                            CategoryName = Convert.ToString(list["CategoryName"]),
                            IsActive = Convert.ToBoolean(list["IsActive"])
                        });
                }
                return MyProducts;
            }
            finally
            {
                _connect.Close();
            }
        }

        public async Task<string> AddProduct(int SellerId,AddProduct product)
        {
            try
            {
                Connection();
                _connect.Open();
                SqlCommand command = new SqlCommand("SP_AddProduct", _connect);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@ProductDescription", product.ProductDescription);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@ProductQuantity", product.ProductQuantity);
                command.Parameters.AddWithValue("@CategoryName", product.CategoryName);
                command.Parameters.AddWithValue("@CategoryDescription", product.CategoryDescription);
                command.Parameters.AddWithValue("@SellerId", SellerId);


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

        public async Task<string> UpdateProduct(int ProductId,ProductUpdate product)
        {
            try
            {
                Connection();
                _connect.Open();
                SqlCommand command = new SqlCommand("SP_UpdateProduct", _connect);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductId",ProductId);
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@ProductDescription", product.ProductDescription);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@ProductQuantity", product.ProductQuantity);
                command.Parameters.AddWithValue("@CategoryName", product.CategoryName);
                command.Parameters.AddWithValue("@CategoryDescription", product.CategoryDescription);

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

        public async Task<bool> UpdateSellerProfile(int UserId, userModel user)
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

        public async Task<bool> DeleteProduct(int ProductId)
        {
            try
            {
                Connection();
                _connect.Open();
                SqlCommand command = new SqlCommand("SP_DeleteProduct", _connect);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductId", ProductId);
               
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

    }
}
