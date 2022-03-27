// https://contest.yandex.ru/contest/23815/run-report/66483350/
// после рефакторинга https://contest.yandex.ru/contest/23815/run-report/66502252/

/* 
 * -- ПРИНЦИП РАБОТЫ --
 * Выбираем случайным образом опорный элемент, используя класс Random из стандартной библиотеки .NET.
 * Дальше сортируем алгоритмом быстрой сортировки, меняя элементы местами в исходном массиве.
 * Из-за этого сортировка будет нестабильной, но зато более экономной по памяти.
 * 
 * -- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
 * Алгоритв верен, поскольку на каждом этапе мы
 * 1) не довавляем новые и не удаляем никакие элементы из массива
 * 2) каждый рекурсивный спуск переставляет элементы так, чтобы они располагались в порядке неубывания
 * 3) спуск происходит вплоть до одного элемента, поэтому все элементы окажутся правильно раставленными
 * 
 * -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
 * В общем случае О(n^2), но в среднем O(n*log(n)), считалась на уроке. При больших n вероятность, 
 * что всегда будет выбираться
 * гриничный элемент стремится к нулю.
 * 
 * -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
 * O(1), мы храним все элементы исходного массива, но не используем дополнительную память.
 */


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
            var persons = ReadPersons(n);

            QuickSort(persons, 0, n - 1);

            PrintResult(persons);

            CloseStreams();
        }

        private static void QuickSort(List<Person> persons, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(persons, left, right);
                if (pivot > 1)
                {
                    QuickSort(persons, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    QuickSort(persons, pivot + 1, right);
                }
            }
        }

        private static int Partition(List<Person> persons, int left, int right)
        {
            var pivot = persons[_random.Next(left, right)];
            while (true)
            {
                while (persons[left] < pivot)
                {
                    left++;
                }

                while (persons[right] > pivot)
                {
                    right--;
                }
                if (left < right)
                {
                    if (persons[left] == persons[right]) return right;

                    var temp = persons[left];
                    persons[left] = persons[right];
                    persons[right] = temp;
                }
                else
                {
                    return right;
                }
            }
        }

        private static void PrintResult(List<Person> persons)
        {
            var result = string.Join('\n', persons.Select(p => p.Name));
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

        private static List<Person> ReadPersons(int n)
        {
            var persons = new List<Person>();
            for (var i = 0; i < n; i++)
            {
                var items = _reader.ReadLine()
                .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries);
                persons.Add(new Person
                {
                    Name = items[0],
                    Score = int.Parse(items[1]),
                    Fine = int.Parse(items[2])
                });
            }
            return persons;
        }
    }

    // В этом же файле, чтобы можно было загрузить в проверяющую систему
    internal class Person : IComparable<Person>
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public int Fine { get; set; }

        public int CompareTo(Person other)
        {
            return (-Score, Fine, Name).CompareTo((-other.Score, other.Fine, other.Name));
        }

        public static bool operator <(Person op1, Person op2)
        {
            return op1.CompareTo(op2) < 0;
        }

        public static bool operator >(Person op1, Person op2)
        {
            return op1.CompareTo(op2) > 0;
        }

    }
}
