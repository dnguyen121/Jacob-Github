using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace HumanManagement.Utils
{
    public static class StringUtils
    {

        public static Dictionary<string, string> characterMaps = new Dictionary<string, string>();

        static StringUtils()
        {
            characterMaps.Add("Á", "a");
            characterMaps.Add("á", "a");
            characterMaps.Add("À", "a");
            characterMaps.Add("à", "a");
            characterMaps.Add("Ã", "a");
            characterMaps.Add("ã", "a");
            characterMaps.Add("Ả", "a");
            characterMaps.Add("ả", "a");
            characterMaps.Add("ạ", "a");
            characterMaps.Add("Ạ", "a");

            characterMaps.Add("Â", "a");
            characterMaps.Add("â", "a");
            characterMaps.Add("Ấ", "a");
            characterMaps.Add("ấ", "a");
            characterMaps.Add("Ầ", "a");
            characterMaps.Add("ầ", "a");
            characterMaps.Add("Ẫ", "a");
            characterMaps.Add("ẫ", "a");
            characterMaps.Add("Ẩ", "a");
            characterMaps.Add("ẩ", "a");
            characterMaps.Add("Ậ", "a");
            characterMaps.Add("ậ", "a");

            characterMaps.Add("Ă", "a");
            characterMaps.Add("ă", "a");
            characterMaps.Add("Ắ", "a");
            characterMaps.Add("ắ", "a");
            characterMaps.Add("Ằ", "a");
            characterMaps.Add("ằ", "a");
            characterMaps.Add("Ẵ", "a");
            characterMaps.Add("ẵ", "a");
            characterMaps.Add("Ẳ", "a");
            characterMaps.Add("ẳ", "a");
            characterMaps.Add("Ặ", "a");
            characterMaps.Add("ặ", "a");

            characterMaps.Add("É", "e");
            characterMaps.Add("é", "e");
            characterMaps.Add("È", "e");
            characterMaps.Add("è", "e");
            characterMaps.Add("Ẽ", "e");
            characterMaps.Add("ẽ", "e");
            characterMaps.Add("Ẻ", "e");
            characterMaps.Add("ẻ", "e");
            characterMaps.Add("Ẹ", "e");
            characterMaps.Add("ẹ", "e");

            characterMaps.Add("Ê", "e");
            characterMaps.Add("ê", "e");
            characterMaps.Add("Ế", "e");
            characterMaps.Add("ế", "e");
            characterMaps.Add("Ề", "e");
            characterMaps.Add("ề", "e");
            characterMaps.Add("Ễ", "e");
            characterMaps.Add("ễ", "e");
            characterMaps.Add("Ể", "e");
            characterMaps.Add("ể", "e");
            characterMaps.Add("Ệ", "e");
            characterMaps.Add("ệ", "e");

            characterMaps.Add("Í", "i");
            characterMaps.Add("í", "i");
            characterMaps.Add("Ì", "i");
            characterMaps.Add("ì", "i");
            characterMaps.Add("Ĩ", "i");
            characterMaps.Add("ĩ", "i");
            characterMaps.Add("Ỉ", "i");
            characterMaps.Add("ỉ", "i");
            characterMaps.Add("Ị", "i");
            characterMaps.Add("ị", "i");

            characterMaps.Add("Ó", "o");
            characterMaps.Add("ó", "o");
            characterMaps.Add("Ò", "o");
            characterMaps.Add("ò", "o");
            characterMaps.Add("Õ", "o");
            characterMaps.Add("õ", "o");
            characterMaps.Add("Ỏ", "o");
            characterMaps.Add("ỏ", "o");
            characterMaps.Add("Ọ", "o");
            characterMaps.Add("ọ", "o");

            characterMaps.Add("Ô", "o");
            characterMaps.Add("ô", "o");
            characterMaps.Add("Ố", "o");
            characterMaps.Add("ố", "o");
            characterMaps.Add("Ồ", "o");
            characterMaps.Add("ồ", "o");
            characterMaps.Add("Ỗ", "o");
            characterMaps.Add("ỗ", "o");
            characterMaps.Add("Ổ", "o");
            characterMaps.Add("ổ", "o");
            characterMaps.Add("Ộ", "o");
            characterMaps.Add("ộ", "o");

            characterMaps.Add("Ơ", "o");
            characterMaps.Add("ơ", "o");
            characterMaps.Add("Ớ", "o");
            characterMaps.Add("ớ", "o");
            characterMaps.Add("Ờ", "o");
            characterMaps.Add("ờ", "o");
            characterMaps.Add("Ỡ", "o");
            characterMaps.Add("ỡ", "o");
            characterMaps.Add("Ở", "o");
            characterMaps.Add("ở", "o");
            characterMaps.Add("Ợ", "o");
            characterMaps.Add("ợ", "o");

            characterMaps.Add("Ú", "u");
            characterMaps.Add("ú", "u");
            characterMaps.Add("Ù", "u");
            characterMaps.Add("ù", "u");
            characterMaps.Add("Ũ", "u");
            characterMaps.Add("ũ", "u");
            characterMaps.Add("Ủ", "u");
            characterMaps.Add("ủ", "u");
            characterMaps.Add("Ụ", "u");
            characterMaps.Add("ụ", "u");

            characterMaps.Add("Ư", "u");
            characterMaps.Add("ư", "u");
            characterMaps.Add("Ứ", "u");
            characterMaps.Add("ứ", "u");
            characterMaps.Add("Ừ", "u");
            characterMaps.Add("ừ", "u");
            characterMaps.Add("Ữ", "u");
            characterMaps.Add("ữ", "u");
            characterMaps.Add("Ử", "u");
            characterMaps.Add("ử", "u");
            characterMaps.Add("Ự", "u");
            characterMaps.Add("ự", "u");

            characterMaps.Add("Ý", "y");
            characterMaps.Add("ý", "y");
            characterMaps.Add("Ỳ", "y");
            characterMaps.Add("ỳ", "y");
            characterMaps.Add("Ỹ", "y");
            characterMaps.Add("ỹ", "y");
            characterMaps.Add("Ỷ", "y");
            characterMaps.Add("ỷ", "y");
            characterMaps.Add("Ỵ", "y");
            characterMaps.Add("ỵ", "y");

            characterMaps.Add("đ", "d");
            characterMaps.Add("Đ", "d");
            characterMaps.Add(" ", "-");
        }

        public static string ToSimple(this string str)
        {
            if (str == null) return null;
            string returnString = str.ToLower();
            for (int i = 0; i < returnString.Length; i++)
            {
                string character = returnString[i].ToString();
                if (characterMaps.ContainsKey(character))
                {
                    returnString = returnString.Replace(character, characterMaps[character]);
                }
            }
            return returnString;
        }

        public static string ToSimpleUrl(this string str)
        {
            if (str == null) return null;
            string simple = str.ToSimple();
            simple = simple.ReplaceSpace();
            return simple;
        }

        public static string CutString(this string str, int count)
        {
            if (str == null) return null;
            if (str.Length < count) return str;
            if (count <= 0) return str;
            int cutLength = count;
            for (int i = count - 1; i >= 0; i--)
            {
                if (str[i] == ' ')
                {
                    cutLength = i;
                    break;
                }
            }
            cutLength = cutLength != 0 ? cutLength : count;
            string cut = str.Substring(0, cutLength);
            cut = cut + "...";
            return cut;
        }

        public static string ReplaceSpace(this string str)
        {
            string newStr = str.Replace(' ', '-');
            newStr = newStr.Replace('>', '-');
            newStr = newStr.Replace('>', '-');
            newStr = newStr.Replace('<', '-');
            newStr = newStr.Replace('"', '-');
            newStr = newStr.Replace('.', '-');
            newStr = newStr.Replace(':', '-');
            newStr = newStr.Replace('&', '-');
            newStr = newStr.Replace('/', '-');
            newStr = newStr.Replace("?", "");

            return newStr;
        }

        public static string CamelCase(this string s)
        {
            if (String.IsNullOrEmpty(s)) return null;
            String returnString = "";
            String[] splitStrings = s.Split('_');
            foreach (String sample in splitStrings)
            {
                if (String.IsNullOrEmpty(sample)) continue;
                returnString += sample.First().ToString().ToUpper() + sample.Substring(1);
            }
            return returnString;
        }

        public static string DeCamelCase(this string s)
        {
            if (String.IsNullOrEmpty(s)) return s;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsUpper(s[i]) && i != 0)
                {
                    builder.Append('_');
                }
                builder.Append(char.ToLower(s[i]));
            }
            return builder.ToString();
        }

        public static string ToSimplePropName(this string s)
        {
            return s.Replace("_", "").ToLower();
        }

        const string DATE_FORMAT = "d/M/yyyy";

        public static string ToVNDateFormat(this DateTime dateTime)
        {
            return dateTime.ToString(DATE_FORMAT);
        }

        public static DateTime? ToVNDate(this string dateTime)
        {
            try
            {
                return DateTime.ParseExact(dateTime, DATE_FORMAT, CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

        public static string SetLength(this string str, int maxLength)
        {
            if (str == null)
            {
                return str;
            }
            string newString = str;
            if (str.Length < maxLength)
            {
                for (int i = str.Length; i < maxLength; i++)
                {
                    newString += " ";
                }
                return newString;
            }
            newString = str.Substring(0, maxLength);
            int Count = newString.LastIndexOf(' ');
            if (Count > 0)
            {
                newString = newString.Substring(0, Count);
            }
            return newString + "...";
        }

        public static string FormatMoney(long number)
        {
            return number.ToString("#,#");
        }
    }
}