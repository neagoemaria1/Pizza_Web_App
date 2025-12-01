namespace Pizzeria_Toscana.Services.Interfaces
{
    public interface IPdfService
    {
        byte[] GenerateProductSpecificationPdf(string productId);
    }
}
 