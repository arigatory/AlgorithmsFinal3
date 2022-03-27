// https://contest.yandex.ru/contest/23815/run-report/66461801/
using System;
using System.Collections.Generic;

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

