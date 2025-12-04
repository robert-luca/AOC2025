using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC2025.Day3
{
    internal class Day3
    {
        private readonly string path;
        private readonly List<BatteryBank> BatteryBanks = [];
        public Day3()
        {
            path = Path.Combine(AppContext.BaseDirectory, "Day3", "Input.txt");
            if (!File.Exists(path))
                throw new FileNotFoundException($"Required input file not found: {path}");

            using var reader = new StreamReader(path);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                BatteryBanks.Add(new BatteryBank(line));
            }
        }

        public int P1()
        {
            int sum = 0;
            foreach (var bank in BatteryBanks)
            {
                int high = 0;
                int low = 0;
                for (int i = 9; i >= 1; i--)
                {
                    var x = bank.Batteries.IndexOf(i);
                    if (x == -1)
                        continue;

                    if (x == bank.Batteries.Count - 1)
                        continue;

                    if (x == bank.Batteries.Count - 2)
                    {
                        high = bank.Batteries[x];
                        low = bank.Batteries[bank.Batteries.Count - 1];
                        break;
                    }
                    else
                    {
                        high = bank.Batteries[x];
                        for (int j = 9; j >= 1; j--)
                        {
                            var y = bank.Batteries.IndexOf(j, x + 1);
                            if (y == -1)
                            {
                                continue;
                            }
                            else
                            {
                                low = bank.Batteries[y];
                                break;
                            }
                        }
                        break;
                    }
                }
                if (high != 0 && low != 0)
                {
                    string concat = high.ToString() + low.ToString();
                    if (int.TryParse(concat, out int product))
                    {
                        sum += product;
                    }
                }
            }
            return sum;
        }
        public long P1v2()
        {
            long sum = 0;
            foreach (var bank in BatteryBanks)
            {
                long toAdd = ProcessBank(bank, 2);
                sum += toAdd;
            }
            return sum;
        }

        public long P2()
        {
            long sum = 0;
            foreach (var bank in BatteryBanks)
            {
                long toAdd = ProcessBank(bank, 12);
                sum += toAdd;
            }
            return sum;
        }

        public static long ProcessBank(BatteryBank bank, int nrOfBatteries)
        {
            List<int> selectedBatteriesValues = new(nrOfBatteries);
            int lastIndex = 0;


            while (selectedBatteriesValues.Count < nrOfBatteries)
            {

                int remainingToPick = nrOfBatteries - selectedBatteriesValues.Count;
                int maxStartIndex = bank.Batteries.Count - remainingToPick;
                for (int i = 9; i >= 1; i--)
                {
                    var x = bank.Batteries.IndexOf(i, lastIndex, maxStartIndex - lastIndex + 1);
                    if (x == -1)
                    {
                        continue;
                    }

                    if (x > maxStartIndex)
                    {
                        continue;
                    }

                    selectedBatteriesValues.Add(bank.Batteries[x]);
                    lastIndex = x + 1;
                    break;
                }
            }

            string concat = string.Join("", selectedBatteriesValues);
            if (long.TryParse(concat, out long product))
            {
                return product;
            } else
            {
                throw new Exception("Failed to parse concatenated battery values.");
            }
            
        }
    }

    internal class BatteryBank
    {
        public List<int> Batteries = [];

        public BatteryBank(string batteryString)
        {
            foreach (var ch in batteryString)
            {
                Batteries.Add(ch - '0');
            }
        }
    }
}
