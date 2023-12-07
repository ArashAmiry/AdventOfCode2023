using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Problem5
{
    public class Solution1
    {
        public double Solution(string filePath)
        {
            var locationValues = new List<long>();

            string[] lines = File.ReadAllLines(filePath);

            List<string[]> chunks = SplitArrayIntoChunks(lines, "");

            var seeds = chunks[0][0].Split(' ');

            for (int i = 1; i < seeds.Length; i++)
            {
                // Value to search for
                long searchValue = Int64.Parse(seeds[i]);
                var seedToLocation = MappingValues(chunks, searchValue, 1);
                locationValues.Add(seedToLocation);
            }
            return locationValues.Min();
        }

        private List<T[]> SplitArrayIntoChunks<T>(T[] array, T separator)
        {
            List<T[]> chunks = new List<T[]>();
            List<T> currentChunk = new List<T>();

            foreach (T element in array)
            {
                if (EqualityComparer<T>.Default.Equals(element, separator))
                {
                    if (currentChunk.Any())
                    {
                        chunks.Add(currentChunk.ToArray());
                        currentChunk.Clear();
                    }
                }
                else
                {
                    currentChunk.Add(element);
                }
            }

            if (currentChunk.Any())
            {
                chunks.Add(currentChunk.ToArray());
            }

            return chunks;
        }

        private long MappingValues(List<string[]> chunks, long searchValue, int index)
        {

            if (index >= chunks.Count - 1)
            {
                for (int i = 1; i < chunks[index].Length; i++)
                {
                    var values = chunks[index][i].Split(' ');

                    if (searchValue >= Int64.Parse(values[1]) && searchValue < Int64.Parse(values[1]) + Int64.Parse(values[2]))
                    {
                        return searchValue + (Int64.Parse(values[0]) - Int64.Parse(values[1]));
                    }
                }

                return searchValue;
            }

            for (int i = 1; i < chunks[index].Length; i++)
            {
                var values = chunks[index][i].Split(' ');

                if (searchValue >= Int64.Parse(values[1]) && searchValue < Int64.Parse(values[1]) + Int64.Parse(values[2])){
                    
                    return MappingValues(chunks, searchValue + (Int64.Parse(values[0]) - Int64.Parse(values[1])), index + 1);
                }
            }

            return MappingValues(chunks, searchValue, index + 1);
        }
    }
}
