using System.Globalization;
using _project.Scripts.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace _project.Scripts.Editor
{
    [CustomEditor(typeof(ScoreDataScriptableObject))]
    public class ScoreDataScriptableObjectEditor : UnityEditor.Editor
    {
        private Vector2 _scrollPos;
        private ScoreDataScriptableObject Data => target as ScoreDataScriptableObject;
        private int _pageNumber = 0;
        private int _maxPageNumber = 50;
        private int _perPageItemCount = 10;
        
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // EditorGUI.BeginDisabledGroup(true);

            EditorGUILayout.LabelField("Target Score per stage: (1 * stage ^ multiplier)");
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
            EditorGUI.indentLevel++;

            for (int i = _pageNumber * _perPageItemCount; i < (_pageNumber+1) * _perPageItemCount; i++)
            {
                string s = Data.GetTargetScoreForStageInt(i).ToString("#,##0", CultureInfo.InvariantCulture);
                EditorGUILayout.LabelField($"Stage {i}: {s}");
            }

            EditorGUI.indentLevel--;
            // EditorGUI.EndDisabledGroup();
            
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("0"))
            {
                _pageNumber = 0;
            }
            if (GUILayout.Button("Previous Page"))
            {
                _pageNumber = Mathf.Max(_pageNumber - 1, 0);
            }
            if (GUILayout.Button("Next Page"))
            {
                _pageNumber = Mathf.Min(_pageNumber + 1, _maxPageNumber);
            }
            GUILayout.EndHorizontal();
            EditorGUILayout.EndScrollView();

        }
    }
}