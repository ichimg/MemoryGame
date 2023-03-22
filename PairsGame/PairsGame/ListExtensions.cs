using System;
using System.Collections.Generic;

namespace PairsGame
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this List<List<T>> list)
        {
            Random random = new Random();
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
                Random random = new Random();
                int n = list.Count;
                while (n > 1)
                {
                    n--;
                    int k = random.Next(n + 1);
                    T value = list[k];
                    list[k] = list[n];
                    list[n] = value;
                }
            }
        
    }
}
