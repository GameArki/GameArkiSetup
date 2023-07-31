using GameArki.CSharpGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameArki.CSharpGen {

    public class MethodEditor : IMethodEditor {

        VisitLevel visitLevel;
        bool isStatic;
        string returnType;
        string name;
        List<OneParameterInfo> parameters = new List<OneParameterInfo>();
        List<string> genericTypes = new List<string>();
        StringBuilder content;

        public MethodEditor() {
            this.parameters = new List<OneParameterInfo>();
            this.content = new StringBuilder();
        }

        public string Generate(int nestedCount = 0) {

            string staticStr = (isStatic ? "static " : "");
            string paramStr = "";
            int index = 0;
            parameters.ForEach(value => {
                if (index == parameters.Count - 1) {
                    paramStr += value.type + " " + value.name;
                } else {
                    paramStr += value.type + " " + value.name + ", ";
                }
                index += 1;
            });

            string genericTypeStr = "";
            index = 0;
            if (genericTypes.Count > 0) {
                genericTypeStr += "<";
                genericTypes.ForEach(value => {
                    if (index == genericTypes.Count - 1) {
                        genericTypeStr += value;
                    } else {
                        genericTypeStr += value + ", ";
                    }
                });
                genericTypeStr += ">";
            }
            string str = visitLevel.ToFullString() + " " + staticStr + "" + returnType + " " + name + genericTypeStr + "(" + paramStr + ") {";
            str += content.ToString();
            str += "}";

            return str;

        }

        public string GenerateInterface(int nestedCount = 0) {
            string methodStr = "namespace FunnyAST {\n\tinterface Demo{\n";
            string returnType = GetReturnType();
            string name = GetName();

            string parmeters = "";
            List<OneParameterInfo> parameterList = parameters;
            for (int i = 0; i < parameterList.Count; i++) {
                parmeters += parameterList[i].type + " " + parameterList[i].name;
                if (i != parameterList.Count - 1) {
                    parmeters += ",";
                }
            }

            methodStr += StringUtil.Repeat("\t", nestedCount) + returnType + " " + name + "(" + parmeters + ");\r\n";

            methodStr += "\t}\r\n}\r\n";
            return methodStr;
        }

        public void AddParameter(string type, string name) {
            OneParameterInfo p = new OneParameterInfo();
            p.type = type;
            p.name = name;
            parameters.Add(p);
        }

        /// <summary>
        /// 示例：AddParameters(new string[] { "int i", "float f", "string s" });
        /// </summary>
        public void AddParameters(params string[] typeAndName) {
            for (int i = 0; i < typeAndName.Length; i++) {
                string[] tempStr = typeAndName[i].Split(' ');
                AddParameter(tempStr[0], tempStr[1]);
            }
        }

        public void AddGenericType(params string[] types) {
            for (int i = 0; i < types.Length; i += 1) {
                genericTypes.Add(types[i]);
            }
        }

        public void Initialize(VisitLevel visitLevel, bool isStatic, string returnType, string name) {
            this.visitLevel = visitLevel;
            this.isStatic = isStatic;
            this.returnType = returnType;
            this.name = name;
        }

        public void Initialize(string returnType, string name) {
            this.returnType = returnType;
            this.name = name;
        }

        public void AppendLine(string txt) {
            content.AppendLine(txt);
        }

        public void SetMethodName(string name) {
            this.name = name;
        }

        public void SetReturnType(string returnType) {
            this.returnType = returnType;
        }

        public void SetStatic(bool isStatic) {
            this.isStatic = isStatic;
        }

        public void SetVisitLevel(VisitLevel visitLevel) {
            this.visitLevel = visitLevel;
        }

        public VisitLevel GetVisitLevel() {
            return this.visitLevel;
        }

        public bool GetIsStatic() {
            return isStatic;
        }

        public string GetReturnType() {
            return returnType;
        }

        public string GetName() {
            return name;
        }

    }
}
