using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GameArki.CSharpGen {

    public static class RoslynExtention {

        public static ClassDeclarationSyntax FindClass(this SyntaxNode root) {
            return Find<ClassDeclarationSyntax>(root);
        }

        public static NamespaceDeclarationSyntax FindNameSpace(this SyntaxNode root) {
            return Find<NamespaceDeclarationSyntax>(root);
        }

        public static MethodDeclarationSyntax FindMethod(this SyntaxNode root, string methodName) {
            List<MethodDeclarationSyntax> list = FindAll<MethodDeclarationSyntax>(root);
            return list.Find(value => value.Identifier.ValueText == methodName);
        }

        public static ConstructorDeclarationSyntax FindConstructor(this SyntaxNode root, string methodName) {
            List<ConstructorDeclarationSyntax> list = FindAll<ConstructorDeclarationSyntax>(root);
            return list.Find(value => value.Identifier.ValueText == methodName);
        }

        public static T Find<T>(this SyntaxNode root) where T : SyntaxNode {
            foreach (var node in root.DescendantNodes()) {
                T classSyntax = node as T;
                if (classSyntax != null) {
                    return classSyntax;
                }
            }
            return null;
        }

        public static string GetFieldType(this FieldDeclarationSyntax field) {
            return field.Declaration.Type.ToString();
        }

        public static string GetFieldName(this FieldDeclarationSyntax field) {
            if (field.Declaration.Variables.Count > 1) {
                throw new System.Exception("暂不支持多变量");
            }
            return field.Declaration.Variables[0].ToString();
        }

        public static List<T> FindAll<T>(this SyntaxNode root) where T : SyntaxNode {
            List<T> list = new List<T>();
            foreach (var node in root.DescendantNodes()) {
                T syntax = node as T;
                if (syntax != null) {
                    list.Add(syntax);
                }
            }
            return list;
        }

        public static string GetName(this MethodDeclarationSyntax method) {
            return method.Identifier.ValueText;
        }

        public static List<OneParameterInfo> GetParameters(this MethodDeclarationSyntax method) {
            var parameterInfoList = new List<OneParameterInfo>();
            var args = method.ParameterList;
            int index = 0;
            foreach (var arg in args.Parameters) {
                OneParameterInfo oneParameter = new OneParameterInfo();
                string paramType = arg.Identifier.GetPreviousToken().ValueText;
                string paramName = arg.Identifier.ValueText;
                oneParameter.index = index;
                oneParameter.type = paramType;
                oneParameter.name = paramName;
                parameterInfoList.Add(oneParameter);
                index += 1;
            }
            return parameterInfoList;
        }

        public static MemberDeclarationSyntax FindMethod(this SyntaxList<MemberDeclarationSyntax> members, string name) {
            for (int i = 0; i < members.Count; i++) {
                MemberDeclarationSyntax memberTemp = members[i];
                if (memberTemp.Kind() == SyntaxKind.FieldDeclaration) {
                    FieldDeclarationSyntax member = (FieldDeclarationSyntax)memberTemp;
                    for (int j = 0; j < member.Declaration.Variables.Count; j++) {
                        if (member.Declaration.Variables[j].Identifier.ValueText == name) {
                            return member;
                        }
                    }
                } else if (memberTemp.Kind() == SyntaxKind.ConstructorDeclaration) {
                    ConstructorDeclarationSyntax member = (ConstructorDeclarationSyntax)memberTemp;
                    if (member.Identifier.ValueText == name) {
                        return member;
                    }
                } else if (memberTemp.Kind() == SyntaxKind.MethodDeclaration) {
                    MethodDeclarationSyntax member = (MethodDeclarationSyntax)members[i];
                    if (member.Identifier.ValueText == name) {
                        return member;
                    }
                }

            }

            return null;
        }

        public static List<OneParameterInfo> GetParameter(this MethodDeclarationSyntax method) {
            List<OneParameterInfo> parameterList = new List<OneParameterInfo>();

            for (int i = 0; i < method.ParameterList.Parameters.Count; i++) {
                ParameterSyntax parameterSyntax = method.ParameterList.Parameters[i];
                OneParameterInfo parameter = new OneParameterInfo();
                parameter.name = parameterSyntax.Identifier.ValueText;
                parameter.type = parameterSyntax.Type.ToString();
                parameterList.Add(parameter);
            }

            return parameterList;
        }
    }

}