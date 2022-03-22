using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.GUI;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace BobBuildTools
{
    public class AddressablesData
    {
        private GUIStyle m_smallTextStyle = null;
        private GUIStyle SmallTextStyle
        {
            get
            {
                if (m_smallTextStyle == null)
                {
                    m_smallTextStyle = new GUIStyle("button");
                    m_smallTextStyle.fontSize = 11;
                    m_smallTextStyle.fontStyle = FontStyle.Bold;
                }
                return m_smallTextStyle;
            }
        }
        private AddressableAssetSettings m_addressablesSettings = null;
        private AddressableAssetSettings AddressablesSettings
        {
            get
            {
                if (m_addressablesSettings == null)
                {
                    m_addressablesSettings = AddressableAssetSettingsDefaultObject.Settings;
                }
                return m_addressablesSettings;
            }
        }

        private bool m_autoBuildContent = false;

        public AddressablesData()
        {
            m_autoBuildContent = BobTheBuilder.GetAutoBuildContent();
        }

        private void UpdateData()
        {
            BobTheBuilder.SetAutoBuildContent(m_autoBuildContent);
        }

        public void DrawAddressablesData()
        {
            if (AddressablesSettings == null)
            {
                // Only null while compiling code. Just don't render this one frame.
                return;
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label("Addressables Settings", EditorStyles.boldLabel);
            GUILayout.Space(10f);
            if (GUILayout.Button("Open", SmallTextStyle, GUILayout.Width(40f), GUILayout.Height(16f)))
            {
                // WHY TF IS THE ADDRESSABLES WINDOW CLASS PRIVATEEEEEEEEEEEE!@!@@!@!@!@!@
                Assembly assembly = typeof(AnalyzeWindow).Assembly; // A random *public* class to access its assembly.
                Type addressablesWindowType = assembly.GetType("UnityEditor.AddressableAssets.GUI.AddressableAssetsWindow");
                MethodInfo initFunc = addressablesWindowType.GetMethod("Init", BindingFlags.NonPublic | BindingFlags.Static);
                initFunc?.Invoke(null, new object[] { });
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            DrawCurrentProfile();
            if (GUILayout.Button("Build Player Content", GUILayout.Width(160f)))
            {
                AddressableAssetSettings.BuildPlayerContent();
            }
            if (GUILayout.Button("Clean", GUILayout.Width(60f)))
            {
                AddressableAssetSettings.CleanPlayerContent();
                // To clean specific builder, pass in: "AddressablesSettings.ActivePlayerDataBuilder"
            }
            GUILayout.EndHorizontal();

            EditorGUI.BeginChangeCheck();
            m_autoBuildContent = EditorGUILayout.Toggle(new GUIContent("Auto Build Player Content", "Automatically build addressables when building project."), m_autoBuildContent);
            if (EditorGUI.EndChangeCheck())
            {
                UpdateData();
            }
        }

        private void DrawCurrentProfile()
        {
            var profSettings = AddressablesSettings.profileSettings;
            List<string> profileNames = profSettings.GetAllProfileNames();
            List<string> profileIds = new List<string>(profSettings.GetAllVariableIds());

            int index = profileNames.FindIndex(x => x == profSettings.GetProfileName(AddressablesSettings.activeProfileId));
            EditorGUI.BeginChangeCheck();
            index = EditorGUILayout.Popup("Current Profile ", index, profileNames.ToArray());
            if (EditorGUI.EndChangeCheck())
            {
                m_addressablesSettings.activeProfileId = profSettings.GetProfileId(profileNames[index]);
            }
        }
    }
}