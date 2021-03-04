using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Localization
{
    private static readonly Dictionary<string, string []> local = new Dictionary<string, string []>
    {
        { "info", new string[] { "INFO", "»Õ‘Œ"} },

    };

    public static string GetString(string str)
    {
        return local [str] [PlayerStats.GetPlayerLanguage ()];
    }

}
