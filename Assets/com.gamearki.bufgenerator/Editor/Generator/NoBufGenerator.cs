using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GameArki.CSharpGen;

namespace GameArki.NoBuf {

    public static class NoBufGenerator {

        const string METHOD_NAME_WRITE_TO = "WriteTo";
        const string METHOD_NAME_FROM_BYTES = "FromBytes";
        const string METHOD_NAME_DISPOSE = "Dispose";

        const string PARAM_TYPE_DST = "byte[]";
        const string PARAM_NAME_DST = "dst";
        const string PARAM_TYPE_SRC = "byte[]";
        const string PARAM_NAME_SRC = "src";
        const string PARAM_TYPE_OFFSET = "ref int";
        const string PARAM_NAME_OFFSET = "offset";

        public static void GenModel(string inputDir) {

            List<string> files = FindAllFileWithExt(inputDir, "*.cs");
            files.ForEach(value => {
                string code = File.ReadAllText(value);
                ClassEditor classEditor = new ClassEditor();
                classEditor.LoadCode(code);

                MethodEditor writeToMethod = WriteTo_GenMethod(classEditor);
                classEditor.RemoveMethod(writeToMethod.GetName());
                classEditor.AddMethod(writeToMethod);

                MethodEditor fromBytesMethod = GenFromBytesMethod(classEditor);
                classEditor.RemoveMethod(fromBytesMethod.GetName());
                classEditor.AddMethod(fromBytesMethod);

                MethodEditor disposeMethod = GenDisposeMethod(classEditor);
                classEditor.RemoveMethod(disposeMethod.GetName());
                classEditor.AddMethod(disposeMethod);

                classEditor.AddUsing(nameof(System));
                classEditor.AddUsing(nameof(GameArki) + "." + nameof(GameArki.NoBuf));
                classEditor.AddUsing(nameof(GameArki) + "." + nameof(GameArki.NativeBytes));

                File.WriteAllText(value, classEditor.Generate());

            });
        }

        // - WriteTo(byte[] dst, ref int offset)
        static MethodEditor WriteTo_GenMethod(ClassEditor classEditor) {

            MethodEditor methodEditor = new MethodEditor();
            methodEditor.Initialize(VisitLevel.Public, false, "void", METHOD_NAME_WRITE_TO);
            methodEditor.AddParameter(PARAM_TYPE_DST, PARAM_NAME_DST);
            methodEditor.AddParameter(PARAM_TYPE_OFFSET, PARAM_NAME_OFFSET);
            var fieldList = classEditor.GetAllFields();
            for (int i = 0; i < fieldList.Count; i += 1) {
                var field = fieldList[i];
                string type = field.GetFieldType();
                string name = field.GetFieldName();
                string line = WriteTo_MethodLine(type, name);
                methodEditor.AppendLine(line);
            }
            return methodEditor;
        }

        static string WriteTo_MethodLine(string fieldType, string fieldName) {
            const string WRITER = nameof(NBWriter) + ".";
            string paramStr = PARAM_NAME_DST + ", " + fieldName + ", " + "ref " + PARAM_NAME_OFFSET;
            string writeSuffix = $"({paramStr});";
            switch (fieldType) {
                case "bool": return WRITER + nameof(NBWriter.W_Bool) + writeSuffix;
                case "char": return WRITER + nameof(NBWriter.W_Char) + writeSuffix;
                case "byte": return WRITER + nameof(NBWriter.W_U8) + writeSuffix;
                case "sbyte": return WRITER + nameof(NBWriter.W_I8) + writeSuffix;
                case "short": return WRITER + nameof(NBWriter.W_I16) + writeSuffix;
                case "ushort": return WRITER + nameof(NBWriter.W_U16) + writeSuffix;
                case "int": return WRITER + nameof(NBWriter.W_I32) + writeSuffix;
                case "uint": return WRITER + nameof(NBWriter.W_U32) + writeSuffix;
                case "long": return WRITER + nameof(NBWriter.W_I64) + writeSuffix;
                case "ulong": return WRITER + nameof(NBWriter.W_U64) + writeSuffix;
                case "float": return WRITER + nameof(NBWriter.W_F32) + writeSuffix;
                case "double": return WRITER + nameof(NBWriter.W_F64) + writeSuffix;
                case "NBArray<bool>": return WRITER + nameof(NBWriter.W_BoolArr) + writeSuffix;
                case "NBArray<byte>": return WRITER + nameof(NBWriter.W_U8Arr) + writeSuffix;
                case "NBArray<sbyte>": return WRITER + nameof(NBWriter.W_I8Arr) + writeSuffix;
                case "NBArray<short>": return WRITER + nameof(NBWriter.W_I16Arr) + writeSuffix;
                case "NBArray<ushort>": return WRITER + nameof(NBWriter.W_U16Arr) + writeSuffix;
                case "NBArray<int>": return WRITER + nameof(NBWriter.W_I32Arr) + writeSuffix;
                case "NBArray<uint>": return WRITER + nameof(NBWriter.W_U32Arr) + writeSuffix;
                case "NBArray<long>": return WRITER + nameof(NBWriter.W_I64Arr) + writeSuffix;
                case "NBArray<ulong>": return WRITER + nameof(NBWriter.W_U64Arr) + writeSuffix;
                case "NBArray<float>": return WRITER + nameof(NBWriter.W_F32Arr) + writeSuffix;
                case "NBArray<double>": return WRITER + nameof(NBWriter.W_F64Arr) + writeSuffix;
                case "NBString": return WRITER + nameof(NBWriter.W_NBString) + writeSuffix;
                case "NBArray<NBString>": return WRITER + nameof(NBWriter.W_StringArr) + writeSuffix;
                default:
                    throw new Exception($"未处理该类型: {fieldType}");
            }
        }

