using AspReactTestApp.Entities.Concrete;

namespace AspReactTestApp.Services.FileService
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;


        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// Generates a unique filename for profile images.
        /// </summary>
        /// <returns>A unique filename.</returns>
        public string GenerateFileName()    
        {
            return string.Format(@"File_{0}", Guid.NewGuid());
        }

        /// <summary>
        /// Saves the given file to the specified file path.
        /// </summary>
        /// <param name="formFile">The file to save.</param>
        /// <param name="filePath">The file path to save the file to.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SaveFile(IFormFile formFile, string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
        }

        /// <summary>
        /// Saves the given profile image.
        /// </summary>
        /// <param name="formFile">The profile image to save.</param>
        /// <returns>Filepath of the saved ProfileImage</returns>
        public async Task<string> SaveProfileImage(IFormFile formFile)
        {
            string fileName = GenerateFileName();
            string folderPath = Path.Combine(_env.WebRootPath, "images");
            string filePath = Path.Combine(folderPath, fileName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            await SaveFile(formFile, filePath);

            return filePath;
        }

        /// <summary>
        /// Returns the default profile image URL.
        /// </summary>
        /// <returns>A string representing the default profile image URL.</returns>
        public string GetDefaultProfileImageUrl()
        {
            return "/images/UserLogo2.jpeg";
        }
    }
}
 