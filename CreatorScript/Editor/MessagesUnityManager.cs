using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace IRK.Unity3D.CreatorScript
{
    public class MessagesUnityManager : AdvancedDropdown
    {
        private ListView<MessageUnity> _messages;
        public List<MessageUnity> _messagesUnity;
        private AdvancedDropdownItem[] _groupItems;

        public ListView<MessageUnity> listView
        {
            get { return _messages; }
        }

        public MessagesUnityManager(GUIContent contentLable) : base(new AdvancedDropdownState())
        {
            _groupItems = new AdvancedDropdownItem[10];
            int index = 0;
            foreach (string item in System.Enum.GetNames(typeof(GroupMessageType)))
            {
                _groupItems[index++] = new AdvancedDropdownItem(item);
            }

            foreach (var item in _messagesUnity)
            {
                _groupItems[item.indexGroup].AddChild(new AdvancedDropdownItem(item.lable));
            }

            _messages = new ListView<MessageUnity>();
            _messages.lable = contentLable;

            _messages.onAddButton = OnAddButton;
            _messages.GUIItem = GUIItem;

            CreateMethods();
        }

        protected override AdvancedDropdownItem BuildRoot()
        {
            AdvancedDropdownItem result = new AdvancedDropdownItem("Messages");

            foreach (var item in _groupItems)
            {
                result.AddChild(item);
            }

            return result;
        }

        private void CreateMethods()
        {
            _messagesUnity = new List<MessageUnity>();
            
            #region General
            _messagesUnity.Add(new MessageUnity("Awake", 0));
            _messagesUnity.Add(new MessageUnity("FixedUpdate", 0));
            _messagesUnity.Add(new MessageUnity("LateUpdate", 0));
            _messagesUnity.Add(new MessageUnity("Reset", 0));
            _messagesUnity.Add(new MessageUnity("Start", 0));
            _messagesUnity.Add(new MessageUnity("Update", 0));
            _messagesUnity.Add(new MessageUnity("OnGUI", 0));
            _messagesUnity.Add(new MessageUnity("OnAudioFilterRead", 0, "float[]", "data", "int", "channels"));
            _messagesUnity.Add(new MessageUnity("OnBecameInvisible", 0));
            _messagesUnity.Add(new MessageUnity("OnBecameVsisble", 0));
            _messagesUnity.Add(new MessageUnity("OnDrawGizmos", 0));
            _messagesUnity.Add(new MessageUnity("OnDrawGizmosSelected", 0));
            _messagesUnity.Add(new MessageUnity("OnEnable", 0));
            _messagesUnity.Add(new MessageUnity("OnDestroy", 0));
            _messagesUnity.Add(new MessageUnity("OnDisable", 0));
            _messagesUnity.Add(new MessageUnity("OnValidate", 0));
            _messagesUnity.Add(new MessageUnity("OnTransformChildrenChanged", 0)); ;
            _messagesUnity.Add(new MessageUnity("OnTransformParentChanged", 0));
            #endregion

            #region Event
            _messagesUnity.Add(new MessageUnity("OnMouseDown", 1));
            _messagesUnity.Add(new MessageUnity("OnMouseDrag", 1));
            _messagesUnity.Add(new MessageUnity("OnMouseEnter", 1));
            _messagesUnity.Add(new MessageUnity("OnMouseExit", 1));
            _messagesUnity.Add(new MessageUnity("OnMouseOver", 1));
            _messagesUnity.Add(new MessageUnity("OnMouseUp", 1));
            _messagesUnity.Add(new MessageUnity("OnMouseUpAsButton", 1));
            #endregion

            #region Physic
            _messagesUnity.Add(new MessageUnity("OnCollisionEnter", 2, "Collision", "other"));
            _messagesUnity.Add(new MessageUnity("OnCollisionExit", 2, "Collision", "other"));
            _messagesUnity.Add(new MessageUnity("OnCollisionStay", 2, "Collision", "other"));
            _messagesUnity.Add(new MessageUnity("OnTriggerEnter", 2, "Collider", "other"));
            _messagesUnity.Add(new MessageUnity("OnTriggerExit", 2, "Collider", "other"));
            _messagesUnity.Add(new MessageUnity("OnTriggerStay", 2, "Collider", "other"));
            _messagesUnity.Add(new MessageUnity("OnCollisionEnter2D", 2, "Collision2D", "other"));
            _messagesUnity.Add(new MessageUnity("OnCollisionExit2D", 2, "Collision2D", "other"));
            _messagesUnity.Add(new MessageUnity("OnCollisionStay2D", 2, "Collision2D", "other"));
            _messagesUnity.Add(new MessageUnity("OnTriggerEnter2D", 2, "Collider2D", "other"));
            _messagesUnity.Add(new MessageUnity("OnTriggerExit2D", 2, "Collider2D", "other"));
            _messagesUnity.Add(new MessageUnity("OnTriggerStay2D", 2, "Collider2D", "other"));
            _messagesUnity.Add(new MessageUnity("OnControllerColliderHit", 2, "ControllerColliderHit", "hit"));
            _messagesUnity.Add(new MessageUnity("OnJointBreak", 2, "float", "breakForce"));
            _messagesUnity.Add(new MessageUnity("OnJointBreak2D", 2, "Joint2D", "brokenJoint"));
            #endregion

            #region Render
            _messagesUnity.Add(new MessageUnity("OnPostRender", 3));
            _messagesUnity.Add(new MessageUnity("OnPreCull", 3));
            _messagesUnity.Add(new MessageUnity("OnPreRender", 3));
            _messagesUnity.Add(new MessageUnity("OnRenderImage", 3, "RenderTexture", "src", "RenderTexture", "dest"));
            _messagesUnity.Add(new MessageUnity("OnRenderObject", 3));
            _messagesUnity.Add(new MessageUnity("OnWillRenderObject", 3));
            #endregion

            #region Network
            _messagesUnity.Add(new MessageUnity("OnConnectedToServer", 4));
            _messagesUnity.Add(new MessageUnity("OnDisconnectedFromServer", 4, "NetworkDisconnection", "info"));
            _messagesUnity.Add(new MessageUnity("OnFailedToConnect", 4, "NetworkConnectionError", "error"));
            _messagesUnity.Add(new MessageUnity("OnFailedToConnectToMasterServer", 4, "NetworkConnectionError", "error"));
            _messagesUnity.Add(new MessageUnity("OnMasterServerEvent", 4, "MasterServerEvent", "serverEvent"));
            _messagesUnity.Add(new MessageUnity("OnNetworkInstantiate", 4, "NetworkMessageInfo", "info"));
            _messagesUnity.Add(new MessageUnity("OnPlayerConnected", 4, "NetworkPlayer", "player"));
            _messagesUnity.Add(new MessageUnity("OnPlayerDisconnected", 4, "NetworkPlayer", "player"));
            _messagesUnity.Add(new MessageUnity("OnSerializeNetworkView", 4, "BitStream", "stream", "NetworkMessageInfo", "info"));
            _messagesUnity.Add(new MessageUnity("OnServerInitialized", 4));
            #endregion

            #region Application
            _messagesUnity.Add(new MessageUnity("OnApplicationFocus", 5, "bool", "hasFocus"));
            _messagesUnity.Add(new MessageUnity("OnApplicationPause", 5, "bool", "pauseStatus"));
            _messagesUnity.Add(new MessageUnity("OnApplicationQuit", 5));
            #endregion

            #region Particle
            _messagesUnity.Add(new MessageUnity("OnParticleCollision", 6, "GameObject", "other"));
            _messagesUnity.Add(new MessageUnity("OnParticleSystemStopped", 6));
            _messagesUnity.Add(new MessageUnity("OnParticleTrigger", 6));
            _messagesUnity.Add(new MessageUnity("OnParticleUpdateJobScheduled", 6));
            #endregion

            #region Animation
            _messagesUnity.Add(new MessageUnity("OnAnimatorIK", 7, "int", "layerIndex"));
            _messagesUnity.Add(new MessageUnity("OnAnimatorMove", 7));
            #endregion

            #region EditorWindow
            _messagesUnity.Add(new MessageUnity("OnFocus", 8));
            _messagesUnity.Add(new MessageUnity("OnLostFocus", 8));
            _messagesUnity.Add(new MessageUnity("OnHierarchyChange", 8));
            _messagesUnity.Add(new MessageUnity("OnInspectorUpdate", 8));
            _messagesUnity.Add(new MessageUnity("OnProjectChange", 8));
            _messagesUnity.Add(new MessageUnity("OnSelectionChange", 8));
            #endregion

            #region Editor
            _messagesUnity.Add(new MessageUnity("OnSceneGUI", 9));
            #endregion
        }

        protected override void ItemSelected(AdvancedDropdownItem item)
        {
            base.ItemSelected(item);
            AddMessageWithLable(item.name);
        }

        private void AddMessageWithLable(string lbl)
        {
            //Remove the parameters and get the message key
            int indexStart = lbl.IndexOf("(");
            int indexEnd = lbl.IndexOf(")");
            string key = lbl.Remove(indexStart, indexEnd - indexStart + 1);
            AddMessageWithKey(key);
        }

        private void AddMessageWithKey(string key)
        {
            _messages.AddItem(_messagesUnity.Find(m => m.key == key));
        }

        private void OnAddButton(Rect rect)
        {
            base.Show(rect);
        }
         
        private void GUIItem(ref MessageUnity msg)
        {
            EditorGUILayout.LabelField(msg.lable);
        }

        public void ChangeScriptType(ScriptType type)
        {
            _messages.Clear();

            switch (type)
            {
                case ScriptType.MonoBehaviour:
                    AddMessageWithKey("Start");
                    AddMessageWithKey("Update");
                    break;

                case ScriptType.Editor:
                    break;

                case ScriptType.Window:
                    AddMessageWithKey("OnEnable");
                    AddMessageWithKey("OnGUI");
                    break;
            }
        }        
    }
}