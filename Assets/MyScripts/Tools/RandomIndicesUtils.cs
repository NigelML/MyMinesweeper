using System.Collections.Generic;
using UnityEngine;

public static class RandomIndicesUtils
{
    // Retorna 'count' números únicos entre 0 e 'maxRange'
    public static List<int> GetUniqueRandomIndices(int count, int maxRange)
    {
        // 1. Criar o "baralho" ordenado [0, 1, 2, 3, ...]
        int[] deck = new int[maxRange];
        for (int i = 0; i < maxRange; i++)
        {
            deck[i] = i;
        }

        // 2. Fisher-Yates Shuffle (Embaralhar)
        // Só precisamos embaralhar até o número de itens que queremos pegar ('count')
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(i, maxRange);
            
            // Troca (Swap) o elemento atual com o sorteado
            int temp = deck[randomIndex];
            deck[randomIndex] = deck[i];
            deck[i] = temp;
        }

        // 3. Pegar os primeiros 'count' elementos
        List<int> result = new List<int>();
        for (int i = 0; i < count; i++)
        {
            result.Add(deck[i]);
        }

        return result;
    }
}