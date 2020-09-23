using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace IRK.Unity3D.CreatorScript
{
    public class CreateScriptWindow : EditorWindow
    {
        private const float MinWidth = 300.0f;
        private const float MaxWidth = 650.0f;

        private Dictionary<string, GUIContent> _contents;

        private MessagesUnityManager _messagesUnity;

        private ItemsEnumManager _itemsEnumManager;

        private ScriptType _type;
        private string _pathFolder;
        private string _nameFile;
        private string _namespace;
        private string _name;
        private string _base;
        private AccessModifier _access;
        private string _customEditor;
        private bool _createScriptEditor;

        public string pathFolder { get { return _pathFolder; } set { _pathFolder = value; } }
        public ScriptType type { get { return _type; } set { _type = value; } }


        private void InitContents()
        {
            _contents = new Dictionary<string, GUIContent>();

            _contents.Add("scriptType", new GUIContent("Type", "The type of script you want to create"));
            _contents.Add("btn_create", new GUIContent("Create", "Create a new script in the desired folder"));
            _contents.Add("btn_addMessage", new GUIContent("Add Messsge", "Adds a message from the default Unity message"));
            _contents.Add("btn_addItemEnum", new GUIContent("Add Item"));
            _contents.Add("pathFolder", new GUIContent("PathFolder", "The folder where you want your script to be create"));
            _contents.Add("nameFile", new GUIContent("Name File","The name of your script file"));
            _contents.Add("namespace", new GUIContent("Namespace","Name your script 'namespace,leave blank id you do not need to"));
            _contents.Add("nameClass", new GUIContent("Class","Class name"));
            _contents.Add("nameMonoBehaviour", new GUIContent("Class", "Class name"));
            _contents.Add("nameEditor", new GUIContent("Editor","Class name"));
            _contents.Add("nameWindow", new GUIContent("Window","Class name"));
            _contents.Add("nameInterface", new GUIContent("Interface","Interface name"));
            _contents.Add("nameStruct", new GUIContent("Struct","Struct name"));
            _contents.Add("nameEnum", new GUIContent("Enum","Enum name"));
            _contents.Add("base", new GUIContent("Base","The name of the base of your script,leave it blank if you do not need it"));
            _contents.Add("access", new GUIContent("Access","Class access level"));
            _contents.Add("customEditor", new GUIContent("CustomEditor","The name of the class you want the editor to edit"));
            _contents.Add("scriptEditor", new GUIContent("CreateScriptEditor","If enabled it creates an editor for your script in the 'pathFolder/Editor' folder"));
            _contents.Add("messages", new GUIContent("Messages"));
            _contents.Add("itemsEnum", new GUIContent("ItemsEnum"));
        }

        private void OnEnable()
        {
            base.minSize = new Vector2(MinWidth, 300.0f);
            base.maxSize = new Vector2(MaxWidth, 800.0f);

            InitContents();

            _messagesUnity = new MessagesUnityManager(_contents["messages"]);
            _itemsEnumManager = new ItemsEnumManager(_contents["itemsEnum"]);

            OnChangeScriptType();
        }

        private void OnGUI()
        {
            EditorGUI.BeginChangeCheck();
            _type = (ScriptType)EditorGUILayout.EnumPopup(_contents["scriptType"], _type);
            if (EditorGUI.EndChangeCheck())
                OnChangeScriptType();

            _pathFolder = EditorGUILayout.TextField(_contents["pathFolder"], _pathFolder);

            EditorGUI.BeginChangeCheck();
            _nameFile = EditorGUILayout.TextField(_contents["nameFile"], _nameFile);
            if (EditorGUI.EndChangeCheck())
                OnChangeTextField("NameFile");

            if (_type != ScriptType.Empty)
                OnGUIGeneral();

            switch (_type)
            {
                case ScriptType.Empty:
                    break;

                case ScriptType.Class:
                case ScriptType.Struct:
                case ScriptType.Interface:
                    break;

                case ScriptType.MonoBehaviour:
                    OnGUIMonoBehaviourType();
                    break;
                case ScriptType.Editor:
                    OnGUIEditorType();
                    break;
                case ScriptType.Window:
                    OnGUIWindowType();
                    break;

                case ScriptType.Enum:
                    _itemsEnumManager.OnGUI();
                    break;
            }

            //Button 'Create'
            EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(_nameFile) && string.IsNullOrEmpty(_name));
            if (GUILayout.Button(_contents["btn_create"]))
                CreateScript();
            EditorGUI.EndDisabledGroup();

        }

        private void OnChangeScriptType()
        {
            switch (_type)
            {
                case ScriptType.Empty:
                case ScriptType.Class:
                case ScriptType.Interface:
                case ScriptType.Struct:
                case ScriptType.Enum:
                    _base = "";
                    break;

                case ScriptType.MonoBehaviour: _base = "MonoBehaviour"; break;
                case ScriptType.Editor: _base = "Editor";break;
                case ScriptType.Window: _base = "EditorWindow"; break;
            }

            _messagesUnity.ChangeScriptType(_type);
        }

        private void OnChangeTextField(string textField)
        {
            if(textField == "NameFile")
            {
                _name = _nameFile;
            }
        }

        private void OnGUIGeneral()
        {
            //name class
            EditorGUI.BeginDisabledGroup(_type == ScriptType.MonoBehaviour);
            _name = EditorGUILayout.TextField(_contents[string.Format("name{0}",_type.ToString())], _name);
            EditorGUI.EndDisabledGroup();

            //namespace
            _namespace = EditorGUILayout.TextField(_contents["namespace"], _namespace);

            //access
            _access = (AccessModifier)EditorGUILayout.EnumPopup(_contents["access"], _access);

            //base class
            EditorGUI.BeginDisabledGroup(!(_type == ScriptType.Class || _type == ScriptType.Struct));
            _base = EditorGUILayout.TextField(_contents["base"], _base);
            EditorGUI.EndDisabledGroup();

        }

        private void OnGUIEditorType()
        {
            _customEditor = EditorGUILayout.TextField(_contents["customEditor"], _customEditor);

            OnGUIMessagesList();
        }

        private void OnGUIMonoBehaviourType()
        {
            _createScriptEditor = EditorGUILayout.Toggle(_contents["scriptEditor"], _createScriptEditor);

            OnGUIMessagesList();
        }

        private void OnGUIWindowType()
        {
            OnGUIMessagesList();
        }

        private void OnGUIScriptType()
        {
           
        }

        private void OnGUIMessagesList()
        {
            _messagesUnity.listView.OnGUI();
        }

        private void CreateScript()
        {
            CreatorScriptUtitlity.CreateScript
                (
                scriptType: _type,
                pathFolder: _pathFolder,
                fileName: _nameFile,
                name: _name,
                nameNamespace: _namespace,
                baseClass: _base,
                customEditor: _customEditor,
                access: _access,
                messages: _messagesUnity._messagesUnity.ToArray(),
                itemsEnum: _itemsEnumManager.listView.items
                );

            //Create a script editor for 'MonoBehaviour'
            if (_createScriptEditor && _type == ScriptType.MonoBehaviour)
            {
                CreatorScriptUtitlity.CreateScript
                    (
                    scriptType: ScriptType.Editor,
                    pathFolder: _pathFolder,
                    fileName: _nameFile + "Editor",
                    name: _name + "Editor",
                    nameNamespace: _namespace,
                    baseClass: _base,
                    customEditor: _name,
                    access: _access,
                    messages: null,
                    itemsEnum: null
                    );
            }
        }
    }
}