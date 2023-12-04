using AOC.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    public class Problem3
    {
        public int Solution1(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            char[,] matrix = CreateMatrixFromLines(lines);

            var matrixHeight = matrix.GetLength(0);
            var matrixLength = matrix.GetLength(1);

            var currentNumber = "";
            var isValidNumber = false;
            var validNumbers = FindValidNumbers(matrix, matrixLength, matrixHeight, ref currentNumber, ref isValidNumber);

            int sumOfNumbers = CalculateValidNumberSum(validNumbers);

            return sumOfNumbers;
        }

        public int Solution2(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            char[,] matrix = CreateMatrixFromLines(lines);

            var matrixHeight = matrix.GetLength(0);
            var matrixLength = matrix.GetLength(1);

            var currentNumber = "";
            var gearDictionary = new Dictionary<Tuple<int,int>, List<string>>();

            FindGearNumbers(gearDictionary, matrix, matrixLength, matrixHeight, ref currentNumber);

            var filteredDictionary = FilterGearDictionary(gearDictionary);

            int sum = CalculateGears(filteredDictionary);
            return sum;
        }

        private int CalculateGears(Dictionary<Tuple<int, int>, List<int>> filteredDictionary)
        {
            var sum = 0;

            foreach ( var kvp in filteredDictionary )
            {
                sum += kvp.Value.Aggregate((currentProduct, nextValue) => currentProduct * nextValue);
            }

            return sum;
        }

        private Dictionary<Tuple<int, int>, List<int>> FilterGearDictionary(Dictionary<Tuple<int, int>, List<string>> gearDictionary)
        {
            return gearDictionary
            .Where(kv => kv.Value.Count == 2)
            .ToDictionary(
                kv => kv.Key,
                kv => kv.Value.Select(int.Parse).ToList());
        }

        private int CalculateValidNumberSum(List<string> validNumbers)
        {
            var sum = 0;

            foreach(var validNumber in validNumbers)
            {
                sum += Int32.Parse(validNumber);
            }

            return sum;
        }

        private List<string> FindValidNumbers(char[,] matrix, int matrixLength, int matrixHeight, ref string currentNumber, ref bool isValidNumber)
        {
            var validNumbers = new List<string>();

            for (int i = 0; i < matrixHeight; i++)
            {
                for (int j = 0; j < matrixLength; j++)
                {
                    if (Char.IsNumber(matrix[i, j]))
                    {
                        isValidNumber = ValidateNumber(matrix, isValidNumber, i, j);
                        currentNumber += matrix[i, j];
                    }
                    else
                    {
                        if (isValidNumber)
                        {
                            validNumbers.Add(currentNumber);
                            isValidNumber = false;
                        }

                        currentNumber = "";
                    }
                }

                if (isValidNumber)
                    validNumbers.Add(currentNumber);

                isValidNumber = false;
                currentNumber = "";
            }

            return validNumbers;
        }

        private void FindGearNumbers(Dictionary<Tuple<int, int>, List<string>> gearDictionary, char[,] matrix, int matrixLength, int matrixHeight, ref string currentNumber)
        {
            
            for (int i = 0; i < matrixHeight; i++)
            {
                var gearNumberIndex = new List<Tuple<int, int>>();
                List<Tuple<int, int>> uniqueList = new List<Tuple<int, int>>();

                for (int j = 0; j < matrixLength; j++)
                {
                    if (Char.IsNumber(matrix[i, j]))
                    {
                        GetGearAdjacent(matrix, i, j, gearNumberIndex);
                        currentNumber += matrix[i, j];
                    }
                    else
                    {
                        uniqueList = gearNumberIndex.Distinct().ToList();

                        if (currentNumber != "")
                        {
                            foreach (var tuple in uniqueList)
                            {
                                if (gearDictionary.ContainsKey(tuple))
                                {
                                    gearDictionary[tuple].Add(currentNumber);
                                }

                                else
                                {
                                    gearDictionary.Add(tuple, new List<string>() { currentNumber });
                                }
                            }

                            currentNumber = "";
                        }

                        gearNumberIndex = new List<Tuple<int, int>>();
                    }
                }

                uniqueList = gearNumberIndex.Distinct().ToList();

                if (currentNumber != "")
                {
                    foreach (var tuple in uniqueList)
                    {
                        if (gearDictionary.ContainsKey(tuple))
                        {
                            gearDictionary[tuple].Add(currentNumber);
                        }

                        else
                        {
                            gearDictionary.Add(tuple, new List<string>() { currentNumber });
                        }
                    }

                    currentNumber = "";
                }
                
            }

        }

        private bool ValidateNumber(char[,] matrix, bool isValidNumber, int i, int j)
        {
            List<char> adjacentElements = GetAdjacent(matrix, i, j);
            foreach (char element in adjacentElements)
            {
                if (element != '.' && !Char.IsNumber(element))
                {
                    isValidNumber = true;
                }
            }

            return isValidNumber;
        }

        private char[,] CreateMatrixFromLines(string[] lines)
        {
            var rowCount = lines.Length;
            var columnCount = lines.First().Length;
            char[,] matrix = new char[rowCount, columnCount];

            for (int i = 0; i < rowCount; i++)
            {
                var line = lines[i];

                for (int j = 0; j < columnCount; j++)
                {
                    matrix[i, j] = line[j];
                }
            }

            return matrix;
        }

        private bool isValidPos(int i, int j, int n, int m)
        {
            if (i < 0 || j < 0 || i > n - 1 || j > m - 1)
                return false;
            return true;
        }

        private List<Tuple<int, int>> GetGearAdjacent(char[,] arr, int i, int j, List<Tuple<int, int>> v) {
            // Size of given 2d array
            var n = arr.GetLength(0);
            var m = arr.GetLength(1);

            // Checking for all the possible adjacent positions
            if (isValidPos(i - 1, j - 1, n, m) && arr[i - 1, j - 1] == '*')
            { v.Add(Tuple.Create(i - 1, j - 1)); }
            if (isValidPos(i - 1, j, n, m) && arr[i - 1, j] == '*')
            { v.Add(Tuple.Create(i - 1, j)); }
            if (isValidPos(i - 1, j + 1, n, m) && arr[i - 1, j + 1] == '*')
            { v.Add(Tuple.Create(i - 1, j + 1)); }
            if (isValidPos(i, j - 1, n, m) && arr[i, j - 1] == '*')
            { v.Add(Tuple.Create(i, j - 1)); }
            if (isValidPos(i, j + 1, n, m) && arr[i, j + 1] == '*')
            { v.Add(Tuple.Create(i, j + 1)); }
            if (isValidPos(i + 1, j - 1, n, m) && arr[i + 1, j - 1] == '*')
            { v.Add(Tuple.Create(i + 1, j - 1)); }
            if (isValidPos(i + 1, j, n, m) && arr[i + 1, j] == '*')
            { v.Add(Tuple.Create(i + 1, j)); }
            if (isValidPos(i + 1, j + 1, n, m) && arr[i + 1, j + 1] == '*')
            { v.Add(Tuple.Create(i + 1, j + 1)); }

            // Returning the list
            return v;
        }

        private List<char> GetAdjacent(char[,] arr, int i, int j)
        {
            // Size of given 2d array
            var n = arr.GetLength(0);
            var m = arr.GetLength(1);

            // Initialising a list
            // where adjacent element will be stored

            List<char> v = new List<char>();

            // Checking for all the possible adjacent positions
            if (isValidPos(i - 1, j - 1, n, m))
            { v.Add(arr[i - 1, j - 1]); }
            if (isValidPos(i - 1, j, n, m))
            { v.Add(arr[i - 1, j]); }
            if (isValidPos(i - 1, j + 1, n, m))
            { v.Add(arr[i - 1, j + 1]); }
            if (isValidPos(i, j - 1, n, m))
            { v.Add(arr[i, j - 1]); }
            if (isValidPos(i, j + 1, n, m))
            { v.Add(arr[i, j + 1]); }
            if (isValidPos(i + 1, j - 1, n, m))
            { v.Add(arr[i + 1, j - 1]); }
            if (isValidPos(i + 1, j, n, m))
            { v.Add(arr[i + 1, j]); }
            if (isValidPos(i + 1, j + 1, n, m))
            { v.Add(arr[i + 1, j + 1]); }

            // Returning the list
            return v;
        }
    }
}
