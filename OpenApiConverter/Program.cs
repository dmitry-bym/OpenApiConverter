using System;
using System.Collections.Generic;
using System.IO;
using CommandLine;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Readers;
using OpenApiConverter;

Parser.Default.ParseArguments<Options>(args)
        .WithParsed(OnSuccessParse)
        .WithNotParsed(HandleParseError);

static void OnSuccessParse(Options options)
{
    Process(options.InputPath,
        GetOutput(options.OutputPath),
        ParseVersion(options.Version),
        ParseFormat(options.Format));
}

static Action<string> GetOutput(string output)
{
    return string.IsNullOrEmpty(output)
        ? Console.WriteLine
        : x => File.WriteAllText(output, x);
}

static OpenApiSpecVersion ParseVersion(string version)
{
    return version.ToLower() switch
    {
        "2.0" or "2" => OpenApiSpecVersion.OpenApi2_0,
        "3.0" or "3" => OpenApiSpecVersion.OpenApi3_0,
        _ => throw new ArgumentException("Must have correct value.", nameof(version))
    };
}

static OpenApiFormat ParseFormat(string format)
{
    return format.ToLower() switch
    {
        "json" or "j" => OpenApiFormat.Json,
        "yaml" or "yml" or "y" => OpenApiFormat.Yaml,
        _ => throw new ArgumentException("Must have correct value.", nameof(format))
    };
}

static void Process(string input, Action<string> output, OpenApiSpecVersion version, OpenApiFormat format)
{
    using var readStream = File.OpenRead(input);
    var api = new OpenApiStreamReader().Read(readStream, out _);
    var docString = api.Serialize(version, format);
    output(docString);
}

static void HandleParseError(IEnumerable<Error> errs)
{
}
