using System;
using System.Reflection;

namespace GameArki.AttrInspector {

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class ATButtonAttribute : Attribute {

        string buttonName;
        public string ButtonName => buttonName;

        MethodInfo buttonMethod;
        public MethodInfo ButtonMethod => buttonMethod;

        public ATButtonAttribute(string buttonName) {
            this.buttonName = buttonName;
            buttonMethod = null;
        }

        public void SetButtonFunction(MethodInfo buttonMethod) {
            this.buttonMethod = buttonMethod;
        }
    }
}
