using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem4
{
    public class Solution1
    {
        public double Solution(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            var sum = 0.0;

            foreach (string line in lines)
            {
                sum += ParseCard(line);
            }

            return sum;
        }

        private double ParseCard(string card)
        {
            int delimiterIndex = card.IndexOf(':');
            string cardValues = card.Substring(delimiterIndex + 1).Trim();

            List<int> winningNumbers = cardValues.Split('|')[0].Trim().Split(' ')
                          .Where(s => !string.IsNullOrWhiteSpace(s))
                          .Select(int.Parse)
                          .ToList();

            List<int> arashNumbers = cardValues.Split('|')[1].Trim().Split(' ')
                          .Where(s => !string.IsNullOrWhiteSpace(s))
                          .Select(int.Parse)
                          .ToList();


            var matches = winningNumbers.Intersect(arashNumbers);
            var points = Math.Pow(2, matches.Count() - 1);


            return points < 1 ? 0 : points;
        }
    }
}
