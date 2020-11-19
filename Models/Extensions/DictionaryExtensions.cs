using System.Collections.Generic;
using System.Linq;

namespace ZappitBugTracker.Models.Extensions
{
    public static class DictionaryExtensions
    {
        public static Dictionary<string, dynamic> GetDifferences(this Dictionary<string, dynamic> src, Dictionary<string, dynamic> target)
        {
            // how to check objects that are in the dictionary

            return src.Where(kvp => kvp.Value != target[kvp.Key]).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
    }
}
