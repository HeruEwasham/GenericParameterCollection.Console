using System;
using YngveHestem.GenericParameterCollection;
using YngveHestem.GenericParameterCollection.Console;

namespace TestProject // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        private static ParameterCollection ParameterCollectionDefaultValues = new ParameterCollection
        {
            { "input", "" },
            { "output", "test.mkv" },
            { "volume", 1.0f },
            { "sleepFor", 1000 },
            { "shouldSleep", false }
        };

        static void Main(string[] args)
        {
            var argsAsParameterCollection = ParameterCollectionDefaultValues.SetValuesFromConsoleArguments(args, true, "--");

            Console.WriteLine("The default parameters: " + ParameterCollectionDefaultValues);

            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Parameters with values gotten from arguments: " + argsAsParameterCollection);

            Console.ReadLine();
        }
    }
}