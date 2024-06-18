using Logistics_Management_System.Models;
using LogisticsManagementSystem.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Logistics_Management_System.Respository
{
    public class LogisticsRepository :Connection, ILogistics
    {
        protected readonly IConfiguration? _configuration;

        public LogisticsRepository(IConfiguration configuration):base(configuration) 
        {
           
        }

        #region GET Method Repository

        public async Task<string> CheckOrderStatus(int orderId)
        {
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("[usp_CheckOrderStatus]", SqlConnection))
                {
                    List<Shipment> shipments = new List<Shipment>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return reader["Message"].ToString() ?? "Unknown status";
                        }
                    }
                }
                return "Unknown status";
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<IEnumerable<Order>> GetOrder(int orderId)
        {
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("[usp_GetOrderDetailsByID]", SqlConnection))
                {
                    List<Order> order = new List<Order>();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            order.Add(
                            new Order
                            {
                                OrderId = Convert.ToInt32(reader["OrderId"]),
                                Product = new Product
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    ProductName = Convert.ToString(reader["ProductName"]),
                                    ProductDescription = Convert.ToString(reader["ProductDescription"])
                                },
                                OrderQuantity = Convert.ToInt32(reader["OrderQuantity"]),
                                ExpectedDate = Convert.ToDateTime(reader["ExpectedDate"]),
                                OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                                DeliveryAddress = Convert.ToString(reader["DeliveryAddress"]),
                                //Customer = new Customer
                                //{
                                //    CustomerName = Convert.ToString(reader["CustomerName"]),
                                //    CustomerContact = Convert.ToString(reader["CustomerContact"])
                                //}
                            });
                        }
                    }
                    return order;
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("[usp_GetOrderDetails]", SqlConnection))
                {
                    List<Order> orders = new List<Order>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            orders.Add(
                            new Order
                            {
                                OrderId = Convert.ToInt32(reader["OrderId"]),
                                Product = new Product
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    ProductName = Convert.ToString(reader["ProductName"]),
                                    ProductDescription = Convert.ToString(reader["ProductDescription"])
                                },
                                OrderQuantity = Convert.ToInt32(reader["OrderQuantity"]),
                                ExpectedDate = Convert.ToDateTime(reader["ExpectedDate"]),
                                OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                                DeliveryAddress = Convert.ToString(reader["DeliveryAddress"]),
                                //Customer = new Customer
                                //{
                                //    CustomerName = Convert.ToString(reader["CustomerName"]),
                                //    CustomerContact = Convert.ToString(reader["CustomerContact"])
                                //}
                            });
                        }
                    }
                    return orders;
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<IEnumerable<Shipment>> GetShipment(int shipmentId)
        {
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("[usp_GetShipmentByID]", SqlConnection))
                {
                    List<Shipment> shipment = new List<Shipment>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ShipmentID", shipmentId);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            shipment.Add(
                            new Shipment
                            {
                                OrderId = Convert.ToInt32(reader["OrderId"]),
                                ShipmentId = Convert.ToInt32(reader["ShipmentId"]),
                                ShipperId = Convert.ToInt32(reader["ShipperId"]),
                                DeliveryDate = Convert.ToDateTime(reader["DeliveryDate"]),
                                Comments = Convert.ToString(reader["Comments"]),
                                HasDelivered = Convert.ToBoolean(reader["HasDelivered"])
                            });
                        }
                    }
                    return shipment;
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<IEnumerable<Shipment>> GetShipments()
        {
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("[usp_GetShipments]", SqlConnection))
                {
                    List<Shipment> shipments = new List<Shipment>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            shipments.Add(
                            new Shipment
                            {
                                OrderId = Convert.ToInt32(reader["OrderId"]),
                                ShipmentId = Convert.ToInt32(reader["ShipmentId"]),
                                ShipperId = Convert.ToInt32(reader["ShipperId"]),
                                DeliveryDate = Convert.ToDateTime(reader["DeliveryDate"]),
                                Comments = Convert.ToString(reader["Comments"]),
                                HasDelivered = Convert.ToBoolean(reader["HasDelivered"])
                            });
                        }
                    }
                    return shipments;
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<IEnumerable<Shipper>> GetShipper(int shipperId)
        {
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("[usp_GetShipperByID]", SqlConnection))
                {
                    List<Shipper> shipper = new List<Shipper>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ShipperId", shipperId);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            shipper.Add(
                            new Shipper
                            {
                                ShipperId = Convert.ToInt32(reader["ShipperId"]),
                                ShipperName = Convert.ToString(reader["ShipperName"]),
                                ShipperContact = Convert.ToString(reader["ShipperContact"]),
                                ShipperAddress = Convert.ToString(reader["ShipperAddress"]),
                                WorkLocation = Convert.ToString(reader["WorkLocation"]),
                                JoinDate = Convert.ToDateTime(reader["JoinDate"]),
                                Email = Convert.ToString(reader["Email"])

                            });
                        }
                    }
                    return shipper;
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<IEnumerable<Order>> GetShipperOrders(int shipperId)
        {
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("[usp_GetOrdersWithShippers]", SqlConnection))
                {
                    List<Order> orders = new List<Order>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ShipperId", shipperId);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            orders.Add(
                            new Order
                            {
                                OrderId = Convert.ToInt32(reader["OrderId"]),
                                Product = new Product
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    ProductName = Convert.ToString(reader["ProductName"]),
                                    ProductDescription = Convert.ToString(reader["ProductDescription"])
                                },
                                OrderQuantity = Convert.ToInt32(reader["OrderQuantity"]),
                                ExpectedDate = Convert.ToDateTime(reader["ExpectedDate"]),
                                OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                                DeliveryAddress = Convert.ToString(reader["DeliveryAddress"]),
                                Customer = new Customer
                                {
                                    CustomerName = Convert.ToString(reader["CustomerName"]),
                                    CustomerContact = Convert.ToString(reader["CustomerContact"])
                                }
                            });
                        }
                    }
                    return orders;
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<IEnumerable<Shipper>> GetShippers()
        {
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("[usp_GetShippers]", SqlConnection))
                {
                    List<Shipper> shippers = new List<Shipper>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            shippers.Add(
                            new Shipper
                            {
                                ShipperId = Convert.ToInt32(reader["ShipperId"]),
                                ShipperName = Convert.ToString(reader["ShipperName"]),
                                ShipperContact = Convert.ToString(reader["ShipperContact"]),
                                ShipperAddress = Convert.ToString(reader["ShipperAddress"]),
                                WorkLocation = Convert.ToString(reader["WorkLocation"]),
                                JoinDate = Convert.ToDateTime(reader["JoinDate"]),
                                Email = Convert.ToString(reader["Email"])

                            });
                        }
                    }
                    return shippers;
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        #endregion GET Methods Repository

        #region POST Method Repository

        public async Task<string> AddOrder(Order order)
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand("usp_InsertOrder", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@OrderID", order.OrderId);
                    command.Parameters.AddWithValue("@ProductID", order.Product?.ProductId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ProductName", order.Product?.ProductName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ProductDescription", order.Product?.ProductDescription ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@OrderQuantity", order.OrderQuantity);
                    command.Parameters.AddWithValue("@ExpectedDate", order.ExpectedDate);
                    command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    command.Parameters.AddWithValue("@DeliveryAddress", order.DeliveryAddress);
                    command.Parameters.AddWithValue("@SellerID", order.Seller?.SellerId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SellerName", order.Seller?.SellerName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SellerContact", order.Seller?.SellerContact ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SellerAddress", order.Seller?.SellerAddress ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CustomerID", order.Customer?.CustomerId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CustomerName", order.Customer?.CustomerName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CustomerContact", order.Customer?.CustomerContact ?? (object)DBNull.Value);

                    var outputMessage = new SqlParameter("@Message", SqlDbType.NVarChar, 250)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputMessage);

                    await command.ExecuteNonQueryAsync();

                    return outputMessage.Value.ToString()??"";
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<string> AddShipper(Shipper shipper)
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand("[usp_InsertShippers]", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ShipperName", shipper.ShipperName);
                    command.Parameters.AddWithValue("@ShipperContact", shipper.ShipperContact);
                    command.Parameters.AddWithValue("@ShipperAddress", shipper.ShipperAddress);
                    command.Parameters.AddWithValue("@WorkLocation", shipper.WorkLocation);
                    command.Parameters.AddWithValue("@JoinDate", shipper.JoinDate);
                    command.Parameters.AddWithValue("@Email", shipper.Email);
                    command.Parameters.AddWithValue("@Password", shipper.Password);

                    var outputMessage = new SqlParameter("@Message", SqlDbType.NVarChar, 250)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputMessage);

                    await command.ExecuteNonQueryAsync();

                    return outputMessage.Value.ToString()??"Some thing went wrong";
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<LoginResponse> Login(User user)
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand("[usp_ValidateUser]", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Email", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return new LoginResponse
                            {
                                Role = reader["Role"].ToString(),
                                UserId = (int)reader["UserID"],
                                Message = reader["Message"].ToString()
                            };
                        }
                        else
                        {                        
                            return new LoginResponse
                            {
                                Message = "Login unsuccessful"
                            };
                        }
                    }
                }
            }
            finally
            {
                CloseConnection();
            }
        }



        #endregion POST Method Repository

        #region PUT Method Repository
        public async Task<string> ApproveShipper(ShipperApprovel shipperApprovel)
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand("[usp_ApproveShipper]", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ShipperID", shipperApprovel.ShipperId);
                    command.Parameters.AddWithValue("@AdminApproved", shipperApprovel.IsShipperApproved);

                    var outputMessage = new SqlParameter("@Message", SqlDbType.NVarChar, 250)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputMessage);

                    await command.ExecuteNonQueryAsync();

                    return outputMessage.Value.ToString()??"";
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<string> TakeOrder(int shipperId, int orderId)
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand("[usp_UpdateShipperInOrders]", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ShipperID", shipperId);
                    command.Parameters.AddWithValue("@OrderID", orderId);

                    var outputMessage = new SqlParameter("@Message", SqlDbType.NVarChar, 250)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputMessage);

                    await command.ExecuteNonQueryAsync();

                    return outputMessage.Value.ToString() ?? "" ;
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<string> UpdateDeliveryStatus(UpdateDeliveryStatus deliveryStatus)
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand("[usp_UpdateDeliveryStatus]", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@OrderID", deliveryStatus.OrderId);
                    command.Parameters.AddWithValue("@Comments", deliveryStatus.Comments);
                    command.Parameters.AddWithValue("@DeliveryDate", deliveryStatus.DeliveryDate);
                   
                    var outputMessage = new SqlParameter("@Message", SqlDbType.NVarChar, 250)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputMessage);

                    await command.ExecuteNonQueryAsync();

                    return outputMessage.Value.ToString() ?? "";
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<string> UpdateShipment(Shipment shipment)
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand("[usp_UpdateShipment]", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ShipperID", shipment.ShipperId);
                    command.Parameters.AddWithValue("@ShipmentID", shipment.ShipmentId);
                    command.Parameters.AddWithValue("@HasDelivered", shipment.HasDelivered);
                    command.Parameters.AddWithValue("@Comments", shipment.Comments);
                    command.Parameters.AddWithValue("@DeliveryDate", shipment.DeliveryDate);
                    command.Parameters.AddWithValue("@OrderID", shipment.OrderId);

                    var outputMessage = new SqlParameter("@Message", SqlDbType.NVarChar, 250)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputMessage);

                    await command.ExecuteNonQueryAsync();

                    return outputMessage.Value.ToString() ?? "";
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<string> UpdateShipper(Shipper shipper)
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand("usp_UpdateShipper", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ShipperID", shipper.ShipperId);
                    command.Parameters.AddWithValue("@ShipperName", shipper.ShipperName);
                    command.Parameters.AddWithValue("@ShipperContact", shipper.ShipperContact);
                    command.Parameters.AddWithValue("@ShipperAddress", shipper.ShipperAddress);
                    command.Parameters.AddWithValue("@WorkLocation", shipper.WorkLocation);
                    command.Parameters.AddWithValue("@JoinDate", shipper.JoinDate);

                    var outputMessage = new SqlParameter("@Message", SqlDbType.NVarChar, 250)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputMessage);

                    await command.ExecuteNonQueryAsync();

                    return outputMessage.Value.ToString() ?? "";
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        #endregion PUT Method Repository
    }
}
