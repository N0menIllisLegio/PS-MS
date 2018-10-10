using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static Instruments.Data;

namespace Instruments
{
    public class Extracter
    {
        private static string Rows; 

        static List<byte> IdRow = new List<byte>();
        static List<int> DeathsRow = new List<int>();
        static List<int> DeathRateRow = new List<int>();
        
        private static void ExtractValues()
        {
            string rowPattern =
                @"(?<id>\d+)\,(?<state>(\w+|\w+\s\w+))\,(?<deaths>\d+)\,(?<deathrate>(\d+|\d+\.\d+))\,(?<other>.*)";
            string separatorPattern = @"\.";
            Regex valueParser = new Regex(rowPattern);
            Regex separator = new Regex(separatorPattern);

            MatchCollection matches = valueParser.Matches(Rows);
            foreach (Match match in matches)
            {
                var id = match.Groups["id"].Value;
                string state = match.Groups["state"].Value;
                var deaths = match.Groups["deaths"].Value;
                var deathRate = match.Groups["deathrate"].Value;
                deathRate = separator.Replace(deathRate, ",");

                IdRow.Add(Byte.Parse(id));
                DeathsRow.Add(Int32.Parse(deaths));
                DeathRateRow.Add((int)Single.Parse(deathRate) * 10);
                
                if (!States.ContainsValue(state))
                {
                    States.Add(IdRow.Last(), match.Groups["state"].Value);
                }
            }
        }

        private static void CreateDataSets()
        {
            IdDescreteRow = IdRow.ToArray();
            DeathsIntervalRow = DeathsRow.ToArray();
            DeathRateIntervalRow = DeathRateRow.ToArray();

            Array.Sort(IdDescreteRow);
            Array.Sort(DeathsIntervalRow);
            Array.Sort(DeathRateIntervalRow);
        }

        public static void Extract(string DB_File)
        {
            Rows = File.ReadAllText(DB_File);
            ExtractValues();
            CreateDataSets();
        }
    }
}
