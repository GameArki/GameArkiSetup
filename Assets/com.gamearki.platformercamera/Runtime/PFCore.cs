using UnityEngine;
using GameArki.PlatformerCamera.Facades;
using GameArki.PlatformerCamera.Phases;

namespace GameArki.PlatformerCamera {

    public class PFCore {

        // ==== API ====
        SetterAPI setterAPI;
        public ISetterAPI SetterAPI => setterAPI;

        // ==== Context ====
        AllPFContext ctx;
        AllPFDomain domain;

        // ==== Phases ====
        PFInitPhase initPhase;
        PFStatePhase statePhase;
        PFApplyPhase applyPhase;

        // ==== State ====
        bool isInit;

        public PFCore() {

            this.setterAPI = new SetterAPI();
            this.ctx = new AllPFContext();
            this.domain = new AllPFDomain();

            this.initPhase = new PFInitPhase();
            this.statePhase = new PFStatePhase();
            this.applyPhase = new PFApplyPhase();

            this.domain.Inject(ctx);
            this.initPhase.Inject(ctx, domain);
            this.statePhase.Inject(ctx, domain);
            this.applyPhase.Inject(ctx, domain);

            this.setterAPI.Inject(ctx, domain);

        }

        public void Initialize(Camera main) {
            this.ctx.Inject(main);

            initPhase.Init();

            isInit = true;
        }

        public void Tick(float dt) {
            if (!isInit) {
                return;
            }
            statePhase.Tick(dt);
            applyPhase.Tick(dt);
        }

        public void Editor_DrawGizmos() {
#if UNITY_EDITOR
        float radio = (float)Screen.width / Screen.height;
            var curCam = ctx.Repo.Current;
            if (curCam == null) {
                return;
            }
            var info = curCam.CurrentInfoCom;
            var confiner = curCam.ConfinerCom;
            if (confiner.IsEnable) {
                var viewSize = info.GetViewSize(radio);
                var confinerSize = confiner.GetSize();
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(info.Pos, viewSize);

                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(confiner.GetCenter(), confinerSize);
                
            }
#endif
        }


    }

}