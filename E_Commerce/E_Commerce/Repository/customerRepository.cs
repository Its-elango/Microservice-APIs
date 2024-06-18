using E_Commerce.Model;
using E_Commerce.Repository.Interface;
using System.Data;
using System.Data.SqlClient;

namespace E_Commerce.Repository
{
    public class customerRepository:ICustomer
    {
        private readonly IConfiguration _configuration;
        private SqlConnection _connect;

        public customerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Connection()
        {
            string connects = _configuration.GetConnectionString("connect");
            _connect = new SqlConnection(connects);
        }
        public async Task<List<userModel>> GetMyProfile(int UserId)
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
                        });
                }
                return customerList;
            }
            finally
            {
                _connect.Close();
            }
        }

        public async Task<orderModel> GetOrderDetails(int orderId)
        {
            try
            {
                Connection();
                _connect.Open();
                SqlCommand command = new SqlCommand("SP_GetByOrder", _connect);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("OrderId", orderId);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    return null; // No order found with the given OrderId
                }

                DataRow row = dt.Rows[0];

                var order = new orderModel
                {
                    OrderId = Convert.ToInt32(row["OrderId"]),
                    OrderQuantity = Convert.ToInt32(row["OrderQuantity"]),
                    ExpectedDate = Convert.ToDateTime(row["ExpectedDate"]),
                    OrderDate = Convert.ToDateTime(row["OrderDate"]),
                    DeliveryAddress = Convert.ToString(row["DeliveryAddress"]),
                    //OrderStatus = Convert.ToString(row["OrderStatus"]),
                    //DeliveredDate = row["DeliveredDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["DeliveredDate"]) : null,
                    //PaymentMethod = Convert.ToString(row["PaymentMethod"]),
                    Product = new Product
                    {
                        ProductId = Convert.ToInt32(row["ProductId"]),
                        ProductName = Convert.ToString(row["ProductName"]?? null),
                        ProductDescription = Convert.ToString(row["ProductDescription"])
                    },
                    Seller = new Seller
                    {
                        SellerId = Convert.ToInt32(row["SellerId"]),
                        SellerName = Convert.ToString(row["SellerName"]),
                        SellerContact = Convert.ToString(row["SellerContact"]),
                        SellerAddress = Convert.ToString(row["SellerAddress"])
                    },
                    Customer = new Customer
                    {
                        CustomerId = Convert.ToInt32(row["CustomerId"]),
                        CustomerName = Convert.ToString(row["CustomerName"]),
                        CustomerContact = Convert.ToString(row["CustomerContact"])
                    }
                };

                return order;
            }
            finally
            {
                _connect.Close();
            }
        }


        public async Task<List<AllProduct>> GetAllActiveProducts()
        {
            try
            {
                Connection();
                _connect.Open();
                List<AllProduct> productList = new List<AllProduct>();
                SqlCommand command = new SqlCommand("SP_ValidGetProductById", _connect);
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
        public async Task<List<AllProduct>> GetAllActiveProductsById(int ProductId)
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

        public async Task<List<AllProduct>> GetByCategory(string? CategoryName)
        {
            try
            {
                Connection();
                _connect.Open();
                List<AllProduct> productList = new List<AllProduct>();
                SqlCommand command = new SqlCommand("SP_GetByCategory", _connect);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("CategoryName", CategoryName);
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



        public async Task<bool> UpdateCustomerProfile(int UserId, userModel user)
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



        public async Task<OrderResponse> PlaceOrder(OrderPlace order)
        {
            try
            {
                Connection();
                _connect.Open();
                SqlCommand command = new SqlCommand("SP_PlaceOrder", _connect);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductId", order.ProductId);
                command.Parameters.AddWithValue("@OrderQuantity", order.OrderQuantity);
                command.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                command.Parameters.AddWithValue("@PaymentMethod", order.PaymentMethod);
                command.Parameters.AddWithValue("@DeliveryAddress", order.DeliveryAddress);

                var outputMessage = new SqlParameter("@ResultMessage", SqlDbType.NVarChar, 250)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputMessage);

                var outputOrderId = new SqlParameter("@OrderId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputOrderId);

                var outputExpectedDate = new SqlParameter("@ExpectedDate", SqlDbType.Date)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputExpectedDate);

                var outputOrderDate = new SqlParameter("@OrderDate", SqlDbType.Date)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputOrderDate);

                await command.ExecuteNonQueryAsync();

                // Retrieve the output values
                var response = new OrderResponse
                {
                    ResultMessage = outputMessage.Value.ToString(),
                    OrderId = (int)outputOrderId.Value,
                    ExpectedDate = (DateTime)outputExpectedDate.Value,
                    OrderDate = (DateTime)outputOrderDate.Value
                };

                return response;
            }
            finally
            {
                _connect.Close();
            }
        }


    }
}
