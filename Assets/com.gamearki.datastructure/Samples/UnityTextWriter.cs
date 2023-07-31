using System.Text;
using System.IO;
using UnityEngine;

namespace GameArki.FPDataStructure.Sample {

    public class UnityTextWriter : TextWriter {

        public override void WriteLine(string value) {
            Debug.Log(value);
        }

        public override Encoding Encoding {
            get { return Encoding.UTF8; }
        }

    }

}