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
    }
}
