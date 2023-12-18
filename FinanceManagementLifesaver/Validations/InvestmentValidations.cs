using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.Models.BaseClass;
using FluentValidation;

namespace FinanceManagementLifesaver.Validations
{
    public class InvestmentValidations : AbstractValidator<BaseClassInvestment>
    {
        public InvestmentValidations() {
            RuleSet("Enums", () =>
            {
                RuleFor(i => i.InvestmentType).IsInEnum().WithMessage("Kein richtiger Investitionstyp");
            });

            RuleSet("Dates", () =>
            {
                RuleFor(i => i.StartDate).NotNull().WithMessage("Kein Datum angegeben");
            });
            RuleSet("Money", () =>
            {
                RuleFor(i => i.StartMoney).ExclusiveBetween(0, 999999999).WithMessage("Muss eine angemessene Zahl sein");
                RuleFor(i => i.InvestedMoney).ExclusiveBetween(0, 999999999).WithMessage("Muss eine angemessene Zahl sein");
            });
            RuleSet("Account", () =>
            {
                RuleFor(i => i.Account.Id).NotNull().WithMessage("Jede Transaktion braucht einen dazugehörigen Account");
            });
        }
        public static bool IsTimeFrameValid(int timeframe)
        {
            if (timeframe >= 37 || timeframe < 0) {
                return false;
            }
            return true;
        }
    }
}
