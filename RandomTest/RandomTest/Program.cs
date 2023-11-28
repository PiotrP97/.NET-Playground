using System;
using System.Text;

namespace RandomTest
{
    internal class Program
    {
        const int DEFAULT_AMOUNT = 1000;
        const int DEFAULT_MAX = 100;

        static void Main(string[] args)
        {
            #region parser members
            int n = DEFAULT_AMOUNT;
            int max = DEFAULT_MAX;
            bool help = false;
            bool printTable = false;
            #endregion parser members

            var parser = new Mono.Options.OptionSet()
            {
                {"n|number=", "amount of random number to be tested", (int v) => n = v},
                {"h|help", "shows help", h => help = h != null },
                {"m|max=", "max range", (int m) => max = m},
                {"print-table", "print raw table", pt => printTable = pt != null},
            };

            try
            {
                parser.Parse(args);
            }
            catch (Mono.Options.OptionException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            if (help)
            {
                parser.WriteOptionDescriptions(Console.Out);
                return;
            }

            PVector randomVector = new PVector(n, max);

            if(printTable)
                Console.Write(randomVector.ToString());
        }
    }

    class PVector
    {
        private int[] vector;
        private readonly int max;

        public int[] Vector
        {
            get
            {
                return vector;
            }
            private set
            {
                vector = value;
            }
        }

        public PVector(int amount, int max)
        {
            int[] rand_array = new int[max];
            var generator = new Random();

            for (int i = 0; i < amount; i++)
                rand_array[generator.Next(max)]++;

            Vector = rand_array;
            this.max = max;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < max; i++)
                sb.AppendLine(string.Format("{0}: {1}", i, Vector[i]));

            return sb.ToString();
        }
    }
}
