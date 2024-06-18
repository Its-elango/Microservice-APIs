using CardManagementSystem.Models;
using System.Transactions;

namespace CardManagementSystem.Repository.Interface
{
    public interface ICardTransaction
    {

        #region GET Methods

        Task<IEnumerable<Card>> GetCardById(int cardId);

        Task<IEnumerable<Card>> GetCardByCustomer(int customerId);
        Task<IEnumerable<Transactions>> GetTransactions(int? transactionId=null);
        Task<IEnumerable<Customer>> GetCustomer(int? customerId=null);

        #endregion GET Methods

        #region POST Methods

        Task<string> CreateCard(Card card);
        Task<string> CreateCustomer(Customer customer);
        Task<string> CreateTransaction(Transactions transaction);

        #endregion POST Methods

        #region PUT Methods

        Task<string> UpdateCardStatus(CardStatus cardStatus);
        Task<string> UpdateCustomer(Customer customer);

        #endregion PUT Methods
    }
}
