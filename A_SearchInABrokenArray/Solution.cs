// https://contest.yandex.ru/contest/23815/run-report/66461801/
using System;
using System.Collections.Generic;


/* 
 * -- ПРИНЦИП РАБОТЫ --
 * 2 раза применяем бинарный поиск. Первый раз для поиска наименьшего элемента массива. Для этого мы сравниваем первый элемент со средним.
 * В результате сравнения понимаем, что если они упорядочены, то первая часть массива отсортирована. Значит, нужно искать в другой.
 * Аналогично наоборот.
 * После того как мы нашли стык, можем отталкиваться от него, считая, что это 0 (в новых координатах).
 * После чего ищем требуемый элемент классическим бинарным поиском, учитывая, что индексы должны быть сдвинуты на число, которое нашли в 
 * первой части решения.
 * 
 * -- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
 * Алгоритм верен, поскольку на каждом этапе мы просматриваем все элементы, потонциально могут быть нам интересны. Не просматриваем те,
 * которые заведомо не подходят по условию.
 * 
 * -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
 * О(log(n)),так как на обоих этапах мы используем бинарный поиск, который требует О(log(n))
 * О(log(n) + log(n)) = О(2log(n)) = О(log(n))
 * 
 * -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
 * O(1), мы храним все элементы исходного массива, но не используем дополнительную память.
 */

public class Solution
{
    public static int BrokenSearch(List<int> array, int el)
    {
        int lo = 0;
        int n = array.Count;
        int hi = n - 1;

        while (lo < hi)
        {
            int mid = (hi + lo) >> 1;

            if (array[0] < array[mid])
            {
                lo = mid + 1;
            }
            else
            {
                hi = mid - 1;
            }
        }

        if (array[lo] == el)
        {
            return lo;
        }

        lo = (lo + 1) % n;
        hi = lo + n - 1;
        while (lo < hi)
        {
            int mid = (hi + lo) >> 1;
            int i = mid % n;
            if (el == array[i])
            {
                return i;

            }
            else if (el < array[i])
            {
                hi = mid;
            }
            else
            {
                lo = mid + 1;
            }
        }

        hi %= n;
        if (array[hi] == el)
        {
            return hi;
        }
        return -1;
    }

    private static void Test()
    {
        var arr = new List<int> { 19, 21, 100, 101, 1, 4, 5, 7, 12 };
        Console.WriteLine(BrokenSearch(arr, 5) == 6);
    }
}

