using iText.Commons.Actions.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzeria_Toscana.Models;
using Pizzeria_Toscana.Services.Interfaces;
using Stripe.Checkout;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria_Toscana.Controllers
{
    [Authorize(Roles = "Admin,Basic")]
    public class CosController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICosService _cosService;
        private readonly ICos_ProdusService _cosProdusService;
        private readonly IProdusService _produsService;
        private readonly IComandaService _comandaService;
        private readonly IComanda_ProdusService _comandaProdusService;

        public CosController(IUserService userService, ICosService cosService, ICos_ProdusService cosProdusService, IProdusService produsService, IComandaService comandaService, IComanda_ProdusService comandaProdusService)
        {
            _userService = userService;
            _cosService = cosService;
            _cosProdusService = cosProdusService;
            _produsService = produsService;
            _comandaService = comandaService;
            _comandaProdusService = comandaProdusService;
        }

        public IActionResult Index()
        {
            var user = _userService.GetCurrentUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = user.Id;
            var cos = _cosService.GetCosByUserId(userId);

            if (cos == null)
            {
                cos = new Cos
                {
                    ID_User = userId,
                    Pret_total = 0,
                    CosProdus = new List<Cos_Produs>()
                };
                _cosService.AddCos(cos);
            }
            var cosProduse = _cosProdusService.GetAllCosProduseByCosId(cos.ID_Cos);

            var model = new Cos
            {
                ID_Cos = cos.ID_Cos,
                Pret_total = cos.Pret_total,
                ID_User = cos.ID_User,
                // Mapam produsele din cos
                CosProdus = cosProduse.Select(cp => new Cos_Produs
                {
                    ID_CosProdus = cp.ID_CosProdus,
                    Cantitate = cp.Cantitate,
                    Pret = cp.Pret,
                    ID_Cos = cp.ID_Cos,
                    COD_Produs = cp.COD_Produs,
                    Produs = _produsService.GetProdusByCod(cp.COD_Produs)
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string codProdus)
        {
            var user = _userService.GetCurrentUser();
            if (user == null) return RedirectToAction("Login", "Account");

            var cos = _cosService.GetCosByUserId(user.Id);
            if (cos != null)
            {
                var cosProdus = _cosProdusService.GetCosProdusByCosIdAndProductId(cos.ID_Cos, codProdus);
                if (cosProdus != null)
                {
                    await _cosProdusService.RemoveCosProdusAsync(cosProdus);
                    cos.Pret_total -= cosProdus.Pret * cosProdus.Cantitate;
                    if (cos.Pret_total < 0)
                    {
                        cos.Pret_total = 0;
                    }
                    _cosService.UpdateCos(cos);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddMore()
        {
            return RedirectToAction("Index", "Meniu");
        }

        [HttpPost]
        public async Task<IActionResult> SubmitOrder()
        {
            var user = _userService.GetCurrentUser();
            if (user == null)
                return RedirectToAction("Login", "Account");

            var cos = _cosService.GetCosByUserId(user.Id);
            var cosProduse = _cosProdusService.GetAllCosProduseByCosId(cos.ID_Cos);
            if (cos == null || cosProduse == null || !cosProduse.Any())
            {
                TempData["Error"] = "Cosul este gol!";
                return RedirectToAction(nameof(Index));
            }

            try
            {

                var comanda = new Comanda
                {
                    Descriere = "Comanda creata cu succes",
                    Status = true, // considerăm comanda confirmata
                    Suma = cos.Pret_total,
                    User = user,
                    CosID_Cos = cos.ID_Cos,
                    OrderDate = DateTime.Now,
                };
                _comandaService.AddComanda(comanda);

                foreach (var item in cosProduse)
                {
                    var comandaProdus = new Comanda_Produs
                    {
                        ComandaId = comanda.NR_Comanda,
                        ProdusId = item.COD_Produs,
                        Cantitate = item.Cantitate

                    };
                    _comandaProdusService.AddComandaProdus(comandaProdus);
                }

                // Golirea coșului după crearea comenzii
                await _cosProdusService.ClearCosProdusesForCosAsync(cos.ID_Cos);
                cos.Pret_total = 0;
                _cosService.UpdateCos(cos);

                return View("Success");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"A apărut o eroare la crearea comenzii: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }


        // Confirmarea comenzii 
        public async Task<IActionResult> Success()
        {
            var user = _userService.GetCurrentUser();
            if (user == null) return RedirectToAction("Login", "Account");

            var cos = _cosService.GetCosByUserId(user.Id);
            var cosProduse = _cosProdusService.GetAllCosProduseByCosId(cos.ID_Cos);

            // Creare comanda dupa succesul platii
            var comanda = new Comanda
            {
                Descriere = "Comanda plasata!",
                Status = true,
                Suma = cos.Pret_total,
                User = user,
                CosID_Cos = cos.ID_Cos,
                OrderDate = DateTime.Now,


            };
            _comandaService.AddComanda(comanda);

            foreach (var item in cosProduse)
            {
                var comandaProdus = new Comanda_Produs
                {
                    ComandaId = comanda.NR_Comanda,
                    ProdusId = item.COD_Produs,
                    Cantitate = item.Cantitate

                    
                };
                _comandaProdusService.AddComandaProdus(comandaProdus);
            }

            // Golirea cosului 
            await _cosProdusService.ClearCosProdusesForCosAsync(cos.ID_Cos);
            cos.Pret_total = 0;
            _cosService.UpdateCos(cos);

            return View("Success");
        }


        //Metoda pentru actualizarea pretului si a cantitatii unui produs
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(string codProdus, string actionType)
        {
            var user = _userService.GetCurrentUser();
            if (user == null)
                return RedirectToAction("Login", "Account");

            var cos = _cosService.GetCosByUserId(user.Id);
            if (cos != null)
            {
                // Ne asiguram ca lista de produse din cos nu este null
                cos.CosProdus ??= new List<Cos_Produs>();
                var cosProdus = _cosProdusService.GetCosProdusByCosIdAndProductId(cos.ID_Cos, codProdus);
                if (cosProdus != null)
                {
                    // Logica pentru + și -
                    if (actionType == "increase")
                    {
                        cosProdus.Cantitate += 1;
                        cos.Pret_total += cosProdus.Pret;
                    }
                    else if (actionType == "decrease" && cosProdus.Cantitate > 1)
                    {
                        cosProdus.Cantitate -= 1;
                        cos.Pret_total -= cosProdus.Pret;
                    }
                    _cosProdusService.UpdateCosProdus(cosProdus);


                    _cosService.UpdateCos(cos);
                }
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
