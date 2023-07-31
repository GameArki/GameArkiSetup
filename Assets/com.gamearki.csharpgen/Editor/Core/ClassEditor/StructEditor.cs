using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GameArki.CSharpGen {

    public class StructEditor {

        CompilationUnitSyntax root;

        // ==== INIT ====
        public StructEditor(string nameSpace, string className) {

            StringBuilder codeSb = new StringBuilder();

            if (!string.IsNullOrEmpty(nameSpace)) {
                codeSb.AppendLine("namespace " + nameSpace + " {");
            }

            codeSb.AppendLine("public struct " + className + "{}");

            if (!string.IsNullOrEmpty(nameSpace)) {
                codeSb.AppendLine("}");
            }

            LoadCode(codeSb.ToString());

        }

        public StructEditor(string codeText) {
            LoadCode(codeText);
        }

        void LoadCode(string codeText) {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(codeText);
            root = tree.GetCompilationUnitRoot();
        }

        public string GenerateCode() {
            root = root.NormalizeWhitespace();
            return root.ToString();
        }

        // ==== USING ====
        public void AddUsing(string usingName) {
            root = root.AddUsing(usingName);
        }

        // ==== TYPE ====
        public void InheritInterface(string interfaceName) {
            root = root.InheritInterface(interfaceName);
        }

        // ==== FIELD ====
        public void AddField(VisitLevel visitLevel, string fieldType, string fieldName, bool isStatic = false) {
            FieldDeclarationSyntax field = RoslynSyntaxFactory.CreateField(visitLevel, fieldType, fieldName, isStatic);
            root = root.AddField(field);
        }

    }

}