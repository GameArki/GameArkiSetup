using UnityEngine;
using GameArki.TripodCamera.Facades;
using GameArki.TripodCamera.Domain;
using GameArki.TripodCamera.Controller;
using GameArki.TripodCamera.API;

namespace GameArki.TripodCamera {

    public class TCCore {

        bool isInit;

        bool isPause;
        public bool IsPause => isPause;

        // ==== API ====
        TCSetterAPI setterAPI;
        public ITCSetterAPI SetterAPI => setterAPI;

        TCGetterAPI getterAPI;
        public ITCGetterAPI GetterAPI => getterAPI;

        // ==== Facades ====
        TCContext context;
        TCDomain domain;

        // ==== Controller ====
        TCController controller;

        public TCCore() {

            this.isInit = false;
            this.isPause = false;

            this.setterAPI = new TCSetterAPI();
            this.getterAPI = new TCGetterAPI();

            this.context = new TCContext();
            this.domain = new TCDomain();

            this.controller = new TCController();

        }

        public void Initialize(Camera mainCamera) {

            // ==== Inject ====
            // - API
            setterAPI.Inject(context, domain);
            getterAPI.Inject(context, domain);

            // - Facades
            context.Inject(mainCamera);
            domain.Inject(context);

            // - Controller
            controller.Inject(context, domain);

            // ==== Init ====
            controller.Init();

            isInit = true;

        }

        public void Pause() {
            isPause = true;
        }

        public void Resume() {
            isPause = false;
        }

        /// <summary>
        /// Recommended to call this method in "LateUpdate()" or "end of Update()"
        /// </summary>
        public void Tick(float dt) {
            if (!isInit || isPause) {
                return;
            }

            var activeCam = context.CameraRepo.CurrentTCCam;
            if (activeCam == null) {
                return;
            }

            controller.Tick(dt);
        }

        bool isGUIShowDeadZone;
        public void TickGUI() {
            if (!isInit || isPause) {
                return;
            }

            var curTCCam = context.CameraRepo.CurrentTCCam;
            if (curTCCam == null) {
                return;
            }

            // ======== 绘制 相机死区
            {
                Color color = GUI.color;

                GUI.color = isGUIShowDeadZone ? Color.red : Color.white;
                if (GUILayout.Button(!isGUIShowDeadZone ? "TC-显示死区" : "TC-关闭死区", GUILayout.Width(100), GUILayout.Height(25))) {
                    isGUIShowDeadZone = !isGUIShowDeadZone;
                }

                if (!isGUIShowDeadZone) return;

                var beforeInfo = curTCCam.BeforeInfo;
                var composerModel = curTCCam.LookAtComponent.model.composerModel;
                var screenWidth = beforeInfo.ScreenWidth;
                var screenHeight = beforeInfo.ScreenHeight;
                var deadZoneLT = composerModel.GetDeadZoneLT(screenWidth, screenHeight);
                var deadZoneRB = composerModel.GetDeadZoneRB(screenWidth, screenHeight);
                var lt = new Vector2(deadZoneLT.x, Screen.height - deadZoneLT.y);
                var rb = new Vector2(deadZoneRB.x, Screen.height - deadZoneRB.y);
                var composerType = composerModel.composerType;

                if (composerType != TCLookAtComposerType.None) GUI.color = Color.green;
                else GUI.color = Color.black;

                var text = composerType == TCLookAtComposerType.None ? "无" :
                            composerType == TCLookAtComposerType.LookAtTarget ? "仅看向点" : "未知";
                GUI.Box(new Rect(lt.x, lt.y, rb.x - lt.x, rb.y - lt.y), $"相机死区[{text}]: {deadZoneLT} - {deadZoneRB}");

                GUI.color = color;
            }
        }

        public void TickDrawGizmos() {
            if (!isInit || isPause) {
                return;
            }

            var curTCCam = context.CameraRepo.CurrentTCCam;
            if (curTCCam == null) {
                return;
            }

            // ======== 跟随点 & 聚焦点 绘制
            {
                var targeterModel = curTCCam.TargetorModel;
                var followCom = curTCCam.FollowComponent;
                var lookAtCom = curTCCam.LookAtComponent;
                if (targeterModel.HasFollowTarget) {
                    Color color = Gizmos.color;
                    Gizmos.color = Color.green;
                    Gizmos.DrawSphere(targeterModel.FollowTargetPos, 0.2f);
                    Gizmos.color = color;
                }

                if (targeterModel.HasLookAtTarget) {
                    Color color = Gizmos.color;
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(targeterModel.LookAtTargetPos, 0.2f);
                }
            }
        }

        // ==== Unsafe API ====
        public TCContext GetFacadesThatYouCanVisitEverythingButNotRecommended() {
            return context;
        }

        public TCDomain GetDomainThatYouCanVisitEverythingButNotRecommended() {
            return domain;
        }

    }

}