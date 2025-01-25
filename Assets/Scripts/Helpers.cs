using System.Collections.Generic;
using UnityEngine;

public static class Helpers 
{ 
    public static string DictionaryToString<TKey,TValue>(Dictionary<TKey,TValue> dictionary)
    {
        var text = "";
        
        foreach (var damageSource in dictionary)
        {
            text = $"{damageSource.Key}:{damageSource.Value}\n";
        }
        return text;
    }
}
