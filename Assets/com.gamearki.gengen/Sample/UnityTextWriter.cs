using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace GameArki {

    public class UnityTextWriter : TextWriter {

        public override Encoding Encoding => Encoding.UTF8;

        StringBuilder sb;

        public UnityTextWriter() {
            this.sb = new StringBuilder();
        }

        public override void Write(string value) { sb.Append(value); }
        public override void Write(ulong value) { sb.Append(value); }
        public override void Write(long value) { sb.Append(value); }
        public override void Write(uint value) { sb.Append(value); }
        public override void Write(int value) { sb.Append(value); }
        public override void Write(char value) { sb.Append(value); }
        public override void Write(float value) { sb.Append(value); }
        public override void Write(double value) { sb.Append(value); }
        public override void Write(decimal value) { sb.Append(value); }
        public override void Write(bool value) { sb.Append(value); }
        public override void Write(object value) { sb.Append(value); }

        public override void Flush() {
            Debug.Log(sb.ToString());
            sb.Clear();
        }

        public override void WriteLine(string value) { Debug.Log(value); }
        public override void WriteLine(ulong value) { Debug.Log(value); }
        public override void WriteLine(long value) { Debug.Log(value); }
        public override void WriteLine(uint value) { Debug.Log(value); }
        public override void WriteLine(int value) { Debug.Log(value); }
        public override void WriteLine(char value) { Debug.Log(value); }
        public override void WriteLine(float value) { Debug.Log(value); }
        public override void WriteLine(double value) { Debug.Log(value); }
        public override void WriteLine(decimal value) { Debug.Log(value); }
        public override void WriteLine(bool value) { Debug.Log(value); }
        public override void WriteLine(object value) { Debug.Log(value); }

    }

}