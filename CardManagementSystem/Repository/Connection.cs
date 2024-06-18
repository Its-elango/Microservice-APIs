using System.Data.SqlClient;

namespace CardManagementSystem.Repository
{
    public class Connection
    {
        protected SqlConnection SqlConnection { get; private set; }
        private readonly IConfiguration _configuration;

        public Connection(IConfiguration configuration)
        {
            _configuration = configuration;
            string connectionString = _configuration.GetConnectionString("SQLConnection") ?? "";
            SqlConnection = new SqlConnection(connectionString);
        }
        public void OpenConnection()
        {
            if (SqlConnection.State != System.Data.ConnectionState.Open)
            {
                SqlConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (SqlConnection.State != System.Data.ConnectionState.Closed)
            {
                SqlConnection.Close();
            }
        }
    }
}
