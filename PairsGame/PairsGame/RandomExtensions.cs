using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PairsGame
{
    public static class RandomExtensions
    {
        private static Random rng = new Random();
        public static void Shuffle<T>(Random random, List<List<T>> list)
        {
            int lengthRow = list[0].Count;

            for (int i = list.Count * lengthRow - 1; i > 0; i--)
            {
                int i0 = i / lengthRow;
                int i1 = i % lengthRow;

                int j = random.Next(i + 1);
                int j0 = j / lengthRow;
                int j1 = j % lengthRow;

                T temp = list[i0][i1];
                list[i0][i1] = list[j0][j1];
                list[j0][j1] = temp;
            }
        }

            public static void Shuffle<T>(this IList<T> list)
            {
                int n = list.Count;
                while (n > 1)
                {
                    n--;
                    int k = rng.Next(n + 1);
                    T value = list[k];
                    list[k] = list[n];
                    list[n] = value;
                }
            }
        
    }
}
