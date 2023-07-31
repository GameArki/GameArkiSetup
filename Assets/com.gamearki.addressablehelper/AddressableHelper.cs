#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace GameArki.AddressableHelper {

    public static class AddressableHelper {

        #region [添加Addressable资源的接口]

        /// <summary>
        /// 设置所有UnityEngine.Object为Addressable资源
        /// </summary>
        public static void SetAddressables(UnityEngine.Object[] assets) {
            SetAddressables(assets, null, null, false);
        }

        /// <summary>
        /// 设置所有设置所有UnityEngine.Object为Addressable资源,并设置标签
        /// </summary>
        public static void SetAddressables(UnityEngine.Object[] assets, string addressableLableName) {
            SetAddressables(assets, addressableLableName, null, false);
        }

        /// <summary>
        /// 设置所有设置所有UnityEngine.Object为Addressable资源,并设置标签、组
        /// </summary>
        public static void SetAddressables(UnityEngine.Object[] assets, string addressableLableName = null, string addressableGroupName = null, bool isSetSimplifiedName = false) {
            AddressableReflection(
                out var Settings_obj, out var groups_obj, out var DefaultGroup_prop, out var DefaultGroup_obj,
                out var CreateOrMoveEntry_met, out var GatherTargetInfos_met, out var SetAaEntry_met
            );

            // 检查组名
            if (!HasGroup(groups_obj, addressableGroupName, out var targetGroup_obj)) {
                targetGroup_obj = DefaultGroup_obj;
            }

            // 设置资源 标签、名称、组
            SetLables(addressableLableName, assets, Settings_obj, targetGroup_obj, CreateOrMoveEntry_met);
            if (isSetSimplifiedName) SetSimplifiedNames(assets, Settings_obj, targetGroup_obj, CreateOrMoveEntry_met);

            // 临时设置DefaultGroup
            DefaultGroup_prop.SetValue(Settings_obj, targetGroup_obj);

            // 最终方法调用以生效配置
            object targetInfos_obj = GatherTargetInfos_met.Invoke(null, new System.Object[] { assets, Settings_obj });
            SetAaEntry_met.Invoke(null, new System.Object[] { Settings_obj, targetInfos_obj, true });

            // 恢复原DefaultGroup
            DefaultGroup_prop.SetValue(Settings_obj, DefaultGroup_obj);

            foreach (var obj in assets) {
                Debug.Log($"添加Addressable资源: {obj.name}");
            }
        }

        /// <summary>
        /// 设置单个UnityEngine.Object为Addressable资源
        /// </summary>
        public static void SetAddressable(UnityEngine.Object asset) {
            SetAddressable(asset, null, null);
        }

        /// <summary>
        /// 设置单个UnityEngine.Object为Addressable资源, 并设置标签
        /// </summary>
        public static void SetAddressable(UnityEngine.Object asset, string addressableLableName) {
            SetAddressable(asset, addressableLableName, null);
        }

        /// <summary>
        /// 设置单个UnityEngine.Object为Addressable资源,并设置标签、组  
        /// </summary>
        public static void SetAddressable(UnityEngine.Object asset, string addressableLableName = null, string addressableGroupName = null) {
            AddressableReflection(
                out var Settings_obj, out var groups_obj, out var DefaultGroup_prop, out var DefaultGroup_obj,
                out var CreateOrMoveEntry_met, out var GatherTargetInfos_met, out var SetAaEntry_met
            );

            // 检查组名
            if (!HasGroup(groups_obj, addressableGroupName, out var targetGroup_obj)) {
                targetGroup_obj = DefaultGroup_obj;
            }

            // 设置资源 标签、名称、组
            SetLable(addressableLableName, asset, Settings_obj, targetGroup_obj, CreateOrMoveEntry_met);
            SetSimplifiedName(asset, Settings_obj, targetGroup_obj, CreateOrMoveEntry_met);
        }

        #endregion

        #region [简化资源名称的接口]

        /// <summary>
        /// 设置所有UnityEngine.Object的简化名称
        /// </summary>
        public static void SetSimplifiedNames(UnityEngine.Object[] assets) {
            AddressableReflection(
                out var Settings_obj, out var groups_obj, out var DefaultGroup_prop, out var DefaultGroup_obj,
                out var CreateOrMoveEntry_met, out var GatherTargetInfos_met, out var SetAaEntry_met
            );

            SetSimplifiedNames(assets, Settings_obj, DefaultGroup_obj, CreateOrMoveEntry_met);
        }

        /// <summary>
        /// 设置单个UnityEngine.Object的简化名称
        /// </summary>
        public static void SetSimplifiedName(UnityEngine.Object asset) {
            AddressableReflection(
                out var Settings_obj, out var groups_obj, out var DefaultGroup_prop, out var DefaultGroup_obj,
                out var CreateOrMoveEntry_met, out var GatherTargetInfos_met, out var SetAaEntry_met
            );

            SetSimplifiedName(asset, Settings_obj, DefaultGroup_obj, CreateOrMoveEntry_met);
        }

        #endregion

        #region [设置资源标签的接口]

        /// <summary>
        /// 设置所有UnityEngine.Object的标签
        /// </summary>
        public static void SetLables(UnityEngine.Object[] assets, string lableName) {
            AddressableReflection(
                out var Settings_obj, out var groups_obj, out var DefaultGroup_prop, out var DefaultGroup_obj,
                out var CreateOrMoveEntry_met, out var GatherTargetInfos_met, out var SetAaEntry_met
            );

            SetLables(lableName, assets, Settings_obj, DefaultGroup_obj, CreateOrMoveEntry_met);
        }

        /// <summary>
        /// 设置单个UnityEngine.Object的标签
        /// </summary>
        public static void SetLable(string lableName, UnityEngine.Object asset) {
            AddressableReflection(
                out var Settings_obj, out var groups_obj, out var DefaultGroup_prop, out var DefaultGroup_obj,
                out var CreateOrMoveEntry_met, out var GatherTargetInfos_met, out var SetAaEntry_met
            );

            SetLable(lableName, asset, Settings_obj, DefaultGroup_obj, CreateOrMoveEntry_met);
        }

        #endregion

        #region [设置资源组的接口]

        /// <summary>
        /// 设置所有UnityEngine.Object的组
        /// </summary>
        public static void SetGroups(UnityEngine.Object[] assets, string groupName) {
            AddressableReflection(
                out var Settings_obj, out var groups_obj, out var DefaultGroup_prop, out var DefaultGroup_obj,
                out var CreateOrMoveEntry_met, out var GatherTargetInfos_met, out var SetAaEntry_met
            );

            // 检查组名
            if (!HasGroup(groups_obj, groupName, out var targetGroup_obj)) {
                targetGroup_obj = DefaultGroup_obj;
            }

            // 设置资源 标签、名称、组
            SetLables(null, assets, Settings_obj, targetGroup_obj, CreateOrMoveEntry_met);
            SetSimplifiedNames(assets, Settings_obj, targetGroup_obj, CreateOrMoveEntry_met);
        }

        /// <summary>
        /// 设置单个UnityEngine.Object的组
        /// </summary>
        public static void SetGroup(UnityEngine.Object asset, string groupName) {
            AddressableReflection(
                out var Settings_obj, out var groups_obj, out var DefaultGroup_prop, out var DefaultGroup_obj,
                out var CreateOrMoveEntry_met, out var GatherTargetInfos_met, out var SetAaEntry_met
            );

            // 检查组名
            if (!HasGroup(groups_obj, groupName, out var targetGroup_obj)) {
                targetGroup_obj = DefaultGroup_obj;
            }

            // 设置资源 标签、名称、组
            SetLable(null, asset, Settings_obj, targetGroup_obj, CreateOrMoveEntry_met);
            SetSimplifiedName(asset, Settings_obj, targetGroup_obj, CreateOrMoveEntry_met);
        }

        #endregion

        #region [私有方法]

        /// <summary>
        /// 获取当前域中的程序集(Addressable相关)
        /// </summary>
        static void AddressableReflection(
            out object Settings_obj, out object groups_obj, out PropertyInfo DefaultGroup_prop, out object DefaultGroup_obj,
            out MethodInfo method_CreateOrMoveEntry, out MethodInfo GatherTargetInfos_methodInfo, out MethodInfo SetAaEntry_methodInfo) {

            Settings_obj = null;
            DefaultGroup_obj = null;
            groups_obj = null;
            DefaultGroup_prop = null;
            method_CreateOrMoveEntry = null;
            GatherTargetInfos_methodInfo = null;
            SetAaEntry_methodInfo = null;

            if (!GetCurDomainAssembly("Unity.Addressables.Editor", out var aa_Assembly)) {
                Debug.LogError($"Assembly not found: Unity.Addressables.Editor");
                return;
            }

            // - AddressableAssetInspectorGUI
            Type AddressableAssetInspectorGUI_type = aa_Assembly.GetType("UnityEditor.AddressableAssets.GUI.AddressableAssetInspectorGUI", true);
            Type AddressableAssetSettingsDefaultObject_type = aa_Assembly.GetType("UnityEditor.AddressableAssets.AddressableAssetSettingsDefaultObject", true);

            // - AddressableAssetSettings
            PropertyInfo Settings_prop = AddressableAssetSettingsDefaultObject_type.GetProperty("Settings");
            Type Settings_type = Settings_prop.PropertyType;
            Settings_obj = Settings_prop.GetValue(null);

            // - AddressableAssetGroup
            DefaultGroup_prop = Settings_obj.GetType().GetProperty("DefaultGroup");
            DefaultGroup_obj = DefaultGroup_prop.GetValue(Settings_obj);

            /// 获取所有组以此进行组名检查
            PropertyInfo groups_prop = Settings_obj.GetType().GetProperty("groups");
            groups_obj = groups_prop.GetValue(Settings_obj);

            // 相关方法
            method_CreateOrMoveEntry = Settings_type.GetMethod(
               "CreateOrMoveEntry",
               BindingFlags.Public | BindingFlags.Instance
           );
            GatherTargetInfos_methodInfo = AddressableAssetInspectorGUI_type.GetMethod("GatherTargetInfos", BindingFlags.NonPublic | BindingFlags.Static);
            SetAaEntry_methodInfo = AddressableAssetInspectorGUI_type.GetMethod("SetAaEntry", BindingFlags.NonPublic | BindingFlags.Static);
        }

        static void SetLables(string aaLableName, UnityEngine.Object[] aaAssetArray, object Settings_obj, object targetGroup_obj, MethodInfo method_CreateOrMoveEntry) {
            var len = aaAssetArray.Length;
            for (int i = 0; i < len; i++) {
                var asset = aaAssetArray[i];
                SetLable(aaLableName, asset, Settings_obj, targetGroup_obj, method_CreateOrMoveEntry);
            }
        }

        static void SetSimplifiedNames(UnityEngine.Object[] aaAssetArray, object Settings_obj, object targetGroup_obj, MethodInfo method_CreateOrMoveEntry) {
            var len = aaAssetArray.Length;
            for (int i = 0; i < len; i++) {
                var asset = aaAssetArray[i];
                SetSimplifiedName(asset, Settings_obj, targetGroup_obj, method_CreateOrMoveEntry);
            }
        }

        static void SetLable(string aaLableName, UnityEngine.Object asset, object Settings_obj, object targetGroup_obj, MethodInfo method_CreateOrMoveEntry) {
            if (asset == null) {
                Debug.LogWarning($"AA包资源为空!");
                return;
            }

            var path = AssetDatabase.GetAssetPath(asset);
            var guid = AssetDatabase.AssetPathToGUID(path);
            object assetAAEntry = method_CreateOrMoveEntry.Invoke(Settings_obj, new System.Object[] { guid, targetGroup_obj, null, null });

            // - Label
            if (aaLableName != null) {
                MethodInfo method_SetLable = assetAAEntry.GetType().GetMethod("SetLabel");
                method_SetLable.Invoke(assetAAEntry, new System.Object[] { aaLableName, true, null, null });
            }
        }

        static void SetSimplifiedName(UnityEngine.Object asset, object Settings_obj, object targetGroup_obj, MethodInfo method_CreateOrMoveEntry) {
            if (asset == null) {
                Debug.LogWarning($"AA包资源为空!");
                return;
            }

            var path = AssetDatabase.GetAssetPath(asset);
            var guid = AssetDatabase.AssetPathToGUID(path);
            object assetAAEntry = method_CreateOrMoveEntry.Invoke(Settings_obj, new System.Object[] { guid, targetGroup_obj, null, null });

            // - Name
            MethodInfo method_SetAddress = assetAAEntry.GetType().GetMethod(
                "SetAddress",
                BindingFlags.Public | BindingFlags.Instance
            );
            method_SetAddress.Invoke(assetAAEntry, new System.Object[] { asset.name, null });
        }

        static bool HasLabel(object Settings_obj, string aaLableName) {
            // 检查标签名
            PropertyInfo labelTable_prop = Settings_obj.GetType().GetProperty("labelTable", BindingFlags.NonPublic | BindingFlags.Instance);
            object labelTable_obj = labelTable_prop.GetValue(Settings_obj);

            PropertyInfo labelNames_prop = labelTable_obj.GetType().GetProperty("labelNames", BindingFlags.NonPublic | BindingFlags.Instance);
            object labelNames_obj = labelNames_prop.GetValue(labelTable_obj);

            var lableNames_ie = (IEnumerable<object>)labelNames_obj;
            bool hasLable = false;
            foreach (var labelName in lableNames_ie) {
                if (labelName.ToString() == aaLableName) {
                    hasLable = true;
                    break;
                }
            }
            return hasLable;
        }

        static bool HasGroup(object groups_obj, string groupName, out object tarGroup_obj) {
            tarGroup_obj = null;
            var ie = (IEnumerable<object>)groups_obj;
            foreach (var group in ie) {
                var name = group.GetType().GetProperty("Name").GetValue(group).ToString();
                if (name == groupName) {
                    tarGroup_obj = group;
                    break;
                }
            }

            return tarGroup_obj != null;
        }

        static bool GetCurDomainAssembly(string assemblyName, out Assembly assembly) {
            // Assembly
            var domain = System.AppDomain.CurrentDomain;
            var allAssembly = domain.GetAssemblies();
            assembly = null;
            foreach (var asm in allAssembly) {
                if (asm.GetName().Name == assemblyName) {
                    assembly = asm;
                    break;
                }
            }

            return assembly != null;
        }

        #endregion

    }

}

#endif
