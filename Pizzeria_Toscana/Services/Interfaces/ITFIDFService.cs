namespace Pizzeria_Toscana.Services.Interfaces
{
    public interface ITFIDFService
    {
        void ComputeIDFScores(List<string> productTitles);
        double ComputeTFIDF(string productTitle, string searchQuery);
        List<string> GetTopSuggestions(string searchTerm, List<string> productTitles);
    }
}