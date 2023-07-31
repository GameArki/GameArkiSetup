using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FixMath.NET;
using GameArki.FPPhysics2D.API;
using System;
using GameArki.FPPhysics2D;

namespace GameArki.Sample {

    public class Test_Raycast : MonoBehaviour {

        GameObject[] allGo;
        FPRigidbody2DEntity[] allRB;

        FPSpace2D space2D;
        FPRigidbody2DEntity role => allRB[0];
        FPGetterAPI getterAPI;

        public GameObject rayStartGo;
        public GameObject rayEndGo;

        public GameObject segmentStartGo;
        public GameObject segmentEndGo;
        public int goCount = 6;

        RayCastHit2DArgs[] rayHits;
        SegmentCastHit2DArgs[] segmentHits;
        FPVector2[] rayHitPoints;
        FPVector2[] segmentHitPoints;
        FPContactFilter2DArgs filter;

        private void Awake() {
            System.Console.WriteLine("开始");
            rayHitPoints = new FPVector2[goCount];
            segmentHitPoints = new FPVector2[goCount * 2];

            space2D = new FPSpace2D(new FPVector2(0, -981 * FP64.EN2), new FPVector2(1000, 500), 8);
            getterAPI = space2D.GetterAPI;

            allGo = new GameObject[goCount];
            allRB = new FPRigidbody2DEntity[goCount];

            rayHits = new RayCastHit2DArgs[goCount];
            segmentHits = new SegmentCastHit2DArgs[goCount * 2];

            allGo[0] = new GameObject("go0");
            allRB[0] = FPRigidbody2DFactory.CreateBoxRB(new FPVector2(0, 0), 0, new FPVector2(3, 3));
            allGo[0].transform.position = allRB[0].TF.Pos.ToVector2();
            allRB[0].SetGravityScale(0);
            space2D.Add(allRB[0]);

            allGo[1] = new GameObject("go1");
            allRB[1] = FPRigidbody2DFactory.CreateBoxRB(new FPVector2(2, 0), 0, new FPVector2(1, 1));
            allGo[1].transform.position = allRB[1].TF.Pos.ToVector2();
            allRB[1].SetGravityScale(0);
            space2D.Add(allRB[1]);

            allGo[2] = new GameObject("go2");
            allRB[2] = FPRigidbody2DFactory.CreateCircleRB(new FPVector2(0, -4), 1);
            allGo[2].transform.position = allRB[2].TF.Pos.ToVector2();
            allRB[2].SetGravityScale(0);
            space2D.Add(allRB[2]);

            // Test Holder Filter
            allGo[3] = new GameObject("holderFilter");
            allRB[3] = FPRigidbody2DFactory.CreateCircleRB(new FPVector2(2, -4), 2);
            allGo[3].transform.position = allRB[3].TF.Pos.ToVector2();
            allRB[3].SetGravityScale(0);
            space2D.Add(allRB[3]);

            // Test Layer Filter
            allGo[4] = new GameObject("layerFilter");
            allRB[4] = FPRigidbody2DFactory.CreateCircleRB(new FPVector2(4, -4), 1);
            allGo[4].transform.position = allRB[4].TF.Pos.ToVector2();
            allRB[4].SetGravityScale(0);
            allRB[4].SetLayer(2);
            space2D.Add(allRB[4]);

            // Test Trigger Filter
            allGo[5] = new GameObject("triggerFilter");
            allRB[5] = FPRigidbody2DFactory.CreateCircleRB(new FPVector2(6, -4), 1);
            allGo[5].transform.position = allRB[5].TF.Pos.ToVector2();
            allRB[5].SetGravityScale(0);
            allRB[5].SetTrigger(true);
            space2D.Add(allRB[5]);

            rayStartGo = new GameObject("rayStartGo");
            rayStartGo.transform.position = new Vector2(-3, 3);
            rayEndGo = new GameObject("rayEndGo");
            rayEndGo.transform.position = new Vector2(3, 3);

            segmentStartGo = new GameObject("segmentStartGo");
            segmentStartGo.transform.position = new Vector2(-3, 8);
            segmentEndGo = new GameObject("segmentEndGo");
            segmentEndGo.transform.position = new Vector2(3, 8);

            filter = new FPContactFilter2DArgs();
            filter.isFiltering = true;
            filter.useTriggers = false;
            filter.useLayerMask = true;
            filter.layerMask = 2;

            UnityTextWriter writer = new UnityTextWriter();

            Debug.Log("初始化成功");

        }

