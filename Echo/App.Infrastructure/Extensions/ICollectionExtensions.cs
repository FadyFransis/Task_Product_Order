using System;
using System.Collections.Generic;

namespace App.Infrastructure.Extensions
{
    public static class ICollectionExtensions
    {
        public static ICollection<T> AddRange<T>(this ICollection<T>  Destination, ICollection<T> Source)
        {
            foreach (var item in Source)
            {
                Destination.Add(item);
            }
            return Destination;

        }
    }
}
