using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GameArki.CSharpGen {

    public class ClassEditor : IClassEditor {

        SyntaxTree tree;
        CompilationUnitSyntax root = null;

        public ClassEditor() {

        }

        // ==== INIT ====
        public void LoadCode(string code) {
            tree = CSharpSyntaxTree.ParseText(code);
            root = tree.GetCompilationUnitRoot();
        }

        public void NewClass(string nameSpace, string classname, bool isStruct = false) {

            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(nameSpace)) {
                sb.AppendLine("namespace " + nameSpace + "{");
            }

            if (string.IsNullOrEmpty(classname)) {
                return;
            }

            string typeStr = isStruct ? "struct" : "class";
            sb.AppendLine("public " + typeStr + " " + classname + " {");
            sb.AppendLine("}");

            if (!string.IsNullOrEmpty(nameSpace)) {
                sb.AppendLine("}");
            }

            LoadCode(sb.ToString());
        }

        // ==== GENERATE ====
        public string Generate() {
            root = root.NormalizeWhitespace();
            return root.ToString();
        }

        // ==== NAMESPACE ====
        public string GetNameSpace() {
            var ns = root.FindNameSpace();
            if (ns == null) {
                return null;
            }
            return ns.Name.ToString();
        }

        // ==== USING ====
        public void AddUsing(string usingName) {
            var usings = root.FindAll<UsingDirectiveSyntax>();
            if (usings.FindIndex(value => value.Name.ToString() == usingName) != -1) {
                return;
            }
            UsingDirectiveSyntax newUsing = RoslynSyntaxFactory.CreateUsing(usingName);
            root = root.AddUsings(newUsing);
        }

        public void RemoveAllUsings() {
            root = root.RemoveAllUsings();
        }

        // ==== TYPE ====
        public void RenameClass(string newName) {
            root = root.RenameTypeName(newName);
        }

        public string GetClassName() {
            TypeDeclarationSyntax classDeclaration = root.Find<TypeDeclarationSyntax>();
            return classDeclaration.GetName();
        }

        public void InheritInterface(string interfaceName) {
            root = root.InheritInterface(interfaceName);
        }

        public void InheritClass(string className) {
            root = root.InheritClass(className);
        }

        public void RemoveInherit(string baseName) {
            root = root.RemoveInherit(baseName);
        }

        public void RemoveAllInherit() {
            root = root.RemoveAllInherit();
        }

        // ==== FIELD ====
        public void AddField(VisitLevel visitLevel, string fieldType, string fieldName, bool isStatic = false) {
            FieldDeclarationSyntax field = RoslynSyntaxFactory.CreateField(visitLevel, fieldType, fieldName, isStatic);
            root = root.AddField(field);
        }

        public List<FieldDeclarationSyntax> GetAllFields() {
            return root.FindAll<FieldDeclarationSyntax>();
        }

        public void RemoveField(string fieldName) {
            root = root.RemoveField(fieldName);
        }

        public void RemoveAllFields() {
            root = root.RemoveAllFields();
        }

        // ==== METHOD ====
        public void AddMethod(IMethodEditor method) {

            BaseMethodDeclarationSyntax syntax = RoslynSyntaxFactory.CreateMethod(method);
            if (syntax == null) {
                throw new Exception("Method syntax is null");
            }

            TypeDeclarationSyntax old = root.Find<TypeDeclarationSyntax>();
            
            TypeDeclarationSyntax newClass = old.AddMembers(syntax);
            root = root.ReplaceNode(old, newClass);

        }

        public List<MethodDeclarationSyntax> GetAllMethods() {
            return root.FindAll<MethodDeclarationSyntax>();
        }

        public void RenameMethod(string methodName, string newName) {
            root = root.RenameMethod(methodName, newName);
        }

        public void RemoveMethod(string methodName) {
            root = root.RemoveMethod(methodName);
        }

        public void RemoveAllMethods() {
            root = root.RemoveAllMethods();
        }

        public void RemoveAllConstructor() {
            List<ConstructorDeclarationSyntax> li = root.FindAll<ConstructorDeclarationSyntax>();
            root = root.RemoveNodes(li, SyntaxRemoveOptions.KeepNoTrivia);
        }

        // ==== ATTRIBUTE ====
        public void AddMemberAttribute(string memberName, string attributeStr) {

            TypeDeclarationSyntax oldClass = root.FindClass();

            SyntaxList<MemberDeclarationSyntax> members = oldClass.Members;
            MemberDeclarationSyntax demoMethod = members.FindMethod(memberName);

            SyntaxTree tree = CSharpSyntaxTree.ParseText(attributeStr);
            CompilationUnitSyntax treeRoot = tree.GetCompilationUnitRoot();
            AttributeListSyntax attribute = treeRoot.Members[0].AttributeLists[0];
            MemberDeclarationSyntax newMethod = demoMethod.AddAttributeLists(attribute);
            TypeDeclarationSyntax newClass = oldClass.ReplaceNode(demoMethod, newMethod);
            root = root.ReplaceNode(oldClass, newClass);

        }

        public void AddClassAttribute(string className, string attributeStr) {

            List<TypeDeclarationSyntax> classDeclarations = root.FindAll<TypeDeclarationSyntax>();
            TypeDeclarationSyntax oldClass = null;
            for (int i = 0; i < classDeclarations.Count; i++) {
                if (classDeclarations[i].Identifier.ValueText == className) {
                    oldClass = classDeclarations[i];
                }
            }

            SyntaxTree tree = CSharpSyntaxTree.ParseText(attributeStr);
            CompilationUnitSyntax treeRoot = tree.GetCompilationUnitRoot();
            AttributeListSyntax attribute = treeRoot.Members[0].AttributeLists[0];

            TypeDeclarationSyntax newClass = oldClass.AddAttributeLists(attribute);
            root = root.ReplaceNode(oldClass, newClass);

        }

        public bool HasClassAttribute(string attrName) {
            TypeDeclarationSyntax oldClass = root.Find<TypeDeclarationSyntax>();
            if (oldClass == null) {
                return false;
            }
            foreach (var list in oldClass.AttributeLists) {
                foreach (var attr in list.Attributes) {
                    if (attr.Name.ToString() == attrName) {
                        return true;
                    }
                }
            }
            return false;
        }

        public void RemoveMemberAttribute(string memberName, string attributeStr) {
            List<TypeDeclarationSyntax> classDeclarations = root.FindAll<TypeDeclarationSyntax>();
            TypeDeclarationSyntax oldClass = classDeclarations[0];

            SyntaxList<MemberDeclarationSyntax> members = oldClass.Members;
            MemberDeclarationSyntax demoMethod = members.FindMethod(memberName);

            for (int i = 0; i < demoMethod.AttributeLists.Count; i++) {
                if (demoMethod.AttributeLists[i].ToString() == attributeStr) {
                    AttributeListSyntax attribute = demoMethod.AttributeLists[i];
                    TypeDeclarationSyntax newClass = oldClass.RemoveNode(attribute, SyntaxRemoveOptions.KeepNoTrivia);
                    root = root.ReplaceNode(oldClass, newClass);

                }
            }
        }

        public void RemoveMemberAttribute(string memberName) {
            List<TypeDeclarationSyntax> classDeclarations = root.FindAll<TypeDeclarationSyntax>();
            TypeDeclarationSyntax oldClass = classDeclarations[0];

            SyntaxList<MemberDeclarationSyntax> members = oldClass.Members;
            MemberDeclarationSyntax methodTemp = members.FindMethod(memberName);

            TypeDeclarationSyntax newClass = oldClass.RemoveNodes(methodTemp.AttributeLists, SyntaxRemoveOptions.KeepNoTrivia);
            root = root.ReplaceNode(oldClass, newClass);

        }

        public void RemoveClassAttribute(string className, string attributeStr) {
            List<TypeDeclarationSyntax> classDeclarations = root.FindAll<TypeDeclarationSyntax>();
            TypeDeclarationSyntax oldClass = null;
            for (int i = 0; i < classDeclarations.Count; i++) {
                if (classDeclarations[i].Identifier.ValueText == className) {
                    oldClass = classDeclarations[i];
                }
            }

            for (int i = 0; i < oldClass.AttributeLists.Count; i++) {
                if (oldClass.AttributeLists[i].ToString() == attributeStr) {
                    AttributeListSyntax attribute = oldClass.AttributeLists[i];
                    TypeDeclarationSyntax newClass = oldClass.RemoveNode(attribute, SyntaxRemoveOptions.KeepNoTrivia);
                    root = root.ReplaceNode(oldClass, newClass);

                }
            }
        }

        public void RemoveClassAttribute(string className) {
            List<TypeDeclarationSyntax> classDeclarations = root.FindAll<TypeDeclarationSyntax>();
            TypeDeclarationSyntax oldClass = null;
            for (int i = 0; i < classDeclarations.Count; i++) {
                if (classDeclarations[i].Identifier.ValueText == className) {
                    oldClass = classDeclarations[i];
                }
            }

            TypeDeclarationSyntax newClass = oldClass.RemoveNodes(oldClass.AttributeLists, SyntaxRemoveOptions.KeepNoTrivia);
            root = root.ReplaceNode(oldClass, newClass);

        }

        public void RemoveAllAttribute() {
            List<AttributeListSyntax> li = root.FindAll<AttributeListSyntax>();
            root = root.RemoveNodes(li, SyntaxRemoveOptions.KeepNoTrivia);
        }

        /// <summary>
        /// 此方法尚未完成，不要使用！
        /// </summary>
        public void AddProperty(string type, string name, bool get, bool set) {
            string treeStr = "public " + type + " " + name + " { ";
            if (get) {
                treeStr += "get; ";
            }
            if (set) {
                treeStr += "set; ";
            }
            treeStr += "}";

            SyntaxTree tree = CSharpSyntaxTree.ParseText(treeStr);
            PropertyDeclarationSyntax syntax = tree.GetCompilationUnitRoot().Members[0] as PropertyDeclarationSyntax;
            Console.WriteLine(syntax);

            TypeDeclarationSyntax classes = root.FindClass();
            TypeDeclarationSyntax classesOld = classes;

            classes = classes.AddMembers(syntax);

            root = root.ReplaceNode(classesOld, classes);
        }

        public void AddMethodNotes(string memberName, string notes) {

            List<TypeDeclarationSyntax> classDeclarations = root.FindAll<TypeDeclarationSyntax>();
            TypeDeclarationSyntax oldClass = classDeclarations[0];

            SyntaxList<MemberDeclarationSyntax> members = oldClass.Members;
            MemberDeclarationSyntax targetMethod = members.FindMethod(memberName);

            string nodestxt = "namespace FunnyAST.CodeGen {";
            nodestxt += @"/// <summary>";
            nodestxt += @"/// " + notes;
            nodestxt += @"/// </summary>";
            nodestxt += "class Temp {} }";
            SyntaxTree tree = CSharpSyntaxTree.ParseText(nodestxt);
            CompilationUnitSyntax treeRoot = tree.GetCompilationUnitRoot();
            TypeDeclarationSyntax tarClass = treeRoot.FindClass();
            SyntaxTriviaList syntaxTrivias = tarClass.GetLeadingTrivia();
            SyntaxTriviaList targetTriviaList = targetMethod.GetLeadingTrivia();
            TypeDeclarationSyntax newClass = oldClass.InsertTriviaAfter(targetTriviaList.Last(), syntaxTrivias);

            root = root.ReplaceNode(oldClass, newClass);

        }

    }

}
