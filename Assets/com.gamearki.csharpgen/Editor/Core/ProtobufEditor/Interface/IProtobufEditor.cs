using System;

namespace GameArki.CSharpGen.Protobuf {

    public interface IProtobufEditor {
        string Generate();
        void SetNamespace(string nameSpace);
        void SetName(string name);
        void AddField(ProtobufBaseFieldType fieldType, string fieldName, bool isRepeat = false);
        void AddField(string fieldType, string fieldName, bool isRepeat = false);
    }

}