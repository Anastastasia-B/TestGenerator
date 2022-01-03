using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestGeneratorLib;

namespace NUnitTestGenerator
{
    public class Tests
    {
        private SyntaxNode _root;
        private const string example = @"using System;
                                         using System.Collections.Generic;
                                         using System.Linq;
                                         using System.Text;
                                         using TestLibrary.Tree.TreeNode;
                                         
                                         namespace TestLibrary.Tree
                                         {
                                             public class TreeClass
                                             {
                                                 public TreeClass(int count, Tree tree)
                                                 {
                                         
                                                 }
                                         
                                                 public void AddElem(int a, out IList elem)
                                                 {
                                                     
                                                 }
                                         
                                                 public int CheckElem()
                                                 {
                                         
                                                 }
                                             }


                                             public class AnotherClass
                                             {
                                                 public int Check()
                                                 {
                                             
                                                 }
                                             }
                                         }";

        [SetUp]
        public void Init()
        {
            _root = CSharpSyntaxTree.ParseText(TestGenerator.Generate(example)).GetRoot();
        }

        [Test]
        public void CheckUsings()
        {
            List<string> usingsName = new List<string>() { "System", "System.Collections.Generic", "System.Linq", "System.Text", "TestLibrary.Tree.TreeNode", "NUnit.Framework" };
            var usings = _root.DescendantNodes().OfType<UsingDirectiveSyntax>().Select(us => us.Name.ToString()).ToList();
            Assert.AreEqual(usingsName.Count, usings.Count);
            foreach (var name in usingsName)
            {
                Assert.IsTrue(usings.Contains(name));
            }
        }

        [Test]
        public void CheckClass()
        {
            List<string> classesName = new List<string>() { "TreeClassTest", "AnotherClassTest" };
            var classes = _root.DescendantNodes().OfType<ClassDeclarationSyntax>().Select(cl => cl.Identifier.ValueText).ToList();
            Assert.AreEqual(classesName.Count, classes.Count);
            foreach (var name in classesName)
            {
                Assert.IsTrue(classes.Contains(name));
            }
        }

        [Test]
        public void CheckMethod()
        {
            List<string> methodsName = new List<string>() { "AddElemTest", "CheckElemTest", "CheckTest"};
            var methods = _root.DescendantNodes().OfType<MethodDeclarationSyntax>().Select(mt => mt.Identifier.ValueText).ToList();
            Assert.AreEqual(methodsName.Count, methods.Count);
            foreach (var name in methodsName)
            {
                Assert.IsTrue(methods.Contains(name));
            }
        }
    }
}
