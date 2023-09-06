using UnityEngine;
using GameArki.TripodCamera.Facades;
using GameArki.TripodCamera.Entities;
using GameArki.TripodCamera.Hook;
using GameArki.TripodCamera.Template;

namespace GameArki.TripodCamera.Domain {

    public class TCApplyDomain {

        TCContext context;
        TCCameraDomain cameraDomain;
        TCDirectorDomain directorDomain;

        public TCApplyDomain() {

        }

        public void Inject(TCContext context, TCCameraDomain cameraDomain, TCDirectorDomain directorDomain) {
            this.context = context;
            this.cameraDomain = cameraDomain;
            this.directorDomain = directorDomain;
        }

        public void ApplyHook(TCCameraHook hook) {
            hook.Tick();
        }

        public void _ApplyNormal(TCCameraEntity tcCam, float dt) {

            tcCam.CopyBeforeInfo2AfterInfo(); // Mutable ReadFrom Base

            _ApplyNormal_Follow(tcCam, dt);
            _ApplyMISC_MoveSpeedLimit(tcCam, dt);
            _ApplyNormal_LookAt(tcCam, dt);
            _ApplyNormal_LookAtComposer(tcCam, dt);
            _ApplyNormal_PhysicsRecoil(tcCam, dt);
            _ApplyMISC_LookLimit(tcCam, dt);

            tcCam.CopyAfterInfo2BeforeInfo(); // Mutable WriteTo Base

        }

        // ==== Normal ====
        void _ApplyNormal_Follow(TCCameraEntity tcCam, float dt) {
            var afterInfo = tcCam.AfterInfo;
            var followCom = tcCam.FollowComponent;

            var dollyTrackStateComponent = tcCam.DollyTrackStateComponent;
            if (dollyTrackStateComponent.IsActivated) {
                afterInfo.SetPosition(dollyTrackStateComponent.GetDollyPos());
                return;
            }

            var targeterModel = tcCam.TargetorModel;
            if (!targeterModel.HasFollowTarget) return;

            ref var followModel = ref followCom.model;
            var followType = followModel.followType;
            followCom.Tick(dt);

            if (followType == TCFollowType.Normal) {
                Vector3 camPos = followCom.GetCameraEasedPos();
                afterInfo.SetPosition(camPos);
                return;
            }

            if (followType == TCFollowType.RoundWhenLookAt) {
                var camPos = followCom.GetCameraEasedPos();
                var lookAtCom = tcCam.LookAtComponent;
                if (lookAtCom.CanLookAt()) {
                    camPos = followCom.GetTargetEasedPos() + afterInfo.Rotation * followModel.normalFollowOffset;
                }
                afterInfo.SetPosition(camPos);
                return;
            }

            if (followType == TCFollowType.MachineArm) {
                var followTargetPos = followCom.GetTargetEasedPos();
                var camPos = afterInfo.Position;
                var dir = followTargetPos - camPos;
                dir.y = 0;
                dir.Normalize();

                var normalFollowOffset = followModel.normalFollowOffset;
                var camOffset = Quaternion.LookRotation(dir) * normalFollowOffset;
                afterInfo.SetPosition(followTargetPos + camOffset);

                var newCamRot = Quaternion.LookRotation(-camOffset.normalized);
                afterInfo.SetRotation(newCamRot);

                return;
            }
        }

        void _ApplyNormal_LookAt(TCCameraEntity tcCam, float dt) {
            var composerModel = tcCam.LookAtComponent.model.composerModel;
            if (composerModel.composerType != TCLookAtComposerType.None
            && !tcCam.RoundStateComponent.IsActivated
            && !tcCam.MovementStateComponent.IsActivated) return;
            var followType = tcCam.FollowComponent.model.followType;
            if (followType == TCFollowType.MachineArm) return;

            var lookAtCom = tcCam.LookAtComponent;
            var afterInfo = tcCam.AfterInfo;
            lookAtCom.Tick(afterInfo, dt);
            var targeterModel = tcCam.TargetorModel;

            if (lookAtCom.CanLookAt()) {
                // - Look at target
                var targetEasedPos = lookAtCom.GetTargetEasedPos();
                var rot = Quaternion.LookRotation(targetEasedPos - afterInfo.Position);
                afterInfo.SetRotation(rot);
            } else if (lookAtCom.model.normalLookActivated) {
                Quaternion camRot = Quaternion.Euler(lookAtCom.model.normalLookAngles);
                afterInfo.SetRotation(camRot);
            }

        }

