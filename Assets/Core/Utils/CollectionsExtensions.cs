using System.Collections.Generic;
using UnityEngine;

namespace Core.Utils
{
    public static class CollectionsExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;

            for (int i = n - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}
