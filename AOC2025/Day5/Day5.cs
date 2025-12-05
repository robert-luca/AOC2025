using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AOC2025.Day5
{
    internal class Interval(long start, long end)
    {
        public long Start { get; set; } = start;
        public long End { get; set; } = end;
    }

    internal class Day5
    {
        private readonly string path;
        private readonly List<Interval> FreshIngredientsRanges = [];
        private readonly List<long> AvailableIngredients = [];

        public Day5()
        {
            path = Path.Combine(AppContext.BaseDirectory, "Day5", "Input.txt");
            if (!File.Exists(path))
                throw new FileNotFoundException($"Required input file not found: {path}");

            using var reader = new StreamReader(path);
            bool moveToAvailableIngredients = false;
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    MergeFreshRanges();
                    moveToAvailableIngredients = true;
                    continue;
                }

                if (!moveToAvailableIngredients)
                {
                    var bounds = line.Trim().Split('-');
                    var idStart = long.Parse(bounds[0]);
                    var idEnd = long.Parse(bounds[1]);
                    FreshIngredientsRanges.Add(new Interval(idStart, idEnd));
                }
                else
                {
                    var id = long.Parse(line.Trim());
                    AvailableIngredients.Add(id);
                }
            }
        }

        private void MergeFreshRanges()
        {
            if (FreshIngredientsRanges.Count <= 1)
                return;

            var intervals = FreshIngredientsRanges
                .OrderBy(i => i.Start)
                .ToList();

            var merged = new List<Interval>();
            long curStart = intervals[0].Start;
            long curEnd = intervals[0].End;

            foreach (var interval in intervals.Skip(1))
            {
                if (interval.Start <= curEnd)
                {
                    curEnd = Math.Max(curEnd, interval.End);
                }
                else
                {
                    merged.Add(new Interval(curStart, curEnd));
                    curStart = interval.Start;
                    curEnd = interval.End;
                }
            }

            merged.Add(new Interval(curStart, curEnd));

            FreshIngredientsRanges.Clear();
            FreshIngredientsRanges.AddRange(merged);
        }

        public long P1()
        {
            long freshCount = 0;
            foreach (var id in AvailableIngredients)
            {
                foreach (var FreshIngredientsRange in FreshIngredientsRanges)
                {
                    if (FreshIngredientsRange.Start <= id && id <= FreshIngredientsRange.End)
                    {
                        freshCount++;
                        break;
                    }
                }
            }
            return freshCount;
        }

        public long P2()
        {
            long total = 0;
            foreach (var interval in FreshIngredientsRanges)
            {
                total += interval.End - interval.Start + 1;
            }
            return total;
        }
    }
}