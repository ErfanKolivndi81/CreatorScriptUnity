using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace IRK.Unity3D.CreatorScript
{
	public class ItemEnum
	{
		private int _number;
		private string _name;

		public int number { get { return _number; } set { _number = value; } }
		public string name { get { return _name; } set { _name = value; } }
        public string code { get { return string.Format("{0} = {1},",_name,_number); } }
	}

	public class ItemsEnumManager
	{
        private ListView<ItemEnum> _list;

        public ListView<ItemEnum> listView { get { return _list; } }

        public ItemsEnumManager(GUIContent contentLable)
        {
            _list = new ListView<ItemEnum>();
            _list.lable = contentLable;

            _list.onAddButton = AddItem;
            _list.GUIItem = GUIItem;
        }

		private void AddItem(Rect rect)
		{
            int count = _list.count;
            ItemEnum newItem = new ItemEnum();
            newItem.number = count == 0 ? 0 : _list[count - 1].number + 1;
            newItem.name = "Item" + newItem.number;
            _list.AddItem(newItem);
        }

		public void OnGUI()
		{
            _list.OnGUI();
        }

        private void GUIItem(ref ItemEnum item)
        {
            item.name = EditorGUILayout.TextField(item.name);

            EditorGUILayout.Space(50.0f);

            item.number = EditorGUILayout.IntField(item.number);
        }
	}
}
