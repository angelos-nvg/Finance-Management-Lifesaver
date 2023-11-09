using FinanceManagementLifesaver.Enums;

namespace FinanceManagementLifesaver.DTO
{
    public class AccountUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal AccountBalance { get; set; }
        public AccountType AccountType { get; set; }
        public int UserId { get; set; }
    }
}