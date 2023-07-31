using System.Collections.Generic;

namespace GameArki.CSharpGen {
    public interface IInterfaceEditor {
        void Rename(string newName);
        void AddMethod(IMethodEditor method);
        void AddUsing(string usingName);
        string Generate();
        void LoadCode(string code);
        void RemoveAllMethods();
        void RemoveAllUsings();
        void RemoveMethod(string methodName);
        void RenameMethod(string oldName, string newName);
        List<OneParameterInfo> GetParameter(string methodName);
        List<MethodMember> GetAllMethodParameter();
    }
}