using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GameArki.CSharpGen {

    public static class RoslynSyntaxFactory {

        public static FieldDeclarationSyntax CreateField(VisitLevel visitLevel, string fieldType, string fieldName, bool isStatic = false) {
            string visit = visitLevel.ToFullString();
            string staticType = isStatic ? " static" : "";
            string fieldStr = visit + staticType + " " + fieldType + " " + fieldName + ";";
            SyntaxTree tree = CSharpSyntaxTree.ParseText(fieldStr);
            FieldDeclarationSyntax syntax = tree.GetCompilationUnitRoot().Members[0] as FieldDeclarationSyntax;
            return syntax;
        }

        public static BaseMethodDeclarationSyntax CreateMethod(IMethodEditor methodEditor) {

            string str = "class N {";
            str += methodEditor.Generate();
            str += "}";

            SyntaxTree tree = CSharpSyntaxTree.ParseText(str);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();
            string returnType = methodEditor.GetReturnType();
            string methodName = methodEditor.GetName();
            if (string.IsNullOrEmpty(returnType)) {
                return root.FindConstructor(methodName);
            } else {
                return root.FindMethod(methodName);
            }

        }

        public static UsingDirectiveSyntax CreateUsing(string usingName) {
            SyntaxTree tree = CSharpSyntaxTree.ParseText("using " + usingName + ";");
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();
            UsingDirectiveSyntax using_ = root.Usings[0];
            return using_;
        }

    }

}