using System;
using System.Collections.Generic;

namespace Eclus.NET.MKO
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> elements, Action<T> action)
        {
            if (elements == null || action == null)
                return;

            foreach (var elem in elements)
                action(elem);
        }
    }
}