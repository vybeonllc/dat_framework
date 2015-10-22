using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Extensions
{
    public static class StringExtension
    {
        static Dictionary<string, string> HexToAscii = new Dictionary<string, string>()
        {
            {"\\x00","^@"},
            {"\\x01","^A"},
            {"\\x02","^B"},
            {"\\x03","^C"},
            {"\\x04","^D"},
            {"\\x05","^E"},
            {"\\x06","^F"},
            {"\\x07","^G"},
            {"\\x08","^H"},
            {"\\x09","^I"},
            {"\\x0a","^J"},
            {"\\x0b","^K"},
            {"\\x0c","^L"},
            {"\\x0d","^M"},
            {"\\x0e","^N"},
            {"\\x0f","^O"},
            {"\\x10","^P"},
            {"\\x11","^Q"},
            {"\\x12","^R"},
            {"\\x13","^S"},
            {"\\x14","^T"},
            {"\\x15","^U"},
            {"\\x16","^V"},
            {"\\x17","^W"},
            {"\\x18","^X"},
            {"\\x19","^Y"},
            {"\\x1a","^Z"},
            {"\\x1b","^["},
            {"\\x1c",@"^\"},
            {"\\x1d","^]"},
            {"\\x1e","^^"},
            {"\\x1f","^_"},
            {"\\x20"," " },
            {"\\x21","!" },
            {"\\x22","\""},
            {"\\x23","#" },  
            {"\\x24","$" },  
            {"\\x25","%" },  
            {"\\x26","&" },  
            {"\\x27","'" },  
            {"\\x28","(" },  
            {"\\x29",")" },  
            {"\\x2a","*" }, 
            {"\\x2b","+" },  
            {"\\x2c","," },  
            {"\\x2d","-" },  
            {"\\x2e","." },  
            {"\\x2f","/" },  
            {"\\x30","0" },  
            {"\\x31","1" },  
            {"\\x32","2" },  
            {"\\x33","3" },  
            {"\\x34","4" },  
            {"\\x35","5" },  
            {"\\x36","6" },  
            {"\\x37","7" },  
            {"\\x38","8" },  
            {"\\x39","9" },  
            {"\\x3a",":" },  
            {"\\x3b",";" },  
            {"\\x3c","<" },  
            {"\\x3d","=" },  
            {"\\x3e",">" },  
            {"\\x3f","?" },  
            {"\\x40","@" },  
            {"\\x41","A" },  
            {"\\x42","B" },  
            {"\\x43","C" },  
            {"\\x44","D" },  
            {"\\x45","E" },  
            {"\\x46","F" },  
            {"\\x47","G" },  
            {"\\x48","H" },  
            {"\\x49","I" },  
            {"\\x4a","J" },  
            {"\\x4b","K" },  
            {"\\x4c","L" },  
            {"\\x4d","M" },  
            {"\\x4e","N" },  
            {"\\x4f","O" },  
            {"\\x50","P" },  
            {"\\x51","Q" },  
            {"\\x52","R" },  
            {"\\x53","S" },  
            {"\\x54","T" },  
            {"\\x55","U" }, 
            {"\\x56","V" },  
            {"\\x57","W" },  
            {"\\x58","X" },  
            {"\\x59","Y" },  
            {"\\x5a","Z" },  
            {"\\x5b","[" },  
            {"\\x5c",@"\"},  
            {"\\x5d","]" },  
            {"\\x5e","^" },  
            {"\\x5f","_" },  
            {"\\x60","`" },  
            {"\\x61","a" },  
            {"\\x62","b" },  
            {"\\x63","c" },  
            {"\\x64","d" },  
            {"\\x65","e" },  
            {"\\x66","f" },  
            {"\\x67","g" },  
            {"\\x68","h" },  
            {"\\x69","i" },  
            {"\\x6a","j" },  
            {"\\x6b","k" },  
            {"\\x6c","l" },  
            {"\\x6d","m" },  
            {"\\x6e","n" },  
            {"\\x6f","o" },  
            {"\\x70","p" },  
            {"\\x71","q" },  
            {"\\x72","r" },  
            {"\\x73","s" },  
            {"\\x74","t" },  
            {"\\x75","u" },  
            {"\\x76","v" },  
            {"\\x77","w" },  
            {"\\x78","x" },  
            {"\\x79","y" },  
            {"\\x7a","z" },  
            {"\\x7b","{" },  
            {"\\x7c","|" },  
            {"\\x7d","}" },  
            {"\\x7e","~" },  
            {"\\x7f"," " }
        };
        public static string ConvertHextoAscii(this string HexValue)
        {
            foreach (KeyValuePair<string, string> kv in HexToAscii)
                HexValue = HexValue.Replace(kv.Key, kv.Value);
            return HexValue;
        }
        /// <summary>
        /// Takes all the text you give it and only returns the numbers inside of it
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetNumbers(this string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;

            string numbers = string.Empty;
            try
            {
                foreach (char character in text.ToCharArray())
                    if ((character >= (char)48) && (character <= (char)57)) numbers += character;
            }
            catch { }

            return numbers;
        }

        public static string GetDomainName(this string domainName)
        {
            String url = "";
            String tld = "com";
            String[] split = domainName.Split('.');
            if (split.Length > 1)
            {
                if (split[0] == "www")
                {
                    if (split.Length > 2 && new String[] { "com", "net", "org", "info", "us", "biz", "tel", "ca" }.Contains(split[2]))
                    {
                        url = split[1];
                        tld = split[2];
                    }
                    else
                    {
                        url = split[1];
                    }
                }
                else if (new String[] { "com", "net", "org", "info", "us", "biz", "tel", "ca" }.Contains(split[1]))
                {
                    url = split[0];
                    tld = split[1];
                }
                else
                    url = split[0];
            }
            else
                url = split[0];

            return url + "." + tld;
        }

        /// <summary>
        /// Returns true of false depending on if it's a number
        /// </summary>
        /// <param name="strTextEntry"></param>
        /// <returns></returns>
        public static Boolean IsNumeric(this string strTextEntry)
        {
            var objNotWholePattern = new Regex("[^0-9]");
            return !objNotWholePattern.IsMatch(strTextEntry)
                 && (strTextEntry != "");
        }

        public static Boolean HasAnyNumber(this String text)
        {
            try
            {
                foreach (char character in text.ToCharArray())
                    if ((character >= (char)48) && (character <= (char)57)) return true;
            }
            catch { }
            return false;
        }

        public static string RemoveNumbers(this string text)
        {
            return string.Join(string.Empty, text.Where(t => !t.ToString().IsNumeric()).ToArray());
        }

        /// <summary>
        /// Returns true of false depending on if it's a number
        /// </summary>
        /// <param name="strTextEntry"></param>
        /// <returns></returns>
        public static Boolean EndsWithNumber(this string strTextEntry)
        {
            return strTextEntry.Last().ToString().IsNumeric();
        }

        public static String CleanString(this String str)
        {
            return System.Text.RegularExpressions.Regex.Replace(str, @"[^\w\.-]", "");
        }
        public static String RandomString(this Random rnd, int length)
        {
            string _allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            char[] chars = new char[length];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < length; ++i)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * rnd.NextDouble())];
            };
            return new string(chars);
        }
        public static String RightOf(this String text, Int32 index)
        {
            return index > text.Length || index < 0 ? text : text.Substring(index);
        }
      
        public static String LeftOf(this String text, Int32 index)
        {
            return index > text.Length || index < 0 ? text : text.Substring(0, index);
        }
        public static String LeftOf(this String text, String input)
        {
            return text.IndexOf(input) > -1 ? text.LeftOf(text.IndexOf(input)) : text;
        }
        public static String RightOf(this String text, String input)
        {
            return text.IndexOf(input) > -1 ? text.RightOf(text.IndexOf(input) + input.Length) : text;
        }
        public static String LeftOfLast(this String text, String input)
        {
            return text.LastIndexOf(input) > -1 ? text.LeftOf(text.LastIndexOf(input)) : text;
        }
        public static String RightOfLast(this String text, String input)
        {
            return text.LastIndexOf(input) > -1 ? text.RightOf(text.LastIndexOf(input) + input.Length) : text;
        }
        /// <summary>
        /// Get the characters from the end of string, String.fromRight("TwentyNinePrime", 5) => "Prime"
        /// </summary>
        /// <param name="text"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static String fromRight(this String text, Int32 count)
        {
            return count < text.Length ? text.Substring(text.Length - count) : text;
        }
        /// <summary>
        /// Get the characters from the first of string, String.fromLeft("TwentyNinePrime", 6) => "Twenty"
        /// </summary>
        /// <param name="text"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static String fromLeft(this String text, Int32 count)
        {
            return text.LeftOf(count);
        }
        /// <summary>
        /// Replace("TwentyNinePrime","TwentyNine","29","Prime","'") => 29'
        /// </summary>
        /// <param name="text"></param>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static String Replace(this String text, params Object[] args)
        {
            for (Int32 i = 0; i < args.Length; i += 2)
            {
                text = text.Replace(args[i].ToString(), i + 1 >= args.Length ? String.Empty : args[i + 1].ToString());
            };
            return text;
        }
        public static String ReplaceAllWith(this String text, Object replaceWith, params Object[] args)
        {
            args.ToList().ForEach(delegate(Object x)
            {
                text = text.Replace(x.ToString(), replaceWith.ToString());
            });
            return text;
        }
        public static System.IO.Stream ToStream(this string s)
        {
            return new System.IO.MemoryStream(System.Text.Encoding.Unicode.GetBytes(s));
        }

        public static string ToDisplayPhoneNumber(this string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return phoneNumber;

            string numbersOnly = phoneNumber.GetNumbers();

            if (numbersOnly.Length == 7)
                return string.Concat(numbersOnly.Substring(0, 3), "-", numbersOnly.Substring(3));
            else if (numbersOnly.Length == 10)
                return string.Concat("(", numbersOnly.Substring(0, 3), ") ", numbersOnly.Substring(3, 3), "-", numbersOnly.Substring(6));
            else if (numbersOnly.Length >= 11)
                return string.Concat("(", numbersOnly.Substring(0, 3), ") ", numbersOnly.Substring(3, 3), "-", numbersOnly.Substring(6, 4), " Ext. ", numbersOnly.Substring(10));

            return phoneNumber;
        }
    }
}
