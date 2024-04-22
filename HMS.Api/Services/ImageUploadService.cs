using HMS.Library.Models;

namespace HMS.Api.Services
{
    public class ImageUploadService
    {
        private readonly IWebHostEnvironment hostEnvironment;

        public ImageUploadService(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }


        public async Task<string?> Upload(ImageUpload upload)
        {
            try
            {
                string base64Data = upload.ImageData.Replace('_', '/').Replace('-', '+');

               
                //string base64Data = upload.ImageData.Split(";base64,")[1];

                byte[] bits = Convert.FromBase64String(upload.ImageData);

                if (string.IsNullOrEmpty(upload.ImageName))
                {
                    upload.ImageName = Guid.NewGuid().ToString() + ".png";
                }

                upload.ImageName = "/images/" + upload.ImageName;
                string path = this.hostEnvironment + "/" + upload.ImageName;
                using (FileStream ms = new FileStream(path, FileMode.Create))
                {
                    await ms.WriteAsync(bits);
                    await ms.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }

            return upload.ImageName;
        }


    }
}

