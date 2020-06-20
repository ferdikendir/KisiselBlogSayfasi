using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace proje_blog.Models
{
    public class htmltemiz
    {
        public static string HTMLTemizle(string text)
        {
            return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
        }
        public static string HTMLTemiz(string text)
        {
                string icerik = Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
                if (icerik.Length > 300)
                    return icerik.Substring(0, 300);
                else return icerik;
            
        }
        public static string HTMLTemizler(string text)
        {
            
                string icerik = Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
                if (icerik.Length > 100)
                    return icerik.Substring(0, 100);
                else
                    return icerik;
          

        }
        public static string HTMLTemizlerr(string text)
        {
            
                string icerik = Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
                if (icerik.Length > 200)
            {
                return icerik.Substring(0, 200);
            }
            else
            {
                return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
            }

        }
    }
}