using System;
using UnityEngine;
using GameArki.FPEasing;
using GameArki.TripodCamera.Facades;
using GameArki.TripodCamera.Entities;

namespace GameArki.TripodCamera.Domain {

    public class TCCameraDomain {

        TCContext context;

        public TCCameraDomain() { }

        public void Inject(TCContext context) {
            this.context = context;
        }

        public int Spawn(Vector3 pos, Quaternion rot, float fov) {
            var repo = context.CameraRepo;
            var mainCam = context.MainCamera;
            var tcCam = new TCCameraEntity();
            var camID = context.FetchCameraID();
            tcCam.SetID(camID);
            tcCam.InitInfo(pos, rot, fov, mainCam.aspect, mainCam.nearClipPlane, mainCam.farClipPlane, Screen.width, Screen.height);
            if (!repo.TryAdd(tcCam)) {
                Debug.LogError($"[TCCameraDomain] Spawn: Failed to add camera to repo. ID: {camID}");
                return -1;
            }

            Debug.Log($"[TCCameraDomain] Spawn: {camID}");
            return camID;
        }

        public bool SetTCCameraActive(bool active, int id) {
            var repo = context.CameraRepo;
            if (!repo.TryGet(id, out var cam)) {
                Debug.LogError($"[TCCameraDomain] SetActiveTCCamera: Failed to get camera from repo. ID: {id}");
                return false;
            }

            cam.SetActivated(active);
            return true;
        }

        public void SetTCCameraPosition(in Vector3 pos, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;
            tcCam.SetPosition(pos);
        }

        public void SetTCCameraRotation(in Quaternion rot, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;
            tcCam.SetRotation(rot);
        }

        public void Remove(int id) {
            var repo = context.CameraRepo;
            repo.Remove(id);
        }

        // ==== Basic ====
        public void Push_In(float value, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;


            var infoCom = tcCam.BeforeInfo;
            var mainCam = context.MainCamera;
            var forward = mainCam.transform.forward;
            infoCom.SetPosition(infoCom.Position + forward * value);
        }

        public void Move(Vector2 value, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;

            tcCam.Move(value);
        }

        public void Move_AndChangeLookAtOffset(in Vector2 value, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;

            tcCam.Move_AndChangeLookAtOffset(value);
        }

        // ==== Basic ====
        public void Rotate_Roll(float degree, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;
            var curInfoCom = tcCam.BeforeInfo;
            var euler = curInfoCom.Rotation.eulerAngles;
            euler.z += degree;
            curInfoCom.SetRotation(Quaternion.Euler(euler));
        }

        public void Zoom_In(float addition, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;
            var infoCom = tcCam.BeforeInfo;
            infoCom.AddFOV(addition);
        }

        // ==== Advance ====
        //- Follow
        public void Follow_SetInit(Transform target,
                                   in Vector3 offset,
                                   EasingType easingType_horizontal,
                                   float easingTime_horizontal,
                                   EasingType easingType_vertical,
                                   float easingTime_vertical,
                                   int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;
            tcCam.Follow_SetInit(target, offset, easingType_horizontal, easingTime_horizontal, easingType_vertical, easingTime_vertical);
        }

        public void Follow_SetEasing(EasingType easingType_horizontal, float easingTime_horizontal, EasingType easingType_vertical, float easingTime_vertical, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;
            tcCam.Follow_SetEasing(easingType_horizontal, easingTime_horizontal, easingType_vertical, easingTime_vertical);
        }

        public void Follow_ChangeTarget(Transform target, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;

            tcCam.Follow_ChangeTarget(target);
        }

        public void Follow_ChangeOffset(Vector3 offset, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;
            tcCam.Follow_ChangeOffset(offset);
        }

        public void Follow_SetFollowType(TCFollowType followType, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;
            var followCom = tcCam.FollowComponent;
            followCom.model.followType = followType;
        }

        public bool Follow_HasTarget(int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return false;
            var targeterModel = tcCam.TargetorModel;
            return targeterModel.HasFollowTarget;
        }

        public Transform Follow_GetTransform(int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return null;
            return tcCam.TargetorModel.FollowTarget;
        }

        public Vector3 Follow_GetNormalOffset(int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return Vector3.zero;
            return tcCam.FollowComponent.GetNormalOffset();
        }

        //- LookAt
        public void LookAt_SetEnable(bool flag, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;

            var lookAtComponent = tcCam.LookAtComponent;
            lookAtComponent.SetLookAtEnable(flag);
        }

        public void LookAt_SetInit(Transform target,
                                   Vector3 offset,
                                   EasingType horizontalEasingType,
                                   float horizontalEasingTime,
                                   EasingType verticalEasingType,
                                   float verticalEasingTime,
                                   int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;

            tcCam.LookAt_SetInit(target, offset, horizontalEasingType, horizontalEasingTime, verticalEasingType, verticalEasingTime);
        }

        public void LookAt_SetNormalLookActivated(bool flag, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;

            var lookAtComponent = tcCam.LookAtComponent;
            lookAtComponent.model.normalLookActivated = flag;
        }

