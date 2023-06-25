# GenericParameterCollection.Console

This provides controls for using [GenericParameterCollection](https://github.com/HeruEwasham/GenericParameterCollection) in a Console-application.

## Supported features

### Convert a list of console-arguments to a ParameterCollection

You provide a ParameterCollection with some default values. Then you can send the array of string-arguments that are provided in the Main-method to the extension-method SetValuesFromConsoleArguments(..). This method will then parse the arguments and try the best to convert the values based on the converters available.

This method supports setting a custom prefix for defining parameter-keys in the console (default is "--"). Mark that all parameter-keys must be only one word.

## FAQ

### Are all ParameterType's supported?

All types that can convert from a string is supported. If the type has not support for a string-value as default, or the default string-conversion is not suitable, you can create your own custom converter for that type.

### I want to get the contents of a file as a byte-array. Will this library support that?

Currently we do not supply any special converters that opens up a file and get the contents as a byte array. But it should be easy to implement such a converter by creating a custom converter and add it to the ParameterCollection.

### What do I need to think about when creating custom converters?

The only thing you neeed to think about when creating custom converters for a program using this library is that all arguments and other inputs given by the user are given as strings.

So if you for example want to create a converter for a Color. You must let the converter support the conversion from string. For example supporting hexadecimal values or strings like "blue", "red", "green", "orange".