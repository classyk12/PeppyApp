using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Peppy2.Helpers
{
    public class Filehelper
    {
        public static byte[] Readfully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
