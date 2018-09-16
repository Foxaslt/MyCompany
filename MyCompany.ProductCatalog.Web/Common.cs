using Microsoft.AspNetCore.Http;

namespace MyCompany.ProductCatalog.Web
{
    public class Common
    {
        public static byte[] ImageToBytes(IFormFile imageData)
        {
            if (imageData != null)
            {
                var stream = imageData.OpenReadStream();
                byte[] bytes = new byte[imageData.Length];
                stream.Read(bytes, 0, (int)imageData.Length);
                return bytes;
            }

            return null;
        }
    }
}
