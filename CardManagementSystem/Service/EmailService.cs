using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Net;
using CardManagementSystem.Repository;
using CardManagementSystem.Models;
using System.Reflection;

namespace CardManagementSystem.Service
{
    public class EmailService:Connection
    {
        protected readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }
        public async Task SendMail<T>(T model, string emailSubject)
        {

            List<Customer> recipients = await GetRecipients(model);

            string emailPassKey = _configuration["Email:PassKey"] ?? "";
            string emailServerId = _configuration["Email:ServerId"] ?? "";
            string senderEmailAddress = _configuration["Email:EmailId"] ?? "";
            string subject = emailSubject;
            string emailBody = Emailbody(model);

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(emailServerId, emailPassKey)
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmailAddress),
                Subject = subject,
                Body = emailBody,
                IsBodyHtml = true,
            };

            foreach (var email in recipients)
            {
                if (!string.IsNullOrEmpty(email?.Email))
                {
                    mailMessage.To.Add(email.Email);
                }
            }
            await client.SendMailAsync(mailMessage);
        }


        private string Emailbody<T>(T Model)
        {
            string body = $@"
                        <h2 style=""text-align: center; color: #007bff;"">Card Successfully Generated</h2>
    
    <p>Dear Customer,</p>

    <p>Congratulations! Your card has been successfully generated. You will receive further details about dispatch from your bank.</p>
    
    <p>If you have any questions or concerns, feel free to contact us.</p>
    
    <p>Thank you,</p>
    <p>ABC Card</p>";
            return body;
        }


        private async Task<List<Customer>> GetRecipients<T>(T Model)
        {
            try
            {
                var customerIdProperty = typeof(T).GetProperty("CustomerId");

                if (customerIdProperty == null)
                {
                    throw new ArgumentException("Model does not contain a property named 'CustomerId'.");
                }

                var customerIdValue = customerIdProperty.GetValue(Model);

                if (customerIdValue == null || customerIdValue == DBNull.Value)
                {
                    throw new ArgumentException("CustomerId property value is null.");
                }

                int customerId = (int)customerIdValue;

                OpenConnection();

                using (SqlCommand cmd = new SqlCommand("usp_GetCustomer", SqlConnection))
                {
                    List<Customer> recipients = new List<Customer>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            recipients.Add(new Customer
                            {
                                Email = Convert.ToString(reader["Email"]),
                            });
                        }
                    }
                    return recipients;
                }
            }
            finally
            {
                CloseConnection();
            }
        }

    }
}
