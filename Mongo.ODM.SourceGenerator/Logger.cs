using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Mongo.ODM.SourceGenerator;

internal static class Log
{
    public static List<string> Logs { get; } = new();

    public static void Print(string msg) => Logs.Add("//\t" + msg.Replace(Environment.NewLine, $@"{Environment.NewLine}//") + Environment.NewLine);

// More print methods ...

    public static void FlushLogs(GeneratorExecutionContext context)
    {
        context.AddSource($"logs.g.cs", SourceText.From(string.Join("\n", Logs), Encoding.UTF8));
    }
}