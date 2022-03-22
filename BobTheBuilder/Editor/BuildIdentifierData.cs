using UnityEditor;
using UnityEngine;

namespace BobBuildTools
{
    public class BuildIdentifierData
    {
        private bool m_showProductName = true;
        private bool m_showDate = true;
        private bool m_showTime = true;
        private bool m_showCustomText = true;
        private bool m_showRandomId = true;
        private string m_customText = "";

        public BuildIdentifierData()
        {
            m_showProductName = BobTheBuilder.GetIdentifierShowProductName();
            m_showDate = BobTheBuilder.GetIdentifierShowDate();
            m_showTime = BobTheBuilder.GetIdentfierShowTime();
            m_showCustomText = BobTheBuilder.GetIdentifierShowCustomText();
            m_showRandomId = BobTheBuilder.GetIdentifierShowRandomId();
            m_customText = BobTheBuilder.GetIdentifierCustomText();
        }

        private void UpdateData()
        {
            BobTheBuilder.SetIdentifierShowProductName(m_showProductName);
            BobTheBuilder.SetIdentifierShowDate(m_showDate);
            BobTheBuilder.SetIdentifierShowTime(m_showTime);
            BobTheBuilder.SetIdentifierShowCustomText(m_showCustomText);
            BobTheBuilder.SetIdentifierShowRandomId(m_showRandomId);
            BobTheBuilder.SetIdentifierCustomText(m_customText);
        }

        public void DrawBuildNameData()
        {
            GUILayout.Label("Build Identifier", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();
            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical(GUILayout.Width(320f));
            DrawSingleOption("1. Include Product Name", ref m_showProductName, PlayerSettings.productName);
            DrawSingleOption("2. Include Date", ref m_showDate, BobTheBuilder.GetDateString());
            DrawSingleOption("3. Include Time", ref m_showTime, BobTheBuilder.GetTimeString());
            GUILayout.EndVertical();

            GUILayout.BeginVertical(GUILayout.Width(320f));
            DrawCustomTextOption("4. Include Custom Text", ref m_showCustomText);
            DrawSingleOption("5. Include Random Id", ref m_showRandomId, "???");
            GUILayout.EndVertical();

            GUILayout.EndHorizontal();
            if (EditorGUI.EndChangeCheck())
            {
                UpdateData();
            }

            GUI.enabled = false;
            GUILayout.BeginVertical("OL box");
            GUILayout.Label("Identifier Preview:   " + BobTheBuilder.GenerateIdentifierId(false));
            GUILayout.EndVertical();
            GUI.enabled = true;
        }

        private void DrawSingleOption(string label, ref bool boolVal, string preview)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(label, GUILayout.Width(150f));
            boolVal = GUILayout.Toggle(boolVal, GUIContent.none, GUILayout.Width(10f));
            if (boolVal)
            {
                GUI.enabled = false;
                GUILayout.Label(preview);
                GUI.enabled = true;
            }
            GUILayout.EndHorizontal();
        }

        private void DrawCustomTextOption(string label, ref bool boolVal)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(label, GUILayout.Width(150f));
            boolVal = GUILayout.Toggle(boolVal, GUIContent.none, GUILayout.Width(10f));
            if (boolVal)
            {
                GUILayout.Space(4f);
                m_customText = EditorGUILayout.TextField(m_customText);
            }
            GUILayout.EndHorizontal();
        }
    }
}