        void _ApplyNormal_LookAtComposer(TCCameraEntity tcCam, float dt) {
            if (tcCam.RoundStateComponent.IsActivated) return;
            if (tcCam.MovementStateComponent.IsActivated) return;

            var composer = tcCam.LookAtComponent.model.composerModel;
            var composerType = composer.composerType;
            if (composerType == TCLookAtComposerType.None) return;

            var followType = tcCam.FollowComponent.model.followType;
            if (followType == TCFollowType.MachineArm) return;

            var targeterModel = tcCam.TargetorModel;
            var afterInfo = tcCam.AfterInfo;
            var beforeInfo = tcCam.BeforeInfo;
            var screenWidth = afterInfo.ScreenWidth;
            var screenHeight = afterInfo.ScreenHeight;

            var deadZoneLT = composer.GetDeadZoneLT(screenWidth, screenHeight);
            var deadZoneRB = composer.GetDeadZoneRB(screenWidth, screenHeight);

            var eyePos = afterInfo.Position;
            var eyeRot = afterInfo.Rotation;
            var nearClipPlane = afterInfo.NearClipPlane;
            var farClipPlane = afterInfo.FarClipPlane;
            var aspect = afterInfo.Aspect;
            var fov = afterInfo.FOV;

            if (composerType == TCLookAtComposerType.LookAtTarget) {
                ApplyComposer_OneTarget();
            }

            ApplyComposer_NormalLook();

            void ApplyComposer_OneTarget() {
                if (!targeterModel.HasLookAtTarget) {
                    return;
                }

                var lookAtTargetPos = targeterModel.LookAtTargetPos;
                var projectionMatrix = cameraDomain.GetProjectionMatrix(eyePos,
                                                                        fov,
                                                                        aspect,
                                                                        nearClipPlane,
                                                                        farClipPlane);
                eyeRot = cameraDomain.LookAtComposer_OneTarget(lookAtTargetPos,
                                                               eyePos,
                                                               eyeRot,
                                                               composer,
                                                               projectionMatrix,
                                                               screenWidth,
                                                               screenHeight);
                // TODO : 设置为死区模型的DST Rotation, 在LookAtComponent进行Easing
                afterInfo.SetRotation(eyeRot);
                afterInfo.SetPosition(eyePos);
            }

            void ApplyComposer_NormalLook() {
                if (!composer.normalLookActivated) {
                    return;
                }

                float normalLookAngle = composer.normalLookAngles.x;
                float normalLookDamping = composer.normalDamping;
                normalLookDamping = normalLookDamping != 0f ? normalLookDamping : TCDefaultConfig.LOOKAT_COMPOSER_DEFAULT_NORMAL_DAMPING;

                float lookAngle = eyeRot.eulerAngles.x;
                lookAngle = lookAngle > 180 ? lookAngle - 360 : lookAngle;
                normalLookAngle = normalLookAngle > 180 ? normalLookAngle - 360 : normalLookAngle;
                float horAngleDiff = lookAngle - normalLookAngle;
                Quaternion normalRot = Quaternion.identity;
                if (horAngleDiff < 0f) {
                    // - look down
                    float strideAngle = normalLookDamping != 0 ? dt * horAngleDiff / normalLookDamping : horAngleDiff;
                    Quaternion strideRot = Quaternion.Euler(new Vector3(-strideAngle, 0, 0));
                    normalRot = eyeRot * strideRot;
                } else {
                    // - look up
                    float strideAngle = normalLookDamping != 0 ? dt * horAngleDiff / normalLookDamping : horAngleDiff;
                    Quaternion strideRot = Quaternion.Euler(new Vector3(-strideAngle, 0, 0));
                    normalRot = eyeRot * strideRot;
                }

                var lookAtTargetPos = targeterModel.LookAtTargetPos;
                var projectionMatrix = cameraDomain.GetProjectionMatrix(eyePos,
                                                                        fov,
                                                                        aspect,
                                                                        nearClipPlane,
                                                                        farClipPlane);
                Vector3 screenPoint = cameraDomain.WorldToScreenPoint(lookAtTargetPos,
                                                                      eyePos,
                                                                      normalRot,
                                                                      projectionMatrix,
                                                                      screenWidth,
                                                                      screenHeight);
                if (composer.IsInDeadZone_Horizontal(screenPoint, screenWidth, screenHeight, out _)) {
                    eyeRot.eulerAngles = new Vector3(eyeRot.eulerAngles.x, normalRot.eulerAngles.y, eyeRot.eulerAngles.z);
                }
                if (composer.IsInDeadZone_Vertical(screenPoint, screenWidth, screenHeight, out _)) {
                    eyeRot.eulerAngles = new Vector3(normalRot.eulerAngles.x, eyeRot.eulerAngles.y, eyeRot.eulerAngles.z);
                }
                afterInfo.SetRotation(eyeRot);
            }
        }

