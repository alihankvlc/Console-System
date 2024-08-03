using System.Collections.Generic;
using System.Linq;
using Debugging.Runtime.Core;
using UnityEngine;

namespace Debugging.Runtime.Sample.PlayerItem
{
    [System.Serializable]
    public class ItemData
    {
        public int Id;
        public string Name;
    }

    public class ItemDatabase : CommandMonoBehaviour
    {
        [SerializeField] private List<ItemData> _items = new();

        [Command("/addItem", "Adds an item to the database")]
        private void AddItem(int id, string name)
        {
            _items.Add(new ItemData
            {
                Id = id,
                Name = name
            });
        }

        public ItemData GetItem(int id)
        {
            return _items.FirstOrDefault(r => r.Id == id);
        }
    }
}