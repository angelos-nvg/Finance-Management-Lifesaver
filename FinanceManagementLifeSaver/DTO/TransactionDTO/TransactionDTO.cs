using FinananceManagementLifesaver.Enums
namespace FinanaceManagementLifesaver.DTO
{
    public class TransactionDTO
    {
        public int Id;
        public decimal Amount;
        public TransactionType TransactionType;
        public string Date;
        public string Description;
    }
}