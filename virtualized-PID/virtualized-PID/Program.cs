using System;
using System.Threading;

namespace virtualized_PID
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;

            //Imports random:
            Random rnd = new Random();

            //Generates random desired power percentage values for right and left motors representing percentage, assuming that both motors are perfect:
            int powerLevelR = rnd.Next(0, 101);
            int powerLevelL = rnd.Next(0, 101);

            /*
             * Generates random actual power ratios representing error and variability in motors and finds the actual power levels:
             * These values can be anywhere in between 0 and 200 percent for this example
             * If the value for a factor is 1, it is a perfect motor that matches the desired power percentage before the algorithm correction loop takes place
             * If the value for a factor is less than one, it has problems such as friction to overcome, meaning that actual power has to be increased
             * If the value for a factor is more than one, it is more powerful than the actual desired power value, so power is reduced
             */
            double rFactor = rnd.NextDouble() * 2;
            double lFactor = rnd.NextDouble() * 2;

            double actualPowerLevelR = powerLevelR * rFactor;
            double actualPowerLevelL = powerLevelL * lFactor;

            actualPowerLevelR = Math.Round(actualPowerLevelR);
            actualPowerLevelL = Math.Round(actualPowerLevelL);

            //Displays desired and real power levels:
            Console.WriteLine("Desired R motor power: " + powerLevelR + "%");
            Console.WriteLine("Desired L motor power: " + powerLevelL + "%");
            Console.WriteLine("Actual R motor power: " + actualPowerLevelR + "%");
            Console.WriteLine("Actual L motor power: " + actualPowerLevelL + "%");
            Console.WriteLine();
            Console.ReadKey();

            //In real life, the robot would't know the ratio to increase or decrease by, so it has to find it:
            double rError = powerLevelR - actualPowerLevelR;
            double lError = powerLevelL - actualPowerLevelL;

            bool pidCalculated = false;
            int trials = 0;

            while (!pidCalculated)
            {
                trials++;

                if (powerLevelR == actualPowerLevelR && powerLevelL == actualPowerLevelL)
                {
                    pidCalculated = true;
                }

                if (actualPowerLevelR < powerLevelR)
                {
                    actualPowerLevelR++;
                }

                if (actualPowerLevelR > powerLevelR)
                {
                    actualPowerLevelR--;
                }

                if (actualPowerLevelL < powerLevelL)
                {
                    actualPowerLevelL++;
                }

                if (actualPowerLevelL > powerLevelL)
                {
                    actualPowerLevelL--;
                }

                if (!pidCalculated)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Trial: " + trials);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(" Actual Power R: " + actualPowerLevelR + " Desired Power R: " + powerLevelR + ", Actual Power L: " + actualPowerLevelL + " Desired Power L: " + powerLevelL);
                }

                if (pidCalculated)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Trial: " + trials);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(" Actual Power R: " + actualPowerLevelR + " Desired Power R: " + powerLevelR + ", Actual Power L: " + actualPowerLevelL + " Desired Power L: " + powerLevelL);
                }
            }

            Console.ReadKey();
        }
    }
}