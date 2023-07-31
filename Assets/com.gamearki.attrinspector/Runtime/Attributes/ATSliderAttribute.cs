using System;
using System.Reflection;

namespace GameArki.AttrInspector {

    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public sealed class ATSliderAttribute : Attribute {

        FieldInfo belongField;
        public FieldInfo BelongField => belongField;
        public void SetBelongField(FieldInfo value) => belongField = value;

        public string MinName { get; }
        public string MaxName { get; }

        public ATSliderAttribute(string minName, string maxName) {
            this.MinName = minName;
            this.MaxName = maxName;
        }
        
        
    }

}
