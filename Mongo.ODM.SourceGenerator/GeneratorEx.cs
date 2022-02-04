using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Scriban;
using System.IO;
namespace Mongo.ODM.SourceGenerator;

public static partial class GeneratorEx
{

    public static void FromTemplateFile(this GeneratorExecutionContext context, object model, string templatePath)
    {

        foreach (AdditionalText file in context.AdditionalFiles)
        {
            Log.Print(file.Path);
            if (file.Path.Contains(templatePath, StringComparison.OrdinalIgnoreCase))
            {
                context.FromTemplateString(model, System.IO.File.ReadAllText(file.Path));
            }
        }
        Log.Print(Directory.GetCurrentDirectory());
        if(System.IO.File.Exists(templatePath))
            context.FromTemplateString(model, System.IO.File.ReadAllText(templatePath));
        else
            Log.Print($@"not found: {templatePath}");
    }

    public static void FromTemplateString(this GeneratorExecutionContext context, object model, string templateString, string outputName = "")
    {
        
        Log.Print($@"FromTemplateString{Environment.NewLine}{templateString}");
        var template = Template.Parse(templateString);

        if(template.HasErrors)
        {
            var errors = string.Join(" | ", template.Messages.Select(x => x.Message));
            throw new InvalidOperationException($"Template parse error: {template.Messages}");
        }

        var result = template.Render(model);
        if(outputName == "")
        outputName = Guid.NewGuid().ToString();
        Log.Print($@"{outputName} \n {result}");
        context.AddSource($@"{outputName}.cs", SourceText.From(result, Encoding.UTF8));
        

    }
}
