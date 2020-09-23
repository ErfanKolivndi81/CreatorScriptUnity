using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace IRK.Unity3D.CreatorScript
{
	public class CreatorScriptMenuManager
	{
        public static void ShowCreatorScriptWindow(string pathFolder,ScriptType type)
        {
            CreateScriptWindow win = EditorWindow.GetWindow<CreateScriptWindow>();
            win.titleContent = new GUIContent("CreateScripts");
            win.Show();

            win.pathFolder = pathFolder;
            win.type = type;
        }

        public static void ShowWindowMenuAssets(ScriptType type)
        {
            ShowCreatorScriptWindow(AssetDatabase.GetAssetPath(Selection.activeObject), type);
        }

        [MenuItem("Window/IRK/CreatorScript")]
        public static void ShowWindow()
        {
            ShowCreatorScriptWindow("Assets/", ScriptType.MonoBehaviour);
        }

        [MenuItem("Assets/Create/Scripts/Class")]
        public static void ShowWindowClass()
        {
            ShowWindowMenuAssets(ScriptType.Class);
        }

        [MenuItem("Assets/Create/Scripts/Interface")]
        public static void ShowWindowInterface()
        {
            ShowWindowMenuAssets(ScriptType.Interface);
        }

        [MenuItem("Assets/Create/Scripts/Struct")]
        public static void ShowWindowStruct()
        {
            ShowWindowMenuAssets(ScriptType.Struct);
        }

        [MenuItem("Assets/Create/Scripts/MonoBehaviour")]
        public static void ShowWindowMonoBehaviour()
        {
            ShowWindowMenuAssets(ScriptType.MonoBehaviour);
        }

        [MenuItem("Assets/Create/Scripts/EditorScript")]
        public static void ShowWindowEditor()
        {
            ShowWindowMenuAssets(ScriptType.Editor);
        }

        [MenuItem("Assets/Create/Scripts/EditorWindowScript")]
        public static void ShowWindowEditorWindow()
        {
            ShowWindowMenuAssets(ScriptType.Window);
        }
    }
}

