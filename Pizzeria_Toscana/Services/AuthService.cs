using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Pizzeria_Toscana.Models;
using Pizzeria_Toscana.Services.Interfaces;
using Pizzeria_Toscana.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using Microsoft.AspNetCore.Authentication;

namespace Pizzeria_Toscana.Services 
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserStore<User> _userStore;
        private readonly IUserEmailStore<User> _emailStore;
        private readonly SignInManager<User> _signInManager;
        private readonly ICosService _cosService;
        private readonly IUserService _userService;

        public AuthService(UserManager<User> userManager, IUserStore<User> userStore, SignInManager<User> signInManager, ICosService cosService, IUserService userService)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _signInManager = signInManager;
            _cosService = cosService;
            _userService = userService;
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterModel.InputModel inputModel)
        {
            var user = new User { UserName = inputModel.Username, Email = inputModel.Email };
            await _userStore.SetUserNameAsync(user, inputModel.Username, CancellationToken.None);

            await _emailStore.SetEmailAsync(user, inputModel.Email, CancellationToken.None);

            return await _userManager.CreateAsync(user, inputModel.Password);
        }

        public async Task<IActionResult> HandleUserRegistrationAsync(string email, string returnUrl)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var cos = _cosService.GetCosByUserId(user.Id);

            if (cos == null)
            {
                // Create a new Cos for the user
                cos = new Cos
                {
                    ID_User = user.Id,
                    Pret_total = 0,
                    CosProdus = new List<Cos_Produs>()
                };

                _cosService.AddCos(cos);
            }
            user.Cos = cos;


            _userService.UpdateUser(user);


            if (_userManager.Options.SignIn.RequireConfirmedAccount)
            {
                return new RedirectToPageResult("RegisterConfirmation", new { email, returnUrl });
            }
            else
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return new LocalRedirectResult(returnUrl);
            }
        }
        public async Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
        {
            return await _signInManager.GetExternalAuthenticationSchemesAsync();
        }


        public async Task<IActionResult> HandleUserLoginAsync(LoginModel.InputModel inputModel, string returnUrl)
        {
            var result = await _signInManager.PasswordSignInAsync(inputModel.Username, inputModel.Password, inputModel.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return new LocalRedirectResult(returnUrl);
            }
            if (result.RequiresTwoFactor)
            {
                return new RedirectToPageResult("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = inputModel.RememberMe });
            }
            if (result.IsLockedOut)
            {
                return new RedirectToPageResult("./Lockout");
            }

            return null;
        }

        private IUserEmailStore<User> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<User>)_userStore;
        }

        private User CreateUser()
        {
            try
            {
                return Activator.CreateInstance<User>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(User)}'. " +
                                                    $"Ensure that '{nameof(User)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                                                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
}
