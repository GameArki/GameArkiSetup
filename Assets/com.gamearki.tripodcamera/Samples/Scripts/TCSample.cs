using System.Collections.Generic;
using UnityEngine;
using GameArki.FPEasing;
using GameArki.TripodCamera.Entities;
using GameArki.TripodCamera.Template;

namespace GameArki.TripodCamera.Sample {

    public class TCSample : MonoBehaviour {

        TCCore tcCore;

        Vector2 mousePos;

        List<GameObject> targets;
        PrimitiveType[] randomPrimitiveTypes = new PrimitiveType[] {
            PrimitiveType.Cube,
            PrimitiveType.Cylinder,
            PrimitiveType.Sphere,
            PrimitiveType.Capsule,
        };

        // ==== Input ====
        float sensitivity = 0.05f;

        // - Shake State
        float shakeAmplitudeX = 0.1f;
        float shakeAmplitudeY = 0.1f;
        float shakeFrequency = 10;
        float shakeDuration = 1;
        int shakeEasingType = (int)EasingType.Linear;

        // - Move State
        Vector2 moveOffset = new Vector2(0.1f, 0.1f);
        float moveDuration = 1;
        int moveEasingType = (int)EasingType.Linear;
        bool isInherit_move;
        bool isExitReset_move;

        // - Rotate State
        Vector2 rotOffset = new Vector2(0.1f, 0.1f);
        float rotDuration = 1;
        int rotEasingType = (int)EasingType.Linear;
        bool isInherit_rotate;
        bool isExitReset_rotate;

        // - Push State
        float pushOffset = 1;
        float pushDuration = 1;
        int pushEasingType = (int)EasingType.Linear;
        bool isInherit_push;
        bool isExitReset_push;

        // - FOV State
        float fovOffset = 1;
        float fovDuration = 1;
        int fovEasingType = (int)EasingType.Linear;
        bool isInherit_fov;
        bool isExitReset_fov;

        // - Round State
        Vector2 roundOffset = Vector2.right;
        float roundDuration = 1;
        int roundEasingType = (int)EasingType.Linear;
        bool isInherit_round;
        bool isExitReset_round;

        // - Auto Facing State(Only In NoLockMode)
        bool isAutoFacing;
        float autoFacingduration = 1f;
        float minAngleDiff = 0;
        float sameForwardBreakTime = 0;

        bool showMenu;
        List<int> camIDs;

        void Awake() {

            tcCore = new TCCore();
            tcCore.Initialize(Camera.main);
            var setter = tcCore.SetterAPI;

            camIDs = new List<int>();
            camIDs.Add(setter.SpawnTCCamera());
            camIDs.Add(setter.SpawnTCCamera());
            camIDs.ForEach(id => setter.SetTCCameraActive(true, id));
            setter.CutToTCCamera(camIDs[0]);

            this.targets = new List<GameObject>();
            targets.Add(GameObject.Find("默认跟随目标"));

        }

        void Update() {

            var setter = tcCore.SetterAPI;
            var getter = tcCore.GetterAPI;

            var mouseScroll = Input.mouseScrollDelta.y;
            if (Input.GetKey(KeyCode.LeftControl)) {
                if (mouseScroll != 0) {
                    setter.Zoom_In(mouseScroll, -1);
                }
            } else if (Input.GetKey(KeyCode.LeftShift)) {
                setter.Push_In(mouseScroll, -1);
            } else if (Input.GetKey(KeyCode.LeftAlt)) {
                if (mouseScroll != 0) {
                    setter.Rotate_Roll(mouseScroll, -1);
                }
            }

            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            if (x != 0 || y != 0) {
                if (Input.GetKey(KeyCode.LeftControl)) {
                    setter.Move_AndChangeLookAtOffset(new Vector2(x, y), -1);
                } else {
                    // tcSetter.Move(new Vector2(x, y));
                }
            }

            if (Input.GetMouseButton(1)) {
                var mouseDelta = (Vector2)Input.mousePosition - mousePos;
                if (mouseDelta != Vector2.zero) {
                    setter.Rotate_Horizontal(mouseDelta.x, -1);
                    setter.Rotate_Vertical(mouseDelta.y, -1);
                }
            }
            mousePos = Input.mousePosition;
        }

