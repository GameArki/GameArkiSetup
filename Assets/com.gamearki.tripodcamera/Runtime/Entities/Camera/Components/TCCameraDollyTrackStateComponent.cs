using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Entities {

    public class TCCameraDollyTrackStateComponent {

        TCTargetorModel targetorModel;
        public void SetTargetorModel(in TCTargetorModel targetorModel) => this.targetorModel = targetorModel;

        public TCDollyTrackStateModel model;

        float curTime;
        bool isActivated;
        public bool IsActivated => isActivated;

        public TCCameraDollyTrackStateComponent() {}

        public void Enter(TCDollyTrackStateModel model) {
            this.model = model;
            this.curTime = 0;
            this.isActivated = true;
        }

        public void Tick(float dt) {
            if (!isActivated) {
                return;
            }

            curTime += dt;
            if (curTime > model.totalDuraration) {
                curTime = 0;
                isActivated = false;
                return;
            }

        }

        public Vector3 GetDollyPos() {
            if (model.trackType == TCDollyTrackType.Bezier) {
                var totalDuraration = model.totalDuraration;
                var wayPoint = TCDollyTrackRuntimeUtil.GetBezierWayPointLv3(model.bezierSlineModelArray, curTime, out int elementIndex, out float ratioT);
                return targetorModel.HasFollowTarget ?
                wayPoint.position + targetorModel.FollowTargetPos : wayPoint.position;
            }

            Debug.LogWarning("TCDollyTrackType not implemented");
            return Vector3.zero;
        }

    }

}