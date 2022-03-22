using UnityEngine;
using UnityEditor;
using System.Reflection;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.AddressableAssets.Settings;

namespace BobBuildTools
{
    public class BobBuildPlayerWindow : EditorWindow, IPreprocessBuildWithReport
    {
        private BuildPlayerWindow m_buildPlayerWindow = null;
        private BuildPlayerWindow BuildPlayerWindow
        {
            get
            {
                if (m_buildPlayerWindow == null)
                    m_buildPlayerWindow = ScriptableObject.CreateInstance<BuildPlayerWindow>();

                return m_buildPlayerWindow;
            }
        }
        private MethodInfo m_bpwOnGUI = null;
        private MethodInfo BPW_OnGUI
        {
            get
            {
                if (m_bpwOnGUI == null)
                    m_bpwOnGUI = typeof(BuildPlayerWindow).GetMethod("OnGUI", BindingFlags.Instance | BindingFlags.NonPublic);

                return m_bpwOnGUI;
            }
        }

        public int callbackOrder { get { return 0; } }

        private ProductSettingsData m_productSettingsData = null;
        private AddressablesData m_addressablesData = null;
        private BuildIdentifierData m_buildIdentifierData = null;

        [MenuItem("File/BobTheBuilder")]
        public static void OnOpenButton()
        {
            BobBuildPlayerWindow window = EditorWindow.GetWindow<BobBuildPlayerWindow>("BobTheBuilder");
            window.minSize = new Vector2(670f, 820f);
            window.Show();
        }

        private void OnEnable()
        {
            if (m_productSettingsData == null)
            {
                m_productSettingsData = new ProductSettingsData();
            }
            if (m_addressablesData == null)
            {
                m_addressablesData = new AddressablesData();
            }
            if (m_buildIdentifierData == null)
            {
                m_buildIdentifierData = new BuildIdentifierData();
            }
        }

        private void OnGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10f);

            GUILayout.BeginVertical();

            DrawLine(Color.gray);
            m_productSettingsData.DrawProductSettingsData();
            GUILayout.Space(8f);
            DrawLine(Color.gray);
            m_addressablesData.DrawAddressablesData();
            GUILayout.Space(8f);
            DrawLine(Color.gray);
            m_buildIdentifierData.DrawBuildNameData();
            GUILayout.Space(8f);
            DrawLine(Color.gray);

            GUILayout.EndVertical();

            GUILayout.Space(10f);
            GUILayout.EndHorizontal();

            BPW_OnGUI?.Invoke(BuildPlayerWindow, new object[] { });
        }

        private void DrawLine(Color color, int thickness = 1, int padding = 10)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
            r.height = thickness;
            r.y += padding / 2;
            r.x -= 2;
            r.width += 6;
            EditorGUI.DrawRect(r, color);
        }

        public void OnPreprocessBuild(BuildReport report)
        {
            BobTheBuilder.SetFinalIdentifierId(BobTheBuilder.GenerateIdentifierId(true));
            Debug.Log("Build Identifier: " + BobTheBuilder.GetFinalIdentifierId());

            if (BobTheBuilder.GetAutoBuildContent())
            {
                Debug.Log("Auto building addressables.");
                AddressableAssetSettings.BuildPlayerContent();
            }
        }
    }
}