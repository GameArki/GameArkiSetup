using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GameArki.CSharpGen;

namespace GameArki.BufferIO.Editor {

    public class BufferExtraGenerator {

        const string WRITE_TO_METHOD_NAME = "WriteTo";
        const string FROM_BYTES_METHOD_NAME = "FromBytes";
        const string GET_EVELUATED_SIZE_METHOD_NAME = "GetEvaluatedSize";
        const string TO_BYTES_METHOD_NAME = "ToBytes";

        static string ATTR = nameof(BufferIOMessageObjectAttribute).Replace("Attribute", "");

        public static void GenModel(string inputDir) {

            List<string> files = FindAllFileWithExt(inputDir, "*.cs");
            files.ForEach(value => {
                string code = File.ReadAllText(value);
                ClassEditor classEditor = new ClassEditor();
                classEditor.LoadCode(code);
                bool hasAttr = classEditor.HasClassAttribute(ATTR);
                if (!hasAttr) {
                    return;
                }

                MethodEditor writeToMethod = GenWriteToMethod(inputDir, classEditor);
                classEditor.RemoveMethod(writeToMethod.GetName());
                classEditor.AddMethod(writeToMethod);

                MethodEditor fromBytesMethod = GenFromBytesMethod(inputDir, classEditor);
                classEditor.RemoveMethod(fromBytesMethod.GetName());
                classEditor.AddMethod(fromBytesMethod);

                MethodEditor getEvaluatedSizeMethod = GenGetEvaluatedSizeMethod(inputDir, classEditor);
                classEditor.RemoveMethod(getEvaluatedSizeMethod.GetName());
                classEditor.AddMethod(getEvaluatedSizeMethod);

                MethodEditor toBytesMethod = GenToBytesMethod(inputDir, classEditor);
                classEditor.RemoveMethod(toBytesMethod.GetName());
                classEditor.AddMethod(toBytesMethod);

                string typeName = $"{typeof(IBufferIOMessage<>).Name.Replace("`1", "")}<{classEditor.GetClassName()}>";
                classEditor.RemoveInherit(typeName);
                classEditor.InheritInterface(typeName);

                classEditor.AddUsing(nameof(System));
                classEditor.AddUsing(nameof(GameArki) + "." + nameof(GameArki.BufferIO));

                File.WriteAllText(value, classEditor.Generate());

            });

        }

        static MethodEditor GenWriteToMethod(string inputDir, ClassEditor classEditor) {

            const string DST_PARAM_TYPE = "byte[]";
            const string DST_PARAM_NAME = "dst";
            const string OFFSET_PARAM_TYPE = "ref int";
            const string OFFSET_PARAM_NAME = "offset";

            string WriteLine(string fieldType, string fieldName) {
                const string WRITER = nameof(BufferWriter) + ".";
                string paramStr = DST_PARAM_NAME + ", " + fieldName + ", " + "ref " + OFFSET_PARAM_NAME;
                string writeSuffix = $"({paramStr});";
                switch (fieldType) {
                    case "bool": return WRITER + nameof(BufferWriter.WriteBool) + writeSuffix;
                    case "char": return WRITER + nameof(BufferWriter.WriteChar) + writeSuffix;
                    case "byte": return WRITER + nameof(BufferWriter.WriteUInt8) + writeSuffix;
                    case "sbyte": return WRITER + nameof(BufferWriter.WriteInt8) + writeSuffix;
                    case "short": return WRITER + nameof(BufferWriter.WriteInt16) + writeSuffix;
                    case "ushort": return WRITER + nameof(BufferWriter.WriteUInt16) + writeSuffix;
                    case "int": return WRITER + nameof(BufferWriter.WriteInt32) + writeSuffix;
                    case "uint": return WRITER + nameof(BufferWriter.WriteUInt32) + writeSuffix;
                    case "long": return WRITER + nameof(BufferWriter.WriteInt64) + writeSuffix;
                    case "ulong": return WRITER + nameof(BufferWriter.WriteUInt64) + writeSuffix;
                    case "float": return WRITER + nameof(BufferWriter.WriteSingle) + writeSuffix;
                    case "double": return WRITER + nameof(BufferWriter.WriteDouble) + writeSuffix;
                    case "bool[]": return WRITER + nameof(BufferWriter.WriteBoolArr) + writeSuffix;
                    case "byte[]": return WRITER + nameof(BufferWriter.WriteUint8Arr) + writeSuffix;
                    case "sbyte[]": return WRITER + nameof(BufferWriter.WriteInt8Arr) + writeSuffix;
                    case "short[]": return WRITER + nameof(BufferWriter.WriteInt16Arr) + writeSuffix;
                    case "ushort[]": return WRITER + nameof(BufferWriter.WriteUInt16Arr) + writeSuffix;
                    case "int[]": return WRITER + nameof(BufferWriter.WriteInt32Arr) + writeSuffix;
                    case "uint[]": return WRITER + nameof(BufferWriter.WriteUInt32Arr) + writeSuffix;
                    case "long[]": return WRITER + nameof(BufferWriter.WriteInt64Arr) + writeSuffix;
                    case "ulong[]": return WRITER + nameof(BufferWriter.WriteUInt64Arr) + writeSuffix;
                    case "float[]": return WRITER + nameof(BufferWriter.WriteSingleArr) + writeSuffix;
                    case "double[]": return WRITER + nameof(BufferWriter.WriteDoubleArr) + writeSuffix;
                    case "string": return WRITER + nameof(BufferWriter.WriteUTF8String) + writeSuffix;
                    case "string[]": return WRITER + nameof(BufferWriter.WriteUTF8StringArr) + writeSuffix;
                    default:

                        if (fieldType == classEditor.GetClassName()) {
                            throw new Exception($"不可循环依赖: {fieldType}");
                        }

                        const string WRITER_EXTRA = nameof(BufferWriterExtra) + ".";
                        if (fieldType.Contains("[]")) {
                            string trueType = fieldType.Replace("[]", "");
                            if (IsBufferObject(inputDir, trueType)) {
                                return WRITER_EXTRA + nameof(BufferWriterExtra.WriteMessageArr) + writeSuffix;
                            } else {
                                throw new Exception($"未处理该类型: {fieldType}");
                            }
                        } else {
                            if (IsBufferObject(inputDir, fieldType)) {
                                return WRITER_EXTRA + nameof(BufferWriterExtra.WriteMessage) + writeSuffix;
                            } else {
                                throw new Exception($"未处理该类型: {fieldType}");
                            }
                        }

                }
            }

            MethodEditor methodEditor = new MethodEditor();
            methodEditor.Initialize(VisitLevel.Public, false, "void", WRITE_TO_METHOD_NAME);
            methodEditor.AddParameter(DST_PARAM_TYPE, DST_PARAM_NAME);
            methodEditor.AddParameter(OFFSET_PARAM_TYPE, OFFSET_PARAM_NAME);
            var fieldList = classEditor.GetAllFields();
            for (int i = 0; i < fieldList.Count; i += 1) {
                var field = fieldList[i];
                string type = field.GetFieldType();
                string name = field.GetFieldName();
                string line = WriteLine(type, name);
                methodEditor.AppendLine(line);
            }
            return methodEditor;
        }

        static MethodEditor GenFromBytesMethod(string inputDir, ClassEditor classEditor) {
            const string SRC_PARAM_TYPE = "byte[]";
            const string SRC_PARAM_NAME = "src";
            const string OFFSET_PARAM_TYPE = "ref int";
            const string OFFSET_PARAM_NAME = "offset";

            string WriteLine(string fieldType, string fieldName) {
                const string READER = nameof(BufferReader) + ".";
                string paramStr = SRC_PARAM_NAME + ", " + "ref " + OFFSET_PARAM_NAME;
                string readPrefix = fieldName + " = " + READER;
                string readSuffix = $"({paramStr});";
                switch (fieldType) {
                    case "bool": return readPrefix + nameof(BufferReader.ReadBool) + readSuffix;
                    case "char": return readPrefix + nameof(BufferReader.ReadChar) + readSuffix;
                    case "byte": return readPrefix + nameof(BufferReader.ReadUInt8) + readSuffix;
                    case "sbyte": return readPrefix + nameof(BufferReader.ReadInt8) + readSuffix;
                    case "short": return readPrefix + nameof(BufferReader.ReadInt16) + readSuffix;
                    case "ushort": return readPrefix + nameof(BufferReader.ReadUInt16) + readSuffix;
                    case "int": return readPrefix + nameof(BufferReader.ReadInt32) + readSuffix;
                    case "uint": return readPrefix + nameof(BufferReader.ReadUInt32) + readSuffix;
                    case "long": return readPrefix + nameof(BufferReader.ReadInt64) + readSuffix;
                    case "ulong": return readPrefix + nameof(BufferReader.ReadUInt64) + readSuffix;
                    case "float": return readPrefix + nameof(BufferReader.ReadSingle) + readSuffix;
                    case "double": return readPrefix + nameof(BufferReader.ReadDouble) + readSuffix;
                    case "bool[]": return readPrefix + nameof(BufferReader.ReadBoolArr) + readSuffix;
                    case "byte[]": return readPrefix + nameof(BufferReader.ReadUInt8Arr) + readSuffix;
                    case "sbyte[]": return readPrefix + nameof(BufferReader.ReadInt8Arr) + readSuffix;
                    case "short[]": return readPrefix + nameof(BufferReader.ReadInt16Arr) + readSuffix;
                    case "ushort[]": return readPrefix + nameof(BufferReader.ReadUInt16Arr) + readSuffix;
                    case "int[]": return readPrefix + nameof(BufferReader.ReadInt32Arr) + readSuffix;
                    case "uint[]": return readPrefix + nameof(BufferReader.ReadUInt32Arr) + readSuffix;
                    case "long[]": return readPrefix + nameof(BufferReader.ReadInt64Arr) + readSuffix;
                    case "ulong[]": return readPrefix + nameof(BufferReader.ReadUInt64Arr) + readSuffix;
                    case "float[]": return readPrefix + nameof(BufferReader.ReadSingleArr) + readSuffix;
                    case "double[]": return readPrefix + nameof(BufferReader.ReadDoubleArr) + readSuffix;
                    case "string": return readPrefix + nameof(BufferReader.ReadUTF8String) + readSuffix;
                    case "string[]": return readPrefix + nameof(BufferReader.ReadUTF8StringArr) + readSuffix;
                    default:

                        if (fieldType == classEditor.GetClassName()) {
                            throw new Exception($"不可循环依赖: {fieldType}");
                        }

                        const string READER_EXTRA = nameof(BufferReaderExtra) + ".";
                        if (fieldType.Contains("[]")) {
                            // 处理自定义类型数组
                            string trueType = fieldType.Replace("[]", "");
                            if (IsBufferObject(inputDir, trueType)) {
                                return $"{fieldName} = " + READER_EXTRA + nameof(BufferReaderExtra.ReadMessageArr) + $"({SRC_PARAM_NAME}, () => new {trueType}(), ref {OFFSET_PARAM_NAME});";
                            } else {
                                throw new Exception($"未处理该类型: {fieldType}");
                            }
                        } else {
                            // 处理单自定义类型
                            if (IsBufferObject(inputDir, fieldType)) {
                                return $"{fieldName} = " + READER_EXTRA + nameof(BufferReaderExtra.ReadMessage) + $"({SRC_PARAM_NAME}, () => new {fieldType}(), ref {OFFSET_PARAM_NAME});";
                            } else {
                                throw new Exception($"未处理该类型: {fieldType}");
                            }
                        }

                }
            }

            MethodEditor methodEditor = new MethodEditor();
            methodEditor.Initialize(VisitLevel.Public, false, "void", FROM_BYTES_METHOD_NAME);
            methodEditor.AddParameter(SRC_PARAM_TYPE, SRC_PARAM_NAME);
            methodEditor.AddParameter(OFFSET_PARAM_TYPE, OFFSET_PARAM_NAME);
            var fieldList = classEditor.GetAllFields();
            for (int i = 0; i < fieldList.Count; i += 1) {
                var field = fieldList[i];
                string type = field.GetFieldType();
                string name = field.GetFieldName();
                string line = WriteLine(type, name);
                methodEditor.AppendLine(line);
            }
            return methodEditor;

        }

        static MethodEditor GenGetEvaluatedSizeMethod(string inputDir, ClassEditor classEditor) {

            const string CERTAIN_PARAM_TYPE = "out bool";
            const string CERTAIN_PARAM_NAME = "isCertain";
            const string COUNT_TYPE = "int";
            const string COUNT_VAR = "count";

            MethodEditor methodEditor = new MethodEditor();
            methodEditor.Initialize(VisitLevel.Public, false, "int", GET_EVELUATED_SIZE_METHOD_NAME);
            methodEditor.AddParameter(CERTAIN_PARAM_TYPE, CERTAIN_PARAM_NAME);

            StringBuilder certainLine = new StringBuilder();
            StringBuilder dealCertainLine = new StringBuilder();
            StringBuilder evaluatedStringLine = new StringBuilder();
            StringBuilder evaluatedObjectLine = new StringBuilder();

            bool isCertain = true;
            int certainCount = 0;

            void WriteCount(string fieldType, string fieldName) {

                string dealStr = COUNT_VAR + " += " + fieldName + ".Length";

                switch (fieldType) {

                    case "bool":
                    case "byte":
                    case "sbyte": certainCount += 1; break;
                    case "char":
                    case "short":
                    case "ushort": certainCount += 2; break;
                    case "int":
                    case "uint":
                    case "float": certainCount += 4; break;
                    case "long":
                    case "ulong":
                    case "double": certainCount += 8; break;

                    case "bool[]":
                    case "byte[]":
                    case "sbyte[]":
                        dealCertainLine.AppendLine($"if ({fieldName} != null) " + "{");
                        dealCertainLine.AppendLine(dealStr + ";");
                        dealCertainLine.AppendLine("}");
                        certainCount += 2;
                        break;
                    case "short[]":
                    case "ushort[]":
                        dealCertainLine.AppendLine($"if ({fieldName} != null) " + "{");
                        dealCertainLine.AppendLine(dealStr + " * 2;");
                        dealCertainLine.AppendLine("}");
                        certainCount += 2;
                        break;
                    case "int[]":
                    case "uint[]":
                    case "float[]":
                        dealCertainLine.AppendLine($"if ({fieldName} != null) " + "{");
                        dealCertainLine.AppendLine(dealStr + " * 4;");
                        dealCertainLine.AppendLine("}");
                        certainCount += 2;
                        break;
                    case "long[]":
                    case "ulong[]":
                    case "double[]":
                        dealCertainLine.AppendLine($"if ({fieldName} != null) " + "{");
                        dealCertainLine.AppendLine(dealStr + " * 8;");
                        dealCertainLine.AppendLine("}");
                        certainCount += 2;
                        break;

                    case "string":
                        isCertain = false;
                        evaluatedStringLine.AppendLine($"if ({fieldName} != null) " + "{");
                        evaluatedStringLine.AppendLine(COUNT_VAR + " += " + fieldName + ".Length * 4;");
                        evaluatedStringLine.AppendLine("}");
                        certainCount += 2;
                        break;

                    case "string[]":
                        isCertain = false;
                        evaluatedStringLine.AppendLine($"if ({fieldName} != null) " + "{");
                        string str = $"for (int i = 0; i < {fieldName}.Length; i += 1)" + "{" + $"{COUNT_VAR} += {fieldName}[i].Length * 4;" + "}";
                        evaluatedStringLine.AppendLine(str);
                        evaluatedStringLine.AppendLine("}");
                        certainCount += 2;
                        break;

                    default:

                        if (fieldType == classEditor.GetClassName()) {
                            throw new Exception($"不可循环依赖: {fieldType}");
                        }

                        if (fieldType.Contains("[]")) {
                            string trueType = fieldType.Replace("[]", "");
                            if (IsBufferObject(inputDir, trueType)) {
                                const string CHILD = "__child";
                                string s = $"if ({fieldName} != null)" + "{"
                                        + $"for (int i = 0; i < {fieldName}.Length; i += 1)" + "{"
                                            + $"var {CHILD} = {fieldName}[i];"
                                            + $"{COUNT_VAR} += {CHILD}." + GET_EVELUATED_SIZE_METHOD_NAME + $"(out bool _cb_{fieldName});"
                                            + CERTAIN_PARAM_NAME + "&=" + $"_cb_{fieldName};"
                                            + "}"
                                        + "}";
                                evaluatedObjectLine.AppendLine(s);
                                certainCount += 2;
                            } else {
                                throw new Exception($"未处理该类型: {fieldType}");
                            }
                        } else {
                            if (IsBufferObject(inputDir, fieldType)) {
                                string s = COUNT_VAR + $" += {fieldName}.{GET_EVELUATED_SIZE_METHOD_NAME}(out bool _b{fieldName});\r\n";
                                s += CERTAIN_PARAM_NAME + "&=" + $"_b{fieldName};";
                                evaluatedObjectLine.AppendLine($"if ({fieldName} != null) " + "{");
                                evaluatedObjectLine.AppendLine(s);
                                evaluatedObjectLine.AppendLine("}");
                                certainCount += 2;
                            } else {
                                throw new Exception($"未处理该类型: {fieldType}");
                            }
                        }
                        break;
                }
            }

            var fieldList = classEditor.GetAllFields();
            for (int i = 0; i < fieldList.Count; i += 1) {
                var field = fieldList[i];
                string type = field.GetFieldType();
                string name = field.GetFieldName();
                WriteCount(type, name);
            }

            methodEditor.AppendLine(COUNT_TYPE + " " + COUNT_VAR + $" = {certainCount};");
            methodEditor.AppendLine(CERTAIN_PARAM_NAME + " = " + isCertain.ToString().ToLower() + ";");
            methodEditor.AppendLine(certainLine.ToString());
            methodEditor.AppendLine(dealCertainLine.ToString());
            methodEditor.AppendLine(evaluatedStringLine.ToString());
            methodEditor.AppendLine(evaluatedObjectLine.ToString());
            methodEditor.AppendLine("return " + COUNT_VAR + ";");
            return methodEditor;

        }

        static MethodEditor GenToBytesMethod(string inputDir, ClassEditor classEditor) {

            const string COUNT_TYPE = "int";
            const string COUNT_VAR = "count";
            const string OFFSET_TYPE = "int";
            const string OFFSET_VAR = "offset";
            const string CERTAIN_TYPE = "out bool";
            const string CERTAIN_VAR = "isCertain";
            const string SRC_VAR = "src";
            const string DST_VAR = "dst";

            MethodEditor methodEditor = new MethodEditor();
            methodEditor.Initialize(VisitLevel.Public, false, "byte[]", TO_BYTES_METHOD_NAME);
            methodEditor.AppendLine(COUNT_TYPE + " " + COUNT_VAR + " = " + GET_EVELUATED_SIZE_METHOD_NAME + $"({CERTAIN_TYPE} {CERTAIN_VAR});");
            methodEditor.AppendLine(OFFSET_TYPE + " " + OFFSET_VAR + " = 0;");
            methodEditor.AppendLine("byte[] " + SRC_VAR + $" = new byte[{COUNT_VAR}];");
            methodEditor.AppendLine(WRITE_TO_METHOD_NAME + $"({SRC_VAR}, ref {OFFSET_VAR});");
            methodEditor.AppendLine($"if ({CERTAIN_VAR})" + "{ return " + SRC_VAR + ";}");
            methodEditor.AppendLine("else {"
                                    + $"byte[] {DST_VAR} = new byte[{OFFSET_VAR}];"
                                    + $"Buffer.BlockCopy({SRC_VAR}, 0, {DST_VAR}, 0, {OFFSET_VAR});"
                                    + $"return {DST_VAR};"
                                    + "}");
            return methodEditor;

        }

        static bool IsBufferObject(string inputDir, string fieldType) {
            string filePath = FindFileWithExt(inputDir, fieldType, "*.cs");
            if (string.IsNullOrEmpty(filePath)) {
                throw new Exception($"找不到代码文件: {fieldType}.cs");
            }

            ClassEditor tarTypeClass = new ClassEditor();
            tarTypeClass.LoadCode(File.ReadAllText(filePath));
            bool hasAttr = tarTypeClass.HasClassAttribute(ATTR);
            return hasAttr;
        }

        // 找到某个文件
        static string FindFileWithExt(string rootPath, string fileName, string ext) {
            List<string> all = FindAllFileWithExt(rootPath, ext);
            return all.Find(value => value.Contains(fileName + ext.TrimStart('*')));
        }

        // 递归
        static List<string> FindAllFileWithExt(string rootPath, string ext) {

            List<string> fileList = new List<string>();

            DirectoryInfo directoryInfo = new DirectoryInfo(rootPath);
            FileInfo[] allFiles = directoryInfo.GetFiles(ext);
            for (int i = 0; i < allFiles.Length; i += 1) {
                var file = allFiles[i];
                fileList.Add(file.FullName);
            }

            DirectoryInfo[] childrenDirs = directoryInfo.GetDirectories();
            for (int i = 0; i < childrenDirs.Length; i += 1) {
                var dir = childrenDirs[i];
                fileList.AddRange(FindAllFileWithExt(dir.FullName, ext));
            }

            return fileList;

        }

    }

}