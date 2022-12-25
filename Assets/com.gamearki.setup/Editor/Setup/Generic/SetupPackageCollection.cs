namespace GameArki.Setup {

    public static class SetupPackageCollection {

        public readonly static SetupPackageModel[] packages = new SetupPackageModel[] {

            new SetupPackageModel {
                title = "MenuTool",
                name = "com.gamearki.menutool",
                desc = "菜单工具, 包含: 1.重新生成.csproj; 2.导出切割的 Sprites",
                versions = new string[] {"0.1.1", "main"},
                gitUrl = "ssh://git@github.com/gamearki/arkimenutool.git",
                docuUrl = "https://www.github.com/gamearki/arkimenutool",
                path = "?path=Assets/com.gamearki.menutool",
                dependencies = new string[0],
                assemblies = new string[] {"GameArki.MenuTool"}
            },

            new SetupPackageModel {
                title = "FPMath",
                name = "com.gamearki.fpmath",
                desc = "定点数数学库",
                versions = new string[] {"1.7.0", "1.8.0", "main"},
                gitUrl = "ssh://git@github.com/gamearki/fpmath.git",
                docuUrl = "https://www.github.com/gamearki/fpmath",
                path = "?path=Assets/com.gamearki.fpmath",
                dependencies = new string[0],
                assemblies = new string[] {"GameArki.FPMath"}
            },

            new SetupPackageModel {
                title = "ArkiDataStructure",
                name = "com.gamearki.datastructure",
                desc = "(浮点数版)游戏数据结构",
                versions = new string[] {"0.2.0", "main"},
                gitUrl = "ssh://git@github.com/gamearki/ArkiDataStructure.git",
                docuUrl = "https://www.github.com/gamearki/ArkiDataStructure",
                path = "?path=Assets/com.gamearki.datastructure",
                dependencies = new string[] { },
                assemblies = new string[] {"GameArki.DataStructure"}
            },

            new SetupPackageModel {
                title = "ArkiDataStructure",
                name = "com.gamearki.datastructure.fp",
                desc = "(定点数版)游戏数据结构",
                versions = new string[] {"0.2.0", "main"},
                gitUrl = "ssh://git@github.com/gamearki/ArkiDataStructure.git",
                docuUrl = "https://www.github.com/gamearki/ArkiDataStructure",
                path = "?path=Assets/com.gamearki.datastructure.fp",
                dependencies = new string[] { "FPMath" },
                assemblies = new string[] {"GameArki.DataStructure.FP"}
            },

            new SetupPackageModel {
                title = "FPPhysics2D",
                name = "com.gamearki.fpphysics2d",
                desc = "定点数 2D 物理引擎",
                versions = new string[] {"0.2.0", "0.3.0", "main"},
                gitUrl = "ssh://git@github.com/gamearki/FPPhysics2D.git",
                docuUrl = "https://www.github.com/gamearki/FPPhysics2D",
                path = "?path=Assets/com.gamearki.fpphysics2d",
                dependencies = new string[] { "FPMath", "ArkiDataStructure" },
                assemblies = new string[] {"GameArki.FPPhysics2D"}
            },

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
