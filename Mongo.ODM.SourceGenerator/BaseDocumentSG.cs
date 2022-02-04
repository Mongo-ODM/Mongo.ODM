using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Mongo.ODM.SourceGenerator;


public class BaseDocumentModel 
{
    public List<string> Props { get; set; }
    public string ClassName { get; set; }
    public string ClassBase { get; set; }
    public string Namespace { get; set; }
}

[Generator]
public class BaseDocumentSG : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        try
        {
            

        var baseDocuments = 
        ((BaseDocumentFinder) context.SyntaxReceiver)?.BaseDocuments;
        if(baseDocuments != null)
            foreach (var item in baseDocuments)
            {
                Log.Print($@"Building class for {item.ClassName}");
                context.FromTemplateString(item, script);
                
            }
        }
        finally
        {
            Log.Print("all done");
            Log.FlushLogs(context);
        }
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new BaseDocumentFinder());
    }

    string script = @"
    
    using Mongo.ODM;
    using MongoDB.Driver.Fluent;
    namespace {{ Namespace }};

    public partial class {{ ClassName }} : {{ ClassBase }}
    {

        public {{ ClassName }}()
        {
            CleanDocument = this;
        }

        //[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<FluentUpdateDefinitionBuilder<{{ ClassName }}>> GetFluentUpdateDefinitionBuilderAsync()
        {
            var current = ({{ ClassName }})CleanDocument;
            var update = FluentUpdateDefinitionBuilder<{{ ClassName }}>.New();
            {{ for prop in Props }}
            if( {{prop}} != current.{{ prop }} )
                update.Set(i => i.{{prop}}, {{prop}});
            {{ end }}

            return update;

        }

        //[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<UpdateDefinitionBuilder<{{ ClassName }}>> GetUpdateDefinitionBuilderAsync()
        {
            
            return (await GetFluentUpdateDefinitionBuilder()).Build();

        }

        //[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool _isDirty()
        {
            var current = ({{ ClassName }})CleanDocument;
            {{ for prop in Props }}
            if( {{prop}} != current.{{ prop }} )
                return false;
            {{ end }}

            return true;

        }
    }

    ";

}
