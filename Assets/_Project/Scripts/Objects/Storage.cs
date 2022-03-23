using System;
using System.Collections.Generic;
using UnityEngine;

namespace stuartmillman.dissertation
{
    public class Storage : WorldObject
    {
        private readonly Dictionary<string, int> _items = new Dictionary<string, int>();

        public override bool Interact()
        {
            IsInteracting = true;
            IsInteractionComplete = true;

            return true;
        }

        public override void CancelInteract()
        {
        }

        public bool HasInStorage(string name)
        {
            return _items.ContainsKey(name);
        }

        public int QueryAmount(string name)
        {
            if (!HasInStorage(name))
                return -1;

            return _items[name];
        }

        public void AddToStorage(string name, int amount)
        {
            if (_items.ContainsKey(name))
            {
                _items[name] += amount;
            }
            else
            {
                _items.Add(name, amount);
            }
        }

        public int RemoveFromStorage(string name, int amount)
        {
            if (!HasInStorage(name))
                return 0;

            if (_items[name] < amount)
            {
                var amnt = _items[name];
                _items.Remove(name);
                return amnt;
            }

            _items[name] -= amount;
            return amount;
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical("Storage");
            GUILayout.Label("Storage Contents");
            if (_items.Count == 0)
            {
                GUILayout.Label("Empty");
            }
            else
            {
                foreach (var pair in _items)
                {
                    GUILayout.Label(pair.Key + ": " + pair.Value);
                }
            }

            GUILayout.EndVertical();
        }
    }
}