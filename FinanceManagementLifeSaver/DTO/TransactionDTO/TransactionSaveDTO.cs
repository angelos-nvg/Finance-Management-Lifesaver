using FinanceManagementLifesaver.Enums;

namespace FinanceManagementLifesaver.DTO
{
    public class TransactionSaveDTO
    {
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Date {  get; set; }
        public string Description { get; set; }
    }
}