using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.Models;
using FluentValidation;

namespace FinanceManagementLifesaver.Validations
{
    public class TransactionValidations : AbstractValidator<TransactionSaveDTO>
    {
        public TransactionValidations()
        {
            RuleSet("Enums", () =>
            {
                RuleFor(t => t.TransactionType).IsInEnum().WithMessage("Keine richtiger Transaktionstyp");
            });

            RuleSet("Dates", () =>
            {
                RuleFor(t => t.Date).NotNull().WithMessage("Kein Datum angegeben");
            });
            RuleSet("Description", () =>
            {
                RuleFor(t => t.Description).MaximumLength(300).WithMessage("Beschreibung kann maximal 300 Zeichen lang sein");
            });
            RuleSet("Money", () =>
            {
                RuleFor(t => t.Amount).ExclusiveBetween(0, 999999999).WithMessage("Muss eine angemessene Zahl sein");
            });
            RuleSet("Accounts", () =>
            {
                RuleFor(t => t.AccountId).NotNull().WithMessage("Jede Transaktion braucht einen dazugehörigen Account");
            });
        }
    }
}
