using System.Data;
using System;
using System.Data.SqlClient;
using E_Commerce.Model;

namespace E_Commerce.Repository
{
    public class loginRepository
    {
        private IConfiguration _configuration;
        private SqlConnection _connect;

        public loginRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection();
        }
        public void Connection()
        {
            string connect = _configuration.GetConnectionString("connect");
            _connect = new SqlConnection(connect);
        }

        public async Task<bool> VerifySignIn(loginModel login)
        {
            try
            {
                Connection();
                SqlCommand command = new SqlCommand("SP_VaildUser", _connect);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", login.Username);
                command.Parameters.AddWithValue("@Password", login.Password);
                _connect.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
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
