using UnityEngine;

namespace GameArki.TripodCamera.Entities {

    public class TCInfoModel {

        Vector3 position;
        public Vector3 Position => position;
        public void SetPosition(Vector3 value) => position = value;

        Quaternion rotation;
        public Quaternion Rotation => rotation;
        public void SetRotation(Quaternion value) => rotation = value;

        // - FOV
        float fov;
        public float FOV => fov;
        public void SetFOVByClamp(float addition) => fov = Mathf.Clamp(fov + addition, fovRange.x, fovRange.y);

        Vector2 fovRange;
        public Vector2 FOVRange => fovRange;
        public void SetFOVRange(Vector2 value) => fovRange = value;

        float aspect;
        public float Aspect => aspect;
        public void SetAspect(float value) => aspect = value;

        float nearClipPlane;
        public float NearClipPlane => nearClipPlane;
        public void SetNearClipPlane(float value) => nearClipPlane = value;

        float farClipPlane;
        public float FarClipPlane => farClipPlane;
        public void SetFarClipPlane(float value) => farClipPlane = value;

        int screenWidth;
        public int ScreenWidth => screenWidth;
        public void SetScreenWidth(int value) => screenWidth = value;

        int screenHeight;
        public int ScreenHeight => screenHeight;
        public void SetScreenHeight(int value) => screenHeight = value;

        public Vector3 GetForward() => rotation * Vector3.forward;

        public void Init(Vector3 pos,
                         Quaternion rot,
                         float fov,
                         float aspect,
                         float nearClipPlane,
                         float farClipPlane,
                         int screenWidth,
                         int screenHeight) {
            this.position = pos;
            this.rotation = rot;
            this.fov = fov;
            this.aspect = aspect;
            this.nearClipPlane = nearClipPlane;
            this.farClipPlane = farClipPlane;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            Debug.Log($"TCCameraInfoComponent Init. pos: {pos}, rot: {rot}, fov: {fov}, aspect: {aspect}, nearClipPlane: {nearClipPlane}, farClipPlane: {farClipPlane}, screenWidth: {screenWidth}, screenHeight: {screenHeight}");
        }

        public void CopyFrom(TCInfoModel other) {
            this.position = other.position;
            this.rotation = other.rotation;
            this.fov = other.fov;
            this.fovRange = other.fovRange;
            this.aspect = other.aspect;
            this.nearClipPlane = other.nearClipPlane;
            this.farClipPlane = other.farClipPlane;
            this.screenWidth = other.screenWidth;
            this.screenHeight = other.screenHeight;
        }
    }

}