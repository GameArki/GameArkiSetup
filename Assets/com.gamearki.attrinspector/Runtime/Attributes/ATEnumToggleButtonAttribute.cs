using System;
using System.Reflection;

namespace GameArki.AttrInspector {

    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class ATEnumToggleButtonAttribute : Attribute {

        string label;
        public string Label => label;

        bool[] isButtonClick;
        public bool[] IsButtonClick => isButtonClick;
        public void InitButtonClick(int buttonNums) => isButtonClick = new bool[buttonNums];

        FieldInfo belongField;
        public FieldInfo BelongField => belongField;
        public void SetBelongField(FieldInfo belongField) => this.belongField = belongField;

        int buttonWidth;
        public int ButtonWidth => buttonWidth;

        int buttonHeight;
        public int ButtonHeight => buttonHeight;
        
        public ATEnumToggleButtonAttribute(string label) {
            this.label = label;
            belongField = null;
            buttonWidth = 105;
            buttonHeight = 30;
        }

        public ATEnumToggleButtonAttribute(string label,int buttonWidth,int buttonHeight){
            this.label = label;
            belongField = null;
           this.buttonWidth = buttonWidth;
           this.buttonHeight = buttonHeight;
        }

        public void SetBelongFieldValue(object target, int value) {
            if (belongField == null) return;
            if (belongField.FieldType.IsEnum) {
                if (belongField.FieldType.IsDefined(typeof(System.FlagsAttribute), false)) {
                    int currentValue = (int)belongField.GetValue(target);
                    if ((currentValue & value) == value) {
                        currentValue -= value;
                    } else {
                        currentValue += value;
                    }
                    belongField.SetValue(target, currentValue);
                } else {
                    belongField.SetValue(target, value);
                }
            }
        }

        public bool IsContainValue(object target, int value) {
            if (belongField.FieldType.IsDefined(typeof(System.FlagsAttribute), false)) {
                return ((int)belongField.GetValue(target) & value) == value;
            }
            return (int)belongField.GetValue(target) == value;
        }

    }

}
