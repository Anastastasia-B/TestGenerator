using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace TestGeneratorLib
{
    public static class TestGenerator
    {
        public static string Generate(string content)
        {
            SyntaxNode root = CSharpSyntaxTree.ParseText(content).GetRoot();
            TestCreator testCreator = new TestCreator(root);
            return testCreator.GetTest();
        }
    }
}
