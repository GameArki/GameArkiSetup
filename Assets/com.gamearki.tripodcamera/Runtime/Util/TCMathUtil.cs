using GameArki.TripodCamera.Entities;
using GameArki.TripodCamera.Facades;
using UnityEngine;

namespace GameArki.TripodCamera {

    public static class TCMathUtil {

        public static Quaternion LookAtComposer_Horizontal(in Vector3 lookAtTargetPos,
                                                           in Vector3 eyePos,
                                                           in Quaternion eyeRot,
                                                           in TCLookAtComposerModel composer,
                                                           in Matrix4x4 projectionMatrix,
                                                           int screenWidth,
                                                           int screenHeight) {
            Quaternion returnRot = eyeRot;

            Vector3 screenPoint = WorldToScreenPoint(lookAtTargetPos, eyePos, eyeRot, projectionMatrix, screenWidth, screenHeight);
            if (composer.IsInDeadZone_Horizontal(screenPoint, screenWidth, screenHeight, out float pixelOffsetX)) {
                return returnRot;
            }

            Quaternion rot = Quaternion.identity;
            if (pixelOffsetX > 0) {
                Vector2 deadZoneRB = composer.GetDeadZoneRB(screenWidth, screenHeight);
                Vector3 fromPoint = new Vector3(deadZoneRB.x, screenPoint.y, 1);
                rot = FromToScreenPoint(fromPoint, screenPoint, projectionMatrix, screenWidth, screenHeight);
            } else {
                Vector2 deadZoneLT = composer.GetDeadZoneLT(screenWidth, screenHeight);
                rot = FromToScreenPoint(new Vector3(deadZoneLT.x, screenPoint.y, 1), screenPoint, projectionMatrix, screenWidth, screenHeight);
            }

            var eulerAngles = rot.eulerAngles;
            if (screenPoint.z <= 0) rot = Quaternion.Euler(eulerAngles.x, eulerAngles.y + 180, eulerAngles.z);
            returnRot = rot * returnRot;

            return returnRot;
        }

        public static Quaternion LookAtComposer_Vertical(in Vector3 lookAtTargetPos,
                                                         in Vector3 followTargetPos,
                                                         in Vector3 eyePos,
                                                         in Quaternion eyeRot,
                                                         in TCLookAtComposerModel composer,
                                                         in Matrix4x4 projectionMatrix,
                                                         int screenWidth,
                                                         int screenHeight) {
            Quaternion returnRot = eyeRot;

            Vector3 screenPoint_lt = WorldToScreenPoint(lookAtTargetPos, eyePos, eyeRot, projectionMatrix, screenWidth, screenHeight);
            Vector3 screenPoint_ft = WorldToScreenPoint(followTargetPos, eyePos, eyeRot, projectionMatrix, screenWidth, screenHeight);

            // Compare camera distance between follow target and lookAt target.
            if (screenPoint_lt.z < screenPoint_ft.z) {
                return returnRot;
            }

            if (composer.IsInDeadZone_Vertical(screenPoint_lt, screenWidth, screenHeight, out float pixelOffsetY)) {
                return returnRot;
            }

            if (pixelOffsetY > 0) {
                Vector2 deadZoneLT = composer.GetDeadZoneLT(screenWidth, screenHeight);
                Quaternion rot = FromToScreenPoint(new Vector3(screenPoint_lt.x, deadZoneLT.y, 1), screenPoint_lt, projectionMatrix, screenWidth, screenHeight);
                var eulerAngles = rot.eulerAngles;
                returnRot = returnRot * rot;
            } else {
                Vector2 deadZoneRB = composer.GetDeadZoneRB(screenWidth, screenHeight);
                Quaternion rot = FromToScreenPoint(new Vector3(screenPoint_lt.x, deadZoneRB.y, 1), screenPoint_lt, projectionMatrix, screenWidth, screenHeight);
                var eulerAngles = rot.eulerAngles;
                returnRot = returnRot * rot;
            }

            return returnRot;
        }

        public static Quaternion LookAtComposer_Horizontal(in Vector3 eyePos,
                                                           in Quaternion eyeRot,
                                                           in TCLookAtComposerModel composer,
                                                           in Matrix4x4 projectionMatrix,
                                                           int screenWidth,
                                                           int screenHeight,
                                                           in Vector3 screenPoint) {
            Quaternion returnRot = eyeRot;

            if (composer.IsInDeadZone_Horizontal(screenPoint, screenWidth, screenHeight, out float pixelOffsetX)) {
                return returnRot;
            }

            Quaternion rot = Quaternion.identity;
            if (pixelOffsetX > 0) {
                Vector2 deadZoneRB = composer.GetDeadZoneRB(screenWidth, screenHeight);
                Vector3 fromPoint = new Vector3(deadZoneRB.x, screenPoint.y, 1);
                rot = FromToScreenPoint(fromPoint, screenPoint, projectionMatrix, screenWidth, screenHeight);
            } else {
                Vector2 deadZoneLT = composer.GetDeadZoneLT(screenWidth, screenHeight);
                rot = FromToScreenPoint(new Vector3(deadZoneLT.x, screenPoint.y, 1), screenPoint, projectionMatrix, screenWidth, screenHeight);
            }

            var eulerAngles = rot.eulerAngles;
            if (screenPoint.z <= 0) rot = Quaternion.Euler(eulerAngles.x, eulerAngles.y + 180, eulerAngles.z);
            returnRot = rot * returnRot;

            return returnRot;
        }

