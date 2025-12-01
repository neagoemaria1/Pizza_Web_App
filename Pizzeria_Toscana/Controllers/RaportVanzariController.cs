using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pizzeria_Toscana.Models;
using Pizzeria_Toscana.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Pizzeria_Toscana.Controllers
{
    [Authorize(Roles = "Admin")] // Doar adminii pot accesa raportul
    public class RaportVanzariController : Controller
    {
        private readonly IComandaService _comandaService;
        private readonly IComanda_ProdusService _comandaProdusService;
        private readonly IProdusService _produsService;

        public RaportVanzariController(
            IComandaService comandaService,
            IComanda_ProdusService comandaProdusService,
            IProdusService produsService)
        {
            _comandaService = comandaService;
            _comandaProdusService = comandaProdusService;
            _produsService = produsService;
        }

        [HttpGet]
        [Route("RaportVanzari")]
        public IActionResult Index()
        {
            // Obține toate produsele din toate comenzile
            var comenziProduse = _comandaProdusService.GetAllComenziProduse();
            var produseVandute = comenziProduse
                .GroupBy(cp => cp.ProdusId)
                .Select(grup =>
                {
                    var produs = _produsService.GetProdusByCod(grup.Key);
                    var pret = produs?.Pret ?? 0;
                    var cantitateTotala = grup.Sum(cp => cp.Cantitate);
                    var sumaTotala = cantitateTotala * pret; // Calculează venitul total

                    return new
                    {
                        COD_Produs = produs.COD_Produs,
                        Denumire = produs?.Denumire ?? "Produs Necunoscut",
                        Pret = pret,
                        Cantitate = cantitateTotala,
                        SumaTotala = sumaTotala, // Trimite suma calculata
                        Imagine = produs?.ProdusPicture != null
                            ? "data:image/jpeg;base64," + Convert.ToBase64String(produs.ProdusPicture)
                            : "/imagini/default.png"
                    };
                })
                .OrderByDescending(p => p.Cantitate) 
                .ToList();

            ViewBag.ProduseVandute = produseVandute;

            return View();
        }

    }
}
