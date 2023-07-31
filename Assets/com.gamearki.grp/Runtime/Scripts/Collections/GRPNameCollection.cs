using UnityEngine;
using UnityEngine.Rendering;

namespace GRP {

    public static class GRPNameCollection {

        public const string CMD_CAMERA = "GRP Camera";
        public const string CMD_SKYBOX = "GRP Skybox";
        public const string CMD_LIGHT = "GRP Lights";
        public const string CMD_SHADOW = "GRP Shadows";

        public static readonly string LIGHT_DIR_COUNT = "_GRP_LIGHT_DIR_COUNT";
        public static readonly string LIGHT_DIR_DIRS = "_GRP_LIGHT_DIR_DIRS";
        public static readonly string LIGHT_DIR_COLORS = "_GRP_LIGHT_DIR_COLORS";

        public static readonly string LIGHT_POINT_COUNT = "_GRP_LIGHT_POINT_COUNT";
        public static readonly string LIGHT_POINT_POSES = "_GRP_LIGHT_POINT_POSES";
        public static readonly string LIGHT_POINT_COLORS = "_GRP_LIGHT_POINT_COLORS";

        public static readonly string LIGHT_SPOT_COUNT = "_GRP_LIGHT_SPOT_COUNT";
        public static readonly string LIGHT_SPOT_POSES = "_GRP_LIGHT_SPOT_POSES";
        public static readonly string LIGHT_SPOT_COLORS = "_GRP_LIGHT_SPOT_COLORS";

        public static readonly string CAMERA_POS = "_GRP_CAMERA_POS";
        public static readonly string CAMERA_DIR = "_GRP_CAMERA_DIR";

        public static readonly int ID_SHADOW_DIR_ATLAS = Shader.PropertyToID("_GRP_SHADOW_DIR_ATLAS");
        public static readonly int ID_SHADOW_DIR_MATRICES = Shader.PropertyToID("_GRP_SHADOW_DIR_MATRICES");

    }
}