        public static Quaternion LookAtComposer_Vertical(in Vector3 eyePos,
                                                         in Quaternion eyeRot,
                                                         in TCLookAtComposerModel composer,
                                                         in Matrix4x4 projectionMatrix,
                                                         int screenWidth,
                                                         int screenHeight,
                                                         in Vector3 screenPoint_lt,
                                                         in Vector3 screenPoint_ft) {
            Quaternion returnRot = eyeRot;

            // Compare camera distance between follow target and lookAt target.
            if (screenPoint_lt.z < screenPoint_ft.z) {
                return returnRot;
            }

            if (composer.IsInDeadZone_Vertical(screenPoint_lt, screenWidth, screenHeight, out float pixelOffsetY)) {
                return returnRot;
            }

            if (pixelOffsetY > 0) {
                Vector2 deadZoneLT = composer.GetDeadZoneLT(screenWidth, screenHeight);
                Quaternion rot = FromToScreenPoint(new Vector3(screenPoint_lt.x, deadZoneLT.y, 1), screenPoint_lt, projectionMatrix, screenWidth, screenHeight);
                var eulerAngles = rot.eulerAngles;
                returnRot = returnRot * rot;
            } else {
                Vector2 deadZoneRB = composer.GetDeadZoneRB(screenWidth, screenHeight);
                Quaternion rot = FromToScreenPoint(new Vector3(screenPoint_lt.x, deadZoneRB.y, 1), screenPoint_lt, projectionMatrix, screenWidth, screenHeight);
                var eulerAngles = rot.eulerAngles;
                returnRot = returnRot * rot;
            }

            return returnRot;
        }

        public static Vector3 WorldToScreenPoint(in Vector3 worldPosition,
                                          in Vector3 eyePosition,
                                          in Quaternion eyeRotation,
                                          in Matrix4x4 projectionMatrix,
                                          int screenWidth,
                                          int screenHeight) {

            Vector3 viewportPoint = WorldToViewportPoint(worldPosition, eyePosition, eyeRotation, projectionMatrix);
            //- 将 视口坐标 ===> 屏幕坐标 
            Vector3 screenPoint = new Vector3(viewportPoint.x * screenWidth, viewportPoint.y * screenHeight, viewportPoint.z);

            // Debug.Log($"WorldToScreenPoint\nviewportPoint = {viewportPoint}\nscreenPoint = {screenPoint}");
            return screenPoint;
        }

        public static Vector3 WorldToViewportPoint(in Vector3 worldPosition,
                                                   in Vector3 cameraPosition,
                                                   in Quaternion cameraRotation,
                                                   in Matrix4x4 projectionMatrix) {
            //- 世界坐标 ===> 相机空间坐标
            Vector3 cameraSpacePoint = Quaternion.Inverse(cameraRotation) * (worldPosition - cameraPosition);
            //- 相机空间坐标 ===> 裁剪空间坐标
            Vector4 clipSpacePoint = projectionMatrix * cameraSpacePoint;
            //- 裁剪空间坐标 ===> 规范化设备坐标 (透视除法)
            Vector3 ndcPoint = clipSpacePoint / clipSpacePoint.w;
            //- 规范化设备坐标 ===> 归一化坐标(此处也为视口坐标)
            Vector3 viewportPoint = new Vector3((-ndcPoint.x + 1.0f) / 2, (-ndcPoint.y + 1.0f) / 2, cameraSpacePoint.z);

            // Debug.Log($"WorldToViewportPoint\ncameraSpacePoint = {cameraSpacePoint}\nclipSpacePoint = {clipSpacePoint}\nndcPoint = {ndcPoint}\nviewportPoint = {viewportPoint}");
            return viewportPoint;
        }

        public static Vector3 ScreenToViewportPoint(Vector3 screenPoint, int screenWidth, int screenHeight) {
            Vector3 viewportPoint = new Vector3(screenPoint.x / screenWidth, screenPoint.y / screenHeight, screenPoint.z);
            return viewportPoint;
        }

