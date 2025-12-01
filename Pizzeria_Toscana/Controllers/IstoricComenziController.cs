using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pizzeria_Toscana.Models;
using Pizzeria_Toscana.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Pizzeria_Toscana.Controllers
{
    [Authorize(Roles = "Admin,Basic")]
    public class IstoricComenziController : Controller
    {
        private readonly IComandaService _comandaService;
        private readonly IComanda_ProdusService _comandaProdusService;
        private readonly IProdusService _produsService;
        private readonly IUserService _userService;

        public IstoricComenziController(
            IComandaService comandaService,
            IComanda_ProdusService comandaProdusService,
            IProdusService produsService,
            IUserService userService)
        {
            _comandaService = comandaService;
            _comandaProdusService = comandaProdusService;
            _produsService = produsService;
            _userService = userService;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            var user = _userService.GetCurrentUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = user.Id;

            // Obține comenzile utilizatorului
            var comenzi = _comandaService.GetAllComenzi()
                .Where(c => c.UserId == userId) //de sters pt comenzi
                .OrderByDescending(c => c.OrderDate)
                .ToList();

            // Cream o listă de produse pentru a fi transmise prin ViewBag
            var produseList = new List<string>();

            foreach (var comanda in comenzi)
            {
                // Obținem produsele asociate comenzii
                var comandaProduse = _comandaProdusService.GetAllComenziProduse()
                    .Where(cp => cp.ComandaId == comanda.NR_Comanda)
                    .ToList();

                foreach (var cp in comandaProduse)
                {
                    var produs = _produsService.GetProdusByCod(cp.ProdusId);
                    if (produs != null)
                    {
                        produseList.Add($"Comanda {comanda.NR_Comanda}: {produs.Denumire} - {cp.Cantitate} buc.");
                    }
                }
            }

            // Transmitem lista de produse prin ViewBag
            ViewBag.ProduseComenzi = produseList;

            return View(comenzi);
        }
    }
}
