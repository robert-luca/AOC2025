using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AOC2025.Day6
{
    internal class Day6
    {
        private readonly Char[,] Grid;
        private readonly List<string> _input = File.ReadAllLines(Path.Combine(AppContext.BaseDirectory, "Day6", "Input.txt")).ToList();
        public Day6()
        {

            Grid = new char[_input.Count, _input[0].Length];

            for (var i = 0; i < _input.Count; i++)
            {
                var characters = _input[i].ToCharArray();

                for (var j = 0; j < characters.Length; j++)
                {
                    Grid[i, j] = characters[j];
                }
            }
        }

        public long P1()
        {
            var grid = ParseInput();
            var rowCount = grid.GetLength(0);
            var columnCount = grid.GetLength(1);

            var total = 0L;

            for (var col = 0; col < columnCount; col++)
            {
                var columnTotal = grid[rowCount - 1, col] == -1 ? 1L : 0L;

                for (var row = 0; row < rowCount - 1; row++)
                {
                    if (grid[rowCount - 1, col] == -1)
                        columnTotal *= grid[row, col];
                    else
                        columnTotal += grid[row, col];
                }

                total += columnTotal;
            }

            return total;
        }

        public int[,] ParseInput()
        {
            var columnCount = _input[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Length;
            var grid = new int[_input.Count, columnCount];

            for (var i = 0; i < _input.Count; i++)
            {
                var splitNumbers = _input[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                for (var j = 0; j < splitNumbers.Length; j++)
                {
                    if (int.TryParse(splitNumbers[j], out var number))
                    {
                        grid[i, j] = number;
                    }
                    else
                    {
                        grid[i, j] = splitNumbers[j] == "*" ? -1 : -2;
                    }
                }
            }

            return grid;
        }

        public long P2()
        {
            char op = ' ';
            List<long> items = new();
            var total = 0L;
            for (var col = 0; col < _input[0].Length; col++)
            {
                var x = "";

                for (var row = 0; row < _input.Count; row++)
                {
                    if (Grid[row,col] == ' ')
                        continue;

                    if (Grid[row, col] == '+' || Grid[row, col] == '*')
                    {
                        op = Grid[row, col];
                        
                    } else
                    {
                        x += Grid[row, col];
                    }
                }
                if (x.Length > 0)
                {
                    items.Add(long.Parse(x));
                }
                if (_input[0].Length - 1 == col || (Grid[_input.Count - 1, col + 1] == '+' || Grid[_input.Count - 1, col + 1] == '*'))
                {
                    total+= op == '+' ? items.Sum() : items.Aggregate(1L, (a, b) => a * b);
                    items.Clear();
                    op = ' ';
                }

            }

            return total;
        }
    }
}
