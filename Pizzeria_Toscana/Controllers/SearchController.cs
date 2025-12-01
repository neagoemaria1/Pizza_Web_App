using Microsoft.AspNetCore.Mvc;
using Pizzeria_Toscana.Services.Interfaces;

namespace Pizzeria_Toscana.Controllers
{
  
        [Route("Search")]
        public class SearchController : Controller
        {
            private readonly ILuceneIndexService _luceneIndexService;
            private readonly IProdusService _produsService;

            public SearchController(ILuceneIndexService luceneIndexService, IProdusService produsService)
            {
                _luceneIndexService = luceneIndexService;
                _produsService = produsService;
            }

            [HttpGet("SearchSpecifications")]
            public IActionResult SearchSpecifications(string query)
            {
                if (string.IsNullOrWhiteSpace(query))
                    return Json(new { results = new List<object>() });

                var results = _luceneIndexService.SearchSpecifications(query, 10);

                var productResults = results.Select(result =>
                {
                    var product = _produsService.GetProdusByCod(result.COD_Produs);
                    return new
                    {
                        product.Denumire,
                        product.Descriere,
                        product.Pret,
                        product.COD_Produs,
                        Score = result.Score
                    };
                }).OrderByDescending(p => p.Score).ToList();

                return Json(new { results = productResults });
            }
        }
    }