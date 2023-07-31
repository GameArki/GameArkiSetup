using System;
using System.Text;
using System.Collections.Generic;

namespace GameArki.CSharpGen.Protobuf {

    public class ProtobufEditor : IProtobufEditor {

        int index;

        string name;
        string nameSpace;
        List<string> fieldList; // TODO - 用独立的数据结构

        public ProtobufEditor() {
            this.index = 1;
            this.name = string.Empty;
            this.nameSpace = string.Empty;
            this.fieldList = new List<string>();
        }

        public void AddField(ProtobufBaseFieldType fieldType, string fieldName, bool isRepeat = false) {
            
            string field = "";
            
            if (isRepeat) {
                field += "repeated ";
            }

            field += fieldType.ToString().ToLower() + " ";

            field += fieldName + " = " + index.ToString() + ";";

            index += 1;

            fieldList.Add(field);

        }

        public void AddField(string fieldType, string fieldName, bool isRepeat = false) {
            ProtobufBaseFieldType type = ToProtobufBaseFieldType(fieldType);
            AddField(type, fieldName, isRepeat);
        }

        ProtobufBaseFieldType ToProtobufBaseFieldType(string fieldType) {
            switch(fieldType) {
                case "bool":
                    return ProtobufBaseFieldType.Bool;
                case "byte":
                case "ushort":
                case "uint":
                    return ProtobufBaseFieldType.UInt32;
                case "int":
                case "short":
                    return ProtobufBaseFieldType.Int32;
                case "sbyte":
                    return ProtobufBaseFieldType.SInt32;
                case "long":
                    return ProtobufBaseFieldType.Int64;
                case "ulong":
                    return ProtobufBaseFieldType.UInt64;
                case "float":
                    return ProtobufBaseFieldType.Float;
                case "double":
                    return ProtobufBaseFieldType.Double;
                case "string":
                    return ProtobufBaseFieldType.String;
                case "byte[]":
                    return ProtobufBaseFieldType.Bytes;
                default:
                    throw new Exception("未实现: " + fieldType);
            }
        }

        public string Generate() {

            if (string.IsNullOrEmpty(name)) {
                throw new Exception("name 不可为空");
            }

            StringBuilder code = new StringBuilder();

            code.AppendLine("syntax = \"proto3\";\r\n");

            if (!string.IsNullOrEmpty(nameSpace)) {
                code.AppendLine("package " + nameSpace + ";\r\n");
            }

            code.AppendLine("message " + name + " {");

            fieldList.ForEach(value => {
                code.AppendLine("\t" + value);
            });

            code.AppendLine("}");

            return code.ToString();

        }

        public void SetName(string name) {
            this.name = name;
        }

        public void SetNamespace(string nameSpace) {
            if (string.IsNullOrEmpty(nameSpace)) {
                return;
            }
            this.nameSpace = nameSpace;
        }

    }

}