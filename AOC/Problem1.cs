using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    public class Problem1
    {
        private double total = 0;
        private string firstNumber;
        private string secondNumber;

        public double Solution1(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                foreach (char c in line)
                {
                    if (Char.IsNumber(c))
                    {
                        firstNumber = Convert.ToString(Char.GetNumericValue(c));
                        break;
                    }
                }

                var reversedLine = ReverseString(line);

                foreach (char c in reversedLine)
                {
                    if (Char.IsNumber(c))
                    {
                        secondNumber = Convert.ToString(Char.GetNumericValue(c));
                        break;
                    }
                }

                string joinNumber = firstNumber + secondNumber;
                total += Int32.Parse(joinNumber);
            }

            return total;
        }

        private string ReverseString(string line)
        {
            char[] charArray = line.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public double Solution2(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                Tuple<string, int, int>[] indexesText = findOccurenceIndexes(line);
                int loop1 = 0;
                int loop2 = 0;

                foreach (char c in line)
                {
                    loop1++;

                    if (Char.IsNumber(c))
                    {
                        firstNumber = findFirstIndex(indexesText, Convert.ToString(Char.GetNumericValue(c)), line);
                        break;
                    }
                    if (line.Length == loop1)
                    {
                        firstNumber = findFirstIndexOfText(indexesText);
                    }

                    
                }

                

                var reversedLine = ReverseString(line);

                foreach (char c in reversedLine)
                {
                    loop2++;
                    if (Char.IsNumber(c))
                    {
                        secondNumber = findLastIndex(indexesText, Convert.ToString(Char.GetNumericValue(c)), line);
                        break;
                    }

                    if(line.Length == loop2)
                    {
                        secondNumber = findLastIndexOfText(indexesText);
                    }
                    
                }

                string joinNumber = firstNumber + secondNumber;
                total += Int32.Parse(joinNumber);
            }

            return total;
        }

        private Tuple<string, int, int>[] findOccurenceIndexes(string line)
        {
            Tuple<string, int, int>[] indexesNumber =
            {

                Tuple.Create("1", line.IndexOf("one"), line.LastIndexOf("one")),
                Tuple.Create("2", line.IndexOf("two"), line.LastIndexOf("two")),
                Tuple.Create("3", line.IndexOf("three"), line.LastIndexOf("three")),
                Tuple.Create("4", line.IndexOf("four"), line.LastIndexOf("four")),
                Tuple.Create("5", line.IndexOf("five"),line.LastIndexOf("five")),
                Tuple.Create("6", line.IndexOf("six"), line.LastIndexOf("six")),
                Tuple.Create("7", line.IndexOf("seven"), line.LastIndexOf("seven")),
                Tuple.Create("8", line.IndexOf("eight"), line.LastIndexOf("eight")),
                Tuple.Create("9", line.IndexOf("nine"), line.LastIndexOf("nine")),
                Tuple.Create("0", line.IndexOf("zero"), line.LastIndexOf("zero"))
            };

           return indexesNumber;
        }

        private string findFirstIndex(Tuple<string, int, int>[] indexesNumber, string firstNumber, string line)
        {
            var min = indexesNumber.Where(t => t.Item2 != -1)?.OrderBy(t => t.Item2)?.FirstOrDefault()?.Item1;
            var minValue = indexesNumber.Where(t => t.Item2 != -1)?.OrderBy(t => t.Item2)?.FirstOrDefault()?.Item2;

            if (min == null || !min.Any())
                return firstNumber;
            else if (minValue >= line.IndexOf(firstNumber))
                return firstNumber;
            else
                return min;
        }


        private string findFirstIndexOfText(Tuple<string, int, int>[] indexesNumber)
        {
            var min = indexesNumber.Where(t => t.Item2 != -1)?.OrderBy(t => t.Item2)?.FirstOrDefault()?.Item1;
            var minValue = indexesNumber.Where(t => t.Item2 != -1)?.OrderBy(t => t.Item2)?.FirstOrDefault()?.Item2;

            return min;
        }

        private string findLastIndex(Tuple<string, int, int>[] indexesNumber, string lastNumber, string line)
        {
            var max = indexesNumber.Where(t => t.Item3 != -1)?.OrderByDescending(t => t.Item3)?.FirstOrDefault()?.Item1;
            var maxValue = indexesNumber.Where(t => t.Item3 != -1)?.OrderByDescending(t => t.Item3)?.FirstOrDefault()?.Item3;

            if (max == null || !max.Any())
                return lastNumber;
            else if (maxValue <= line.IndexOf(lastNumber))
                return lastNumber;
            else
                return max;
        }

        private string findLastIndexOfText(Tuple<string, int, int>[] indexesNumber)
        {
            var max = indexesNumber.Where(t => t.Item3 != -1)?.OrderByDescending(t => t.Item3)?.FirstOrDefault()?.Item1;
            var maxValue = indexesNumber.Where(t => t.Item3 != -1)?.OrderByDescending(t => t.Item3)?.FirstOrDefault()?.Item3;

            return max;
        }
    }
}
