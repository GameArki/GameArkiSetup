using UnityEngine;
using GameArki.TripodCamera.Facades;
using GameArki.TripodCamera.Entities;
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

        public void TickAndApply(TCCameraEntity tc, float dt) {
            // Warning: Don't change the execute order easily!!!

            tc.CopyBeforeInfo2AfterInfo();

            _Apply_Follow(tc, dt);
            _ApplyMISC_MoveSpeedLimit(tc, dt);

            _Apply_LookAt(tc, dt);
            _Apply_LookAtComposer(tc, dt);
            _ApplyMISC_RotateSpeedLimit(tc, dt);

            _Apply_PhysicsRecoil(tc, dt);

            _ApplyMISC_LookLimit(tc, dt);

            tc.CopyAfterInfo2BeforeInfo();
        }

        // ==== Normal ====
        void _Apply_Follow(TCCameraEntity tc, float dt) {
            var afterInfo = tc.AfterInfo;
            var followCom = tc.FollowComponent;

            var dollyTrackStateComponent = tc.DollyTrackStateComponent;
            if (dollyTrackStateComponent.IsActivated) {
                afterInfo.SetPosition(dollyTrackStateComponent.GetDollyPos());
                return;
            }

            var targetorModel = tc.TargetorModel;
            if (!targetorModel.HasFollowTarget) return;

            ref var followModel = ref followCom.model;
            var followType = followModel.followType;
            followCom.Tick(dt);

            var director = context.directorEntity;
            if (director.FSMComponent.FSMState == TCDirectorFSMState.ManualRounding) return;

            if (followType == TCFollowType.Normal) {
                Vector3 camPos = followCom.GetCameraEasedPos();
                afterInfo.SetPosition(camPos);
                return;
            }

            if (followType == TCFollowType.RoundWhenLookAt) {
                var camPos = followCom.GetCameraEasedPos();
                var lookAtCom = tc.LookAtComponent;
                if (lookAtCom.CanLookAt()) {
                    var followTargetPos = followCom.GetTargetEasedPos();
                    camPos = followTargetPos + Quaternion.Euler(0, afterInfo.Rotation.eulerAngles.y, 0) * followModel.normalFollowOffset;
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

        void _Apply_LookAt(TCCameraEntity tc, float dt) {
            var composerModel = tc.LookAtComponent.model.composerModel;
            if (composerModel.composerType != TCLookAtComposerType.None
            && !tc.RoundStateCom.IsActivated
            && !tc.MovementStateCom.IsActivated) return;

            var lookAtCom = tc.LookAtComponent;
            var afterInfo = tc.AfterInfo;
            var followType = tc.FollowComponent.model.followType;

            // - Look at target
            if (lookAtCom.CanLookAt() && followType != TCFollowType.MachineArm) {
                var targetEasedPos = lookAtCom.GetTargetEasedPos();
                var rot = Quaternion.LookRotation(targetEasedPos - afterInfo.Position);
                lookAtCom.Tick(afterInfo, dt);
                afterInfo.SetRotation(rot);
                return;
            }

            // - Normal Look Angle
            if (followType != TCFollowType.MachineArm && lookAtCom.model.normalLookActivated) {
                afterInfo.SetRotation(Quaternion.Euler(lookAtCom.model.normalLookAngles));
                return;
            }

            if (followType == TCFollowType.MachineArm && lookAtCom.model.normalLookActivated) {
                var normalLookAngles = lookAtCom.model.normalLookAngles;
                var afterAngleY = afterInfo.Rotation.eulerAngles.y;
                afterInfo.SetRotation(Quaternion.Euler(normalLookAngles.x, afterAngleY, normalLookAngles.z));
                return;
            }
        }

        void _Apply_LookAtComposer(TCCameraEntity tc, float dt) {
            if (tc.RoundStateCom.IsActivated) return;
            if (tc.MovementStateCom.IsActivated) return;

            var lookAtCom = tc.LookAtComponent;
            if (!lookAtCom.CanLookAt()) return;

            var composer = lookAtCom.model.composerModel;
            var composerType = composer.composerType;
            if (composerType == TCLookAtComposerType.None) return;

            var followType = tc.FollowComponent.model.followType;
            if (followType == TCFollowType.MachineArm) return;

            var targetorModel = tc.TargetorModel;
            var afterInfo = tc.AfterInfo;
            var screenWidth = afterInfo.ScreenWidth;
            var screenHeight = afterInfo.ScreenHeight;

            var eyePos = afterInfo.Position;
            var eyeRot = afterInfo.Rotation;
            var nearClipPlane = afterInfo.NearClipPlane;
            var farClipPlane = afterInfo.FarClipPlane;
            var aspect = afterInfo.Aspect;
            var fov = afterInfo.FOV;

            if (composerType == TCLookAtComposerType.LookAtTarget) {
                ApplyComposer_OneTarget();
                ApplyComposer_NormalLook();
            }

            void ApplyComposer_OneTarget() {
                var projectionMatrix = TCMathUtil.GetProjectionMatrix(eyePos,
                                                                        fov,
                                                                        aspect,
                                                                        nearClipPlane,
                                                                        farClipPlane);

                Vector3 screenPoint_lt = TCMathUtil.WorldToScreenPoint(targetorModel.LookAtTargetPos, eyePos, eyeRot, projectionMatrix, screenWidth, screenHeight);
                Vector3 screenPoint_ft = TCMathUtil.WorldToScreenPoint(targetorModel.FollowTargetPos, eyePos, eyeRot, projectionMatrix, screenWidth, screenHeight);

                eyeRot = TCMathUtil.LookAtComposer_Horizontal(eyePos,
                                                                eyeRot,
                                                                composer,
                                                                projectionMatrix,
                                                                screenWidth,
                                                                screenHeight,
                                                                screenPoint_lt);
                eyeRot = TCMathUtil.LookAtComposer_Vertical(eyePos,
                                                              eyeRot,
                                                              composer,
                                                              projectionMatrix,
                                                              screenWidth,
                                                              screenHeight,
                                                              screenPoint_lt,
                                                              screenPoint_ft);



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

                var lookAtTargetPos = targetorModel.LookAtTargetPos;
                var projectionMatrix = TCMathUtil.GetProjectionMatrix(eyePos,
                                                                        fov,
                                                                        aspect,
                                                                        nearClipPlane,
                                                                        farClipPlane);
                Vector3 screenPoint = TCMathUtil.WorldToScreenPoint(lookAtTargetPos,
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

        void _Apply_PhysicsRecoil(TCCameraEntity tc, float dt) {
            var composerModel = tc.LookAtComponent.model.composerModel;
            if (composerModel.composerType == TCLookAtComposerType.None) {
                return;
            }

            var beforeInfo = tc.BeforeInfo;
            var afterInfo = tc.AfterInfo;
            var baseRot = beforeInfo.Rotation;
            var mutableRot = afterInfo.Rotation;
            var targetorModel = tc.TargetorModel;

            var followCom = tc.FollowComponent;
            var followTargetPos = targetorModel.FollowTargetPos;
            var mutablePos = afterInfo.Position;
            var screenWidth = afterInfo.ScreenWidth;
            var screenHeight = afterInfo.ScreenHeight;
            var projectionMatrix = TCMathUtil.GetProjectionMatrix(
                mutablePos,
                afterInfo.FOV,
                afterInfo.Aspect,
                afterInfo.NearClipPlane,
                afterInfo.FarClipPlane
            );

            float borderY = followTargetPos.y - 0.75f;
            if (mutablePos.y <= borderY) {
                Vector3 screenPoint = TCMathUtil.WorldToScreenPoint(followTargetPos, mutablePos, mutableRot, projectionMatrix, screenWidth, screenHeight);
                Vector3 dir = TCMathUtil.ScreenPointToDir(screenPoint, mutablePos, mutableRot, projectionMatrix, screenWidth, screenHeight);
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

        void _ApplyMISC_LookLimit(TCCameraEntity tc, float dt) {
            var miscCom = tc.MISCComponent;
            if (miscCom.model.lookLimitActivated) {
                var maxLookDownDegree = miscCom.model.maxLookDownDegree;
                var maxLookUpDegree = miscCom.model.maxLookUpDegree;
                var afterInfo = tc.AfterInfo;
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

        void _ApplyMISC_MoveSpeedLimit(TCCameraEntity tc, float dt) {
            if (tc.DollyTrackStateComponent.IsActivated) {
                return;
            }

            var miscCom = tc.MISCComponent;
            if (miscCom.model.moveSpeedLimitActivated) {
                var beforeInfo = tc.BeforeInfo;
                var afterInfo = tc.AfterInfo;
                var posOffset = afterInfo.Position - beforeInfo.Position;
                var curMoveSpeed = posOffset.magnitude / dt;
                var maxMoveSpeed = miscCom.model.maxMoveSpeed;

                if (curMoveSpeed > maxMoveSpeed) {
                    var newOffset = posOffset.normalized * maxMoveSpeed * dt;
                    afterInfo.SetPosition(beforeInfo.Position + newOffset);
                }
            }
        }

        void _ApplyMISC_RotateSpeedLimit(TCCameraEntity tc, float dt) {
            if (tc.DollyTrackStateComponent.IsActivated) {
                return;
            }

            var miscCom = tc.MISCComponent;
            if (miscCom.model.rotateSpeedLimitActivated) {
                var beforeInfo = tc.BeforeInfo;
                var afterInfo = tc.AfterInfo;
                var angleOffset = afterInfo.Rotation.eulerAngles - beforeInfo.Rotation.eulerAngles;
                angleOffset.x = angleOffset.x > 180 ? angleOffset.x - 360 : angleOffset.x;
                angleOffset.y = angleOffset.y > 180 ? angleOffset.y - 360 : angleOffset.y;
                angleOffset.z = angleOffset.z > 180 ? angleOffset.z - 360 : angleOffset.z;
                var curRotateSpeed = angleOffset.magnitude / dt;
                var maxRotateSpeed = miscCom.model.maxRotateSpeed;

                if (curRotateSpeed > maxRotateSpeed) {
                    var newAngleOffset = angleOffset.normalized * maxRotateSpeed * dt;
                    afterInfo.SetRotation(Quaternion.Euler(beforeInfo.Rotation.eulerAngles + newAngleOffset));
                }
            }
        }

        public void Tick_StateEffect(TCCameraEntity tc, float dt) {
            _Tick_StateEffect_DollyTrack(tc, dt);
            _Tick_StateEffect_Shake(tc, dt);
            _Tick_StateEffect_Movement(tc, dt);
            _Tick_StateEffect_Push(tc, dt);
            _Tick_StateEffect_Round(tc, dt);
            _Tick_StateEffect_AutoFacing(tc, dt);
            _Tick_StateEffect_Rotate(tc, dt);
            _Tick_StateEffect_FOV(tc, dt);
        }

        void _Tick_StateEffect_DollyTrack(TCCameraEntity tc, float dt) {
            var dollyTrackStateCom = tc.DollyTrackStateComponent;
            dollyTrackStateCom.Tick(dt);
        }
        void _Tick_StateEffect_Shake(TCCameraEntity tc, float dt) {
            var shakeStateCom = tc.ShakeStateComponent;
            shakeStateCom.Tick(dt);
        }
        void _Tick_StateEffect_Movement(TCCameraEntity tc, float dt) {
            var movementStateCom = tc.MovementStateCom;
            movementStateCom.Tick(dt);
        }
        void _Tick_StateEffect_Push(TCCameraEntity tc, float dt) {
            var pushStateCom = tc.PushStateCom;
            pushStateCom.Tick(dt);
        }
        void _Tick_StateEffect_Round(TCCameraEntity tc, float dt) {
            var roundStateCom = tc.RoundStateCom;
            if (!roundStateCom.IsActivated) return;
            roundStateCom.Tick(dt);
        }
        void _Tick_StateEffect_AutoFacing(TCCameraEntity tc, float dt) {
            var baseForward = tc.BeforeInfo.GetForward();
            var targetorModel = tc.TargetorModel;
            var autoFacingStateCom = tc.AutoFacingStateComponent;
            if (targetorModel.HasFollowTarget && autoFacingStateCom.IsEnabled) {
                autoFacingStateCom.Tick(targetorModel.FollowTargetForward, baseForward, dt);
            }
        }
        void _Tick_StateEffect_Rotate(TCCameraEntity tc, float dt) {
            var rotateStateCom = tc.RotateStateComponent;
            rotateStateCom.Tick(dt);
        }
        void _Tick_StateEffect_FOV(TCCameraEntity tc, float dt) {
            var fovStateCom = tc.FOVStateComponent;
            fovStateCom.Tick(dt);
        }

        public void Apply_StateEffect(TCCameraEntity tc) {
            _Apply_StateEffect_Shake(tc);
            _Apply_StateEffect_Movement(tc);
            _Apply_StateEffect_Push(tc);
            _Apply_StateEffect_Round(tc);
            _Apply_StateEffect_AutoFacing(tc);
            _Apply_StateEffect_Rotate(tc);
            _Apply_StateEffect_FOV(tc);
        }

        void _Apply_StateEffect_Shake(TCCameraEntity tc) {
            var shakeStateCom = tc.ShakeStateComponent;
            var afterInfo = tc.AfterInfo;
            Vector3 pos = afterInfo.Position;
            pos += shakeStateCom.GetShakeOffset();
            afterInfo.SetPosition(pos);
        }
        void _Apply_StateEffect_Movement(TCCameraEntity tc) {
            var movementStateComponent = tc.MovementStateCom;
            var afterInfo = tc.AfterInfo;
            Vector3 pos = afterInfo.Position;
            pos += movementStateComponent.GetMoveOffset();
            afterInfo.SetPosition(pos);
        }
        void _Apply_StateEffect_Push(TCCameraEntity tc) {
            var pushStateComponent = tc.PushStateCom;
            var afterInfo = tc.AfterInfo;
            Vector3 pos = afterInfo.Position;
            Vector3 forward = afterInfo.GetForward();
            pos += pushStateComponent.GetPushOffset(forward);
            afterInfo.SetPosition(pos);
        }
        void _Apply_StateEffect_Round(TCCameraEntity tc) {
            var roundStateCom = tc.RoundStateCom;
            if (!roundStateCom.IsActivated) return;

            var afterInfo = tc.AfterInfo;
            Vector3 pos = afterInfo.Position;
            var roundOffset = roundStateCom.GetRoundOffset(pos);
            pos += roundOffset;
            afterInfo.SetPosition(pos);
            // - Keep the camera always look at the target while rounding
            var targetorModel = tc.TargetorModel;
            if (targetorModel.HasLookAtTarget) {
                var lookAtRot = Quaternion.LookRotation(targetorModel.LookAtTargetPos - pos);
                afterInfo.SetRotation(lookAtRot);
            }
        }
        void _Apply_StateEffect_AutoFacing(TCCameraEntity tc) {
            var targetorModel = tc.TargetorModel;
            var autoFacingStateCom = tc.AutoFacingStateComponent;
            if (targetorModel.HasFollowTarget && autoFacingStateCom.IsEnabled) {
                var afterInfo = tc.AfterInfo;
                Vector3 pos = afterInfo.Position;
                pos += autoFacingStateCom.GetOffset();
                afterInfo.SetPosition(pos);
            }
        }
        void _Apply_StateEffect_Rotate(TCCameraEntity tc) {
            var afterInfo = tc.AfterInfo;
            Quaternion rot = afterInfo.Rotation;
            var rotateStateCom = tc.RotateStateComponent;
            rot *= Quaternion.Euler(rotateStateCom.GetRotateOffset());
            afterInfo.SetRotation(rot);
        }
        void _Apply_StateEffect_FOV(TCCameraEntity tc) {
            var fovStateCom = tc.FOVStateComponent;
            var afterInfo = tc.AfterInfo;
            float fovOffset = fovStateCom.GetFOVOffset();
            afterInfo.SetFOVAsInit();
            afterInfo.AddFOV(fovOffset);
        }

        // ==== Final ====
        public void ApplyToMain(TCCameraEntity tc, Camera mainCam) {
            if (tc == null) return;

            Apply_StateEffect(tc);
            var afterInfo = tc.AfterInfo;
            var applyPosition = afterInfo.Position;
            var applyRotation = afterInfo.Rotation;
            var applyFOV = afterInfo.FOV;
            mainCam.transform.position = applyPosition;
            mainCam.transform.rotation = applyRotation;
            mainCam.fieldOfView = applyFOV;
        }

        public void ApplyCameraTM(in TCCameraTM tm, int id = -1) {
            TCCameraEntity tc = null;
            if (id == -1) {
                tc = context.CameraRepo.CurrentTCCam;
            } else {
                context.CameraRepo.TryGet(id, out tc);
            }
            if (tc == null) return;

            if (tm.needSet_Follow) tc.FollowComponent.model = TCTM2RuntimeUtil.ToTCFollowModel(tm.followTM);

            if (tm.needSet_LookAt) tc.LookAtComponent.model = TCTM2RuntimeUtil.ToTCLookAtModel(tm.lookAtTM);

            if (tm.needSet_Misc) tc.MISCComponent.model = TCTM2RuntimeUtil.ToTCMiscModel(tm.miscTM);

            if (tm.needSet_Movement) tc.MovementStateCom.Enter(TCTM2RuntimeUtil.ToTCMovementStateModelArray(tm.movementStateTMArray), tm.isExitReset_Movement, tm.exitEasing_Movement, tm.exitDuration_Movement);

            if (tm.needSet_Round) tc.RoundStateCom.Enter(TCTM2RuntimeUtil.ToTCRoundStateModelArray(tm.roundStateTMArray), tm.isExitReset_Round, tm.exitEasing_Round, tm.exitDuration_Round);

            if (tm.needSet_Rotate) tc.RotateStateComponent.Enter(TCTM2RuntimeUtil.ToTCRotateStateModelArray(tm.rotateStateTMArray), tm.isExitReset_Rotate, tm.exitEasing_Rotate, tm.exitDuration_Rotate);

            if (tm.needSet_Push) tc.PushStateCom.Enter(TCTM2RuntimeUtil.ToTCPushStateModelArray(tm.pushStateTMArray), tm.isExitReset_Push, tm.exitEasing_Push, tm.exitDuration_Push);

            if (tm.needSet_FOV) tc.FOVStateComponent.Enter(TCTM2RuntimeUtil.ToTCFOVStateModelArray(tm.fovStateTMArray), tm.isExitReset_FOV, tm.exitEasing_FOV, tm.exitDuration_FOV);

            if (tm.needSet_DollyTrack) tc.DollyTrackStateComponent.Enter(TCTM2RuntimeUtil.ToTCDollyTrackStateModel(tm.dollyTrackStateTM));

            if (tm.needSet_Shake) tc.ShakeStateComponent.Enter(TCTM2RuntimeUtil.ToTCShakeStateModelArray(tm.shakeStateTMArray));
        }

    }

}