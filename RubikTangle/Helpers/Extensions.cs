using System;
using System.Collections.Generic;
using System.Linq;

namespace RubikTangle.Helpers
{
    public static class Extensions
    {
        public static Random Random { get; } = new Random();

        // http://stackoverflow.com/questions/375351/most-efficient-way-to-randomly-sort-shuffle-a-list-of-integers-in-c-sharp/375446#375446
        public static IEnumerable<T> RandomPermutation<T>(this IEnumerable<T> sequence)
        {
            T[] retArray = sequence.ToArray();

            for (int i = 0; i < retArray.Length - 1; i += 1)
            {
                int swapIndex = Random.Next(i, retArray.Length);
                if (swapIndex != i)
                {
                    T temp = retArray[i];
                    retArray[i] = retArray[swapIndex];
                    retArray[swapIndex] = temp;
                }
            }

            return retArray;
        }
    }
}
