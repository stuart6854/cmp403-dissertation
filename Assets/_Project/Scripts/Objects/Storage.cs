using System;
using System.Collections.Generic;
using UnityEngine;

namespace stuartmillman.dissertation
{
    public class Storage : WorldObject
    {
        private readonly Dictionary<string, int> _items = new Dictionary<string, int>();

        public override bool Interact(GameObject user)
        {
            IsInteracting = true;
            IsInteractionComplete = true;
            User = user;

            return true;
        }

        public override void CancelInteract()
        {
        }

        public bool HasInStorage(string itemName)
        {
            return _items.ContainsKey(itemName);
        }

        public int QueryAmount(string itemName)
        {
            if (!HasInStorage(itemName))
                return -1;

            return _items[itemName];
        }

        public void AddToStorage(string itemName, int amount)
        {
            if (_items.ContainsKey(itemName))
            {
                _items[itemName] += amount;
            }
            else
            {
                _items.Add(itemName, amount);
            }
        }

        public int RemoveFromStorage(string itemName, int amount)
        {
            if (!HasInStorage(itemName))
                return 0;

            if (_items[itemName] <= amount)
            {
                var amnt = _items[itemName];
                _items.Remove(itemName);
                return amnt;
            }

            _items[itemName] -= amount;
            return amount;
        }

        private void OnGUI()
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.magenta;

            GUILayout.BeginVertical();
            GUILayout.Label("Storage Contents", style);
            if (_items.Count == 0)
            {
                GUILayout.Label("Empty", style);
            }
            else
            {
                foreach (var pair in _items)
                {
                    GUILayout.Label(pair.Key + ": " + pair.Value, style);
                }
            }

            GUILayout.EndVertical();
        }
    }
}