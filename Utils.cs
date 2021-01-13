

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KoananVPN
{
  internal class Utils
  {
    public static string LRParse(
      string data,
      string lS,
      string rS,
      bool recursive = false,
      bool useRegexLR = false)
    {
      string input = data;
      List<string> stringList = new List<string>();
      if (lS == "" && rS == "")
        return data;
      if (!input.Contains(lS) && lS != "" || !input.Contains(rS) && rS != "")
        return "";
      string str1 = (string) null;
      if (recursive)
      {
        if (useRegexLR)
        {
          try
          {
            string pattern = Utils.BuildLRPattern(lS, rS);
            foreach (Capture match in Regex.Matches(input, pattern))
              str1 = match.Value;
            return str1;
          }
          catch
          {
            return str1;
          }
        }
        else
        {
          try
          {
            string str2;
            for (; (input.Contains(lS) || lS == "") && (input.Contains(rS) || rS == ""); input = str2.Substring(str1.Length + rS.Length))
            {
              int startIndex = lS == "" ? 0 : input.IndexOf(lS) + lS.Length;
              str2 = input.Substring(startIndex);
              int length = rS == "" ? str2.Length - 1 : str2.IndexOf(rS);
              str1 = str2.Substring(0, length);
            }
            return str1;
          }
          catch
          {
            return str1;
          }
        }
      }
      else
      {
        if (useRegexLR)
        {
          string pattern = Utils.BuildLRPattern(lS, rS);
          MatchCollection matchCollection = Regex.Matches(input, pattern);
          if (matchCollection.Count > 0)
            str1 = matchCollection[0].Value;
        }
        else
        {
          try
          {
            int startIndex = lS == "" ? 0 : input.IndexOf(lS) + lS.Length;
            string str2 = input.Substring(startIndex);
            int length = rS == "" ? str2.Length : str2.IndexOf(rS);
            str1 = str2.Substring(0, length);
          }
          catch
          {
          }
        }
        return str1;
      }
    }

    public static string BuildLRPattern(string ls, string rs) => "(?<=" + (string.IsNullOrEmpty(ls) ? "^" : Regex.Escape(ls)) + ").+?(?=" + (string.IsNullOrEmpty(rs) ? "$" : Regex.Escape(rs)) + ")";
  }
}
