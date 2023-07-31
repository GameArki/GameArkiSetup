using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameArki.CSharpGen {
    public class InterfaceEditor : IInterfaceEditor {

        SyntaxTree tree = null;
        CompilationUnitSyntax root = null;

        public InterfaceEditor() {

        }

        public void Rename(string newName) {
            List<InterfaceDeclarationSyntax> classDeclarations = root.FindAll<InterfaceDeclarationSyntax>();
            InterfaceDeclarationSyntax oldInterface = classDeclarations[0];
            string oldClassName = oldInterface.Identifier.ValueText;
            //Console.WriteLine("Identifier.Text:" + classes.Identifier.Text);

            SyntaxToken newToken = SyntaxFactory.ParseToken(newName);
            InterfaceDeclarationSyntax newClass = oldInterface.ReplaceToken(oldInterface.Identifier, newToken);
            root = root.ReplaceNode(oldInterface, newClass);
            RenameMethod(oldClassName, newName);
        }

        public void LoadCode(string code) {
            tree = CSharpSyntaxTree.ParseText(code);
            root = tree.GetCompilationUnitRoot();
        }

        public void AddUsing(string usingName) {
            SyntaxTree newTree = CSharpSyntaxTree.ParseText("using " + usingName + ";\r\n");
            CompilationUnitSyntax newRoot = newTree.GetCompilationUnitRoot();
            UsingDirectiveSyntax newUsing = newRoot.Usings[0];
            root = root.AddUsings(newUsing);
        }

        public void RemoveAllUsings() {
            List<UsingDirectiveSyntax> li = root.FindAll<UsingDirectiveSyntax>();
            root = root.RemoveNodes(li, SyntaxRemoveOptions.KeepNoTrivia);
        }

        public void AddMethod(IMethodEditor method) {

            int nestedCount = 0;

            NamespaceDeclarationSyntax namespaceDeclarationSyntax = root.FindNameSpace();
            if (namespaceDeclarationSyntax != null) {
                nestedCount += 1;
            }

            SyntaxTree tree = CSharpSyntaxTree.ParseText(method.GenerateInterface(nestedCount));
            MethodDeclarationSyntax syntax = tree.GetCompilationUnitRoot().FindMethod(method.GetName());

            List<InterfaceDeclarationSyntax> interfaceDeclarations = root.FindAll<InterfaceDeclarationSyntax>();
            InterfaceDeclarationSyntax classes = interfaceDeclarations[0];
            classes = classes.AddMembers(syntax);
            root = root.ReplaceNode(interfaceDeclarations[0], classes);


        }

        public void RemoveMethod(string methodName) {
            List<MethodDeclarationSyntax> li = root.FindAll<MethodDeclarationSyntax>();
            for (int i = 0; i < li.Count; i++) {
                var syntax = li[i];
                if (syntax.Identifier.ValueText == methodName) {
                    root = root.RemoveNode(syntax, SyntaxRemoveOptions.KeepNoTrivia);
                }
            }
        }

        public void RenameMethod(string oldName, string newName) {
            List<MethodDeclarationSyntax> li = root.FindAll<MethodDeclarationSyntax>();
            for (int i = 0; i < li.Count; i++) {
                var syntax = li[i];
                if (syntax.Identifier.ValueText == oldName) {
                    SyntaxToken newToken = SyntaxFactory.ParseToken(newName);
                    MethodDeclarationSyntax newMethod = syntax.ReplaceToken(syntax.Identifier, newToken);
                    root = root.ReplaceNode(syntax, newMethod);
                }
            }
        }

        public void RemoveAllMethods() {
            List<MethodDeclarationSyntax> li = root.FindAll<MethodDeclarationSyntax>();
            root = root.RemoveNodes(li, SyntaxRemoveOptions.KeepNoTrivia);
        }

        public string Generate() {
            return root.ToString();
        }

        public void Test() {
            List<InterfaceDeclarationSyntax> li = root.FindAll<InterfaceDeclarationSyntax>();
            InterfaceDeclarationSyntax interfaceDeclaration = li[0];

            //for (int i = 0; i < interfaceDeclaration.Members.Count; i++) {
            //    Console.WriteLine("【" + i + "】" + interfaceDeclaration.Members[i]);
            //    Console.WriteLine(interfaceDeclaration.Members[i].Kind());
            //}

            for (int i = 0; i < interfaceDeclaration.Members.Count; i++) {
                Console.WriteLine("【" + i + "】" + interfaceDeclaration.Members[i]);
                Console.WriteLine(interfaceDeclaration.Members[i].Kind());
            }
        }


        public List<OneParameterInfo> GetParameter(string methodName) {

            MethodDeclarationSyntax syntax = root.FindMethod(methodName);
            return syntax.GetParameter();
        }

        public List<MethodMember> GetAllMethodParameter() {
            List<MethodDeclarationSyntax> li = root.FindAll<MethodDeclarationSyntax>();
            List<MethodMember> methodMembers = new List<MethodMember>();

            for (int i = 0; i < li.Count; i++) {
                MethodDeclarationSyntax method = li[i];
                MethodMember member = new MethodMember();
                member.returnType = method.ReturnType.ToString();
                member.name = method.Identifier.ValueText;
                member.parameterList = method.GetParameter();
                methodMembers.Add(member);
            }

            return methodMembers;
        }
    }
}
