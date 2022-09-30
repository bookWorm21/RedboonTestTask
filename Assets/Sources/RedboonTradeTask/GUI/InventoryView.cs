using System.Collections.Generic;
using Sources.RedboonTradeTask.Core.Trading.InventoryLogic;
using UnityEngine;

namespace Sources.RedboonTradeTask.GUI
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private ItemView _templateView;

        private List<ItemView> _views = new List<ItemView>();
        public Inventory DisplayedInventory { get; private set; }

        public bool IsPlayerInventory { get; private set; }

        public void Init(Inventory inventory, bool isPlayerInventory)
        {
            ClearContainer();
            IsPlayerInventory = isPlayerInventory;
            DisplayedInventory = inventory;

            foreach (var item in inventory.Items)
            {
                var itemView = Instantiate(_templateView, _container);
                itemView.Init(item);
                _views.Add(itemView);
            }
        }

        private void ClearContainer()
        {
            foreach (var view in _views)
            {
                Destroy(view.gameObject);
            }

            _views.Clear();
        }
    }
}