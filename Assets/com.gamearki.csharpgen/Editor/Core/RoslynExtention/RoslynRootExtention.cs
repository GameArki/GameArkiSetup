using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GameArki.CSharpGen {

    public static class RoslynRootExtention {

        // ==== USING ====
        public static CompilationUnitSyntax AddUsing(this CompilationUnitSyntax root, string usingName) {
            UsingDirectiveSyntax newUsing = RoslynSyntaxFactory.CreateUsing(usingName);
            return root.AddUsings(newUsing);
        }

        public static CompilationUnitSyntax RemoveAllUsings(this CompilationUnitSyntax root) {
            List<UsingDirectiveSyntax> li = root.FindAll<UsingDirectiveSyntax>();
            return root.RemoveNodes(li, SyntaxRemoveOptions.KeepNoTrivia);
        }

        // ==== TYPE ====
        public static CompilationUnitSyntax RenameTypeName(this CompilationUnitSyntax root, string newName) {
            TypeDeclarationSyntax oldClass = root.Find<TypeDeclarationSyntax>();
            string oldClassName = oldClass.Identifier.ValueText;

            SyntaxToken newToken = SyntaxFactory.ParseToken(newName);
            TypeDeclarationSyntax newClass = oldClass.ReplaceToken(oldClass.Identifier, newToken);
            root = root.ReplaceNode(oldClass, newClass);
            root = RenameMethod(root, oldClassName, newName);
            return root;
        }

        public static CompilationUnitSyntax InheritInterface(this CompilationUnitSyntax root, string interfaceName) {
            List<TypeDeclarationSyntax> classDeclarations = root.FindAll<TypeDeclarationSyntax>();
            TypeDeclarationSyntax oldClass = classDeclarations[0];

            string classStrTemp = "class N : " + interfaceName + " { }";
            SyntaxTree tree = CSharpSyntaxTree.ParseText(classStrTemp);
            CompilationUnitSyntax treeRoot = tree.GetCompilationUnitRoot();
            List<TypeDeclarationSyntax> treeClass = treeRoot.FindAll<TypeDeclarationSyntax>();
            BaseTypeSyntax baseType = treeClass[0].BaseList.Types[0];

            TypeDeclarationSyntax newClass = (TypeDeclarationSyntax)oldClass.AddBaseListTypes(baseType);
            return root.ReplaceNode(oldClass, newClass);

        }

        public static CompilationUnitSyntax InheritClass(this CompilationUnitSyntax root, string className) {
            TypeDeclarationSyntax oldClass = root.Find<TypeDeclarationSyntax>();
            if (oldClass.BaseList != null) {
                string classStrTemp = "class BaseDemoTemp : " + className + " { }";
                SyntaxTree tree = CSharpSyntaxTree.ParseText(classStrTemp);
                CompilationUnitSyntax treeRoot = tree.GetCompilationUnitRoot();
                List<TypeDeclarationSyntax> treeClass = treeRoot.FindAll<TypeDeclarationSyntax>();
                BaseListSyntax oldBaseList = oldClass.BaseList;
                BaseListSyntax newBaseList = treeClass[0].BaseList;
                newBaseList = newBaseList.AddTypes(oldBaseList.Types.ToArray());
                TypeDeclarationSyntax newClass = oldClass.ReplaceNode(oldBaseList, newBaseList);
                root = root.ReplaceNode(oldClass, newClass);
            } else {
                //如果继承列表为空的话那么添加类的继承和添加接口的继承的顺序都一样
                root = InheritInterface(root, className);
            }
            return root;
        }

        public static CompilationUnitSyntax RemoveAllInherit(this CompilationUnitSyntax root) {
            List<TypeDeclarationSyntax> classDeclarations = root.FindAll<TypeDeclarationSyntax>();
            TypeDeclarationSyntax oldClass = classDeclarations[0];

            BaseTypeSyntax newBase = null;
            TypeDeclarationSyntax newClass = oldClass.ReplaceNode(oldClass.BaseList, newBase);
            return root.ReplaceNode(oldClass, newClass);
        }

        public static CompilationUnitSyntax RemoveInherit(this CompilationUnitSyntax root, string baseName) {
            TypeDeclarationSyntax oldClass = root.Find<TypeDeclarationSyntax>();
            if (oldClass.BaseList == null) {
                return root;
            }
            var types = oldClass.BaseList.Types;

            for (int i = 0; i < types.Count; i += 1) {
                BaseTypeSyntax removeBase = types[i];
                if (removeBase.ToString() == baseName) {
                    TypeDeclarationSyntax newClass = oldClass.RemoveNode(removeBase, SyntaxRemoveOptions.KeepNoTrivia);
                    root = root.ReplaceNode(oldClass, newClass);

                    //如果移除的是最后一个，那么把列表也删了（否则会留下一个冒号）
                    TypeDeclarationSyntax newBaseClass = root.Find<TypeDeclarationSyntax>();
                    if (newBaseClass.BaseList.Types.Count == 0) {
                        root = RemoveAllInherit(root);
                    }
                    break;
                }
            }
            return root;
        }

        // ==== FIELD ====
        public static CompilationUnitSyntax AddField(this CompilationUnitSyntax root, FieldDeclarationSyntax field) {
            TypeDeclarationSyntax old = root.Find<TypeDeclarationSyntax>();
            TypeDeclarationSyntax newClass = old.InsertField(field);
            return root.ReplaceNode(old, newClass);
        }

        public static CompilationUnitSyntax RemoveAllFields(this CompilationUnitSyntax root) {
            List<FieldDeclarationSyntax> li = root.FindAll<FieldDeclarationSyntax>();
            return root.RemoveNodes(li, SyntaxRemoveOptions.KeepNoTrivia);
        }

        public static CompilationUnitSyntax RemoveField(this CompilationUnitSyntax root, string fieldName) {
            List<FieldDeclarationSyntax> list = root.FindAll<FieldDeclarationSyntax>();
            list.ForEach(syntax => {
                for (int j = 0; j < syntax.Declaration.Variables.Count; j++) {
                    VariableDeclaratorSyntax variable = syntax.Declaration.Variables[j];
                    if (variable.Identifier.ValueText == fieldName) {
                        var newSyntax = syntax.RemoveNode(variable, SyntaxRemoveOptions.KeepNoTrivia);
                        root = root.ReplaceNode(syntax, newSyntax);
                    }
                }
            });
            return root;
        }

        // ==== METHOD ====
        public static CompilationUnitSyntax RemoveAllMethods(this CompilationUnitSyntax root) {
            List<MethodDeclarationSyntax> li = root.FindAll<MethodDeclarationSyntax>();
            return root.RemoveNodes(li, SyntaxRemoveOptions.KeepNoTrivia);
        }

        public static CompilationUnitSyntax RemoveMethod(this CompilationUnitSyntax root, string methodName) {
            List<MethodDeclarationSyntax> list = root.FindAll<MethodDeclarationSyntax>();
            list.ForEach(syntax => {
                if (syntax.Identifier.ValueText == methodName) {
                    root = root.RemoveNode(syntax, SyntaxRemoveOptions.KeepNoTrivia);
                }
            });
            return root;
        }

        public static CompilationUnitSyntax RenameMethod(this CompilationUnitSyntax root, string methodName, string newName) {
            List<MethodDeclarationSyntax> methodList = root.FindAll<MethodDeclarationSyntax>();
            methodList.ForEach(syntax => {
                if (syntax.Identifier.ValueText == methodName) {
                    SyntaxToken newToken = SyntaxFactory.ParseToken(newName);
                    MethodDeclarationSyntax newMethod = syntax.ReplaceToken(syntax.Identifier, newToken);
                    root = root.ReplaceNode(syntax, newMethod);
                }
            });

            List<ConstructorDeclarationSyntax> ctorList = root.FindAll<ConstructorDeclarationSyntax>();
            ctorList.ForEach(syntax => {
                if (syntax.Identifier.ValueText == methodName) {
                    SyntaxToken newToken = SyntaxFactory.ParseToken(newName);
                    ConstructorDeclarationSyntax newMethod = syntax.ReplaceToken(syntax.Identifier, newToken);
                    root = root.ReplaceNode(syntax, newMethod);
                }
            });
            return root;
        }

    }

}