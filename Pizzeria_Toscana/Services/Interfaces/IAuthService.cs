
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pizzeria_Toscana.Areas.Identity.Pages.Account;

namespace Pizzeria_Toscana.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterModel.InputModel inputModel);
        Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync(); 
        Task<IActionResult> HandleUserRegistrationAsync(string email, string returnUrl);
        Task<IActionResult> HandleUserLoginAsync(LoginModel.InputModel inputModel, string returnUrl);
    }
}