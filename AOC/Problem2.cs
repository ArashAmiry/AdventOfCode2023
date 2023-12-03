using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    public class Problem2
    {
        private int RedAmount = 12;
        private int GreenAmount = 13;
        private int BlueAmount = 14;
        private int idSum = 0;
        private int gameID = 0;
        public int Solution1(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            

            foreach (string line in lines)
            {
                bool validGame = true;
                gameID++;

                int delimiterIndex = line.IndexOf(':');
                string gameLine = line.Substring(delimiterIndex + 1);

                string[] roundValues = gameLine.Split(new Char[] {';',','}).Select(value => value.Trim()).ToArray();

                foreach (string roundValue in roundValues)
                {
                    if (roundValue.Contains("red") && (Int32.Parse(roundValue.Split(' ').ToArray().First()) > RedAmount) ||
                        roundValue.Contains("green") && (Int32.Parse(roundValue.Split(' ').ToArray().First()) > GreenAmount) ||
                        (roundValue.Contains("blue") && (Int32.Parse(roundValue.Split(' ').ToArray().First()) > BlueAmount)))
                    {    
                        validGame = false;
                        break;
                    }
                }

                if (validGame)
                    idSum += gameID;
            }

            return idSum;
        }

        public int Solution2(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            List<int> gameValues = new List<int> { };
            var gameSum = 0;

            foreach (string line in lines)
            {
                var redMax = 0;
                var greenMax = 0;
                var blueMax = 0;

                int delimiterIndex = line.IndexOf(':');
                string gameLine = line.Substring(delimiterIndex + 1);

                string[] roundValues = gameLine.Split(new Char[] { ';', ',' }).Select(value => value.Trim()).ToArray();

                foreach (string roundValue in roundValues)
                {
                    if (roundValue.Contains("red") && (Int32.Parse(roundValue.Split(' ').ToArray().First()) > redMax)){
                        redMax = Int32.Parse(roundValue.Split(' ').ToArray().First());
                    }
                    else if (roundValue.Contains("green") && (Int32.Parse(roundValue.Split(' ').ToArray().First()) > greenMax)){
                        greenMax = Int32.Parse(roundValue.Split(' ').ToArray().First());
                    }
                    else if (roundValue.Contains("blue") && (Int32.Parse(roundValue.Split(' ').ToArray().First()) > blueMax)){
                        blueMax = Int32.Parse(roundValue.Split(' ').ToArray().First());
                    }
                }

                gameValues.Add(redMax * greenMax *  blueMax);
            }

            foreach (int gameValue in gameValues)
            {
                gameSum += gameValue;
            }

            return gameSum;
        }
    }
}
