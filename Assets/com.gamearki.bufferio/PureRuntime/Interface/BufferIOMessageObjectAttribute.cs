using System;

namespace GameArki.BufferIO {

    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class BufferIOMessageObjectAttribute : Attribute {

        public BufferIOMessageObjectAttribute() {

        }

    }

}