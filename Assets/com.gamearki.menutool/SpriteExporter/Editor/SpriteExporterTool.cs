using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace GameArki.MenuTool {

    public class SpriteToSplit {

        // 切割 Multiple Sprite 导出单个 Sprite
        [MenuItem("Assets/" + nameof(GameArki) + "/ExportSplitedSprite")]
        public static void SpriteChildToExport() {

            var selectedObjects = Selection.objects;

            for (int i = 0; i < selectedObjects.Length; i++) {

                var cur = selectedObjects[i];
                var curName = cur.name;

                // 获得选中对象路径
                string spritePath = AssetDatabase.GetAssetPath(cur);

                // 所有子 Sprite 对象
                Sprite[] sprites = AssetDatabase.LoadAllAssetsAtPath(spritePath).OfType<Sprite>().ToArray();
                if (sprites.Length < 1) {
                    EditorUtility.DisplayDialog("错误", "当前选择文件不是Sprite!", "确认");
                    return;
                }

                var fullPath = Path.ChangeExtension(spritePath, "");
                string fullFolderPath = fullPath.Substring(0, fullPath.Length - 1);

                // 创建文件夹
                if (!AssetDatabase.IsValidFolder(fullFolderPath)) {
                    string adjFolderPath = fullFolderPath.Substring(0, fullFolderPath.Length - curName.Length - 1);
                    _ = AssetDatabase.CreateFolder(adjFolderPath, curName);
                }

                try {

                    for (int j = 0; j < sprites.Length; j += 1) {

                        //创建Texture;
                        Sprite sprite = sprites[j];
                        if (!sprite.texture.isReadable) {
                            Debug.LogError(sprite.texture.name + " 请勾选: Read/Write");
                            break;
                        }

                        var rect = sprite.rect;

                        Texture2D tex = new Texture2D((int)rect.width, (int)rect.height, sprite.texture.format, false);
                        tex.SetPixels(sprite.texture.GetPixels((int)rect.xMin, (int)rect.yMin, (int)rect.width, (int)rect.height));
                        tex.Apply();

                        //判断保存路径;
                        string savePath = Path.Combine(fullFolderPath, sprites[j].name + ".png");

                        //生成png;
                        File.WriteAllBytes(savePath, tex.EncodeToPNG());

                    }

                } catch (Exception ex) {
                    Debug.LogError(ex);
                } finally {
                    AssetDatabase.Refresh();
                }

            }

        }

    }
}
