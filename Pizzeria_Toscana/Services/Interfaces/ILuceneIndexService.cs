using Pizzeria_Toscana.Models;

namespace Pizzeria_Toscana.Services.Interfaces
{
    public interface ILuceneIndexService
    {
        void IndexProductSpecification(string COD_Produs, byte[] pdfBytes);
        SearchResult[] SearchSpecifications(string query, int maxResults = 10);
        void ReindexAllProducts(IEnumerable<Produs> products, IPdfService pdfService);
    }

    public class SearchResult
    {
        public string COD_Produs { get; set; }
        public float Score { get; set; }
    }
}