        void LateUpdate() {
            float dt = Time.deltaTime;
            tcCore.Tick(dt);
        }

        const int MENU_PADDING_TOP = 5;
        void OnGUI() {
            showMenu = GUILayout.Toggle(showMenu, "显示菜单");
            if (!showMenu) {
                return;
            }

            tcCore.TickGUI();

            var setter = tcCore.SetterAPI;
            var getter = tcCore.GetterAPI;

            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical("box");
            GUI_CameraFollow();
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box");
            GUI_CameraLook();
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box");
            GUI_ConfigApply();
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box");
            GUI_Basic();
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box");
            GUI_RandomObject();
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box");
            GUI_CameraLook_Composer();
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box");
            GUI_CameraShake();
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box");
            GUI_CameraMove();
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box");
            GUI_CameraRotate();
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box");
            GUI_CameraPush();
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box");
            GUI_CameraRound();
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box");
            GUI_CameraFOV();
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box");
            GUI_CameraAutoFacing();
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box");
            GUI_CameraMISC();
            GUILayout.EndVertical();

            GUILayout.EndHorizontal();
        }

        void OnDrawGizmos() {
            tcCore?.TickDrawGizmos();
        }

        GameObject followTarget;
        float follow_duration_horizontal;
        float follow_duration_vertical;
        Vector3 followOffset;
        float follow_blendToNormalDamping;
        TCFollowType followType;
        float normalLookAngles;
        bool normalLookActivated;
        float normalLookAngles_composer;
        bool normalLookActivated_composer;
        bool openGUI_CameraFollow = false;
        void GUI_CameraFollow() {
            openGUI_CameraFollow = GUILayout.Toggle(openGUI_CameraFollow, "相机跟随");
            if (!openGUI_CameraFollow) {
                return;
            }

            var setter = tcCore.SetterAPI;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("跟随 默认跟随目标")) {
                followTarget = GameObject.Find("默认跟随目标");
                setter.Follow_SetInit(followTarget.transform, followOffset,
                EasingType.Linear, follow_duration_horizontal,
                EasingType.Linear, follow_duration_vertical, -1);
            }

            if (GUILayout.Button("跟随 下一个")) {
                if (followTarget == null) {
                    followTarget = targets[0];
                } else {
                    var index = targets.IndexOf(followTarget);
                    if (index == targets.Count - 1) {
                        followTarget = targets[0];
                    } else {
                        followTarget = targets[index + 1];
                    }
                }
                setter.Follow_SetInit(followTarget.transform, followOffset,
                EasingType.Linear, follow_duration_horizontal,
                EasingType.Linear, follow_duration_vertical, -1);
            }

            if (GUILayout.Button("取消跟随")) {
                setter.Follow_ChangeTarget(null, -1);
            }
            GUILayout.EndHorizontal();

            normalLookActivated = GUILayout.Toggle(normalLookActivated, "正常视角");
            setter.LookAt_SetNormalLookActivated(normalLookActivated, -1);

            GUILayout.Label($"正常视角:{normalLookAngles.ToString("F2")}");
            normalLookAngles = GUILayout.HorizontalSlider(normalLookAngles, -60, 60, GUILayout.Width(100));
            if (GUILayout.Button("设置正常视角")) {
                setter.LookAt_SetNormalAngles(new Vector3(normalLookAngles, 0, 0), -1);
            }

            normalLookActivated_composer = GUILayout.Toggle(normalLookActivated_composer, "正常视角[Composer]");
            setter.LookAt_SetComposerNormalLookActivated(normalLookActivated_composer, -1);

            GUILayout.Label($"正常视角[Composer]:{normalLookAngles_composer.ToString("F2")}");
            normalLookAngles_composer = GUILayout.HorizontalSlider(normalLookAngles_composer, -60, 60, GUILayout.Width(100));
            if (GUILayout.Button("设置正常视角[Composer]")) {
                setter.LookAt_SetComposerNormalAngles(new Vector3(normalLookAngles_composer, 0, 0), -1);
            }
        }

