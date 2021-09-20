using CommandLine;

namespace OpenApiConverter
{
    public class Options
    {
        [Option('i', "input", Required = true, HelpText = "Input file to convert.")]
        public string InputPath { get; set; }

        [Option('o', "output", Required = false, HelpText = "Output file")]
        public string OutputPath { get; set; }
        
        [Option('v', "version", Required = true, HelpText = "Set output file version 2.0 / 2 or 3.0 / 3.")]
        public string Version { get; set; }
        
        [Option('f', "format", Required = false, HelpText = "Output format yaml / yml / y or json / j.", Default = "json")]
        public string Format { get; set; }
    }
}