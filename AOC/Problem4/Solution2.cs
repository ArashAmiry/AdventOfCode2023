using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem4
{
    public class Solution2
    {
        public double Solution(string filePath)
        {
            var scratchCards = new Dictionary<int, int>();

            string[] lines = File.ReadAllLines(filePath);

            for (int i = 0; i < lines.Length; i++)
            {
                scratchCards[i] = 1;
            }
            
            for (int i = 0; i < lines.Length; i++)
            {
                var cardPoints = CardPoints(lines[i], i);

                for (int j = i + 1; j <= i + cardPoints; j++)
                {
                    scratchCards[j] = scratchCards[j] + scratchCards[i];
                }
            }

            return scratchCards.Values.Sum();
        }

        private double CardPoints(string card, int index)
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
            var points = matches.Count();


            return points < 1 ? 0 : points;
        }
    }
}