        GameObject lookAtTarget;
        GameObject lockOnTarget;
        float lookAt_duration_horizontal;
        float lookAt_duration_vertical;
        float lookAt_blendToNormalDamping;
        bool openGUI_CameraLook = false;
        void GUI_CameraLook() {
            openGUI_CameraLook = GUILayout.Toggle(openGUI_CameraLook, "相机看向");
            if (!openGUI_CameraLook) {
                return;
            }

            var setter = tcCore.SetterAPI;

            // GUILayout.Space(MENU_PADDING_TOP);

            // GUILayout.Label($"看向过渡时间_水平方向:{lookAt_duration_horizontal.ToString("F2")}");
            // lookAt_duration_horizontal = GUILayout.HorizontalSlider(lookAt_duration_horizontal, 0, 2, GUILayout.Width(100));

            // GUILayout.Label($"看向过渡时间_垂直方向:{lookAt_duration_vertical.ToString("F2")}");
            // lookAt_duration_vertical = GUILayout.HorizontalSlider(lookAt_duration_vertical, 0, 2, GUILayout.Width(100));

            // GUILayout.Label($"Composer: 正常看向阻尼: {lookAt_blendToNormalDamping.ToString("F2")}");
            // lookAt_blendToNormalDamping = GUILayout.HorizontalSlider(lookAt_blendToNormalDamping, 0, 3, GUILayout.Width(100));
            // lookAt_blendToNormalDamping = lookAt_blendToNormalDamping == 0 ? 2f : lookAt_blendToNormalDamping;
            // setter.LookAt_SetComposerNormalDamping(lookAt_blendToNormalDamping);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("看向 默认看向目标")) {
                setter.LookAt_SetInit(GameObject.Find("默认看向目标").transform,
                                                 Vector3.zero,
                                                 EasingType.Linear,
                                                 lookAt_duration_horizontal,
                                                 EasingType.Linear,
                                                 lookAt_duration_vertical, -1);
            }