        void _ApplyNormal_PhysicsRecoil(TCCameraEntity tcCam, float dt) {
            var composerModel = tcCam.LookAtComponent.model.composerModel;
            if (composerModel.composerType == TCLookAtComposerType.None) {
                return;
            }

            var beforeInfo = tcCam.BeforeInfo;
            var afterInfo = tcCam.AfterInfo;
            var baseRot = beforeInfo.Rotation;
            var mutableRot = afterInfo.Rotation;
            var targetorModel = tcCam.TargetorModel;

            var followCom = tcCam.FollowComponent;
            var followTargetPos = targetorModel.FollowTargetPos;
            var mutablePos = afterInfo.Position;
            var screenWidth = afterInfo.ScreenWidth;
            var screenHeight = afterInfo.ScreenHeight;
            var projectionMatrix = cameraDomain.GetProjectionMatrix(
                mutablePos,
                afterInfo.FOV,
                afterInfo.Aspect,
                afterInfo.NearClipPlane,
                afterInfo.FarClipPlane
            );

            float borderY = followTargetPos.y - 0.75f;
            if (mutablePos.y <= borderY) {
                Vector3 screenPoint = cameraDomain.WorldToScreenPoint(followTargetPos, mutablePos, mutableRot, projectionMatrix, screenWidth, screenHeight);
                Vector3 dir = cameraDomain.ScreenPointToDir(screenPoint, mutablePos, mutableRot, projectionMatrix, screenWidth, screenHeight);
                Debug.DrawLine(mutablePos, mutablePos + dir * 10, Color.blue, 1f);
                float dis = (borderY - mutablePos.y) / Vector3.Dot(dir, Vector3.up);
                Vector3 physicsRecoilOffset = dir * dis;
                followCom.physicsRecoilOffset = physicsRecoilOffset;
                mutablePos += physicsRecoilOffset;
                afterInfo.SetPosition(mutablePos);
            } else {
                followCom.physicsRecoilOffset = Vector3.zero;
            }
        }

        void _ApplyMISC_LookLimit(TCCameraEntity tcCam, float dt) {
            var miscCom = tcCam.MISCComponent;
            if (miscCom.model.lookLimitActivated) {
                var maxLookDownDegree = miscCom.model.maxLookDownDegree;
                var maxLookUpDegree = miscCom.model.maxLookUpDegree;
                var afterInfo = tcCam.AfterInfo;
                var returnRot = afterInfo.Rotation;
                if (maxLookDownDegree != 0) {
                    Vector3 eulerAngles = returnRot.eulerAngles;
                    float curPitchDegree = eulerAngles.x;
                    curPitchDegree = curPitchDegree > 180 ? curPitchDegree - 360 : curPitchDegree;
                    curPitchDegree = curPitchDegree > maxLookDownDegree ? maxLookDownDegree : curPitchDegree;
                    float curYawDegree = eulerAngles.y;
                    returnRot = Quaternion.Euler(0, curYawDegree, 0) * Quaternion.Euler(curPitchDegree, 0, 0);
                }
                if (maxLookUpDegree != 0) {
                    Vector3 eulerAngles = returnRot.eulerAngles;
                    float curPitchDegree = eulerAngles.x;
                    curPitchDegree = curPitchDegree > 180 ? curPitchDegree - 360 : curPitchDegree;
                    curPitchDegree = curPitchDegree < -maxLookUpDegree ? -maxLookUpDegree : curPitchDegree;
                    returnRot.eulerAngles = new Vector3(curPitchDegree, eulerAngles.y, eulerAngles.z);
                }
                afterInfo.SetRotation(returnRot);
            }
        }