        public void LookAt_SetNormalAngles(in Vector3 eulerAngles, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;

            var lookAtComponent = tcCam.LookAtComponent;
            lookAtComponent.SetNormalLookAngles(eulerAngles);
        }

        public void LookAt_SetEasing(EasingType horizontalEasingType,
                                     float horizontalEasingTime,
                                     EasingType verticalEasingType,
                                     float verticalEasingTime,
                                     int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;

            tcCam.LookAt_SetEasing(horizontalEasingType, horizontalEasingTime, verticalEasingType, verticalEasingTime);
        }

        public void LookAt_ChangeTarget(Transform target, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;

            tcCam.LookAt_ChangeTarget(target);

            if (target == null) {
                var composerModel = tcCam.LookAtComponent.model.composerModel;
                var composerType = composerModel.composerType;
                if (composerType == TCLookAtComposerType.LookAtTarget) {
                    composerModel.composerType = TCLookAtComposerType.None;
                }
            }
        }

        public void LookAt_SetComposerNormalLookActivated(bool flag, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;

            tcCam.LookAtComponent.SetComposerNormalLookActivated(flag);
        }

        public void LookAt_SetComposerNormalAngles(in Vector3 angles, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;

            tcCam.LookAtComponent.SetComposerNormalLookAngles(angles);
        }

        public void LookAt_SetComposerNormalDamping(float damping, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;

            var lookAtComponent = tcCam.LookAtComponent;
            lookAtComponent.SetComposerNormalDamping(damping);
        }

        public Transform LookAt_GetTransform(int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) {
                return null;
            }

            return tcCam.TargetorModel.LookAtTarget;
        }

        public Vector3 LookAt_GetNormalAngle(int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) {
                return Vector3.zero;
            }

