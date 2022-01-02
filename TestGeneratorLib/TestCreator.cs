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
            return _test.ToFullString();
        }
    }
}
