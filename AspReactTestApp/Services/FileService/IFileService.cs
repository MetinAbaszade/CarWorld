namespace AspReactTestApp.Services.FileService
{
    public interface IFileService
    {
        public Task SaveFile(IFormFile formFile, string filePath);
        public Task<string> SaveProfileImage(IFormFile formFile);
        public string GetDefaultProfileImageUrl();
        public string GenerateFileName();
    }
}
