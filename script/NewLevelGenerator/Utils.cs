using System.Collections;
using System.Collections.Generic;
using System;

static class Utils
{
    public static void ShuffleArray<T>(T[] array)
    {
        System.Random random = new System.Random();

        for (int i = array.Length - 1; i > 0; i--)
        {
            // Generate a random index within the remaining unshuffled part of the array
            int randomIndex = random.Next(0, i + 1);

            // Swap the elements at randomIndex and i
            (array[randomIndex], array[i]) = (array[i], array[randomIndex]);
        }
    }

    public static int Sum(List<int> numbers)
    {
        int sum = 0;
        foreach (int number in numbers)
        {
            sum += number;
        }
        return sum;
    }

    public static int Sum(int[] numbers)
    {
        int sum = 0;
        foreach (int number in numbers)
        {
            sum += number;
        }
        return sum;
    }
}

