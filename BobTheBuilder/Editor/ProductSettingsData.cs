using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Text;

namespace BobBuildTools
{
    public class ProductSettingsData
    {
        private string m_companyName = null;
        private string m_productName = null;
        private string m_versionId = null;
        private Texture2D m_appIcon = null;
        private string m_scriptingDefines = null;
        private string m_scriptingDefineToolTip = null;

        /// <summary>
        /// Modify this dictionary outside of the class to update the tooltip for scripting defines.
        /// </summary>
        public static Dictionary<string, string> ScriptingDefineDescriptions = new Dictionary<string, string>();

        public ProductSettingsData()
        {
            m_companyName = PlayerSettings.companyName;
            m_productName = PlayerSettings.productName;
            m_versionId = PlayerSettings.bundleVersion;
            Texture2D[] icons = PlayerSettings.GetIconsForTargetGroup(BuildTargetGroup.Unknown);
            if (icons != null && icons.Length > 0)
            {
                m_appIcon = icons[0];
            }
            m_scriptingDefines = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);

            ScriptingDefineDescriptions.Clear();
            ScriptingDefineDescriptions.Add("DEBUG_YES", "Adds debugs");
            ScriptingDefineDescriptions.Add("OFFLINE", "Makes build offline.");


            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Available Scripting Defines:\n");
            foreach (string key in ScriptingDefineDescriptions.Keys)
            {
                stringBuilder.Append("\n[");
                stringBuilder.Append(key);
                stringBuilder.Append("]: ");
                stringBuilder.Append(ScriptingDefineDescriptions[key]);
            }
            m_scriptingDefineToolTip = stringBuilder.ToString();
        }

        private void UpdateData()
        {
            PlayerSettings.companyName = m_companyName;
            PlayerSettings.productName = m_productName;
            PlayerSettings.bundleVersion = m_versionId;
            PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Unknown, new Texture2D[] { m_appIcon });
        }

        private void SetScriptingDefines()
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, m_scriptingDefines);
        }

        public void DrawProductSettingsData()
        {
            GUILayout.Label("Product Settings", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();
            GUILayout.BeginHorizontal();
            m_appIcon = (Texture2D)EditorGUILayout.ObjectField(m_appIcon, typeof(Texture2D), false, GUILayout.Width(64), GUILayout.Height(64));

            GUILayout.Space(4f);

            GUILayout.BeginVertical();
            m_companyName = EditorGUILayout.TextField("Company Name", m_companyName);
            m_productName = EditorGUILayout.TextField("Product Name", m_productName);
            m_versionId = EditorGUILayout.TextField("Version", m_versionId);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            if (EditorGUI.EndChangeCheck())
            {
                UpdateData();
            }

            GUILayout.Space(2f);
            EditorGUILayout.LabelField(new GUIContent("Scripting Define Symbols", m_scriptingDefineToolTip), GUILayout.Width(150f));

            GUILayout.BeginHorizontal();
            m_scriptingDefines = EditorGUILayout.TextField(m_scriptingDefines);
            if (GUILayout.Button("Update", GUILayout.Width(80f)))
            {
                SetScriptingDefines();
            }
            GUILayout.EndHorizontal();
        }
    }
}