using UnityEngine;

namespace GameArki.BufferIO.Editor {

    public class BufferIOExtraGeneratorSampleTool {
#if GAMEARKI_DEV
        [UnityEditor.MenuItem("GameArki/Sample/GenBufferIO")]
#endif
        public static void GenBufferIO() {

            BufferExtraGenerator.GenModel(Application.dataPath + "/com.gamearki.bufferio/Tests/TestModel");
            BufferExtraGenerator.GenModel(Application.dataPath + "/com.gamearki.bufgenerator/Tests/TestModel/BufferIOModel");

        }

#if GAMEARKI_DEV
        [UnityEditor.MenuItem("GameArki/Sample/GenNoBuf")]
#endif
        public static void GenNoBuf() {

            NoBuf.NoBufGenerator.GenModel(Application.dataPath + "/com.gamearki.nobuf/Tests/TestModel");
            NoBuf.NoBufGenerator.GenModel(Application.dataPath + "/com.gamearki.bufgenerator/Tests/TestModel/NoBufModel");

        }

    }

}