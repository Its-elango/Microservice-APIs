namespace CardManagementSystem.Models
{
    public class Card
    {
        public int? CardId { get; set; }
        public string? CardNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? CardType { get; set; }
        public string? Status { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public List<Transactions>? Transactions { get; set; } 
    }

    public class Transactions
    {
        public int TransactionId { get; set; }
        public int CardId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
    }

    public class CardStatus
    {
        public int CardId { get; set; }
        public string? Status { get; set; }
    }
}
