using CardManagementSystem.Models;
using CardManagementSystem.Repository.Interface;
using CardManagementSystem.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Transactions;

namespace CardManagementSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardTransactionController : ControllerBase
    {
        private readonly ICardTransaction _cardTransaction;
        private readonly EmailService _emailService;
        private readonly InvokeApi _invokeApi;
        private readonly IConfiguration _configuration;

        public CardTransactionController(ICardTransaction cardTransaction, EmailService emailService, IConfiguration configuration,InvokeApi invokeApi)
        {
            _invokeApi = invokeApi;
            _cardTransaction = cardTransaction;
            _emailService = emailService;
            _configuration = configuration;
        }



        #region GET Methods

        [HttpGet("GetCardById")]
        public async Task<IActionResult> GetCardById([FromQuery] int cardId)
        {
            return Ok(await _cardTransaction.GetCardById(cardId));
        }

        [HttpGet("GetCardByCustomer")]
        public async Task<IActionResult> GetCardByCustomer([FromQuery] int CustomerId)
        {
            return Ok(await _cardTransaction.GetCardByCustomer(CustomerId));
        }

        [HttpGet("GetTransactions")]
        public async Task<IActionResult> GetTransactions([FromQuery]int? transactionId=null)
        {
            return Ok(await _cardTransaction.GetTransactions(transactionId));
        }
        [HttpGet("GetCustomer")]
        public async Task<IActionResult> GetCustomer([FromQuery] int? CustomerId = null)
        {
            return Ok(await _cardTransaction.GetCustomer(CustomerId));
        }



        #endregion GET Methods

        #region POST Methods

        [HttpPost("CreateCard")]
        public async Task<IActionResult> CreateCard([FromBody] Card card)
        {

            string path = "UpdateCardDetails";
            string app = "Admin";
            var baseUrl = _configuration["Client:ClientBaseUrl"]??"";
            var result=await _cardTransaction.CreateCard(card);

            if(result== "Card created successfully.")
            {
                var cardValuesCollection = await _cardTransaction.GetCardByCustomer(card.CustomerId.Value);

                foreach (var cardValue in cardValuesCollection)
                {
                    var cardDetails = new
                    {
                        CustomerId = card.CustomerId.Value,
                        CardType = cardValue.CardType,
                        CardNumber = cardValue.CardNumber,
                        CardExpiryDate = cardValue.ExpiryDate // Assuming cardValue has ExpiryDate property
                    };

                   var client = await _invokeApi.SendToClient(baseUrl,app,path,cardDetails);
                }
                //await _emailService.SendMail(card, "Your Card Has Been Successfully Generated!");
                return Ok("Card created successfully");
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            return Ok(await _cardTransaction.CreateCustomer(customer));

        }

        [HttpPost("CreateTransaction")]
        public async Task<IActionResult> CreateTransaction([FromBody] Transactions transaction)
        {
            return Ok(await _cardTransaction.CreateTransaction(transaction));
        }

        #endregion POST Methods

        #region PUT Methods

        [HttpPut("UpdateCardStatus")]
        public async Task<IActionResult> UpdateCardStatus([FromBody] CardStatus cardStatus)
        {
            return Ok(await _cardTransaction.UpdateCardStatus(cardStatus));
        }

        [HttpPut("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
        {
            return Ok(await _cardTransaction.UpdateCustomer(customer));
        }

        #endregion PUT Methods
    }
}
