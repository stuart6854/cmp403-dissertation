using UnityEngine;

namespace stuartmillman.dissertation.bt
{
    public class BTAgent : BaseAgent
    {
        [Header("Settings")]
        public Transform equipmentParent;

        public Transform storageParent;

        // [Header("Equipment/Storage")]
        // public Item equippedItem;

        public GameObject equippedItemObject;

        // public Item storedItem;
        public GameObject storedItemObject;

        private void Start()
        {
            // if (equippedItem != null)
            //     EquipItem(equippedItem);
            //
            // if (storedItem != null)
            //     StoreItem(storedItem);
        }

        /*public void EquipItem(Item item)
        {
            Destroy(equippedItemObject);

            equippedItem = item;
            equippedItemObject = Instantiate(item.itemPrefab, equipmentParent, false);
        }*/

        /*public void StoreItem(Item item)
        {
            Destroy(storedItemObject);

            storedItem = item;
            storedItemObject = Instantiate(item.itemPrefab, storageParent, false);
        }*/

        // public void UnstoreItem()
        // {
        //     Destroy(storedItemObject);
        //     storedItem = null;
        // }
    }
}