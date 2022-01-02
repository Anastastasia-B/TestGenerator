using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestGeneratorLib
{
    internal class TestCreator
    {
        private CompilationUnitSyntax _test;
        private SyntaxNode _root;

        internal TestCreator(SyntaxNode root)
        {
            _root = root;
            _test = SyntaxFactory.CompilationUnit();
        }

        internal string GetTest()
        {
            var usings = _root.DescendantNodes().OfType<UsingDirectiveSyntax>().Select(us => us.Name.ToString()).ToArray();
            _test = _test.AddUsings(usings.Select(us => SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName(us))).ToArray());
            _test = _test.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("NUnit.Framework")));
            var namespaces = _root.DescendantNodes().OfType<NamespaceDeclarationSyntax>().ToArray();
            _test = _test.AddMembers(namespaces.Select(nm => CreateNamespace(nm)).ToArray());
            return _test.NormalizeWhitespace().ToFullString();
        }

        internal NamespaceDeclarationSyntax CreateNamespace(NamespaceDeclarationSyntax namespaceDeclaration)
        {
            var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName(namespaceDeclaration.Name.ToString() + ".Tests"));
            var classes = namespaceDeclaration.DescendantNodes().OfType<ClassDeclarationSyntax>().ToArray();
            @namespace = @namespace.AddMembers(classes.Select(cl => CreateClass(cl)).ToArray());
            return @namespace;
        }

        internal ClassDeclarationSyntax CreateClass(ClassDeclarationSyntax classDeclaration)
        {
            var @class = SyntaxFactory.ClassDeclaration(classDeclaration.Identifier.ValueText + "Test")
                .WithAttributeLists(
                    SyntaxFactory.SingletonList(
                        SyntaxFactory.AttributeList(
                            SyntaxFactory.SingletonSeparatedList(
                                SyntaxFactory.Attribute(
                                    SyntaxFactory.IdentifierName("TestFixture"))))))
                 .WithModifiers(
                      SyntaxFactory.TokenList(
                          SyntaxFactory.Token(SyntaxKind.PublicKeyword)));
            var methods = classDeclaration.DescendantNodes().OfType<MethodDeclarationSyntax>().Where(mt => mt.Modifiers
                .Where(md => md.Kind().Equals(SyntaxKind.PublicKeyword) || md.Kind().Equals(SyntaxKind.InternalKeyword)).Any()).ToArray();
            return @class;
        }
    }
}
