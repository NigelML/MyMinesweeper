using System.Collections.Generic;
using UnityEngine;

public static class RandomIndicesUtils
{
    // Returns 'count' unique numbers between 0 and 'maxRange'.
    public static List<int> GetUniqueRandomIndices(int count, int maxRange)
    {
        // Create the sorted "deck" [0, 1, 2, 3, ...]
        int[] deck = new int[maxRange];
        for (int i = 0; i < maxRange; i++)
        {
            deck[i] = i;
        }

        // Fisher-Yates Shuffle
        // We only need to shuffle until we reach the number of items we want to pick ('count')
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(i, maxRange);

            // Swap the current item with the selected item.
            int temp = deck[randomIndex];
            deck[randomIndex] = deck[i];
            deck[i] = temp;
        }


        //Get the first 'count' elements
        List<int> result = new List<int>();
        for (int i = 0; i < count; i++)
        {
            result.Add(deck[i]);
        }
        return result;
    }
}