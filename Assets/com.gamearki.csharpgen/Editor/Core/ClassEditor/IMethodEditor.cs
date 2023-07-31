using System;
using System.Collections.Generic;

namespace GameArki.CSharpGen {

    public interface IMethodEditor {

        string Generate(int nestedCount = 0);
        string GenerateInterface(int nestedCount = 0);
        void Initialize(VisitLevel visitLevel, bool isStatic, string returnType, string name);
        void Initialize(string returnType, string name);
        void SetVisitLevel(VisitLevel visitLevel);
        void SetStatic(bool isStatic);
        void SetMethodName(string name);
        void SetReturnType(string returnType);
        void AddParameter(string type, string name);
        void AddParameters(string[] typeAndName);
        void AppendLine(string content);
        VisitLevel GetVisitLevel();
        bool GetIsStatic();
        string GetReturnType();
        string GetName();
    }

}