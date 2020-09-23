using UnityEngine;
using System.IO;
 using StringBuilder = System.Text.StringBuilder;

namespace IRK.Unity3D.CreatorScript
{
    public static class CreatorScriptUtitlity
    {
        public const string pathTemplateFolder = "Assets/CreatorScript/ScriptTemplates/";

        public const string templateTypeScript = "$type$";
        public const string templateNamespace = "$name_namespace$";
        public const string templateNameClass = "$name$";
        public const string templateNameBaseClass = "$name_base$";
        public const string templateAccess = "$access$";
        public const string templateCustomEditor = "$customEditor$";
        public const string templateMethods = "$methods$";
        public const string templateItems = "$items$";

        public static void CreateScript(ScriptType scriptType, string pathFolder, string fileName, string name, string nameNamespace, string baseClass, string customEditor, AccessModifier access, MessageUnity[] messages,ItemEnum[] itemsEnum)
        {
            string nameTemplate = "";

            switch (scriptType)
            {
                case ScriptType.Empty:
                    CreateScript(pathFolder, fileName, "");
                    return;

                case ScriptType.Class:
                case ScriptType.Interface:
                case ScriptType.Struct:
                    nameTemplate = "Script";
                    break;

                case ScriptType.MonoBehaviour: nameTemplate = "MonoBehaviour"; break;
                case ScriptType.Editor: nameTemplate = "Editor"; break;
                case ScriptType.Window: nameTemplate = "EditorWindow"; break;
                case ScriptType.Enum: nameTemplate = "Enum"; break;
            }

            StringBuilder template = new StringBuilder(CheckNamespace(ReadScriptTemplate(nameTemplate), nameNamespace));

            //Namespace
            template.Replace(templateNamespace, nameNamespace);
            //Name(class,struct,interface,enum,editor,window)
            template.Replace(templateNameClass, name);
            //LevelAccess
            template.Replace(templateAccess, AccessToString(access));

            //CustomEditor
            if (scriptType == ScriptType.Editor)
                template.Replace(templateCustomEditor, customEditor);

            //Base and scriptType
            if (scriptType == ScriptType.Class || scriptType == ScriptType.Interface || scriptType == ScriptType.Struct)
            {
                template.Replace(templateNameBaseClass,string.IsNullOrEmpty(baseClass) ? "" : ": " + baseClass);
                template.Replace(templateTypeScript, scriptType.ToString().ToLower());
            }

            //ItemsEnum
            if (scriptType == ScriptType.Enum)
                template.Replace(templateItems, CreateItemsEnumCode(itemsEnum));
            else//MessagesUnity
                template.Replace(templateMethods, CreateMessagesCode(messages));

            CreateScript(pathFolder, fileName, template.ToString());
        }

        //A script in the desired folder
        public static void CreateScript(string pathFolder, string fileName, string code)
        {
            File.WriteAllText(Path.GetFullPath(string.Format("{0}/{1}.cs", pathFolder, fileName)), code);
            UnityEditor.AssetDatabase.Refresh();
        }

        public static string CreateItemsEnumCode(ItemEnum[] items)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < items.Length; i++)
            {
                result.AppendLine(items[i].code);
                result.Append("\t");
            }

            return result.ToString();
        }

        public static string CreateMessagesCode(MessageUnity[] methods)
        {
            if (methods == null || methods.Length == 0)
                return string.Empty;

            StringBuilder result = new StringBuilder();

            foreach (MessageUnity method in methods)
            {
                result.AppendLine(method.code);
            }
            
            return result.ToString();
        }

        public static string AccessToString(AccessModifier access)
        {
            switch (access)
            {
                default:
                case AccessModifier.None: return "";
                case AccessModifier.Public:return "public";
                case AccessModifier.Private:return "private";
                case AccessModifier.Protected:return "protected";
                case AccessModifier.Internal:return "internal";
                case AccessModifier.ProtectedInternal: return "protected internal";
                case AccessModifier.PrivateProtected: return "private protected";
            }
        }

        //The file reads the desired template and return the result
        public static string ReadScriptTemplate(string name)
        {
            return File.ReadAllText(Path.GetFullPath(pathTemplateFolder + name + ".txt"));
        }
        
        //The code checks you to see if you have used the 'namespace'
        public static string CheckNamespace(string code,string @namespace)
        {
            string[] codes = code.Split('?');

            bool isNamespace = !string.IsNullOrEmpty(@namespace);
            int index = (isNamespace ? 1 : 0) + 1;

            return codes[0] + codes[index];
        }
    }
}
