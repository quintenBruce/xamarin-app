using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace App1.Helpers
{
    internal class FileFunctions
    {
        //DeleteFile method will delete file given by the path parameter. Will return true if file was deleted successfully, else false
        public static bool DeleteFile(string path)
        {
            File.Delete(path);

            if (File.Exists(path))
                return false;
            return true;
        }

        //WriteFile method will write the stream parameter to a new file given by the path parameter. Will return true if new file has length > 0, else false
        public static async Task<bool> WriteFile(Stream stream, string path)
        {
            var newStream = File.OpenWrite(path);
            await stream.CopyToAsync(newStream);

            if (newStream.Length == 0)
                return false;

            return true;
        }

        // Checks if file exists at <param>path</param> parameter. Renames path until path exists.
        public static string CheckFilePath(string path)
        {
            int count = 1;
            string newPath = path;
            string fileType = "." + path.Split('.').Last();
            while (File.Exists(newPath))
            {
                newPath = newPath.Replace(fileType, "").Replace("(" + (count - 1).ToString() + ")", "");
                newPath = path.Replace(fileType, "") + "(" + count.ToString() + ")" + fileType;
                count++;
            }

            return newPath;
        }
    }
}