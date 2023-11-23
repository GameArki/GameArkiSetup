using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GameArki.CSharpGen;

namespace GameArki.BufferIO.Editor {

    public class BufferExtraGenerator {

        const string n_WriteTo = "WriteTo";
        const string n_FromBytes = "FromBytes";
        const string n_GetEvaluatedSize = "GetEvaluatedSize";
        const string n_ToBytes = "ToBytes";

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

                string typeName = $"{typeof(IBufferIOMessage<>).Name.Replace("`1", "")}<{classEditor.GetClassName()}>";
                classEditor.RemoveInherit(typeName);
                classEditor.InheritInterface(typeName);

                classEditor.AddUsing(nameof(System));
                classEditor.AddUsing(nameof(GameArki) + "." + nameof(GameArki.BufferIO));

                File.WriteAllText(value, classEditor.Generate());

            });

        }

        static MethodEditor GenWriteToMethod(string inputDir, ClassEditor classEditor) {

            const string t_dst = "byte[]";
            const string n_dst = "dst";
            const string t_ref_int = "ref int";
            const string n_offset = "offset";

            string WriteLine(string t_field, string n_field) {
                string paramSuffix = $"{n_dst}, {n_field}, ref {n_offset}";
                switch (t_field) {
                    case "byte": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteUInt8)}({paramSuffix});";
                    case "byte[]": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteUint8Arr)}({paramSuffix});";
                    case "List<byte>": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteUint8List)}({paramSuffix});";
                    case "sbyte": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteInt8)}({paramSuffix});";
                    case "sbyte[]": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteInt8Arr)}({paramSuffix});";
                    case "List<sbyte>": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteInt8List)}({paramSuffix});";
                    case "bool": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteBool)}({paramSuffix});";
                    case "bool[]": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteBoolArr)}({paramSuffix});";
                    case "List<bool>": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteBoolList)}({paramSuffix});";
                    case "short": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteInt16)}({paramSuffix});";
                    case "short[]": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteInt16Arr)}({paramSuffix});";
                    case "List<short>": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteInt16List)}({paramSuffix});";
                    case "ushort": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteUInt16)}({paramSuffix});";
                    case "ushort[]": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteUInt16Arr)}({paramSuffix});";
                    case "List<ushort>": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteUInt16List)}({paramSuffix});";
                    case "int": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteInt32)}({paramSuffix});";
                    case "int[]": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteInt32Arr)}({paramSuffix});";
                    case "List<int>": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteInt32List)}({paramSuffix});";
                    case "uint": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteUInt32)}({paramSuffix});";
                    case "uint[]": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteUInt32Arr)}({paramSuffix});";
                    case "List<uint>": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteUInt32List)}({paramSuffix});";
                    case "long": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteInt64)}({paramSuffix});";
                    case "long[]": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteInt64Arr)}({paramSuffix});";
                    case "List<long>": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteInt64List)}({paramSuffix});";
                    case "ulong": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteUInt64)}({paramSuffix});";
                    case "ulong[]": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteUInt64Arr)}({paramSuffix});";
                    case "List<ulong>": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteUInt64List)}({paramSuffix});";
                    case "float": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteSingle)}({paramSuffix});";
                    case "float[]": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteSingleArr)}({paramSuffix});";
                    case "List<float>": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteSingleList)}({paramSuffix});";
                    case "double": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteDouble)}({paramSuffix});";
                    case "double[]": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteDoubleArr)}({paramSuffix});";
                    case "List<double>": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteDoubleList)}({paramSuffix});";
                    case "char": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteChar)}({paramSuffix});";
                    case "string": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteUTF8String)}({paramSuffix});";
                    case "string[]": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteUTF8StringArr)}({paramSuffix});";
                    case "List<string>": return $"{nameof(BufferWriter)}.{nameof(BufferWriter.WriteUTF8StringList)}({paramSuffix});";
#if UNITY_EDITOR
                    case "Vector2": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteVector2)}({paramSuffix});";
                    case "Vector2[]": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteVector2Array)}({paramSuffix});";
                    case "List<Vector2>": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteVector2List)}({paramSuffix});";
                    case "Vector2Int": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteVector2Int)}({paramSuffix});";
                    case "Vector2Int[]": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteVector2IntArray)}({paramSuffix});";
                    case "List<Vector2Int>": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteVector2IntList)}({paramSuffix});";
                    case "Vector3": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteVector3)}({paramSuffix});";
                    case "Vector3[]": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteVector3Array)}({paramSuffix});";
                    case "List<Vector3>": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteVector3List)}({paramSuffix});";
                    case "Vector3Int": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteVector3Int)}({paramSuffix});";
                    case "Vector3Int[]": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteVector3IntArray)}({paramSuffix});";
                    case "List<Vector3Int>": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteVector3IntList)}({paramSuffix});";
                    case "Vector4": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteVector4)}({paramSuffix});";
                    case "Vector4[]": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteVector4Array)}({paramSuffix});";
                    case "List<Vector4>": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteVector4List)}({paramSuffix});";
                    case "Quaternion": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteQuaternion)}({paramSuffix});";
                    case "Quaternion[]": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteQuaternionArray)}({paramSuffix});";
                    case "List<Quaternion>": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteQuaternionList)}({paramSuffix});";
                    case "Color": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteColor)}({paramSuffix});";
                    case "Color[]": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteColorArray)}({paramSuffix});";
                    case "List<Color>": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteColorList)}({paramSuffix});";
                    case "Color32": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteColor32)}({paramSuffix});";
                    case "Color32[]": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteColor32Array)}({paramSuffix});";
                    case "List<Color32>": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteColor32List)}({paramSuffix});";
                    case "Rect": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteRect)}({paramSuffix});";
                    case "Rect[]": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteRectArray)}({paramSuffix});";
                    case "List<Rect>": return $"{nameof(UnityBufferWriter)}.{nameof(UnityBufferWriter.WriteRectList)}({paramSuffix});";
#endif
                    default:
                        if (t_field.EndsWith("[]>")) {
                            throw new Exception($"未处理该类型: {t_field}");
                        }
                        string className = classEditor.GetClassName();
                        if (t_field == className || t_field == $"{className}[]" || t_field == $"List<{className}>") {
                            throw new Exception($"不可循环依赖: {t_field}");
                        }

                        const string n_BufferWriterExtra = nameof(BufferWriterExtra);
                        if (t_field.EndsWith("[]")) {
                            string trueType = t_field.Replace("[]", "");
                            if (IsBufferObject(inputDir, trueType)) {
                                const string n_WriteMessageArr = nameof(BufferWriterExtra.WriteMessageArr);
                                return $"{n_BufferWriterExtra}.{n_WriteMessageArr}({paramSuffix});";
                            } else {
                                throw new Exception($"未处理该类型: {t_field}");
                            }
                        } else if (t_field.StartsWith("List<") && t_field.EndsWith(">")) {
                            string trueType = t_field.Replace("List<", "").Replace(">", "");
                            if (IsBufferObject(inputDir, trueType)) {
                                const string n_WriteMessageList = nameof(BufferWriterExtra.WriteMessageList);
                                return $"{n_BufferWriterExtra}.{n_WriteMessageList}({paramSuffix});";
                            } else {
                                throw new Exception($"未处理该类型: {t_field}");
                            }
                        } else {
                            if (IsBufferObject(inputDir, t_field)) {
                                const string n_WriteMessage = nameof(BufferWriterExtra.WriteMessage);
                                return $"{n_BufferWriterExtra}.{n_WriteMessage}({paramSuffix});";
                            } else {
                                throw new Exception($"未处理该类型: {t_field}");
                            }
                        }

                }
            }

            MethodEditor methodEditor = new MethodEditor();
            methodEditor.Initialize(VisitLevel.Public, false, "void", n_WriteTo);
            methodEditor.AddParameter(t_dst, n_dst);
            methodEditor.AddParameter(t_ref_int, n_offset);
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
            const string t_src = "byte[]";
            const string n_src = "src";
            const string t_ref_int = "ref int";
            const string n_offset = "offset";

            string WriteLine(string t_field, string n_field) {
                const string n_BufferReader = nameof(BufferReader);
                string paramSuffix = $"{n_src}, ref {n_offset}";
                switch (t_field) {
                    case "byte": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadUInt8)}({paramSuffix});";
                    case "byte[]": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadUInt8Arr)}({paramSuffix});";
                    case "List<byte>": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadUInt8List)}({paramSuffix});";
                    case "sbyte": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadInt8)}({paramSuffix});";
                    case "sbyte[]": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadInt8Arr)}({paramSuffix});";
                    case "List<sbyte>": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadInt8List)}({paramSuffix});";
                    case "bool": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadBool)}({paramSuffix});";
                    case "bool[]": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadBoolArr)}({paramSuffix});";
                    case "List<bool>": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadBoolList)}({paramSuffix});";
                    case "short": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadInt16)}({paramSuffix});";
                    case "short[]": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadInt16Arr)}({paramSuffix});";
                    case "List<short>": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadInt16List)}({paramSuffix});";
                    case "ushort": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadUInt16)}({paramSuffix});";
                    case "ushort[]": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadUInt16Arr)}({paramSuffix});";
                    case "List<ushort>": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadUInt16List)}({paramSuffix});";
                    case "int": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadInt32)}({paramSuffix});";
                    case "int[]": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadInt32Arr)}({paramSuffix});";
                    case "List<int>": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadInt32List)}({paramSuffix});";
                    case "uint": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadUInt32)}({paramSuffix});";
                    case "uint[]": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadUInt32Arr)}({paramSuffix});";
                    case "List<uint>": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadUInt32List)}({paramSuffix});";
                    case "long": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadInt64)}({paramSuffix});";
                    case "long[]": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadInt64Arr)}({paramSuffix});";
                    case "List<long>": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadInt64List)}({paramSuffix});";
                    case "ulong": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadUInt64)}({paramSuffix});";
                    case "ulong[]": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadUInt64Arr)}({paramSuffix});";
                    case "List<ulong>": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadUInt64List)}({paramSuffix});";
                    case "float": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadSingle)}({paramSuffix});";
                    case "float[]": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadSingleArr)}({paramSuffix});";
                    case "List<float>": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadSingleList)}({paramSuffix});";
                    case "double": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadDouble)}({paramSuffix});";
                    case "double[]": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadDoubleArr)}({paramSuffix});";
                    case "List<double>": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadDoubleList)}({paramSuffix});";
                    case "char": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadChar)}({paramSuffix});";
                    case "string": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadUTF8String)}({paramSuffix});";
                    case "string[]": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadUTF8StringArr)}({paramSuffix});";
                    case "List<string>": return $"{n_field} = {n_BufferReader}.{nameof(BufferReader.ReadUTF8StringList)}({paramSuffix});";
#if UNITY_EDITOR
                    case "Vector2": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadVector2)}({paramSuffix});";
                    case "Vector2[]": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadVector2Array)}({paramSuffix});";
                    case "List<Vector2>": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadVector2List)}({paramSuffix});";
                    case "Vector2Int": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadVector2Int)}({paramSuffix});";
                    case "Vector2Int[]": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadVector2IntArray)}({paramSuffix});";
                    case "List<Vector2Int>": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadVector2IntList)}({paramSuffix});";
                    case "Vector3": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadVector3)}({paramSuffix});";
                    case "Vector3[]": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadVector3Array)}({paramSuffix});";
                    case "List<Vector3>": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadVector3List)}({paramSuffix});";
                    case "Vector3Int": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadVector3Int)}({paramSuffix});";
                    case "Vector3Int[]": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadVector3IntArray)}({paramSuffix});";
                    case "List<Vector3Int>": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadVector3IntList)}({paramSuffix});";
                    case "Vector4": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadVector4)}({paramSuffix});";
                    case "Vector4[]": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadVector4Array)}({paramSuffix});";
                    case "List<Vector4>": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadVector4List)}({paramSuffix});";
                    case "Quaternion": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadQuaternion)}({paramSuffix});";
                    case "Quaternion[]": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadQuaternionArray)}({paramSuffix});";
                    case "List<Quaternion>": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadQuaternionList)}({paramSuffix});";
                    case "Color": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadColor)}({paramSuffix});";
                    case "Color[]": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadColorArray)}({paramSuffix});";
                    case "List<Color>": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadColorList)}({paramSuffix});";
                    case "Color32": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadColor32)}({paramSuffix});";
                    case "Color32[]": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadColor32Array)}({paramSuffix});";
                    case "List<Color32>": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadColor32List)}({paramSuffix});";
                    case "Rect": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadRect)}({paramSuffix});";
                    case "Rect[]": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadRectArray)}({paramSuffix});";
                    case "List<Rect>": return $"{n_field} = {nameof(UnityBufferReader)}.{nameof(UnityBufferReader.ReadRectList)}({paramSuffix});";
#endif
                    default:
                        string className = classEditor.GetClassName();
                        if (t_field.EndsWith("[]>")) {
                            throw new Exception($"未处理该类型: {t_field}");
                        }
                        if (t_field == className || t_field == $"{className}[]" || t_field == $"List<{className}>") {
                            throw new Exception($"不可循环依赖: {t_field}");
                        }
                        const string n_BufferReaderExtra = nameof(BufferReaderExtra);
                        if (t_field.EndsWith("[]")) {
                            // 处理自定义类型数组
                            const string n_ReadMessageArr = nameof(BufferReaderExtra.ReadMessageArr);
                            string t_trueField = t_field.Replace("[]", "");
                            if (IsBufferObject(inputDir, t_trueField)) {
                                return $"{n_field} = {n_BufferReaderExtra}.{n_ReadMessageArr}({n_src}, () => new {t_trueField}(), ref {n_offset});";
                            } else {
                                throw new Exception($"未处理该类型: {t_field}");
                            }
                        } else if (t_field.StartsWith("List<") && t_field.EndsWith(">")) {
                            // 处理自定义类型列表
                            const string n_ReadMessageList = nameof(BufferReaderExtra.ReadMessageList);
                            string t_trueField = t_field.Replace("List<", "").Replace(">", "");
                            if (IsBufferObject(inputDir, t_trueField)) {
                                return $"{n_field} = {n_BufferReaderExtra}.{n_ReadMessageList}({n_src}, () => new {t_trueField}(), ref {n_offset});";
                            } else {
                                throw new Exception($"未处理该类型: {t_field}");
                            }
                        } else {
                            // 处理单自定义类型
                            const string n_ReadMessage = nameof(BufferReaderExtra.ReadMessage);
                            if (IsBufferObject(inputDir, t_field)) {
                                return $"{n_field} = {n_BufferReaderExtra}.{n_ReadMessage}({n_src}, () => new {t_field}(), ref {n_offset});";
                            } else {
                                throw new Exception($"未处理该类型: {t_field}");
                            }
                        }
                }
            }

            MethodEditor methodEditor = new MethodEditor();
            methodEditor.Initialize(VisitLevel.Public, false, "void", n_FromBytes);
            methodEditor.AddParameter(t_src, n_src);
            methodEditor.AddParameter(t_ref_int, n_offset);
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