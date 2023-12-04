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

        private List<char> GetAdjacent(char[,] arr, int i, int j) {
            // Size of given 2d array
            var n = arr.GetLength(0);
            var m = arr.GetLength(1);

            // Initialising a list
            // where adjacent element will be stored

            List<char> v = new List<char>();

            // Checking for all the possible adjacent positions
            if (isValidPos(i - 1, j - 1, n, m))
            { v.Add(arr[i - 1,j - 1]); }
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