        void _ApplyMISC_MoveSpeedLimit(TCCameraEntity tcCam, float dt) {
            if (tcCam.DollyTrackStateComponent.IsActivated) {
                return;
            }

            var miscCom = tcCam.MISCComponent;
            if (miscCom.model.moveSpeedLimitActivated) {
                var beforeInfo = tcCam.BeforeInfo;
                var afterInfo = tcCam.AfterInfo;
                var posOffset = afterInfo.Position - beforeInfo.Position;
                var curMoveSpeed = posOffset.magnitude / dt;
                var maxMoveSpeed = miscCom.model.maxMoveSpeed;

                if (curMoveSpeed > maxMoveSpeed) {
                    var newOffset = posOffset.normalized * maxMoveSpeed * dt;
                    afterInfo.SetPosition(beforeInfo.Position + newOffset);
                }
            }
        }

        // ==== State ====
        public void ApplyStateEffect(TCCameraEntity tcCam, float dt) {
            ApplyState_DollyTrack(tcCam, dt);
            ApplyState_Shake(tcCam, dt);
            ApplyState_Move(tcCam, dt);
            ApplyState_Push(tcCam, dt);
            ApplyState_Round(tcCam, dt);
            ApplyState_AutoFacing(tcCam, dt);
            ApplyState_Rotate(tcCam, dt);
            ApplyState_FOV(tcCam, dt);
        }

        void ApplyState_DollyTrack(TCCameraEntity tcCam, float dt) {
            var dollyTrackStateCom = tcCam.DollyTrackStateComponent;
            dollyTrackStateCom.Tick(dt);
        }

        void ApplyState_Shake(TCCameraEntity tcCam, float dt) {
            var shakeStateCom = tcCam.ShakeStateComponent;
            shakeStateCom.Tick(dt);

            var afterInfo = tcCam.AfterInfo;
            Vector3 pos = afterInfo.Position;
            pos += shakeStateCom.GetShakeOffset();
            afterInfo.SetPosition(pos);
        }

        void ApplyState_Move(TCCameraEntity tcCam, float dt) {
            var moveStateCom = tcCam.MovementStateComponent;
            moveStateCom.Tick(dt);

            var afterInfo = tcCam.AfterInfo;
            Vector3 pos = afterInfo.Position;
            pos += moveStateCom.GetMoveOffset();
            afterInfo.SetPosition(pos);
        }

        void ApplyState_Push(TCCameraEntity tcCam, float dt) {
            var pushStateCom = tcCam.PushStateComponent;
            pushStateCom.Tick(dt);

            var afterInfo = tcCam.AfterInfo;
            Vector3 pos = afterInfo.Position;
            Vector3 forward = afterInfo.GetForward();
            pos += pushStateCom.GetPushOffset(forward);
            afterInfo.SetPosition(pos);
        }

        void ApplyState_Round(TCCameraEntity tcCam, float dt) {
            var roundStateCom = tcCam.RoundStateComponent;
            if (!roundStateCom.IsActivated) {
                return;
            }

            roundStateCom.Tick(dt);

            var afterInfo = tcCam.AfterInfo;
            Vector3 pos = afterInfo.Position;
            var roundOffset = roundStateCom.GetRoundOffset(pos);
            pos += roundOffset;
            afterInfo.SetPosition(pos);

            // - Keep the camera always look at the target while rounding
            var targeterModel = tcCam.TargetorModel;
            if (targeterModel.HasLookAtTarget) {
                var lookAtRot = Quaternion.LookRotation(targeterModel.LookAtTargetPos - pos);
                afterInfo.SetRotation(lookAtRot);
            }
        }

