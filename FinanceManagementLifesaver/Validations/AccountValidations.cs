using FinanceManagementLifesaver.Data;
using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.ServiceResponse;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Validations
{
    public class AccountValidation : AbstractValidator<AccountSaveDTO>
    {
        public AccountValidation()
        {
            RuleFor(a => a.Name).NotEmpty().WithMessage("Accountname wird benötigt").
                Length(2, 25).WithMessage("Bitte gebe einen richtigen Namen ein");

            RuleFor(a => a.AccountType).NotEmpty().WithMessage("Accountart wird benötigt").
                IsInEnum().WithMessage("Keine richtige Accountart");

            RuleFor(a => a.AccountBalance).NotEmpty().WithMessage("Wert darf nicht leer sein");
        }
    }
}
