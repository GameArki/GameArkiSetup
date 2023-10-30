using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace GameArki.FPEasing.Sample {
    
    public class Sample_DisplayerPlane : MonoBehaviour {
        
        MeshRenderer displayer;
        Transform cube;
        Transform sphere;
        Texture2D converted;
        Texture2D circle;

        public int circleSize = 512;
        public string functionName = "EaseInSine";
        
        Vector3 a = new (0, 1, 0);
        Vector3 b = new (0, 4, 0);
        
        float t;
        float l;
        int canvasSize;
        
        void Start() {
            
            displayer = GetChild(transform, "Displayer").GetComponent<MeshRenderer>();
            cube = GetChild(transform, "CubeA");
            sphere = GetChild(displayer.transform, "Sphere");
            
            Texture texture = displayer.material.GetTexture("_MainTex");
            circle = DrawCircle(circleSize);
            canvasSize = texture.height;
            l = (56f / (canvasSize + 112f))*10f;   
            
            converted = new Texture2D(canvasSize+112, canvasSize+112);
            for (int x = 0; x < canvasSize+112; x++) {
                for (int y = 0; y < canvasSize+112; y++) {
                        converted.SetPixel(x, y, Color.white);
                }
            }

            RenderTexture currentRT = RenderTexture.active;
            RenderTexture renderTexture = new RenderTexture(canvasSize, canvasSize, 0);
            RenderTexture.active = renderTexture;
            Graphics.Blit(texture, RenderTexture.active);
            converted.ReadPixels(new Rect(0, 0, canvasSize, canvasSize), 56, 56);
            converted.Apply();
            RenderTexture.active = currentRT;
            renderTexture.Release();

            GetComponentInChildren<Text>().text = functionName;

            DrawFunction(1000);
            
        }
        
        void Update() {
            
            float v = GetFuntionResult(functionName , t);
            cube.localPosition = Vector3.LerpUnclamped(a, b, v);
            sphere.localPosition = new Vector3(Mathf.LerpUnclamped(5f - l, -5f + l, t), 0, Mathf.LerpUnclamped(5f - l, -5f + l, v));
            
            t += Time.deltaTime;
            if (t > 1) {
                t = 0;
            }
            
        }

        float GetFuntionResult(string cal , float t) {
            
            Type type = typeof(FunctionHelper);
            MethodInfo methodInfo = type.GetMethod(cal, BindingFlags.Public | BindingFlags.Static);
            object[] p = { t };
            object o = methodInfo.Invoke( cal , p );
            float result = (float)o;
            
            return result;
            
        }
        
        Transform GetChild(Transform parent, string search) {
            
            Transform[] transforms = parent.gameObject.GetComponentsInChildren<Transform>();
            foreach (var tran in transforms) {
                if (tran.gameObject.name == search) {
                    return tran;
                }
            }

            return null;
            
        }

        Texture2D DrawCircle(int size) {
            
            Texture2D c = new Texture2D(size, size);
            Vector2 center = new Vector2((float)(size - 1) / 2, (float)(size - 1) / 2);
            float r = (float)size / 2;

            for (int x = 0; x < size; x++) {
                for (int y = 0; y < size; y++) {
                    Vector2 pixel = new Vector2(x, y);
                    if (Vector2.Distance(pixel, center) <= r) {
                        c.SetPixel(x, y, Color.black);
                    }
                }
            }

            c.Apply();
            return c;
            
        }

        void DrawFunction(int precision) {

            for (int i = 0; i < precision; i++) {
                float v = GetFuntionResult(functionName , (float)i/precision);
                for (int x = 0; x < circleSize; x++) {
                    for (int y = 0; y < circleSize; y++) {
                        int xp = x - circleSize / 2 + (int)(canvasSize * i/precision) + 56;
                        int yp = y - circleSize / 2 + (int)(canvasSize * v) + 56;
                        Color color = circle.GetPixel(x, y);
                        if (color == Color.black && xp >= 0 && yp >= 0 && xp < canvasSize + 112 && yp < canvasSize + 112) {
                            converted.SetPixel(xp, yp, color);
                        }
                    }
                }
            }
            converted.Apply();

            displayer.material.SetTexture("_MainTex", converted);
            
        }
        
    }
    
}
