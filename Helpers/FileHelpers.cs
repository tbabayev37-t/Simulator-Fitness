using System.IO;

namespace Simulator16TB.Helpers
{
    public static class FileHelpers
    {
        public static bool Checksize(this IFormFile file, int mb)
        {
            return file.Length < mb * 1024 * 1024;
        }

        public static bool CheckType(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }

        public static async Task<string> FileUploadAsync(this IFormFile file, string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string uniqueFileName = Guid.NewGuid() + Path.GetFileName(file.FileName);
            string path = Path.Combine(folderPath, uniqueFileName);

            using FileStream stream = new(path, FileMode.Create);
            await file.CopyToAsync(stream);

            return uniqueFileName;
        }

        public static void FileDelete(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}
