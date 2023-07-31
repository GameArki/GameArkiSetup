#ifndef GRP_MACRO_COMMON_H
#define GRP_MACRO_COMMON_H

#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/CommonMaterial.hlsl"
#include "UnityInput.hlsl"

// Const Names
// - Camera
#define _GRP_CAMERA_POS _GRP_CAMERA_POS
#define _GRP_CAMERA_DIR _GRP_CAMERA_DIR

// - Light (Directional)
#define _GRP_LIGHT_DIR_COUNT _GRP_LIGHT_DIR_COUNT
#define _GRP_LIGHT_DIR_COLORS _GRP_LIGHT_DIR_COLORS
#define _GRP_LIGHT_DIR_DIRS _GRP_LIGHT_DIR_DIRS

// - Shadow
#define _GRP_SHADOW_DIR_ATLAS _GRP_SHADOW_DIR_ATLAS
#define _GRP_SHADOW_DIR_ATLAS_SIZE _GRP_SHADOW_DIR_ATLAS_SIZE
#define _GRP_SHADOW_DIR_MATRICES _GRP_SHADOW_DIR_MATRICES

// - Unity Built-in
#define UNITY_MATRIX_M unity_ObjectToWorld
#define UNITY_MATRIX_I_M unity_WorldToObject
#define UNITY_MATRIX_V unity_MatrixV
#define UNITY_MATRIX_VP unity_MatrixVP
#define UNITY_MATRIX_P glstate_matrix_projection
#define UNITY_PREV_MATRIX_M unity_ObjectToWorld
#define UNITY_PREV_MATRIX_I_M unity_WorldToObject

#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/SpaceTransforms.hlsl"

#endif