using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace IRK.Unity3D.CreatorScript
{
    public class ListView<T>
    {
        public delegate void OnAddButton(Rect btnRect);
        public delegate void OnGUIItem(ref T item);

        private List<T> _items;

        private Rect _rectAddButton;
        private Vector2 _scroll;

        private GUIContent _contentLable;
        private GUIContent _contentAddButton;
        private GUIContent _contentRemove;
        private GUIContent _contentUp;
        private GUIContent _contentDown;

        public OnAddButton onAddButton;
        public OnGUIItem GUIItem;

        public T[] items { get { return _items.ToArray(); } }
        public int count { get { return _items.Count; } }
        public GUIContent lable { get { return _contentLable; } set { _contentLable = value; } }

        public ListView()
        {
            _items = new List<T>();

            _contentLable = new GUIContent("Items");
            _contentAddButton = new GUIContent("Add");
            _contentRemove = new GUIContent((Texture)AssetDatabase.LoadAssetAtPath("Assets/CreatorScript/Icons/Remove.png", typeof(Texture)));
            _contentUp = new GUIContent((Texture)AssetDatabase.LoadAssetAtPath("Assets/CreatorScript/Icons/Up.png", typeof(Texture)));
            _contentDown = new GUIContent((Texture)AssetDatabase.LoadAssetAtPath("Assets/CreatorScript/Icons/Down.png", typeof(Texture)));
        }

        public T this[int index]
        {
            get { return _items[index]; }
            set { _items[index] = value; }
        }

        public void OnGUI()
        {
            EditorGUILayout.Space();

            EditorGUILayout.LabelField(_contentLable);

            Rect rectListMethods = EditorGUILayout.BeginVertical();
            EditorGUI.DrawRect(rectListMethods, Color.white / 10.0f);

            EditorGUILayout.Space();

            //Button 'AddMessage' and show messages
            if (GUILayout.Button(_contentAddButton))
                onAddButton(_rectAddButton);

            if (Event.current.type == EventType.Repaint)
                _rectAddButton = GUILayoutUtility.GetLastRect();

            EditorGUILayout.Space();

            //List selected messages
            _scroll = EditorGUILayout.BeginScrollView(_scroll);

            for (int i = 0; i < _items.Count; i++)
            {
                //Item
                EditorGUILayout.BeginHorizontal(GUILayout.MaxHeight(25.0f));

                T item = _items[i];
                GUIItem(ref item);

                float w = 20.0f, h = 20.0f;
                if (GUILayout.Button(_contentUp, GUILayout.Width(w), GUILayout.Height(h)))
                {
                    SwapItem(i, i - 1);
                }

                if (GUILayout.Button(_contentDown, GUILayout.Width(w), GUILayout.Height(h)))
                {
                    SwapItem(i, i + 1);
                }

                //Button 'Remove' to remove the item from the list of items
                if (GUILayout.Button(_contentRemove, GUILayout.Width(w), GUILayout.Height(h)))
                    _items.RemoveAt(i);

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();
        }

        public void AddItem(T item)
        {
            _items.Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        private void SwapItem(int from, int to)
        {
            if (to < 0 || to >= _items.Count)
                return;

            T tmp = _items[from];
            _items[from] = _items[to];
            _items[to] = tmp;
        }
    }
}
