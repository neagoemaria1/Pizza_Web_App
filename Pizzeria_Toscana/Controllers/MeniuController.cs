using Microsoft.AspNetCore.Mvc;
using Pizzeria_Toscana.Models;
using Pizzeria_Toscana.Services;
using Pizzeria_Toscana.Services.Interfaces;
using System.Linq;

public class MeniuController : Controller
{
    private readonly PizzerieContext _context;
    private readonly IProdusService _produsService;
    private readonly ITFIDFService _tfidfService;
    private readonly ILuceneIndexService _luceneIndexService;
    private readonly IUserService _userService;
    private readonly ICategorieService _categorieService;
    private readonly IPdfService _pdfService;

    public MeniuController(PizzerieContext context, IProdusService produsService, ITFIDFService tFIDFService, ILuceneIndexService luceneIndexService, IUserService userService, IPdfService pdfService)
    {
        _context = context;
        _produsService = produsService;
        _tfidfService = tFIDFService;
        _luceneIndexService = luceneIndexService;
        var allProductTitles = _produsService.GetAllProduse().Select(p => p.Denumire).ToList();
        if (allProductTitles.Any())
        {
            _tfidfService.ComputeIDFScores(allProductTitles);
        }
        var products = _produsService.GetAllProduse();
        this._pdfService = pdfService;
        _luceneIndexService.ReindexAllProducts(products, _pdfService);
     

    }

    [HttpGet]
    public IActionResult GetSearchSuggestions(string term)
    {
        if (string.IsNullOrEmpty(term))
            return Json(new List<string>());

        var allProducts = _produsService.GetAllProduse().Select(p => p.Denumire).ToList();
        var suggestions = _tfidfService.GetTopSuggestions(term, allProducts);

        return Json(suggestions);
    }

    public IActionResult Index(int? categorieId, string? searchQuery = null)
    {
        // Adaugam categoriile in ViewBag
        ViewBag.Categorii = _context.Categorie.ToList();

        // Separam produsele in functie de categorie
        var produse = _context.Produse
            .Where(p => !categorieId.HasValue || p.ID_Categorie == categorieId)
            .ToList();
        if (!string.IsNullOrEmpty(searchQuery))
        {
            produse = produse
                .Select(p => new { Produs = p, Score = _tfidfService.ComputeTFIDF(p.Denumire, searchQuery) })
                .Where(p => p.Score > 0)
                .OrderByDescending(p => p.Score)
                .Select(p => p.Produs)
                .ToList();
            ViewBag.searchQuery = searchQuery;
        }

    

        return View(produse);
    }
    [HttpGet]
    public async Task<IActionResult> SearchSpecs(string specQuery, string sortOrder = "desc")
    {
        if (string.IsNullOrWhiteSpace(specQuery))
        {
            return RedirectToAction("Index");
        }

        var results = _luceneIndexService.SearchSpecifications(specQuery, maxResults: 20);

        if (sortOrder == "asc")
            results = results.OrderBy(r => r.Score).ToArray();
        else
            results = results.OrderByDescending(r => r.Score).ToArray();

        var matchingProducts = results
            .Select(r => _produsService.GetProdusByCod(r.COD_Produs))
            .Where(p => p != null)
            .ToList();

        ViewBag.IsSpecSearch = true;
        ViewBag.SpecResults = results;
        ViewBag.SpecQuery = specQuery;
        ViewBag.SortOrder = sortOrder;
        

        return View("Index", matchingProducts);
    }
}