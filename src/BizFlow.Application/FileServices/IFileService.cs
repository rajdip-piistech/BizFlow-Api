using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BizFlow.Application.FileServices;

public interface IFileService
{
    Task<string> Upload(IFormFile file, string folderName);
    void DeleteFile(string fileNameWithExtension,string folderName);
}
public class FileService : IFileService
{
    private readonly IWebHostEnvironment env;

    public FileService(IWebHostEnvironment env)
    {
        this.env = env;
    }

    public void DeleteFile(string fileNameWithExtension, string folderName)
    {
        if (string.IsNullOrEmpty(fileNameWithExtension))
        {
            throw new ArgumentNullException(nameof(fileNameWithExtension));
        }

        var contentPath = env.ContentRootPath;
        var path = Path.Combine(contentPath, "wwwroot", folderName.Replace("/", Path.DirectorySeparatorChar.ToString()).Replace("\\", Path.DirectorySeparatorChar.ToString()), fileNameWithExtension);

        Console.WriteLine($"Constructed path: {path}");

        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"File not found at path: {path}");
        }

        File.Delete(path);
    }

    public async Task<string> Upload(IFormFile file, string folderName)
    {   if(file is null)
        {
            return string.Empty;
        }
        else
        {
            var uploadPath = Path.Combine(env.WebRootPath, folderName);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }
    }

}
