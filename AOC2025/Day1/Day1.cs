
namespace AOC2025.Day1
{
    internal class Day1
    {
        private readonly string path;
        private int CurrentPosition = 50;
        private int ZeroCounter = 0;

        public Day1()
        {
            path = Path.Combine(AppContext.BaseDirectory, "Day1", "Input.txt");
            if (!File.Exists(path))
                throw new FileNotFoundException($"Required input file not found: {path}");
        }

        public int FindPassword()
        {
            using var reader = new StreamReader(path);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                char direction = line[0];
                if (!int.TryParse(line[1..], out int steps))
                    continue;

                FindCurrentPos(direction, steps);
            }

            return ZeroCounter;
        }

        public void FindCurrentPos(char direction, int steps)
        {
            if (steps <= 0)
                return;

            if (direction != 'L' && direction != 'R')
                throw new Exception("Invalid direction in input");

            if (direction == 'R')
            {
                int distToZero = 100 - CurrentPosition;
                if (distToZero == 0)
                    distToZero = 100;

                if (steps < distToZero)
                {
                    CurrentPosition = CurrentPosition + steps;
                    return;
                }

                steps -= distToZero;
                ZeroCounter++;
                CurrentPosition = 0;

                ZeroCounter += steps / 100;
                steps %= 100;

                CurrentPosition = CurrentPosition + steps;
                return;
            }
            else // 'L'
            {
                int distToZero = CurrentPosition;
                if (distToZero == 0)
                    distToZero = 100;

                if (steps < distToZero)
                {
                    CurrentPosition = (CurrentPosition - steps + 100) % 100;
                    return;
                }

                steps -= distToZero;
                ZeroCounter++;
                CurrentPosition = 0;

                ZeroCounter += steps / 100;
                steps %= 100;

                CurrentPosition = (CurrentPosition - steps + 100) % 100;
                return;
            }
        }
    }
}