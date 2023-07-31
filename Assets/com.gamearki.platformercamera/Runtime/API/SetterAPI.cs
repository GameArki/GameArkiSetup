using GameArki.FPEasing;
using GameArki.PlatformerCamera.Entities;
using GameArki.PlatformerCamera.Facades;
using UnityEngine;

namespace GameArki.PlatformerCamera {

    internal class SetterAPI : ISetterAPI {

        AllPFContext ctx;
        AllPFDomain allDomain;

        internal SetterAPI() {}

        internal void Inject(AllPFContext ctx, AllPFDomain domain) {
            this.ctx = ctx;
            this.allDomain = domain;
        }

        // ==== Spawn ====
        PFCameraEntity ISetterAPI.Spawn(int id, Vector3 pos, float heightHalfSize) {
            var domain = allDomain.CameraDomain;
            return domain.Spawn(id, pos, heightHalfSize);
        }

        PFCameraEntity ISetterAPI.SpawnByMain(int id) {
            var domain = allDomain.CameraDomain;
            return domain.SpawnByMain(id);
        }

        // ==== Base Control ====
        void ISetterAPI.Move_Current(Vector3 offset) {
            var cur = ctx.Repo.Current;
            var domain = allDomain.CameraDomain;
            domain.Move_Current(cur, offset);
        }

        // ==== Follow ====
        void ISetterAPI.Follow_Current(Transform target, Vector3 offset, EasingType easingXType, float xDuration, EasingType easingYType, float yDuration) {
            var cur = ctx.Repo.Current;
            var domain = allDomain.CameraDomain;
            domain.Follow_Current(cur, target, offset, easingXType, xDuration, easingYType, yDuration);
        }

        // ==== Confiner ====
        void ISetterAPI.Confiner_Set_Current(bool isEnable, Vector2 min, Vector2 max) {
            var cur = ctx.Repo.Current;
            var domain = allDomain.CameraDomain;
            domain.Confiner_Set_Current(cur, isEnable, min, max);
        }

        // ==== Shake ====
        void ISetterAPI.ShakeOnce_Current(PFShakeStateModel arg) {
            var cur = ctx.Repo.Current;
            var domain = allDomain.CameraDomain;
            domain.ShakeOnce_Current(cur, arg);
        }

        void ISetterAPI.ShakeSeveral_Current(PFShakeStateModel[] args) {
            var cur = ctx.Repo.Current;
            var domain = allDomain.CameraDomain;
            domain.ShakeSeveral_Current(cur, args);
        }

        
    }
    
}