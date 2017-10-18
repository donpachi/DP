using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinChangeProblem
{
    class CoinChangeProblem
    {
        static long getWays(long amount, long[] coins)
        {
            return getWays(amount, coins, 0, new Dictionary<string, long>());
        }

        static long getWays(long amount, long[] coins, int index, Dictionary<string, long> memo)
        {
            if (amount == 0)
                return 1;
            if (index >= coins.Length)
                return 0;
            string key = amount + "-" + index;
            Console.WriteLine("Searching for key " + key);
            if (memo.ContainsKey(key))
            {
                Console.WriteLine("Found Key" + key);
                return memo[key];
            }
            long accruedAmount = 0;
            long numOfWays = 0;
            while (accruedAmount <= amount)
            {
                long remaining = amount - accruedAmount;
                numOfWays += getWays(remaining, coins, index + 1, memo);
                accruedAmount += coins[index];
            }
            memo.Add(key, numOfWays);
            return numOfWays;
        }

        static void Main(string[] args)
        {
            InputGenerator input = new InputGenerator(250, 50, 50);
            int amount = input.GetAmount();
            int numCoins = input.GetNumberCoins();
            long[] coins = input.GetCoinArray();
            // Print the number of ways of making change for 'n' units using coins having the values given by 'c'
            //amount = 4;
            //numCoins = 3;
            //coins = new long[] { 1, 2, 3 };
            long ways = getWays(amount, coins);
            Console.WriteLine(ways);
            Console.WriteLine("Press any key to quit...");
            Console.ReadKey();
        }
    }

    internal class InputGenerator
    {
        int n;
        int m;
        long[] coins;

        public InputGenerator(int unit, int numCoins, int maxValueOfCoin)
        {
            Random randomGenerator = new Random();
            n = randomGenerator.Next(unit);
            m = randomGenerator.Next(numCoins);
            coins = new long[m];
            byte[] distinctArray = new byte[maxValueOfCoin];
            for (int i = 0; i < coins.Length; i++)
            {
                int coin = randomGenerator.Next(maxValueOfCoin);
                if (distinctArray[coin] == 0)
                    coins[i] = coin;
                else
                {
                    while (distinctArray[coin] != 0)
                    {
                        coin = randomGenerator.Next(maxValueOfCoin);
                    }
                    coins[i] = coin;
                }
                distinctArray[coin] = 1;
            }
            Array.Sort(coins);
        }

        public int GetAmount()
        {
            return n;
        }

        public int GetNumberCoins()
        {
            return m;
        }

        public long[] GetCoinArray()
        {
            return coins;
        }
    }
}
