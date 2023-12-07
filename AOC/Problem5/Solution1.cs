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
                var seedToSoil = MappingValues(chunks[1], searchValue);
                var soilToFertilizer = MappingValues(chunks[2], seedToSoil);
                var fertilizerToWater = MappingValues(chunks[3], soilToFertilizer);
                var waterToLight = MappingValues(chunks[4], fertilizerToWater);
                var lightToTemperature = MappingValues(chunks[5], waterToLight);
                var temperatureToHumidity = MappingValues(chunks[6], lightToTemperature);
                var humidityToLocation = MappingValues(chunks[7], temperatureToHumidity);

                locationValues.Add(humidityToLocation);
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

        private long MappingValues(string[] chunk, long searchValue)
        {
            for (int i = 1; i < chunk.Length; i++)
            {
                var values = chunk[i].Split(' ');

                if (searchValue >= Int64.Parse(values[1]) && searchValue < Int64.Parse(values[1]) + Int64.Parse(values[2])){
                    return searchValue + (Int64.Parse(values[0]) - Int64.Parse(values[1]));
                }
            }

            return searchValue;
        }
    }
}
