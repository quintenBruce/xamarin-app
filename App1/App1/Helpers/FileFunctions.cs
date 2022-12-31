using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
    }
}