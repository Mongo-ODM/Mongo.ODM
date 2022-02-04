using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Mongo.ODM.SourceGenerator;

public class BaseDocumentFinder : ISyntaxReceiver
{
    public List<BaseDocumentModel> BaseDocuments { get; }
        = new();
    
    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        try
        {

            
            if (syntaxNode is ClassDeclarationSyntax model)
            {

                var current = model.Identifier.Parent;
                if(current == null) current = syntaxNode;
                do
                {

                    if(model.Identifier.ValueText == "BaseDocument")
                    {
                        Log.Print("model:" + model.ToFullString());
                        
                        if(SyntaxNodeHelper.TryGetParentSyntax<NamespaceDeclarationSyntax>(syntaxNode, out var ns))
                            Log.Print($@"ns:{ns?.Name}");
                        
                        Log.Print($@"class:{model?.Identifier}");
                        var props = model.Members
                        .Where(i => i.GetType() == typeof(PropertyDeclarationSyntax))
                        .Cast<PropertyDeclarationSyntax>()
                        .Where(i => i.AttributeLists.Any(i => i.ToString() == "BsonIgnore"))
                        .Select(i => {
                            return i?.Identifier.ToString();
                        })
                        .Where(i => i != null)
                        .ToList();

                        Log.Print($@"props:{string.Join(',', props)}");
                        var c = model.ToFullString().Split(".");

                        BaseDocuments.Add(new BaseDocumentModel() {
                            ClassName = model?.Identifier.ToString(),
                            Namespace = string.Join('.', c.Take(c.Length - 1)),
                            Props = props
                        });
                    }
                    current = current.Parent;
                } while(current.Parent != null);
            }

        }
        catch(Exception e)
        {

            Log.Print("Failed:" + e.ToString());
        }
        finally
        {
            
            if(SyntaxNodeHelper.TryGetParentSyntax<NamespaceDeclarationSyntax>(syntaxNode, out var ns))
                Log.Print($@"_ns:{ns?.Name}");
            if(SyntaxNodeHelper.TryGetParentSyntax<ClassDeclarationSyntax>(syntaxNode, out var _class))
                Log.Print($@"_class:{_class?.Identifier}");
            Log.Print("done");
        }
    }
}
