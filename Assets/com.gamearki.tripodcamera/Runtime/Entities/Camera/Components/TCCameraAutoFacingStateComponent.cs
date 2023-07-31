using System;
using UnityEngine;
using static UnityEngine.Debug;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Entities {

    public class TCCameraAutoFacingStateComponent {

        Vector3 roleForwad;
        Vector3 camForward;
        float xzDis;

        EasingType easingType;
        float duration;

        float targetRad;
        float curRad;
        float curTime;
        Vector3 startAxis;
        Vector3 endAxis;
        Vector3 restoredOffset;
        float sameForwardTime;

        float minAngleDiff;         // 触发转向的 角色相机 的最小面向角度差
        float sameForwardBreakTime;    // 触发转向的 角色重复操作 的最小时间

        bool isEnabled;
        public bool IsEnabled => isEnabled;

        public TCCameraAutoFacingStateComponent() { }

        public void EnterAutoFacing(in Vector3 roleForward, in Vector3 camForward,
        float xzDis, EasingType easingType, float duration,
        float minAngleDiff, float sameForwardBreakTime) {
            this.easingType = easingType;
            this.duration = duration;
            this.xzDis = xzDis;
            this.startAxis = camForward;
            this.endAxis = roleForward;
            this.minAngleDiff = minAngleDiff;
            this.sameForwardBreakTime = sameForwardBreakTime;

            this.startAxis.y = 0;
            this.startAxis.Normalize();
            this.endAxis.y = 0;
            this.endAxis.Normalize();
            this.curTime = 0;
            this.curRad = 0;
            this.restoredOffset = Vector3.zero;
            this.targetRad = GetTargetRad(startAxis, endAxis);
            isEnabled = true;
        }

        public void QuitAuotFacing() {
            isEnabled = false;
        }

        public void Tick(Vector3 roleForwad, Vector3 camForwad, float dt) {
            if (!isEnabled) {
                return;
            }

            roleForwad.y = 0;
            roleForwad.Normalize();
            camForwad.y = 0;
            camForwad.Normalize();
            Refresh(roleForwad, camForwad, dt);
            if (curTime > duration) {
                return;
            }

            var angle = Vector3.Angle(camForwad, roleForwad);
            if (angle <= minAngleDiff) {
                return;
            }

            curRad = EasingHelper.Ease1D(easingType, curTime, duration, 0, targetRad);
            curTime += dt;
        }

        void Refresh(in Vector3 roleForwad, in Vector3 camForwad, float dt) {
            // 目标朝向已变化
            if (endAxis != roleForwad) {
                restoredOffset += GetCurOffset();
                startAxis = camForwad;
                endAxis = roleForwad;
                targetRad = GetTargetRad(startAxis, endAxis);
                curTime = 0.015f * MathF.Abs(targetRad) / duration;
            }

        }

        float GetTargetRad(in Vector3 axis1, in Vector3 axis2) {
            var cos = Vector3.Dot(axis1, axis2);
            cos = Math.Clamp(cos, -1, 1);
            var cross = Vector3.Cross(axis1, axis2);
            var rad = MathF.Acos(cos);
            rad = cross.y < 0 ? -rad : rad;
            return rad;
        }

        public Vector3 GetOffset() {
            return GetCurOffset() + restoredOffset;
        }

        Vector3 GetCurOffset() {
            if (!isEnabled) {
                return Vector3.zero;
            }
            var startPos = -startAxis * xzDis;
            var curPos = Quaternion.AngleAxis(curRad * Mathf.Rad2Deg, Vector3.up) * startPos;
            var offset = curPos - startPos;
            return offset;
        }

    }

}