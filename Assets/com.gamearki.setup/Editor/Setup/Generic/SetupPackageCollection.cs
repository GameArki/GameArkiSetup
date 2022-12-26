namespace GameArki.Setup
{

    public static class SetupPackageCollection
    {

        public readonly static SetupPackageModel[] packages = new SetupPackageModel[] {

            // MenuTool
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

            // FPMath
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

            // DataStructure
            new SetupPackageModel {
                title = "ArkiDataStructure",
                name = "com.gamearki.datastructure",
                desc = "(浮点数版)游戏数据结构库",
                versions = new string[] {"0.2.0", "main"},
                gitUrl = "ssh://git@github.com/gamearki/ArkiDataStructure.git",
                docuUrl = "https://www.github.com/gamearki/ArkiDataStructure",
                path = "?path=Assets/com.gamearki.datastructure",
                dependencies = new string[] { },
                assemblies = new string[] {"GameArki.DataStructure"}
            },

            // DataStructure.FP
            new SetupPackageModel {
                title = "ArkiDataStructure",
                name = "com.gamearki.datastructure.fp",
                desc = "(定点数版)游戏数据结构库",
                versions = new string[] {"0.2.0", "main"},
                gitUrl = "ssh://git@github.com/gamearki/ArkiDataStructure.git",
                docuUrl = "https://www.github.com/gamearki/ArkiDataStructure",
                path = "?path=Assets/com.gamearki.datastructure.fp",
                dependencies = new string[] { "FPMath" },
                assemblies = new string[] {"GameArki.DataStructure.FP"}
            },

            // FPPhysics2D
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

            // FPEasing
            new SetupPackageModel {
                title = "FPEasing",
                name = "com.gamearki.fpeasing",
                desc = "(浮点数版)缓动函数库",
                versions = new string[] {"1.0.0", "2.3.1", "main"},
                gitUrl = "ssh://git@github.com/gamearki/FPEasing.git",
                docuUrl = "https://www.github.com/gamearki/FPEasing",
                path = "?path=Assets/com.gamearki.fpeasing",
                dependencies = new string[] { },
                assemblies = new string[] {"GameArki.FPEasing"}
            },

            // FPEasing.FP
            new SetupPackageModel {
                title = "FPEasing",
                name = "com.gamearki.fpeasing.fp",
                desc = "(定点数版)缓动函数库",
                versions = new string[] {"1.0.0", "2.3.1", "main"},
                gitUrl = "ssh://git@github.com/gamearki/FPEasing.git",
                docuUrl = "https://www.github.com/gamearki/FPEasing",
                path = "?path=Assets/com.gamearki.fpeasing.fp",
                dependencies = new string[] { "FPMath" },
                assemblies = new string[] {"GameArki.FPEasing.FP"}
            },

            // PureBTTree
            new SetupPackageModel {
                title = "PureBTTree",
                name = "com.gamearki.purebttree",
                desc = "行为树库(指令式)",
                versions = new string[] {"0.2.0", "main"},
                gitUrl = "ssh://git@github.com/gamearki/PureBTTree.git",
                docuUrl = "https://www.github.com/gamearki/PureBTTree",
                path = "?path=Assets/com.gamearki.purebttree",
                dependencies = new string[0],
                assemblies = new string[] {"GameArki.PureBTTree"}
            },

            // BufferIO
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

            // CrossIO
            new SetupPackageModel {
                title = "CrossIO",
                name = "com.gamearki.crossio",
                desc = "Unity 文件存取库(同样适用于手机端)",
                versions = new string[] {"1.2.0", "1.3.0", "main"},
                gitUrl = "ssh://git@github.com/gamearki/crossio.git",
                docuUrl = "https://www.github.com/gamearki/crossio",
                path = "?path=Assets/com.gamearki.crossio",
                dependencies = new string[] { "BufferIO" },
                assemblies = new string[] {"GameArki.CrossIO"}
            },

            // CSharpGen
            new SetupPackageModel {
                title = "ArkiCSharpGen",
                name = "com.gamearki.csharpgen",
                desc = "基于 Roslyn 的元编程工具库",
                versions = new string[] { "1.1.0", "main" },
                gitUrl = "ssh://git@github.com/gamearki/ArkiCSharpGen.git",
                docuUrl = "https://www.github.com/gamearki/ArkiCSharpGen",
                path = "?path=Assets/com.gamearki.csharpgen",
                dependencies = new string[] {},
                assemblies = new string[] {"GameArki.ArkiCSharpGen"}
            },

            // TripodCamera
            new SetupPackageModel {
                title = "TripodCamera",
                name = "com.gamearki.tripodcamera",
                desc = "三脚架 - 3D 相机库(指令式)",
                versions = new string[] { "0.5.0", "0.6.0", "main" },
                gitUrl = "ssh://git@github.com/gamearki/TripodCamera.git",
                docuUrl = "https://www.github.com/gamearki/TripodCamera",
                path = "?path=Assets/com.gamearki.tripodcamera",
                dependencies = new string[] { "FPEasing" },
                assemblies = new string[] {"GameArki.TripodCamera"}
            },

            // PlatformerCamera
            new SetupPackageModel {
                title = "PlatformerCamera",
                name = "com.gamearki.platformercamera",
                desc = "2D 相机库(指令式)",
                versions = new string[] { "0.2.1", "main" },
                gitUrl = "ssh://git@github.com/gamearki/platformercamera.git",
                docuUrl = "https://www.github.com/gamearki/platformercamera",
                path = "?path=Assets/com.gamearki.platformercamera",
                dependencies = new string[] { "FPEasing" },
                assemblies = new string[] {"GameArki.PlatformerCamera"}
            },

            // BufferIOExtra
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

            // NetworkWeaver.TCP
            new SetupPackageModel {
                title = "NetworkWeaver",
                name = "com.gamearki.networkweaver.tcp",
                desc = "(TCP)网络通信库",
                versions = new string[] { "1.1.0", "main" },
                gitUrl = "ssh://git@github.com/gamearki/networkweaver.git",
                docuUrl = "https://www.github.com/gamearki/networkweaver",
                path = "?path=Assets/com.gamearki.networkweaver.tcp",
                dependencies = new string[] { "BufferIO", "BufferIOExtra", "ArkiCSharpGen" },
                assemblies = new string[] {"GameArki.NetworkWeaver.Tcp" }
            },

            // NetworkWeaver.UDP
            new SetupPackageModel {
                title = "NetworkWeaver",
                name = "com.gamearki.networkweaver.udp",
                desc = "(UDP)网络通信库",
                versions = new string[] { "1.1.0", "main" },
                gitUrl = "ssh://git@github.com/gamearki/networkweaver.git",
                docuUrl = "https://www.github.com/gamearki/networkweaver",
                path = "?path=Assets/com.gamearki.networkweaver.udp",
                dependencies = new string[] { "BufferIO", "BufferIOExtra", "ArkiCSharpGen" },
                assemblies = new string[] {"GameArki.NetworkWeaver.UDP" }
            },

            // FreeInput
            new SetupPackageModel {
                title = "FreeInput",
                name = "com.gamearki.freeinput",
                desc = "控制器库(键鼠/手柄/触屏)",
                versions = new string[] { "0.0.1", "main" },
                gitUrl = "ssh://git@github.com/gamearki/FreeInput.git",
                docuUrl = "https://www.github.com/gamearki/FreeInput",
                path = "?path=Assets/com.gamearki.freeinput",
                dependencies = new string[] {  },
                assemblies = new string[] {"GameArki.FreeInput" }
            },

        };

    }
}
