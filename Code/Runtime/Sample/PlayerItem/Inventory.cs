using System.Collections.Generic;
using Debugging.Runtime.Core;
using UnityEngine;

namespace Debugging.Runtime.Sample.PlayerItem
{
    [System.Serializable]
    public class InventorySlot
    {
        public ItemData Data;
        public int Count;
    }

    public class Inventory : CommandMonoBehaviour
    {
        [SerializeField] private List<InventorySlot> _slots = new();
        [SerializeField] private ItemDatabase _database;

        [Command("/giveItem", "Adds an item to the inventory")]
        private void AddItemToInventory(int id, int count)
        {
            ItemData data = _database.GetItem(id);

            _slots.Add(new InventorySlot
            {
                Data = data,
                Count = count
            });
        }
    }
}