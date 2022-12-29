using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace App1.Helpers
{
    class FileFunctions
    {
        public static int DeleteFile (string path)
        {
            File.Delete(path);
            return 1;


        }
    }
}
