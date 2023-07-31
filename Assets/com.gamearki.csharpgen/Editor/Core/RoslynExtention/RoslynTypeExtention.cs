using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GameArki.CSharpGen {

    public static class RoslynTypeExtention {

        public static T InsertField<T>(this T type, FieldDeclarationSyntax field) where T : TypeDeclarationSyntax {

            if (type.Members.Count > 0) {
                //类中有成员，将字段添加在方法的前面
                for (int i = 0; i < type.Members.Count; i++) {
                    //Console.WriteLine(i +" "+classes.Members[i].Kind());
                    if (type.Members[i].Kind() == SyntaxKind.MethodDeclaration) {
                        type = type.InsertNodesBefore(type.Members[i], new List<SyntaxNode>() { field });
                        break;
                    }
                    if (i + 1 >= type.Members.Count) {
                        type = type.InsertNodesAfter(type.Members[i], new List<SyntaxNode>() { field });
                        break;
                    }
                }
            } else {
                //类中无成员
                type = (T)type.AddMembers(field);
            }

            return type;

        }

    }

}