        Color rayColor = Color.yellow;
        Color segmentColor = Color.green;

        void OnDrawGizmos() {
            space2D?.GizmosDrawAllRigidbody();

            if ((rayEndGo != null) && (rayStartGo != null)) {
                Gizmos.color = rayColor;
                Gizmos.DrawLine(rayStartGo.transform.position, rayEndGo.transform.position);
            }
            if ((segmentEndGo != null) && (segmentStartGo != null)) {
                Gizmos.color = segmentColor;
                Gizmos.DrawLine(segmentStartGo.transform.position, segmentEndGo.transform.position);
            }
            if ((rayHitPoints == null) || (segmentHitPoints == null)) {
                return;
            }

            for (int i = 0; i < rayHitPoints.Length; i++) {
                if (rayHitPoints[i] == null) {
                    continue;
                }
                Gizmos.color = Color.cyan;
                Gizmos.DrawCube(rayHitPoints[i].ToVector2(), new Vector2(0.2f, 0.2f));
            }
            for (int i = 0; i < segmentHitPoints.Length; i++) {
                if (segmentHitPoints[i] == null) {
                    continue;
                }
                Gizmos.color = Color.cyan;
                Gizmos.DrawCube(segmentHitPoints[i].ToVector2(), new Vector2(0.2f, 0.2f));
            }
            //hitPoints.Clear();
        }

        public void Clear() {
            for (int i = 0; i < allRB.Length; i += 1) {
                var rb = allRB[i];
                if (rb != null) {
                    space2D.Remove(rb);
                }
            }
            allRB = null;
            space2D = null;
            for (int i = 0; i < allGo.Length; i += 1) {
                DestroyImmediate(allGo[i]);
            }
            allGo = null;
            Debug.Log("回收成功");

        }

        private void Update() {

            if (Input.GetKeyDown(KeyCode.Escape)) {
                Clear();
            }

            if ((allGo != null) && (allRB != null)) {
                for (int i = 0; i < allGo.Length; i++) {
                    if ((allGo[i] != null) && (allRB[i] != null)) {
                        allRB[i].SetPos(new FPVector2((FP64)allGo[i].transform.position.x, (FP64)allGo[i].transform.position.y));
                        allRB[i].SetRotRadianAngle((FP64)allGo[i].transform.rotation.eulerAngles.z);
                    }
                }
            }

            var rayStart = new FPVector2((FP64)rayStartGo.transform.position.x, (FP64)rayStartGo.transform.position.y);
            var rayEnd = new FPVector2((FP64)rayEndGo.transform.position.x, (FP64)rayEndGo.transform.position.y);
            var rayDistance = (rayEnd - rayStart).Length();
            var rayDirection = rayEnd - rayStart;
            var ray = new FPRay2D(rayStart, rayDirection);

            var segmentStart = new FPVector2((FP64)segmentStartGo.transform.position.x, (FP64)segmentStartGo.transform.position.y);
            var segmentEnd = new FPVector2((FP64)segmentEndGo.transform.position.x, (FP64)segmentEndGo.transform.position.y);
            var segment = new FPSegment2D(segmentStart, segmentEnd);

            var isHitRay = getterAPI.Raycast2D(ray, rayDistance, filter, rayHits);
            if (isHitRay) {
                rayColor = Color.red;
                Array.Clear(rayHitPoints, 0, rayHits.Length);
                rayHitPoints[0] = rayHits[0].point;
            } else {
                Array.Clear(rayHitPoints, 0, rayHits.Length);
                rayColor = Color.yellow;
            }

            var isHitSegment = getterAPI.SegmentCast2DAll(segment, filter, segmentHits);
            if (isHitSegment) {
                segmentColor = Color.red;
                segmentHitPoints[0] = segmentHits[0].points[0];
                segmentHitPoints[1] = segmentHits[0].points[1];
            } else {
                Array.Clear(segmentHitPoints, 0, segmentHits.Length);
                segmentColor = Color.green;
            }

            var deltaTime = FP64.EN3 * 34;
            space2D.Tick(deltaTime);
        }

    }

}