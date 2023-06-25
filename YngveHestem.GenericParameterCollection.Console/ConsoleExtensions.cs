using System;
using System.Collections.Generic;

namespace YngveHestem.GenericParameterCollection.Console
{
	public static class ConsoleExtensions
	{
		/// <summary>
		/// Change parameter values based on the inputted parameters.
		/// Only existing parameters can be edited. If any arguments mention parameters which does not exists, it will not happen anything.
		/// If a parameter is mentioned in arguments without any value afterwards, the value will be set to True.
		/// Values will be converted from string to the type defined. This means that if you want a type to be converted a special way, you can add a custom ParameterValueConverter with conversion from string to the desired ParameterType.
		/// </summary>
		/// <param name="parameters">The ParameterCollection to edit.</param>
		/// <param name="arguments">The list of arguments to use. This is meant to be the list sent as input to main.</param>
		/// <param name="createNewCollection">If set to true, the function will instead of edit the inputted ParameterCollection, create a new ParameterCollection instead.</param>
		/// <param name="parameterPrefix">This defines what the prefix of the parameter name should be. For example, if you want the user write "--username Test" as the argumeents for parameter "username" and give the value "Test", the prefix should be "--". If you set this to "!", the user should write "!username Test", for the same functionality. The prefix can not be empty or whitespace, and needs to be something a value should not start with.</param>
		/// <returns>The edited (or new if createNewCollection is true) ParameterCollection.</returns>
		public static ParameterCollection SetValuesFromConsoleArguments(this ParameterCollection parameters, string[] arguments, bool createNewCollection = false, string parameterPrefix = "--")
		{
			if (string.IsNullOrWhiteSpace(parameterPrefix))
			{
				throw new ArgumentException("ParameterPrefix can not be null or whitespace.", nameof(parameterPrefix));
			}

			var argsAsDictionary = ConvertArgsToDictionary(arguments, parameterPrefix);

			if (createNewCollection)
			{
				return CreateNewCollectionWithParameterValues(parameters, argsAsDictionary);
			}
			else
			{
                return EditCollectionWithParameterValues(parameters, argsAsDictionary);
            }
		}

        private static Dictionary<string, string> ConvertArgsToDictionary(string[] arguments, string parameterPrefix)
        {
            if (arguments == null)
            {
                return new Dictionary<string, string>();
            }
            var result = new Dictionary<string, string>();
            var i = 0;
            while (i < arguments.Length)
            {
                if (arguments[i].StartsWith(parameterPrefix))
                {
					if (i + 1 < arguments.Length && !arguments[i+1].StartsWith(parameterPrefix))
					{
						result.Add(arguments[i].Substring(parameterPrefix.Length), arguments[i + 1]);
						i += 2;
					}
					else
					{
                        result.Add(arguments[i].Substring(parameterPrefix.Length), true.ToString());
                        i++;
					}
				}
				else
				{
					i++;
				}
			}

			return result;
        }

        private static ParameterCollection CreateNewCollectionWithParameterValues(ParameterCollection parameters, Dictionary<string, string> argsAsDictionary)
        {
			var result = new ParameterCollection();
			foreach(var parameter in parameters)
			{
				if (argsAsDictionary.ContainsKey(parameter.Key))
				{
					result.Add(new Parameter(parameter.Key, argsAsDictionary[parameter.Key], parameter.Type, parameter.GetAdditionalInfo(), parameter.GetCustomConverters()));
				}
				else
				{
					result.Add(parameter);
				}
			}
			return result;
        }

        private static ParameterCollection EditCollectionWithParameterValues(ParameterCollection parameters, Dictionary<string, string> argsAsDictionary)
        {
            foreach(var argumentWithValue in argsAsDictionary)
			{
				if (parameters.HasKey(argumentWithValue.Key))
				{
					parameters.GetParameterByKey(argumentWithValue.Key).SetValue(argumentWithValue.Value);
				}
			}
			return parameters;
        }
    }
}

