using Minishop.Application.Contracts;
using Minishop.Infrastructure;
using Minishop.Infrastructure.Entities;

namespace Minishop.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly MinishopDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ImageService(MinishopDBContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public bool DeleteImage(int productId)
        {
            var imageDb = _context.Images.FirstOrDefault(x => x.ProductId == productId);
            if (imageDb != null)
            {
                var imageFile = GetFilePath(imageDb.Name);
                if (System.IO.File.Exists(imageFile))
                {
                    System.IO.File.Delete(imageFile);
                }

                _context.Images.Remove(imageDb);
                _context.SaveChanges();

                return true;
            }
            return false;
        }

        private string GetFilePath(string fileName)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            return Path.Combine(($"{wwwRootPath}/images/"), fileName);
        }

        public int SaveImage(IFormFile imageFile, int productId, bool deleteOldImage = false)
        {
            if (deleteOldImage)
            {
                DeleteImage(productId);
            }

            //Save image to wwwroot/images
            string wwwRootPath = _hostEnvironment.WebRootPath;
            //string fileName = Path.GetFileName(imageFile.FileName);
            string extension = Path.GetExtension(imageFile.FileName);
            var fileName = ($"product_{productId}_.{extension}");
            string path = Path.Combine(($"{wwwRootPath}/images/"), fileName);
            try
            {
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }

                var image = new Image
                {
                    Name = fileName,
                    ProductId = productId
                };

                _context.Images.Add(image);
                _context.SaveChanges();

                return image.Id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