        // - FromBytes(byte[] src, ref int offset)
        static MethodEditor GenFromBytesMethod(ClassEditor classEditor) {

            MethodEditor methodEditor = new MethodEditor();
            methodEditor.Initialize(VisitLevel.Public, false, "void", METHOD_NAME_FROM_BYTES);
            methodEditor.AddParameter(PARAM_TYPE_SRC, PARAM_NAME_SRC);
            methodEditor.AddParameter(PARAM_TYPE_OFFSET, PARAM_NAME_OFFSET);
            var fieldList = classEditor.GetAllFields();
            for (int i = 0; i < fieldList.Count; i += 1) {
                var field = fieldList[i];
                string type = field.GetFieldType();
                string name = field.GetFieldName();
                string line = FromBytes_MethodLine(type, name);
                methodEditor.AppendLine(line);
            }
            return methodEditor;

        }

        static string FromBytes_MethodLine(string fieldType, string fieldName) {
            const string READER = nameof(NBReader) + ".";
            string paramStr = PARAM_NAME_SRC + ", " + "ref " + PARAM_NAME_OFFSET;
            string readPrefix = fieldName + " = " + READER;
            string readSuffix = $"({paramStr});";
            switch (fieldType) {
                case "bool": return readPrefix + nameof(NBReader.R_Bool) + readSuffix;
                case "char": return readPrefix + nameof(NBReader.R_Char) + readSuffix;
                case "byte": return readPrefix + nameof(NBReader.R_U8) + readSuffix;
                case "sbyte": return readPrefix + nameof(NBReader.R_I8) + readSuffix;
                case "short": return readPrefix + nameof(NBReader.R_I16) + readSuffix;
                case "ushort": return readPrefix + nameof(NBReader.R_U16) + readSuffix;
                case "int": return readPrefix + nameof(NBReader.R_I32) + readSuffix;
                case "uint": return readPrefix + nameof(NBReader.R_U32) + readSuffix;
                case "long": return readPrefix + nameof(NBReader.R_I64) + readSuffix;
                case "ulong": return readPrefix + nameof(NBReader.R_U64) + readSuffix;
                case "float": return readPrefix + nameof(NBReader.R_F32) + readSuffix;
                case "double": return readPrefix + nameof(NBReader.R_F64) + readSuffix;
                case "NBArray<bool>": return readPrefix + nameof(NBReader.R_BoolArr) + readSuffix;
                case "NBArray<byte>": return readPrefix + nameof(NBReader.R_U8Arr) + readSuffix;
                case "NBArray<sbyte>": return readPrefix + nameof(NBReader.R_I8Arr) + readSuffix;
                case "NBArray<short>": return readPrefix + nameof(NBReader.R_I16Arr) + readSuffix;
                case "NBArray<ushort>": return readPrefix + nameof(NBReader.R_U16Arr) + readSuffix;
                case "NBArray<int>": return readPrefix + nameof(NBReader.R_I32Arr) + readSuffix;
                case "NBArray<uint>": return readPrefix + nameof(NBReader.R_U32Arr) + readSuffix;
                case "NBArray<long>": return readPrefix + nameof(NBReader.R_I64Arr) + readSuffix;
                case "NBArray<ulong>": return readPrefix + nameof(NBReader.R_U64Arr) + readSuffix;
                case "NBArray<float>": return readPrefix + nameof(NBReader.R_F32Arr) + readSuffix;
                case "NBArray<double>": return readPrefix + nameof(NBReader.R_F64Arr) + readSuffix;
                case "NBString": return readPrefix + nameof(NBReader.R_String) + readSuffix;
                case "NBArray<NBString>": return readPrefix + nameof(NBReader.R_StringArr) + readSuffix;
                default:
                    throw new Exception($"未处理该类型: {fieldType}");
            }
        }

        // - Dispose();
        static MethodEditor GenDisposeMethod(ClassEditor classEditor) {
            MethodEditor methodEditor = new MethodEditor();
            methodEditor.Initialize(VisitLevel.Public, false, "void", METHOD_NAME_DISPOSE);
            var fieldList = classEditor.GetAllFields();
            for (int i = 0; i < fieldList.Count; i += 1) {
                var field = fieldList[i];
                string type = field.GetFieldType();
                string name = field.GetFieldName();
                if (type.StartsWith("NBArray<") || type.StartsWith("NBString")) {
                    string line = name + ".Dispose();";
                    methodEditor.AppendLine(line);
                }
            }
            return methodEditor;
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