            return tcCam.LookAtComponent.model.normalLookAngles;
        }

        // ==== State ====
        //- Shake
        public void Enter_Shake(TCShakeStateModel[] mods, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;

            tcCam.ShakeStateComponent.Enter(mods);
        }

        //- Movement
        public void Enter_Movement(TCMovementStateModel[] mods, bool isExitReset, EasingType exitEasing, float exitDuration, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;

            tcCam.MovementStateCom.Enter(mods, isExitReset, exitEasing, exitDuration);
        }

        //- Round
        public void Enter_Round(TCRoundStateModel[] mods, bool isExitReset, EasingType exitEasing, float exitDuration, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;

            tcCam.RoundStateCom.Enter(mods, isExitReset, exitEasing, exitDuration);
        }

        //- Rotation
        public void Enter_Rotation(TCRotateStateModel[] mods, bool isExitReset, EasingType exitEasing, float exitDuration, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;
            tcCam.RotateStateComponent.Enter(mods, isExitReset, exitEasing, exitDuration);
        }

        //- Push
        public void Enter_Push(TCPushStateModel[] mods, bool isExitReset, EasingType exitEasing, float exitDuration, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;
            tcCam.PushStateCom.Enter(mods, isExitReset, exitEasing, exitDuration);
        }

        //- FOV
        public void Enter_FOV(TCFOVStateModel[] mods, bool isExitReset, EasingType exitEasing, float exitDuration, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;
            tcCam.FOVStateComponent.Enter(mods, isExitReset, exitEasing, exitDuration);
        }

        //- Auto Facing
        public void SetAutoFacing(EasingType easingType, float duration, float minAngleDiff, float sameForwardBreakTime, int id) {
            var repo = context.CameraRepo;
            if (!TryGetTCCameraByID(id, out var tcCam)) return;
            var targeterModel = tcCam.TargetorModel;
            var autoFacingStateComponent = tcCam.AutoFacingStateComponent;
            var camTF = context.MainCamera.transform;
            var followPos = targeterModel.FollowTargetPos;
            followPos.y = 0;
            var camPos = camTF.position;
            camPos.y = 0;
            var xzDis = Vector3.Distance(followPos, camPos);
            autoFacingStateComponent.EnterAutoFacing(targeterModel.FollowTargetForward, camTF.forward, xzDis, easingType, duration, minAngleDiff, sameForwardBreakTime);
        }

        #region [Look At Composer]

        public void SetLookAtComposer(TCLookAtComposerModel composer, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;
            composer.deadZoneNormalizedW = composer.deadZoneNormalizedW < 0 ? 0 : composer.deadZoneNormalizedW;
            composer.deadZoneNormalizedH = composer.deadZoneNormalizedH < 0 ? 0 : composer.deadZoneNormalizedH;
            composer.softZoneNormalizedW = composer.softZoneNormalizedW < 0 ? 0 : composer.softZoneNormalizedW;
            composer.softZoneNormalizedH = composer.softZoneNormalizedH < 0 ? 0 : composer.softZoneNormalizedH;
            tcCam.LookAtComponent.SetComposerModel(composer);
        }

        public void SetLookAtComposerType(TCLookAtComposerType composerType, int id) {
            if (!TryGetTCCameraByID(id, out var tcCam)) return;
            var targeterModel = tcCam.TargetorModel;
            if (composerType == TCLookAtComposerType.LookAtTarget && !targeterModel.HasLookAtTarget) {
                Debug.LogWarning("SetLookAtComposerType: LookAtTarget but no lookAtTarget");
                return;
            }
            if (!targeterModel.HasLookAtTarget || !targeterModel.HasFollowTarget) {
                Debug.LogWarning("SetLookAtComposerType: LookAtAndFollowTarget but no lookAtTarget or followTarget");
                return;
            }

            tcCam.LookAtComponent.SetComposerType(composerType);

            //- Reset 
            var followCom = tcCam.FollowComponent;
            followCom.ResetOffset();

            //- Reset 
            var infoCom = tcCam.BeforeInfo;
            infoCom.SetRotation(Quaternion.identity);
        }

        public bool TryGetTCCameraByID(int id, out TCCameraEntity tcCam) {
            var repo = context.CameraRepo;
            if (id == -1) {
                tcCam = repo.CurrentTCCam;
            } else {
                repo.TryGet(id, out tcCam);
            }

            return tcCam != null;
        }

        public Quaternion LookAtComposer_Horizontal(in Vector3 lookAtTargetPos,
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

        public Quaternion LookAtComposer_Vertical(in Vector3 lookAtTargetPos,
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

        public Quaternion LookAtComposer_Horizontal(in Vector3 eyePos,
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

        public Quaternion LookAtComposer_Vertical(in Vector3 eyePos,
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

        #endregion

        #region [Camera Basic]

        public Vector3 WorldToScreenPoint(Vector3 worldPosition,
                                          Vector3 eyePosition,
                                          Quaternion eyeRotation,
                                          Matrix4x4 projectionMatrix,
                                          int screenWidth,
                                          int screenHeight) {

            Vector3 viewportPoint = WorldToViewportPoint(worldPosition, eyePosition, eyeRotation, projectionMatrix);
            //- 将 视口坐标 ===> 屏幕坐标 
            Vector3 screenPoint = new Vector3(viewportPoint.x * screenWidth, viewportPoint.y * screenHeight, viewportPoint.z);

            // Debug.Log($"WorldToScreenPoint\nviewportPoint = {viewportPoint}\nscreenPoint = {screenPoint}");
            return screenPoint;
        }

        public Vector3 WorldToViewportPoint(Vector3 worldPosition,
                                            Vector3 cameraPosition,
                                            Quaternion cameraRotation,
                                            Matrix4x4 projectionMatrix) {
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

        public Vector3 ScreenToViewportPoint(Vector3 screenPoint, int screenWidth, int screenHeight) {
            Vector3 viewportPoint = new Vector3(screenPoint.x / screenWidth, screenPoint.y / screenHeight, screenPoint.z);
            return viewportPoint;
        }

        public Vector3 ScreenPointToDir(Vector3 screenPoint,
                                        Vector3 eyePosition,
                                        Quaternion eyeRotation,
                                        Matrix4x4 projectionMatrix,
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
        public Matrix4x4 GetProjectionMatrix(in Vector3 eyePosition,
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

        public Quaternion FromToScreenPoint(in Vector3 fromScreenPoint,
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

        #endregion

        #region [MISC]

        public void MISC_SetMaxLookDownDegree(float degree, int id) {
            var tcCam = GetTCCamera(id);
            if (tcCam == null) return;

            var miscCom = tcCam.MISCComponent;
            miscCom.model.maxLookDownDegree = degree;
        }

        public void MISC_SetMaxLookUpDegree(float degree, int id) {
            var tcCam = GetTCCamera(id);
            if (tcCam == null) return;

            var miscCom = tcCam.MISCComponent;
            miscCom.model.maxLookUpDegree = degree;
        }

        public void MISC_SetLookLimitActivated(bool activated, int id) {
            var tcCam = GetTCCamera(id);
            if (tcCam == null) return;

            var miscCom = tcCam.MISCComponent;
            miscCom.model.lookLimitActivated = activated;
        }

        public void MISC_SetMoveSpeedLimitActivated(bool activated, int id) {
            var tcCam = GetTCCamera(id);
            if (tcCam == null) return;

            var miscCom = tcCam.MISCComponent;
            miscCom.model.moveSpeedLimitActivated = activated;
        }

        public void MISC_SetMaxMoveSpeed(float speed, int id) {
            var tcCam = GetTCCamera(id);
            if (tcCam == null) return;

            var miscCom = tcCam.MISCComponent;
            miscCom.model.maxMoveSpeed = speed;
        }

        public TCCameraEntity GetTCCamera(int id) {
            TCCameraEntity tcCam;
            var repo = context.CameraRepo;
            if (id == -1) {
                tcCam = repo.CurrentTCCam;
            } else {
                repo.TryGet(id, out tcCam);
            }

            return tcCam;
        }

        #endregion

    }

}