using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class STRING_EXT
{
    public const int STRING_MAX = 20;
    /// <summary>
    /// Shortence the string if its lenght is more than 20
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string STRING_FORMAT(string s)
    {
        if(s.Length >= STRING_MAX)
        {
            return s.Substring(0, 5) + "...." + s.Substring(s.Length - 6, 5);
        }
        else
        {
            return s;
        }
    }

    /// <summary>
    /// Shortence the long number
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static string STRING_NUMBER_REDUCE(long n)
    {
        string result = n.ToString();

        if (n >= 1000000000)
        {
            result = (Mathf.RoundToInt(n / 1000000000)).ToString() + "B";
        }
        else if (n >= 1000000)
        {
            result = (Mathf.RoundToInt(n / 1000000)).ToString() + "M";
        }
        else if (n >= 1000)
        {
            result = (Mathf.RoundToInt(n / 1000)).ToString() + "K";
        }

        return result;
    }

    public static string NUMBER_FORMAT_DOT(long n)
    {
        string result = string.Format("{0:N0}", n);
        return result;
    }
}