        void ApplyState_AutoFacing(TCCameraEntity tcCam, float dt) {
            var baseForward = tcCam.BeforeInfo.GetForward();
            var targeterModel = tcCam.TargetorModel;
            var autoFacingStateCom = tcCam.AutoFacingStateComponent;
            if (targeterModel.HasFollowTarget && autoFacingStateCom.IsEnabled) {
                autoFacingStateCom.Tick(targeterModel.FollowTargetForward, baseForward, dt);
                var afterInfo = tcCam.AfterInfo;
                Vector3 pos = afterInfo.Position;
                pos += autoFacingStateCom.GetOffset();
                afterInfo.SetPosition(pos);
            }
        }

        void ApplyState_Rotate(TCCameraEntity tcCam, float dt) {
            var rotateStateCom = tcCam.RotateStateComponent;
            rotateStateCom.Tick(dt);

            var afterInfo = tcCam.AfterInfo;
            Quaternion rot = afterInfo.Rotation;
            rot *= Quaternion.Euler(rotateStateCom.GetRotateOffset());
            afterInfo.SetRotation(rot);
        }

        void ApplyState_FOV(TCCameraEntity tcCam, float dt) {

            var fovStateCom = tcCam.FOVStateComponent;
            fovStateCom.Tick(dt);

            var afterInfo = tcCam.AfterInfo;
            float fov = fovStateCom.GetFOVOffset();
            afterInfo.SetFOVByClamp(fov);

        }

        // ==== Final ====
        public void ApplyToMain(TCCameraEntity tcCam, Camera mainCam) {
            var afterInfo = tcCam.AfterInfo;
            mainCam.transform.position = afterInfo.Position;
            mainCam.transform.rotation = afterInfo.Rotation;
            mainCam.fieldOfView = afterInfo.FOV;
        }

        public void ApplyCameraTM(in TCCameraTM tm, int id = -1) {
            TCCameraEntity tcCam = null;
            if (id == -1) {
                tcCam = context.CameraRepo.CurrentTCCam;
            } else {
                context.CameraRepo.TryGet(id, out tcCam);
            }
            if (tcCam == null) return;

            if (tm.needSet_Follow) tcCam.FollowComponent.model = TCTM2RuntimeUtil.ToTCFollowModel(tm.followTM);

            if (tm.needSet_LookAt) tcCam.LookAtComponent.model = TCTM2RuntimeUtil.ToTCLookAtModel(tm.lookAtTM);

            if (tm.needSet_Misc) tcCam.MISCComponent.model = TCTM2RuntimeUtil.ToTCMiscModel(tm.miscTM);
          
            if (tm.needSet_Movement) tcCam.MovementStateComponent.Enter(TCTM2RuntimeUtil.ToTCMovementStateModelArray(tm.movementStateTMArray), tm.isExitReset_Movement, tm.exitEasing_Movement, tm.exitDuration_Movement);

            if (tm.needSet_Round) tcCam.RoundStateComponent.Enter(TCTM2RuntimeUtil.ToTCRoundStateModelArray(tm.roundStateTMArray), tm.isExitReset_Round, tm.exitEasing_Round, tm.exitDuration_Round);

            if (tm.needSet_Rotate) tcCam.RotateStateComponent.Enter(TCTM2RuntimeUtil.ToTCRotateStateModelArray(tm.rotateStateTMArray), tm.isExitReset_Rotate, tm.exitEasing_Rotate, tm.exitDuration_Rotate);

            if (tm.needSet_Push) tcCam.PushStateComponent.Enter(TCTM2RuntimeUtil.ToTCPushStateModelArray(tm.pushStateTMArray), tm.isExitReset_Push, tm.exitEasing_Push, tm.exitDuration_Push);

            if (tm.needSet_FOV) tcCam.FOVStateComponent.Enter(TCTM2RuntimeUtil.ToTCFOVStateModelArray(tm.fovStateTMArray), tm.isExitReset_FOV, tm.exitEasing_FOV, tm.exitDuration_FOV);

            if (tm.needSet_DollyTrack) tcCam.DollyTrackStateComponent.Enter(TCTM2RuntimeUtil.ToTCDollyTrackStateModel(tm.dollyTrackStateTM));

            if (tm.needSet_Shake) tcCam.ShakeStateComponent.Enter(TCTM2RuntimeUtil.ToTCShakeStateModelArray(tm.shakeStateTMArray));
        }

    }

}