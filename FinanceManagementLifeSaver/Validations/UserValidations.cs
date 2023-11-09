using FinanceManagementLifesaver.Data;
using FinanceManagementLifesaver.Models;
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
    public class UserValidations : AbstractValidator<User>
    {
        public UserValidations()
        {
            RuleSet("Names", () =>
            {
                RuleFor(u => u.FirstName).NotEmpty().WithMessage("Vorname wird benötigt").
                    NotEqual(u => u.LastName).WithMessage("Vor- und Nachname müssen sich unterscheiden").
                    Length(2,25).WithMessage("Bitte gebe einen richtigen Vornamen ein");

                RuleFor(u => u.LastName).NotEmpty().WithMessage("Nachname wird benötigt").
                    Length(2,25).WithMessage("Bitte gebe einen richtigen Nachnamen ein");
                ;
            });

            RuleSet("Credentials", () =>
            {
            RuleFor(u => u.Email).EmailAddress().WithMessage("Email benötigt '@' Zeichen").
                NotEmpty().WithMessage("Email wird benötigt").
                Must(BeAValidEmail).WithMessage("Ungültige Email").
                Length(5,60).WithMessage("Email muss zwischen 5 und 60 Zeichen lang sein");

            RuleFor(u => u.Password).NotEmpty().WithMessage("Passwort wird benötigt").
                Length(3, 30).WithMessage("Passwort muss zwischen 3 und 30 Zeichen beinhalten");
            });
        }

        //FluentValidation hat zwar einen EmailValidator, allerdings überprüft dieser nur nach einem @ Zeichen
        private bool BeAValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<bool> CheckIfEmailTaken(DataContext _context, string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                return true; 
            }
            return false;
        }
    }
}
