using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace stuartmillman.dissertation
{
    public class Inventory : MonoBehaviour
    {
        private readonly Dictionary<string, int> _inventory = new Dictionary<string, int>();

        public int GetTotalAmount()
        {
            int amount = 0;
            foreach (var pair in _inventory)
            {
                amount += pair.Value;
            }

            return amount;
        }

        public void Clear()
        {
            _inventory.Clear();
        }

        public bool Has(string itemName)
        {
            return _inventory.ContainsKey(itemName);
        }

        public int GetAmount(string itemName)
        {
            return !Has(itemName) ? 0 : _inventory[itemName];
        }

        public string[] GetItems()
        {
            return _inventory.Keys.ToArray();
        }

        public void Add(string itemName, int itemAmount)
        {
            if (Has(itemName))
            {
                _inventory[itemName] += itemAmount;
            }
            else
            {
                _inventory.Add(itemName, itemAmount);
            }
        }

        public bool Remove(string itemName, int itemAmount)
        {
            if (!Has(itemName))
                return false;

            if (_inventory[itemName] == itemAmount)
            {
                _inventory.Remove(itemName);
                return true;
            }
            else if (_inventory[itemName] >= itemAmount)
            {
                _inventory[itemName] -= itemAmount;
                return true;
            }

            return false;
        }
    }
}