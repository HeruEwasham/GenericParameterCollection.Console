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
            { "sleepFor", 1000, new ParameterCollection { { "desc", "How long to sleep in milliseconds." } } },
            { "shouldSleep", false }
        };

        static void Main(string[] args)
        {
            var argsAsParameterCollection = ParameterCollectionDefaultValues.SetValuesFromConsoleArguments(args, true, "--");

            Console.WriteLine("The default parameters: " + ParameterCollectionDefaultValues);

            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Parameters with values gotten from arguments: " + argsAsParameterCollection);

            Console.WriteLine(Environment.NewLine + "Show default parameters as a table:" + Environment.NewLine + ParameterCollectionDefaultValues.GetParametersHelpTable());

            Console.WriteLine(Environment.NewLine + "Show current parameters as a table:" + Environment.NewLine + argsAsParameterCollection.GetParametersHelpTable(null, false));

            Console.ReadLine();
        }
    }
}