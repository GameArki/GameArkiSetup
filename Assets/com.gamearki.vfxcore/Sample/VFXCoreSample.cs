using UnityEngine;
using GameArki.VFX;

public class VFXCoreSample : MonoBehaviour {

    VFXCore vfxCore;
    public GameObject[] vfxAssets;

    float resTime;
    public float intervalTime;

    void Start() {
        vfxCore = new VFXCore();
        vfxCore.Inject(vfxAssets);
        intervalTime = 1f;
    }

    void Update() {
        resTime += Time.deltaTime;
        while (resTime >= intervalTime) {
            resTime -= intervalTime;
            vfxCore.Tick();
        }
    }

    int tickCount;
    int vfxID;
    string vfxName;
    void OnGUI() {

        var api = vfxCore.API;

        GUILayout.BeginHorizontal(GUILayout.Width(200));
        GUILayout.Label("特效列表");
        foreach (var vfxAsset in vfxAssets) {
            if (GUILayout.Button(vfxAsset.name)) {
                vfxName = vfxAsset.name;
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.Label("当前特效:" + vfxName);

        if (GUILayout.Button("播放特效:默认方式")) {
            api.TryStopVFX(vfxID);
            vfxID = api.TryPlayVFX_Default(vfxName, transform);
        }

        if (GUILayout.Button("播放特效:循环方式")) {
            api.TryStopVFX(vfxID);
            vfxID = api.TryPlayVFX(vfxName, true, transform);
        }

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("播放特效:手动Tick方式")) {
            api.TryStopVFX(vfxID);
            vfxID = api.TryPlayVFX_ManualTick(vfxName, tickCount, transform);
        }
        GUILayout.Label("Tick次数:");
        tickCount = int.Parse(GUILayout.TextField(tickCount.ToString() ?? "0", GUILayout.Width(50)));
        GUILayout.EndHorizontal();

        if (GUILayout.Button("停止特效")) {
            api.TryStopVFX(vfxID);
        }

    }

}
