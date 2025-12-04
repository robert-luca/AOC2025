using System;
using System.Collections.Generic;
using System.Text;

namespace AOC2025.Day4
{
    internal class Day4
    {
        private readonly string path;
        private readonly List<List<char>> RollsMatrix = [];
        private int total = 0;

        public Day4()
        {
            path = Path.Combine(AppContext.BaseDirectory, "Day4", "Input.txt");
            if (!File.Exists(path))
                throw new FileNotFoundException($"Required input file not found: {path}");

            using var reader = new StreamReader(path);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                RollsMatrix.Add([.. line.ToCharArray()]);
            }
        }

        public int P1()
        {
            total = 0;
            bool recheck = false;
            for(var i = 0; i < RollsMatrix.Count; i++)
            { 
                for (var j = 0; j < RollsMatrix[i].Count; j++)
                {
                    CheckAdjacentPapers(i, j, recheck);
                }
            }
            return total;
        }

        public int P2()
        {
            total = 0;
            bool recheck = true;
            for (var i = 0; i < RollsMatrix.Count; i++)
            {
                for (var j = 0; j < RollsMatrix[i].Count; j++)
                {
                    CheckAdjacentPapers(i, j, recheck);
                }
            }
            return total;
        }

        public void CheckAdjacentPapers(int i, int j, bool recheck)
        {
            //if is paper
            if (RollsMatrix[i][j] == '@')
            {
                var adjacentPapers = new List<int[]>();

                //check above
                if (i > 0)
                {
                    for (var k = j - 1; k <= j + 1; k++)
                    {
                        if (k >= 0 && k < RollsMatrix[i - 1].Count && RollsMatrix[i - 1][k] == '@')
                        {
                            adjacentPapers.Add([i - 1, k]);

                        }
                    }
                }

                //check same row
                if (j > 0 && RollsMatrix[i][j - 1] == '@')
                {
                    adjacentPapers.Add([i, j - 1]);

                }

                if (j < RollsMatrix[i].Count - 1 && RollsMatrix[i][j + 1] == '@')
                {
                    adjacentPapers.Add([i, j + 1]);

                }

                //check below
                if (i < RollsMatrix.Count - 1)
                {
                    for (var k = j - 1; k <= j + 1; k++)
                    {
                        if (k >= 0 && k < RollsMatrix[i + 1].Count && RollsMatrix[i + 1][k] == '@')
                        {
                            adjacentPapers.Add([i + 1, k]);

                        }
                    }
                }

                if (adjacentPapers.Count < 4)
                {
                    total++;
                    if (recheck)
                    {
                        RollsMatrix[i][j] = 'x';
                        foreach (var adjacentPaper in adjacentPapers)
                        {
                            if (RollsMatrix[adjacentPaper[0]][adjacentPaper[1]] == '@')
                            {
                                CheckAdjacentPapers(adjacentPaper[0], adjacentPaper[1], recheck);
                            }
                        }
                    }
                }
            }
        }
    }
}
