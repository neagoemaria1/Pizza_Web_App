using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Pizzeria_Toscana.Services.Interfaces;
using Pizzeria_Toscana.Models;
using Lucene.Net.QueryParsers.Classic;

namespace Pizzeria_Toscana.Services
{
  public class LuceneIndexService : ILuceneIndexService
    {
        // Directorul unde se salveaza indexul Lucene
        private const string LuceneDirectory = "LuceneIndex";
        private readonly FSDirectory _directory;
        private readonly Analyzer _analyzer; // pentru procesarea textului

        public LuceneIndexService()
        {
            _directory = FSDirectory.Open(new DirectoryInfo(LuceneDirectory));
            _analyzer = new StandardAnalyzer(LuceneVersion.LUCENE_48);
        }

        // Metoda pentru indexarea specificatiilor produsului
        public void IndexProductSpecification(string COD_Produs, byte[] pdfBytes)
        {
            using var writer = new IndexWriter(_directory, new IndexWriterConfig(LuceneVersion.LUCENE_48, _analyzer));

            string pdfText = ExtractTextFromPdf(pdfBytes);

            var doc = new Document
                {
                     new StringField("COD_Produs", COD_Produs, Field.Store.YES),
                     new TextField("Content", pdfText, Field.Store.YES)
            };

            writer.UpdateDocument(new Term("COD_Produs", COD_Produs), doc);
            writer.Commit();
        }

        // Metoda pentru reindexare
        public void ReindexAllProducts(IEnumerable<Produs> products, IPdfService pdfService)
        {
            using var writer = new IndexWriter(_directory, new IndexWriterConfig(LuceneVersion.LUCENE_48, _analyzer));
            writer.DeleteAll();
            writer.Dispose();

            foreach (var product in products)
            {
                try
                {
                    var pdfBytes = pdfService.GenerateProductSpecificationPdf(product.COD_Produs);
                    IndexProductSpecification(product.COD_Produs, pdfBytes);
                }
                catch
                {
                    //log
                }

            }
        }

        // Metoda pentru cautarea specificatiilor
        public SearchResult[] SearchSpecifications(string query, int maxResults = 10)
        {
            using var reader = DirectoryReader.Open(_directory);
            var searcher = new IndexSearcher(reader);

            var parser = new QueryParser(LuceneVersion.LUCENE_48, "Content", _analyzer);
            var luceneQuery = parser.Parse(query);

            var hits = searcher.Search(luceneQuery, maxResults).ScoreDocs;

            return hits.Select(hit =>
            {
                var foundDoc = searcher.Doc(hit.Doc);
                return new SearchResult
                {
                    COD_Produs = foundDoc.Get("COD_Produs"),
                    Score = hit.Score
                };
            }).OrderByDescending(r => r.Score).ToArray();
        }


        private string ExtractTextFromPdf(byte[] pdfBytes)
        {
            using var memoryStream = new MemoryStream(pdfBytes);
            using var pdfReader = new PdfReader(memoryStream);
            using var pdfDocument = new PdfDocument(pdfReader);

            string extractedText = "";
            for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
            {
                extractedText += PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(i));
            }
            return extractedText;
        }
    }
}
