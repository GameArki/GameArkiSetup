using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GameArki.CSharpGen {

    public interface IClassEditor {

        // ---- INITIALIZATION ----
        void LoadCode(string code);

        // ---- GENERATE ----
        string Generate();

        // ---- CLASS ----
        void NewClass(string classNamespace, string classname, bool isStruct);
        void RenameClass(string newName);
        void InheritInterface(string interfaceName);
        void RemoveAllInherit();
        void InheritClass(string className);
        void RemoveInherit(string baseName);

        // ---- NAMESPACE ----
        string GetNameSpace();

        // ---- USING ----
        void RemoveAllUsings();
        void AddUsing(string usingName);

        // ---- FIELD ----
        void RemoveAllFields();
        void AddField(VisitLevel visitLevel, string fieldType, string fieldName, bool isStatic = false);
        void RemoveField(string fieldName);

        // ---- METHOD ----
        void RemoveAllMethods();
        void RemoveAllConstructor();
        void AddMethod(IMethodEditor method);
        void RemoveMethod(string methodName);
        void RenameMethod(string oldName, string newName);
        List<MethodDeclarationSyntax> GetAllMethods();

        // ---- Attribute ----
        void AddMemberAttribute(string memberName, string attributeStr);
        void AddClassAttribute(string className, string attributeStr);
        void RemoveMemberAttribute(string memberName, string attributeStr);
        void RemoveMemberAttribute(string memberName);
        void RemoveClassAttribute(string className, string attributeStr);
        void RemoveClassAttribute(string className);
        void RemoveAllAttribute();

        // ---- PROPERTY ----
        [Obsolete("方法未完成", true)]
        void AddProperty(string type, string name, bool get, bool set);

        // ---- Trivia ----
        void AddMethodNotes(string memberName, string notes);
        List<FieldDeclarationSyntax> GetAllFields();

        // ---- COMMENT ----

        // ---- INHERIT ----

    }
}