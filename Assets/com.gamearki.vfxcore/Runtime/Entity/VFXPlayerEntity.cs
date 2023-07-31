using UnityEngine;

namespace GameArki.VFX {

    public class VFXPlayerEntity {

        static int autoIncreaseID;
        static object lockObj = new object();

        int vfxID;
        public int VFXID => vfxID;

        ParticleSystem[] psAll;
        ParticleSystem psRoot => psAll[0];

        bool isLoopAtFirst;

        public bool IsRootLoop => psRoot.main.loop;

        public bool IsRootPlaying => psRoot.isPlaying;

        string name;
        public string Name => name;
        public void SetName(string v) => name = v;

        bool isManualTick;
        public bool IsManualTick => isManualTick;
        public void SetManualTick(bool v) => isManualTick = v;

        int tickCount;
        public int TickCount => tickCount;
        public void SetTickCount(int v) => tickCount = v;
        public void ReduceTickCount() => tickCount--;

        GameObject vfxGo;

        public VFXPlayerEntity() {
            lock (lockObj) {
                autoIncreaseID++;
                vfxID = autoIncreaseID;
            }
        }

        public void Init() {
        }

        public void TearDown() {
            GameObject.Destroy(vfxGo);
        }

        public void Reset() {
            vfxGo = null;
            tickCount = 0;
            isManualTick = false;
        }

        public void DefaultPlay() {
            if (psAll != null) {
                var rootMain = psRoot.main;
                rootMain.loop = isLoopAtFirst;
                psRoot.Play();

                var len = psAll.Length; 
                for (int i = 1; i < len; i++) {
                    var ps = psAll[i];
                    var main = ps.main;
                    main.loop = isLoopAtFirst;
                    ps.Play();
                }
            }
            vfxGo.SetActive(true);
        }

        public void Play(bool isLoop) {
            var main = psRoot.main;
            main.loop = isLoop;
            if (psAll != null) {
                foreach (var ps in psAll) {
                    if (ps == null) continue;
                    ps.Play();
                }
            }
            vfxGo.SetActive(true);
        }

        public void StopAll() {
            if (psAll != null) {
                foreach (var ps in psAll) {
                    if (ps == null) continue;
                    ps.Stop();
                }
            }
            vfxGo.SetActive(false);
        }


        public void SetParent(Transform parent) {
            if (parent == null) {
                return;
            }

            vfxGo.transform.SetParent(parent, false);
            vfxGo.transform.rotation = parent.rotation;
        }

        public void SetVFXGo(GameObject vfxGo) {
            UnityEngine.Object.DontDestroyOnLoad(vfxGo);
            this.vfxGo = vfxGo;
            this.psAll = vfxGo.GetComponentsInChildren<ParticleSystem>();
            this.isLoopAtFirst = psRoot.main.loop;
            Debug.Log($"VFXPlayerEntity.SetVFXGo: psAll length = {psAll.Length}");
        }

        public bool IsAllStopped() {
            for (int i = 0; i < psAll.Length; i++) {
                var ps = psAll[i];
                if (ps == null) continue;

                if (ps.isPlaying) {
                    return false;
                }
            }

            return true;
        }

    }

}