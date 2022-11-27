namespace Blog.Data.FileManager;

public class FileManager : IFileManager {
    private string _imagePath;

    public FileManager(IConfiguration config) {
        _imagePath = config["Path:Images"];
    }

    public FileStream ImageStream(string image) {
        return new FileStream(Path.Combine(_imagePath, image), FileMode.Open, FileAccess.Read);
    }

    public async Task<string> SaveImage(IFormFile image) {
        try {
            var savePath = Path.Combine(_imagePath);
            if (!Directory.Exists(_imagePath)) Directory.CreateDirectory(savePath);
            var imageType = image.FileName.Substring(image.FileName.LastIndexOf("."));
            var fileName = $"img_{DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}{imageType}"; 
            using(var fileStream = new FileStream(Path.Combine(savePath, fileName), FileMode.Create)) {
                await image.CopyToAsync(fileStream);
            }
            return fileName;
        } catch (Exception e) {
            Console.WriteLine("Unable to save file", e.Message);
            return "Error";
        }
    }
}
