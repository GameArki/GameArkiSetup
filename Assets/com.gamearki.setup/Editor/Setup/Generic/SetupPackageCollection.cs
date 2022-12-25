namespace GameArki.Setup {

    public static class SetupPackageCollection {

        public readonly static SetupPackageModel[] packages = new SetupPackageModel[] {

            new SetupPackageModel {
                title = "BufferIO",
                name = "com.gamearki.bufferio",
                desc = "二进制序列化库",
                versions = new string[] {"1.0.0", "1.1.0", "main"},
                gitUrl = "ssh://git@github.com/gamearki/bufferio.git",
                docuUrl = "https://www.github.com/gamearki/bufferio",
                path = "?path=Assets/com.gamearki.bufferio",
                dependencies = new string[0],
                assemblies = new string[] {"GameArki.BufferIO"}
            },

            new SetupPackageModel {
                title = "BufferIOExtra",
                name = "com.gamearki.bufferioextra",
                desc = "二进制序列化扩展",
                versions = new string[] {"1.0.0", "1.1.0", "main"},
                gitUrl = "ssh://git@github.com/gamearki/bufferioextra.git",
                docuUrl = "https://www.github.com/gamearki/bufferioextra",
                path = "?path=Assets/com.gamearki.bufferioextra",
                dependencies = new string[] {"BufferIO"},
                assemblies = new string[] {"GameArki.BufferIOExtra"}
            },

        };

    }
}
