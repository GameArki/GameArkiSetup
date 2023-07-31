using UnityEngine;
using GameArki.TripodCamera.Entities;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Hook {

    public class TCCameraHook : MonoBehaviour {

        TCCameraEntity entity;

        [Header("Transform")]
        [SerializeField] Vector3 position;
        [SerializeField] Quaternion rotation;

        [Header("FOV")]
        [SerializeField] float fov;

        [Header("Follow")]
        [SerializeField] Vector3 followOffset;
        [SerializeField] EasingType followXEasing;
        [SerializeField] float followXDuration;
        [SerializeField] EasingType followYEasing;
        [SerializeField] float followYDuration;

        [SerializeField] bool enableChange;

        public void Ctor(TCCameraEntity entity) {

            this.entity = entity;

        }

        public void Tick() {
            if (enableChange) {
                ApplyToCam();
            } else {
                RecordFromCam();
            }
        }

        void RecordFromCam() {
            position = entity.BeforeInfo.Position;
            rotation = entity.BeforeInfo.Rotation;

            fov = entity.BeforeInfo.FOV;

            var followCom = entity.FollowComponent;
            var followModel = followCom.model;
            followOffset = followModel.normalFollowOffset;

            followXEasing = followModel.easingType_horizontal;
            followXDuration = followModel.duration_horizontal;

            followYEasing = followModel.easingType_vertical;
            followYDuration = followModel.duration_vertical;
        }

        void ApplyToCam() {

            entity.BeforeInfo.SetPosition(position);
            entity.BeforeInfo.SetRotation(rotation);
            entity.BeforeInfo.SetFOVByClamp(fov);
            
            entity.Follow_ChangeOffset(followOffset);
            entity.Follow_ChangeXEasing(followXEasing, followXDuration);
            entity.Follow_ChangeYEasing(followYEasing, followYDuration);
        }

    }

}