        public static Vector3 ScreenPointToDir(in Vector3 screenPoint,
                                               in Vector3 eyePosition,
                                               in Quaternion eyeRotation,
                                               in Matrix4x4 projectionMatrix,
                                               int screenWidth,
                                               int screenHeight) {

            //- 将 屏幕坐标 ===> 视口坐标
            Vector3 viewportPoint = new Vector3(screenPoint.x / screenWidth, screenPoint.y / screenHeight, screenPoint.z);
            //- 将 视口坐标 ===> 规范化设备坐标
            Vector2 ndcPoint = new Vector2(viewportPoint.x * 2f - 1f, viewportPoint.y * 2f - 1f);
            //- 将 规范化设备坐标 ===> 裁剪空间坐标(Clip Space Coordinates)
            Vector4 clipSpacePoint = new Vector4(ndcPoint.x, ndcPoint.y, -1.0f, 1.0f);
            //- 将 裁剪空间坐标 ===> 相机空间坐标(Camera Space Coordinates).
            Vector4 cameraSpacePoint = projectionMatrix.inverse * clipSpacePoint;
            //- 将 相机空间坐标 ===> 世界空间坐标(World Space Coordinates)
            Vector3 worldSpacePoint = Quaternion.Inverse(eyeRotation) * (cameraSpacePoint);

            Vector3 direction = worldSpacePoint.normalized;
            direction.z = -direction.z;
            if (screenPoint.z < 0) {
                //- 前方
                direction.x = -direction.x;
                direction.y = -direction.y;
            }

            // Debug.Log($"ScreenPointToDir\nviewportPoint = {viewportPoint}\nndcPoint = {ndcPoint}\nclipSpacePoint = {clipSpacePoint}\ncameraSpacePoint = {cameraSpacePoint}\nworldSpacePoint = {worldSpacePoint}\ndirection = {direction}");
            return direction;
        }

        /**************************** 投影矩阵 ************************************
            [ 2n/(r-l)     0       (r+l)/(r-l)       0      ]
            [   0        2n/(t-b)  (t+b)/(t-b)       0      ]
            [   0           0      -(f+n)/(f-n)  -2fn/(f-n) ]
            [   0           0         -1              0      ]
        ***************************************************************************/
        public static Matrix4x4 GetProjectionMatrix(in Vector3 eyePosition,
                                                    float fieldOfView,
                                                    float aspectRatio,
                                                    float nearClipPlane,
                                                    float farClipPlane) {
            Matrix4x4 projectionMatrix;

            // 将视野角度转换为弧度
            float fovRad = fieldOfView * Mathf.Deg2Rad;

            // 计算视野的一半
            float halfFovTan = Mathf.Tan(fovRad * 0.5f);

            // 计算视锥体的高度和宽度
            float height = nearClipPlane * halfFovTan * 2f;
            float width = height * aspectRatio;

            // 构建投影矩阵
            var r = new Vector3(0.5f * width, 0, 0) + eyePosition;
            var l = new Vector3(-0.5f * width, 0, 0) + eyePosition;
            var t = new Vector3(0, 0.5f * height, 0) + eyePosition;
            var b = new Vector3(0, -0.5f * height, 0) + eyePosition;

            var n_2 = nearClipPlane * 2f;
            var r_l = r.x - l.x;
            var t_b = t.y - b.y;
            var f_n = farClipPlane - nearClipPlane;

            projectionMatrix = new Matrix4x4();
            projectionMatrix.m00 = n_2 / r_l;               // X轴缩放因子
            projectionMatrix.m11 = n_2 / t_b;               // Y轴缩放因子
            projectionMatrix.m02 = 0f;                      // X轴平移因子(Untiy默认为0)
            projectionMatrix.m12 = 0f;                      // Y轴平移因子(Untiy默认为0)
            projectionMatrix.m22 = -(farClipPlane + nearClipPlane) / f_n;    // Z轴深度缩放因子
            projectionMatrix.m23 = -2f * farClipPlane * nearClipPlane / f_n; // Z轴深度平移因子
            projectionMatrix.m32 = -1f;                                     // Z轴深度反转因子
            return projectionMatrix;
        }

        public static Quaternion FromToScreenPoint(in Vector3 fromScreenPoint,
                                                   in Vector3 toScreenPoint,
                                                   in Matrix4x4 projectionMatrix,
                                                   int screenWidth,
                                                   int screenHeight) {
            if ((fromScreenPoint - toScreenPoint).sqrMagnitude < float.Epsilon) return Quaternion.identity;

            var fromViewPortPos = ScreenToViewportPoint(fromScreenPoint, screenWidth, screenHeight);
            var toViewportPos = ScreenToViewportPoint(toScreenPoint, screenWidth, screenHeight);

            Quaternion rotation = Quaternion.identity;
            if (Mathf.Abs(fromScreenPoint.x - toScreenPoint.x) > float.Epsilon) {
                var viewportOffsetX = toViewportPos.x - fromViewPortPos.x;
                var rotationAngleY = viewportOffsetX != 0f ? Mathf.Rad2Deg * Mathf.Atan2(viewportOffsetX, projectionMatrix.m00) * 2f : 0f;
                rotation = Quaternion.Euler(0f, rotationAngleY, 0f) * rotation;
            }

            if (Mathf.Abs(fromScreenPoint.y - toScreenPoint.y) > float.Epsilon) {
                var viewportOffsetY = toViewportPos.y - fromViewPortPos.y;
                var rotationAngleX = viewportOffsetY != 0f ? Mathf.Rad2Deg * Mathf.Atan2(viewportOffsetY, projectionMatrix.m00 / projectionMatrix.m11) / -2f : 0f;
                rotation = rotation * Quaternion.Euler(rotationAngleX, 0f, 0f);
            }

            return rotation;
        }

    }

}