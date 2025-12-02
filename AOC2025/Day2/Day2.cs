using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AOC2025.Day2
{
    internal class Day2
    {
        private readonly string path;
        private readonly List<IdRange> Ranges = [];

        public Day2()
        {
            path = Path.Combine(AppContext.BaseDirectory, "Day2", "Input.txt");
            if (!File.Exists(path))
                throw new FileNotFoundException($"Required input file not found: {path}");

            var text = File.ReadAllText(path).Trim();

            if (string.IsNullOrEmpty(text))
                return;

            var rangeTokens = text.Split(',');

            foreach (var token in rangeTokens)
            {
                var bounds = token.Split('-');

                if (bounds.Length != 2)
                    continue;

                if (!long.TryParse(bounds[0], out var min))
                    continue;

                if (!long.TryParse(bounds[1], out var max))
                    continue;


                Ranges.Add(new IdRange(min, max));
            }

        }

        public long CalculateIdSumP1()
        {
            long sum = 0;
            foreach (var range in Ranges)
            {
                foreach (var id in range.IdsInRange)
                {
                    int len = id.Length;

                    if (len % 2 != 0 || len == 0)
                        continue;

                    var firstHalf = id[..(len / 2)];
                    var secondHalf = id[(len/2)..];

                    if (firstHalf == secondHalf)
                    {
                        if (!long.TryParse(id, out var toAddId))
                            continue;

                        sum += toAddId;
                    }
                }
            }
            return sum;
        }

        public long CalculateIdSumP2()
        {
            long sum = 0;
            foreach (var range in Ranges)
            {
                foreach (var id in range.IdsInRange)
                {
                    int len = id.Length;
                    bool isRepeatedSequence = false;
                    for (int seqLen = 1; seqLen <= len / 2; seqLen++)
                    {
                        if (len % seqLen != 0)
                            continue;

                        var sequence = id[..seqLen];
                        int repeatCount = len / seqLen;
                        bool allMatch = true;

                        for (int k = 1; k < repeatCount; k++)
                        {
                            if (id.Substring(k * seqLen, seqLen) != sequence)
                            {
                                allMatch = false;
                                break;
                            }
                        }

                        if (allMatch)
                        {
                            isRepeatedSequence = true;
                            break;
                        }
                    }

                    if (isRepeatedSequence)
                    {
                        if (!long.TryParse(id, out long toAddId))
                            continue;

                        sum += toAddId;
                    }
                }
            }

            return sum;
        }

        internal class IdRange
        {
            public long Min { get; set; }
            public long Max { get; set; }
            public List<string> IdsInRange { get; set; } = [];

            public IdRange(long min, long max)
            {
                Min = min;
                Max = max;
                for (long i = Min; i <= Max; i++)
                {
                    IdsInRange.Add(i.ToString());
                }
            }
        }
    }
}