            if (GUILayout.Button("看向下一个")) {
                if (lookAtTarget == null) {
                    lookAtTarget = targets[0];
                } else {
                    var index = targets.IndexOf(lookAtTarget);
                    if (index == targets.Count - 1) {
                        lookAtTarget = targets[0];
                    } else {
                        lookAtTarget = targets[index + 1];
                    }
                }
                setter.LookAt_SetInit(lookAtTarget.transform,
                                                 Vector3.zero,
                                                 EasingType.Linear,
                                                 lookAt_duration_horizontal,
                                                 EasingType.Linear,
                                                 lookAt_duration_vertical, -1);
                setter.LookAt_SetLookAtEnabled(true, -1);
            }
            if (GUILayout.Button("取消看向")) {
                setter.LookAt_SetLookAtEnabled(false, -1);
            }
            GUILayout.EndHorizontal();
        }

        Vector2 deadZoneLT;
        Vector2 deadZoneRB;
        bool isDrawingDeadZone;
        TCLookAtComposerType composerType;
        bool openGUI_CameraLook_Composer = false;
        void GUI_CameraLook_Composer() {
            openGUI_CameraLook_Composer = GUILayout.Toggle(openGUI_CameraLook_Composer, "相机看向[Composer]");
            if (!openGUI_CameraLook_Composer) {
                return;
            }

            var setter = tcCore.SetterAPI;
            var getter = tcCore.GetterAPI;
            var screenWidth = Screen.width;
            var screenHeight = Screen.height;

            GUILayout.Label($"相机死区 左上角: {deadZoneLT}");
            GUILayout.TextField(deadZoneLT.x.ToString());
            GUILayout.TextField(deadZoneLT.y.ToString());

            GUILayout.Label($"相机死区 右上角: {deadZoneRB}");
            GUILayout.TextField(deadZoneRB.x.ToString());
            GUILayout.TextField(deadZoneRB.y.ToString());

            if (isDrawingDeadZone) {
                if (Input.GetKeyDown(KeyCode.Mouse0)) {
                    deadZoneLT = Input.mousePosition;
                    setter.LookAt_SetComposerType(TCLookAtComposerType.None, -1);
                } else if (Input.GetKey(KeyCode.Mouse0)) {
                    deadZoneRB = Input.mousePosition;
                } else if (Input.GetKeyUp(KeyCode.Mouse0)) {
                    isDrawingDeadZone = false;
                    setter.LookAt_SetComposerType(composerType, -1);
                }
                setter.LookAt_SetComposer(new TCLookAtComposerModel {
                    composerType = composerType,
                    screenNormalizedX = (deadZoneLT.x + deadZoneRB.x) / 2f / screenWidth,
                    screenNormalizedY = (deadZoneLT.y + deadZoneRB.y) / 2f / screenHeight,
                    deadZoneNormalizedW = (deadZoneRB.x - deadZoneLT.x) / screenWidth,
                    deadZoneNormalizedH = (deadZoneLT.y - deadZoneRB.y) / screenHeight,
                    softZoneNormalizedW = 0,
                    softZoneNormalizedH = 0,
                }, -1);
            } else {
                composerType = getter.LookAt_GetComposerType();
            }

            if (GUILayout.Button(isDrawingDeadZone ? "绘制 相机死区 中......" : "绘制  相机死区 ", GUILayout.Width(150), GUILayout.Height(30))) {
                isDrawingDeadZone = !isDrawingDeadZone;
            }

            if (GUILayout.Button(composerType == TCLookAtComposerType.None
                                ? $"相机死区 - [{TCLookAtComposerType.None}]"
                                : $"相机死区 - [{TCLookAtComposerType.LookAtTarget}]", GUILayout.Width(200), GUILayout.Height(30))) {
                var nextComposerType = composerType == TCLookAtComposerType.None ?
                                                        TCLookAtComposerType.LookAtTarget : TCLookAtComposerType.None;
                setter.LookAt_SetComposerType(nextComposerType, -1);
                Debug.Log($"相机看向 Composer Type -> {nextComposerType}");
            }

        }

        public TCCameraSO[] soArray;
        bool openGUI_ConfigApply = false;
        void GUI_ConfigApply() {
            openGUI_ConfigApply = GUILayout.Toggle(openGUI_ConfigApply, "配置应用");
            if (!openGUI_ConfigApply) return;

            var setter = tcCore.SetterAPI;
            GUILayout.Label("配置应用");
            var len = soArray.Length;
            for (int i = 0; i < len; i++) {
                var so = soArray[i];
                var tm = so.tm;
                GUILayout.BeginHorizontal();
                if (GUILayout.Button($"应用相机配置[ID:{so.name}]")) {
                    setter.ApplyCameraTM(tm, -1);
                }
                GUILayout.EndHorizontal();
            }
        }

        int curTCCamIndex = 0;
        EasingType blendEasingType;
        float blendDuration = 1f;
        bool openGUI_Basic = false;
        void GUI_Basic() {
            openGUI_Basic = GUILayout.Toggle(openGUI_Basic, "基础操作");
            if (!openGUI_Basic) return;

            var setter = tcCore.SetterAPI;

            GUILayout.Label($"当前相机: {camIDs[curTCCamIndex]}");
            GUILayout.Label($"相机过渡参数 缓动函数: {blendEasingType}");
            blendEasingType = (EasingType)GUILayout.SelectionGrid((int)blendEasingType, new string[] {
                EasingType.Immediate.ToString(),
                EasingType.Linear.ToString(),
                EasingType.InQuad.ToString(),
            }, 3);
            GUILayout.Label($"相机过渡参数 缓动时间: {blendDuration}");
            blendDuration = GUILayout.HorizontalSlider(blendDuration, 0, 5, GUILayout.Width(100));

            if (GUILayout.Button("切换至下一相机[Cut]")) {
                curTCCamIndex++;
                curTCCamIndex = curTCCamIndex % camIDs.Count;
                setter.CutToTCCamera(curTCCamIndex);
            }

            if (GUILayout.Button("切换至下一相机[Blend]")) {
                curTCCamIndex++;
                curTCCamIndex = curTCCamIndex % camIDs.Count;
                setter.BlendToTCCamera(blendEasingType, blendDuration, curTCCamIndex);
            }

            GUILayout.Label("灵敏度: ");
            sensitivity = GUILayout.HorizontalSlider(sensitivity, 0, 1, GUILayout.Width(100));
            GUILayout.Label(" " + sensitivity.ToString("F2"));

            GUILayout.Space(MENU_PADDING_TOP);

            if (GUILayout.RepeatButton("推")) {
                setter.Push_In(sensitivity, -1);
            }
            if (GUILayout.RepeatButton("拉")) {
                setter.Push_In(-sensitivity, -1);
            }

            GUILayout.Space(MENU_PADDING_TOP);

            if (GUILayout.RepeatButton("左看")) {
                setter.Rotate_Horizontal(-sensitivity, -1);
            }
            if (GUILayout.RepeatButton("右看")) {
                setter.Rotate_Horizontal(sensitivity, -1);
            }
            if (GUILayout.RepeatButton("上看")) {
                setter.Rotate_Vertical(sensitivity, -1);
            }
            if (GUILayout.RepeatButton("下看")) {
                setter.Rotate_Vertical(-sensitivity, -1);
            }

            GUILayout.Space(MENU_PADDING_TOP);

            if (GUILayout.RepeatButton("左转")) {
                setter.Rotate_Roll(-sensitivity, -1);
            }
            if (GUILayout.RepeatButton("右转")) {
                setter.Rotate_Roll(sensitivity, -1);
            }

            GUILayout.Space(MENU_PADDING_TOP);

            if (GUILayout.RepeatButton("左移")) {
                setter.Move(new Vector2(-sensitivity, 0), -1);
            }
            if (GUILayout.RepeatButton("右移")) {
                setter.Move(new Vector2(sensitivity, 0), -1);
            }
            if (GUILayout.RepeatButton("上移")) {
                setter.Move(new Vector2(0, sensitivity), -1);
            }
            if (GUILayout.RepeatButton("下移")) {
                setter.Move(new Vector2(0, -sensitivity), -1);
            }

            GUILayout.Space(MENU_PADDING_TOP);

            if (GUILayout.RepeatButton("放大")) {
                setter.Zoom_In(sensitivity, -1);
            }
            if (GUILayout.RepeatButton("缩小")) {
                setter.Zoom_In(-sensitivity, -1);
            }
        }

        bool openGUI_RandomObject = false;
        void GUI_RandomObject() {
            openGUI_RandomObject = GUILayout.Toggle(openGUI_RandomObject, "随机操作");
            if (!openGUI_RandomObject) return;

            GUILayout.Space(MENU_PADDING_TOP);
            if (GUILayout.Button("生成随机物体")) {
                var go = GameObject.CreatePrimitive(randomPrimitiveTypes[Random.Range(0, randomPrimitiveTypes.Length)]);
                go.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
                targets.Add(go);
            }
            if (GUILayout.Button("清空随机物体")) {
                foreach (var go in targets) {
                    Destroy(go);
                }
                targets.Clear();
            }
        }

        bool openGUI_CameraShake = false;
        void GUI_CameraShake() {
            openGUI_CameraShake = GUILayout.Toggle(openGUI_CameraShake, "震屏");
            if (!openGUI_CameraShake) return;

            var setter = tcCore.SetterAPI;

            GUILayout.Label("震幅 x: ");
            shakeAmplitudeX = GUILayout.HorizontalSlider(shakeAmplitudeX, 0, 10, GUILayout.Width(100));
            GUILayout.Label(" " + shakeAmplitudeX.ToString("F2"));

            GUILayout.Label("震幅 y: ");
            shakeAmplitudeY = GUILayout.HorizontalSlider(shakeAmplitudeY, 0, 10, GUILayout.Width(100));
            GUILayout.Label(" " + shakeAmplitudeY.ToString("F2"));

            GUILayout.Label("震频: ");
            shakeFrequency = GUILayout.HorizontalSlider(shakeFrequency, 0, 100, GUILayout.Width(100));
            GUILayout.Label(" " + shakeFrequency.ToString("F2"));

            GUILayout.Label("震时: ");
            shakeDuration = GUILayout.HorizontalSlider(shakeDuration, 0, 3, GUILayout.Width(100));
            GUILayout.Label(" " + shakeDuration.ToString("F2"));

            GUILayout.Label("震式: ");
            shakeEasingType = (int)GUILayout.HorizontalSlider((int)shakeEasingType, 0, (int)EasingType.InOutBounce, GUILayout.Width(100));
            GUILayout.Label(" " + ((EasingType)shakeEasingType).ToString(), GUILayout.Width(100));

            if (GUILayout.Button("进入震动状态")) {
                var arg = new TCShakeStateModel() {
                    amplitudeOffset = new Vector2(shakeAmplitudeX, shakeAmplitudeY),
                    easingType = (EasingType)shakeEasingType,
                    frequency = shakeFrequency,
                    duration = shakeDuration
                };
                setter.Enter_Shake(new TCShakeStateModel[] { arg }, -1);
            }
        }

        bool openGUI_CameraMove = false;
        void GUI_CameraMove() {
            openGUI_CameraMove = GUILayout.Toggle(openGUI_CameraMove, "移动");
            if (!openGUI_CameraMove) return;

            var setter = tcCore.SetterAPI;
            GUILayout.Label("移动");
            GUILayout.Label("x: ");
            moveOffset.x = GUILayout.HorizontalSlider(moveOffset.x, -100, 100, GUILayout.Width(100));
            GUILayout.Label(" " + moveOffset.x.ToString("F2"));

            GUILayout.Label("y: ");
            moveOffset.y = GUILayout.HorizontalSlider(moveOffset.y, -100, 100, GUILayout.Width(10));
            GUILayout.Label(" " + moveOffset.y.ToString("F2"));

            GUILayout.Label("持续: ");
            moveDuration = GUILayout.HorizontalSlider(moveDuration, 0, 3, GUILayout.Width(100));
            GUILayout.Label(" " + moveDuration.ToString("F2"));

            GUILayout.Label("easing: ");
            moveEasingType = (int)GUILayout.HorizontalSlider((int)moveEasingType, 0, (int)EasingType.InOutBounce, GUILayout.Width(100));
            GUILayout.Label(" " + ((EasingType)moveEasingType).ToString(), GUILayout.Width(100));
            isInherit_move = GUILayout.Toggle(isInherit_move, "isInherit");
            isExitReset_move = GUILayout.Toggle(isExitReset_move, "isExitReset");

            if (GUILayout.Button("进入移动状态")) {
                var arg = new TCMovementStateModel() {
                    offset = moveOffset,
                    easingType = (EasingType)moveEasingType,
                    duration = moveDuration,
                    isInherit = isInherit_move
                };
                setter.Enter_Movement(new TCMovementStateModel[] { arg }, isExitReset_move, EasingType.Linear, 0.5f, -1);
            }
        }

        bool openGUI_CameraRotate = false;
        void GUI_CameraRotate() {
            openGUI_CameraRotate = GUILayout.Toggle(openGUI_CameraRotate, "旋转");
            if (!openGUI_CameraRotate) return;

            var setter = tcCore.SetterAPI;
            GUILayout.Label("x: ");
            rotOffset.x = GUILayout.HorizontalSlider(rotOffset.x, -90, 90, GUILayout.Width(100));
            GUILayout.Label(" " + rotOffset.x.ToString("F2"));

            GUILayout.Label("y: ");
            rotOffset.y = GUILayout.HorizontalSlider(rotOffset.y, -90, 90, GUILayout.Width(100));
            GUILayout.Label(" " + rotOffset.y.ToString("F2"));

            GUILayout.Label("持续: ");
            rotDuration = GUILayout.HorizontalSlider(rotDuration, 0, 3, GUILayout.Width(100));
            GUILayout.Label(" " + rotDuration.ToString("F2"));
            isInherit_rotate = GUILayout.Toggle(isInherit_rotate, "isInherit");
            isExitReset_rotate = GUILayout.Toggle(isExitReset_rotate, "isExitReset");

            if (GUILayout.Button("进入旋转状态")) {
                var arg = new TCRotateStateModel() {
                    offset = rotOffset,
                    easingType = (EasingType)rotEasingType,
                    duration = rotDuration,
                    isInherit = isInherit_rotate
                };
                setter.Enter_Rotation(new TCRotateStateModel[] { arg }, isExitReset_rotate, EasingType.Linear, 0.5f, -1);
            }
        }

        bool openGUI_CameraPush = false;
        void GUI_CameraPush() {
            openGUI_CameraPush = GUILayout.Toggle(openGUI_CameraPush, "推进");
            if (!openGUI_CameraPush) return;

            var setter = tcCore.SetterAPI;
            GUILayout.Label("推进");
            GUILayout.Label("pushOffset: ");
            pushOffset = GUILayout.HorizontalSlider(pushOffset, -100, 100, GUILayout.Width(100));
            GUILayout.Label(" " + pushOffset.ToString("F2"));

            GUILayout.Label("持续: ");
            pushDuration = GUILayout.HorizontalSlider(pushDuration, 0, 10, GUILayout.Width(100));
            GUILayout.Label(" " + pushDuration.ToString("F2"));
            isInherit_push = GUILayout.Toggle(isInherit_push, "isInherit");
            isExitReset_push = GUILayout.Toggle(isExitReset_push, "isExitReset");

            if (GUILayout.Button("进入推进状态")) {
                var arg = new TCPushStateModel() {
                    offset = pushOffset,
                    easingType = (EasingType)pushEasingType,
                    duration = pushDuration,
                    isInherit = isInherit_push
                };
                setter.Enter_Push(new TCPushStateModel[] { arg }, isExitReset_push, EasingType.Linear, 0.5f, -1);
            }
        }

        bool openGUI_CameraRound = false;
        void GUI_CameraRound() {
            openGUI_CameraRound = GUILayout.Toggle(openGUI_CameraRound, "绕柱");
            if (!openGUI_CameraRound) return;

            var setter = tcCore.SetterAPI;
            GUILayout.Label("绕柱");
            GUILayout.Label("roundOffset: ");
            roundOffset.x = GUILayout.HorizontalSlider(roundOffset.x, -360, 360, GUILayout.Width(100));
            GUILayout.Label(" " + roundOffset.x.ToString("F2"));
            roundOffset.y = GUILayout.HorizontalSlider(roundOffset.y, -360, 360, GUILayout.Width(100));
            GUILayout.Label(" " + roundOffset.y.ToString("F2"));

            GUILayout.Label("持续: ");
            roundDuration = GUILayout.HorizontalSlider(roundDuration, 0, 10, GUILayout.Width(100));
            GUILayout.Label(" " + roundDuration.ToString("F2"));
            isInherit_round = GUILayout.Toggle(isInherit_round, "isInherit");
            isExitReset_round = GUILayout.Toggle(isExitReset_round, "isExitReset");

            if (GUILayout.Button("进入绕柱状态")) {
                TCRoundStateModel arg;
                arg.offset = roundOffset;
                arg.easingType = (EasingType)roundEasingType;
                arg.duration = roundDuration;
                arg.isInherit = isInherit_round;

                setter.Enter_Round(new TCRoundStateModel[] { arg }, isExitReset_round, EasingType.Linear, 0.5f, -1);
            }
        }

        bool openGUI_CameraFOV = false;
        void GUI_CameraFOV() {
            openGUI_CameraFOV = GUILayout.Toggle(openGUI_CameraFOV, "FOV");
            if (!openGUI_CameraFOV) return;

            var setter = tcCore.SetterAPI;
            GUILayout.Label("FOV变换");
            GUILayout.Label("fovOffset: ");
            fovOffset = GUILayout.HorizontalSlider(fovOffset, -100, 100, GUILayout.Width(100));
            GUILayout.Label(" " + fovOffset.ToString("F2"));

            GUILayout.Label("持续: ");
            fovDuration = GUILayout.HorizontalSlider(fovDuration, 0, 10, GUILayout.Width(100));
            GUILayout.Label(" " + fovDuration.ToString("F2"));
            isInherit_fov = GUILayout.Toggle(isInherit_fov, "isInherit");
            isExitReset_fov = GUILayout.Toggle(isExitReset_fov, "isExitReset");

            if (GUILayout.Button("进入FOV状态")) {
                var arg = new TCFOVStateModel() {
                    offset = fovOffset,
                    easingType = (EasingType)fovEasingType,
                    duration = fovDuration,
                    isInherit = isInherit_fov
                };
                setter.Enter_FOV(new TCFOVStateModel[] { arg }, isExitReset_fov, EasingType.Linear, 0.5f, -1);
            }
        }

        bool openGUI_CameraAutoFacing = false;
        void GUI_CameraAutoFacing() {
            openGUI_CameraAutoFacing = GUILayout.Toggle(openGUI_CameraAutoFacing, "自动朝向");
            if (!openGUI_CameraAutoFacing) return;

            var setter = tcCore.SetterAPI;
            GUILayout.Label($"自动转向 过渡时间: {autoFacingduration}");
            autoFacingduration = GUILayout.HorizontalSlider(autoFacingduration, 0, 3, GUILayout.Width(100));

            // minAngleDiff = GUILayout.HorizontalSlider(minAngleDiff, 0, 90, GUILayout.Width(100));
            // sameForwardBreakTime = GUILayout.HorizontalSlider(sameForwardBreakTime, 0, 3, GUILayout.Width(100));

            if (GUILayout.Button($"开/关 自动转向:{isAutoFacing} ", GUILayout.Width(150), GUILayout.Height(30))) {
                isAutoFacing = !isAutoFacing;
                if (isAutoFacing) {
                    setter.SetAutoFacing(EasingType.Linear, autoFacingduration, minAngleDiff, sameForwardBreakTime, -1);
                } else {
                    setter.QuitAutoFacing();
                }
            }
        }

        float maxMoveSpeed = 0;
        bool maxMoveSpeedLimitActivated = false;
        float maxLookDownDegree = 0;
        float maxLookUpDegree = 0;
        bool lookLimitActivated = false;
        bool openGUI_CameraMISC = false;
        void GUI_CameraMISC() {
            openGUI_CameraMISC = GUILayout.Toggle(openGUI_CameraMISC, "杂项设置");
            if (!openGUI_CameraMISC) return;

            var setter = tcCore.SetterAPI;
            var getter = tcCore.GetterAPI;
            maxMoveSpeedLimitActivated = GUILayout.Toggle(maxMoveSpeedLimitActivated, "最大相机移动速度限制");
            setter.MISC_SetMaxMoveSpeedLimitActivated(maxMoveSpeedLimitActivated, -1);
            maxMoveSpeed = GUILayout.HorizontalSlider(maxMoveSpeed, 0, 10, GUILayout.Width(100));
            setter.MISC_SetMaxMoveSpeed(maxMoveSpeed, -1);

            lookLimitActivated = GUILayout.Toggle(lookLimitActivated, "相机俯仰角度限制");
            setter.MISC_SetLookLimitActivated(lookLimitActivated, -1);
            GUILayout.Label($"最大俯视角度: {maxLookDownDegree}");
            maxLookDownDegree = GUILayout.HorizontalSlider(maxLookDownDegree, 0, 90, GUILayout.Width(100));
            setter.MISC_SetMaxLookDownDegree(maxLookDownDegree, -1);
            GUILayout.Label($"最大仰视角度: {maxLookUpDegree}");
            maxLookUpDegree = GUILayout.HorizontalSlider(maxLookUpDegree, 0, 90, GUILayout.Width(100));
            setter.MISC_SetMaxLookUpDegree(maxLookUpDegree, -1);

        }

    }

}