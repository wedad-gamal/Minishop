namespace Minishop.Application.Contracts
{
    public interface IImageService
    {
        bool DeleteImage(int productId);
        int SaveImage(IFormFile imageFile, int productId, bool deleteOldImage = false);
    }
}
