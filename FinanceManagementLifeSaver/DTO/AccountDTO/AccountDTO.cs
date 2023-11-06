using FinanceManagementLifesaver.Enums;
namespace FinanceManagementLifesaver.DTO.AccountDTO
{
	public class AccountDTO {
		public int Id { get; set; } 
		public string Name { get; set; }
		public decimal AccountBalance { get; set; }
		public AccountType AccountType { get; set; }
	}
}