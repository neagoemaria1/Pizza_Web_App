using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pizzeria_Toscana.Models;
using Pizzeria_Toscana.Services;
using Pizzeria_Toscana.Services.Interfaces;
using System.Linq;

namespace Pizzeria_Toscana.Controllers
{
    [Authorize(Roles = "Admin,Basic")]
    public class ProdusController : Controller
    {
        private readonly IProdusService _produsService;
        private readonly IUserService _userService;
        private readonly ICosService _cosService;
        private readonly ICos_ProdusService _cosProdusService;
        private readonly IPdfService _pdfService;

        public ProdusController(IProdusService produsService, IUserService userService, ICosService cosService, ICos_ProdusService cosProdusService, IPdfService pdfService)
        {
            _produsService = produsService;
            _userService = userService;
            _cosService = cosService;
            _cosProdusService = cosProdusService;
            _pdfService = pdfService;
        }
        public IActionResult Index()
        {
            return View();
        }

        // Metoda pentru adaugarea in cos
        [HttpPost]
        public IActionResult AdaugaInCos(string productId)
        {
            var user = _userService.GetCurrentUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            // Obtinem cosul utilizatorului pe baza ID-ului
            var cos = _cosService.GetCosByUserId(user.Id);
            if (cos == null)
            {
                cos = new Cos
                {
                    ID_User = user.Id,
                    Pret_total = 0,
                    CosProdus = new List<Cos_Produs>()
                };
                _cosService.AddCos(cos);
            }

            var produs = _produsService.GetProdusWithIngredientsByCod(productId);
            if (produs == null)
            {
                return RedirectToAction("Index");
            }
            // Verificam daca produsul este deja in cos
            var cosProdusExist = _cosProdusService.GetCosProdusByCosIdAndProductId(cos.ID_Cos, productId);
            if (cosProdusExist != null)
            {
                cosProdusExist.Cantitate++;
                _cosProdusService.UpdateCosProdus(cosProdusExist);

                cos.Pret_total += cosProdusExist.Pret;
                _cosService.UpdateCos(cos);
            }
            else
            {
                // Daca produsul nu exista in cos, il adaugam
                var cosProdus = new Cos_Produs
                {
                    ID_Cos = cos.ID_Cos,
                    COD_Produs = productId,
                    Cantitate = 1,
                    Pret = produs.Pret
                };
                _cosProdusService.AddCosProdus(cosProdus);
                cos.Pret_total += produs.Pret;
                _cosService.UpdateCos(cos);
            }


            return RedirectToAction("Index", "Cos");
        }
        public IActionResult DownloadProductSpecification(string productId)
        {
            var produs = _produsService.GetProdusWithIngredientsByCod(productId);

            if (produs == null)
            {
                return NotFound("Produsul nu a fost gasit.");
            }

            var pdfBytes = _pdfService.GenerateProductSpecificationPdf(productId);

            var cleanProductName = string.Join("_", produs.Denumire.Split(Path.GetInvalidFileNameChars()));
            var fileName = $"Specificatii_{cleanProductName}.pdf";

            return File(pdfBytes, "application/pdf", fileName);
        }
    }

}

