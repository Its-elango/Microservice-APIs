using CardManagementSystem.Models;
using CardManagementSystem.Repository.Interface;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace CardManagementSystem.Repository
{
    public class CardTransactionRepository :Connection,ICardTransaction
    {
        protected readonly IConfiguration? _configuration;

        public CardTransactionRepository(IConfiguration configuration):base(configuration)
        {
            _configuration = configuration;
        }


        #region POST Methods
        public async Task<string> CreateCard(Card card)
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand("[usp_CreateCard]", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CardType", card.CardType);
                    command.Parameters.AddWithValue("@CustomerId", card.CustomerId);

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

        public async Task<string> CreateCustomer(Customer customer)
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand("[usp_CreateCustomer]", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                    command.Parameters.AddWithValue("@Name", customer.Name);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    command.Parameters.AddWithValue("@Age", customer.Age);
                    command.Parameters.AddWithValue("@Address", customer.Address);
                    command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);

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

        public async Task<string> CreateTransaction(Transactions transaction)
        {

            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand("[usp_CreateTransaction]", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CardId", transaction.CardId);
                    command.Parameters.AddWithValue("@TransactionDate", transaction.TransactionDate);
                    command.Parameters.AddWithValue("@Amount", transaction.Amount);
                    command.Parameters.AddWithValue("@Description", transaction.Description);

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

        #endregion POST Methods

        #region GET Methods
        public async Task<IEnumerable<Card>> GetCardByCustomer(int customerId)
        {
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("[usp_GetCardByCustomer]", SqlConnection))
                {
                    List<Card> cards = new List<Card>();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            cards.Add(
                            new Card
                            {
                                CardId = Convert.ToInt32(reader["CardId"]),
                                CardNumber = Convert.ToString(reader["CardNumber"]),
                                CardType = Convert.ToString(reader["CardType"]),
                                ExpiryDate = Convert.ToDateTime(reader["ExpiryDate"]),
                                Status = Convert.ToString(reader["Status"]),
                                CreatedAt = Convert.ToDateTime(reader["CustomerCreatedAt"]),
                            }
);
                        }
                    }
                    return cards;
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<IEnumerable<Card>> GetCardById(int cardId)
        {
            var cardDict = new Dictionary<int, Card>();

            try
            {
                OpenConnection();
                using (var cmd = new SqlCommand("usp_GetCard", SqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CardId", cardId);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            int currentCardId = reader.GetInt32(reader.GetOrdinal("CardId"));

                            if (!cardDict.TryGetValue(currentCardId, out var card))
                            {
                                card = new Card
                                {
                                    CardId = currentCardId,
                                    CardNumber = reader.GetString(reader.GetOrdinal("CardNumber")),
                                    CardType = reader.GetString(reader.GetOrdinal("CardType")),
                                    ExpiryDate = reader.GetDateTime(reader.GetOrdinal("ExpiryDate")),
                                    Status = reader.GetString(reader.GetOrdinal("Status")),
                                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                                    Transactions = new List<Transactions>()
                                };
                                cardDict[currentCardId] = card;
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("TransactionId")))
                            {
                                var transaction = new Transactions
                                {
                                    TransactionId = reader.GetInt32(reader.GetOrdinal("TransactionId")),
                                    CardId = currentCardId,
                                    Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                                    Description = reader.GetString(reader.GetOrdinal("Description")),
                                    TransactionDate = reader.GetDateTime(reader.GetOrdinal("TransactionDate"))
                                };
                                card.Transactions?.Add(transaction);
                            }
                        }
                    }
                }
            }
            finally
            {
                CloseConnection();
            }

            return cardDict.Values;
        }


        public async Task<IEnumerable<Customer>> GetCustomer(int? customerId = null)
        {
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("[usp_GetCustomer]", SqlConnection))
                {
                    List<Customer> customers = new List<Customer>();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            customers.Add(
                            new Customer
                            {
                                CustomerId = Convert.ToInt32(reader["CustomerId"]),
                                Name = Convert.ToString(reader["Name"]),
                                Email = Convert.ToString(reader["Email"]),
                                Age = Convert.ToInt32(reader["Age"]),
                                Address = Convert.ToString(reader["Address"]),
                                PhoneNumber = Convert.ToString(reader["PhoneNumber"]),
                            }
);
                        }
                    }
                    return customers;
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<IEnumerable<Transactions>> GetTransactions(int? transactionId=null)
        {
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("[usp_GetTransaction]", SqlConnection))
                {
                    List<Transactions> transactions = new List<Transactions>();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TransactionId", transactionId);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            transactions.Add(
                            new Transactions
                            {
                                TransactionId=Convert.ToInt32(reader["TransactionId"]),
                                CardId = Convert.ToInt32(reader["CardId"]),
                                TransactionDate = Convert.ToDateTime(reader["TransactionDate"]),
                                Amount = Convert.ToDecimal(reader["Amount"]),
                                Description = Convert.ToString(reader["Description"])
                            });
                        }
                    }
                    return transactions;
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        #endregion GET Methods

        #region PUT Methods
        public async Task<string> UpdateCardStatus(CardStatus cardStatus)
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand("[usp_UpdateCard]", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CardId", cardStatus.CardId);
                    command.Parameters.AddWithValue("@Status", cardStatus.Status);

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

        public async Task<string> UpdateCustomer(Customer customer)
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand("[usp_UpdateCustomer]", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                    command.Parameters.AddWithValue("@Name", customer.Name);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    command.Parameters.AddWithValue("@Age", customer.Age);
                    command.Parameters.AddWithValue("@Address", customer.Address);
                    command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);

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

        #endregion PUT Methods
    }
}
