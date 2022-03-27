// https://contest.yandex.ru/contest/23815/run-report/66483350/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace B_EfficientQuicksort
{
    public class Solution
    {
        private static TextReader _reader = new StreamReader(Console.OpenStandardInput());
        private static TextWriter _writer = new StreamWriter(Console.OpenStandardOutput());
        private static Random _random = new();
        public static void Main(string[] args)
        {
            var n = ReadInt();
            var participants = ReadParticipants(n);

            QuickSort(participants, 0, n - 1);

            PrintResult(participants);

            CloseStreams();
        }

        private static void QuickSort(List<(int, int, string)> participants, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(participants, left, right);
                if (pivot > 1)
                {
                    QuickSort(participants, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    QuickSort(participants, pivot + 1, right);
                }
            }
        }

        private static int Partition(List<(int, int, string)> items, int left, int right)
        {
            var pivot = items[_random.Next(left, right)];
            while (true)
            {
                while (items[left].CompareTo(pivot) == -1)
                {
                    left++;
                }

                while (items[right].CompareTo(pivot) == 1)
                {
                    right--;
                }
                if (left < right)
                {
                    if (items[left] == items[right]) return right;

                    var temp = items[left];
                    items[left] = items[right];
                    items[right] = temp;
                }
                else
                {
                    return right;
                }
            }
        }

        private static void PrintResult(List<(int, int, string)> participants)
        {
            var result = string.Join('\n', participants.Select(p => p.Item3));
            _writer.WriteLine(result);
        }

        private static void CloseStreams()
        {
            _reader.Close();
            _writer.Close();
        }

        private static int ReadInt()
        {
            return int.Parse(_reader.ReadLine());
        }

        private static List<(int, int, string)> ReadParticipants(int n)
        {
            var participants = new List<(int, int, string)>();
            for (var i = 0; i < n; i++)
            {
                var items = _reader.ReadLine()
                .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries);
                participants.Add((-int.Parse(items[1]), int.Parse(items[2]), items[0]));
            }
            return participants;
        }
    }

}
