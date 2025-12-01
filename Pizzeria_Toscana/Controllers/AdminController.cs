using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pizzeria_Toscana.Models;
using Pizzeria_Toscana.Services;
using Pizzeria_Toscana.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria_Toscana.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProdusService _produsService;
        private readonly ICategorieService _categorieService;
        private readonly IProdus_IngredientService _produs_IngredientService;
        private readonly IIngredientService _ingredientService;
        public AdminController(IProdusService produsService, ICategorieService categorieService, IProdus_IngredientService produs_IngredientService, IIngredientService ingredientService)
        {
            _produsService = produsService;
            _categorieService = categorieService;
            _ingredientService = ingredientService;
            _produs_IngredientService = produs_IngredientService;
        }

        public async Task<IActionResult> Index()
        {
            var produse = _produsService.GetAllProduse();
            return View(produse);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateProdus()
        {
            ViewBag.Categorii = _categorieService
                .GetAllCategorii()
                .Select(c => new SelectListItem
                {
                    Value = c.ID_Categorie.ToString(),
                    Text = c.Nume
                });
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProdus(Produs produs, IFormFile ProdusPicture)
        {
            if (ModelState.IsValid)
            {
                if (ProdusPicture != null && ProdusPicture.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await ProdusPicture.CopyToAsync(ms);
                        produs.ProdusPicture = ms.ToArray();
                    }
                }

                produs.COD_Produs = _produsService.GetNextProdusCode();

                _produsService.AddProdus(produs);
                return RedirectToAction("Index");
            }

            return View(produs);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditProdus(string id)
        {
            var produs = _produsService.GetProdusByCod(id);
            if (produs == null)
            {
                return NotFound();
            }

            ViewBag.Categorii = new SelectList(new List<SelectListItem>
    {
        new SelectListItem { Text = "Vegana", Value = "1" },
        new SelectListItem { Text = "Non-Vegana", Value = "2" }
    }, "Value", "Text", produs.ID_Categorie);

            return View(produs);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditProdus(Produs produs, IFormFile ProdusPicture)
        {
            ModelState.Remove("ProdusPicture");

            if (ModelState.IsValid)
            {
                var existingProdus = _produsService.GetProdusByCod(produs.COD_Produs);
                if (existingProdus != null)
                {
                    existingProdus.Denumire = produs.Denumire;
                    existingProdus.Pret = produs.Pret;
                    existingProdus.ID_Categorie = produs.ID_Categorie;
                    existingProdus.Descriere = produs.Descriere;

                    // Actualizam imaginea doar dacă s-a furnizat un nou fisier
                    if (ProdusPicture != null && ProdusPicture.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            await ProdusPicture.CopyToAsync(ms);
                            existingProdus.ProdusPicture = ms.ToArray();
                        }
                    }

                    _produsService.UpdateProdus(existingProdus);
                    return RedirectToAction("Index");
                }
                return NotFound();
            }

            return View(produs);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult DeleteProdus(string id)
        {
            var produs = _produsService.GetProdusByCod(id);
            if (produs == null)
            {
                return NotFound();
            }
            return View(produs);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteProdusConfirmed(string id)
        {
            var produs = _produsService.GetProdusByCod(id);
            if (produs != null)
            {
                _produsService.DeleteProdus(id);
            }
            return RedirectToAction("Index");
        
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult LinkIngredient(string id, string? errorMessage)
        {
       
            var produs = _produsService.GetProdusByCod(id);
            if (produs == null)
                return NotFound("Produsul nu a fost găsit.");

            
            var toateIngredientele = _ingredientService.GetAllIngredients().ToList();
            var toateAsocierile = _produs_IngredientService.GetAllProdus_Ingredients().ToList();
            var asocieriCurente = toateAsocierile
                .Where(pi => pi.COD_Produs == id)
                .Select(a => new
                {
                    IngredientName = toateIngredientele.FirstOrDefault(i => i.COD_Ingredient == a.COD_Ingredient)?.Denumire ?? "Necunoscut",
                    Cantitate = a.Cantitate_Ingredient,
                    COD_Ingredient = a.COD_Ingredient
                })
                .ToList();

            
            var ingredienteDisponibile = toateIngredientele
                .Where(i => !asocieriCurente.Any(a => a.COD_Ingredient == i.COD_Ingredient))
                .ToList();

            
            ViewBag.ProdusAsociere = produs;
            ViewBag.ToateIngredientele = ingredienteDisponibile;
            ViewBag.AsocieriCurente = asocieriCurente;
            ViewBag.ErrorMessage = errorMessage;

            return View("LinkIngredient");
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult LinkIngredient(string id, int codIngredient, string cantitate = "0")
        {
            
            var produs = _produsService.GetProdusByCod(id);
            var ingredient = _ingredientService.GetIngredientByCode(codIngredient);

            if (produs == null || ingredient == null)
                return RedirectToAction("LinkIngredient", new
                {
                    codProdus = id,
                    errorMessage = "Produs sau ingredient inexistent!"
                });

            
            bool existaDeja = _produs_IngredientService.GetAllProdus_Ingredients()
                .Any(pi => pi.COD_Produs == id && pi.COD_Ingredient == codIngredient);

            if (existaDeja)
            {
              
                return RedirectToAction("LinkIngredient", new
                {
                    codProdus = id,
                    errorMessage = "Acest ingredient este deja asociat!"
                });
            }

          
            var produsIngredient = new Produs_Ingredient
            {
                COD_Produs = id,
                COD_Ingredient = codIngredient,
                Cantitate_Ingredient = cantitate
            };
            _produs_IngredientService.AddProdus_Ingredient(produsIngredient);

       
            return RedirectToAction("LinkIngredient", new { codProdus = id });
        }

        [HttpPost]
        public IActionResult UnlinkIngredient(string codProdus, int codIngredient)
        {
            var produsIngredient = _produs_IngredientService.GetAllProdus_Ingredients()
                .FirstOrDefault(pi => pi.COD_Produs == codProdus && pi.COD_Ingredient == codIngredient);

            if (produsIngredient != null)
            {
                _produs_IngredientService.DeleteProdus_Ingredient(produsIngredient.COD_Produs_Ingredient);
            }

            return RedirectToAction("LinkIngredient", new { id = codProdus });
        }

       


    }
}

