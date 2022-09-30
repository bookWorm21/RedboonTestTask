using System.Collections.Generic;
using Sources.RedboonTradeTask.Core.Trading.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sources.RedboonTradeTask.GUI
{
    public class TradeVisualizationWindow : MonoBehaviour
    {
        [SerializeField] private InventoryView _playerInventoryView;
        [SerializeField] private InventoryView _traderInventoryView;
        [SerializeField] private WalletView _walletView;
        
        private ITradeService _traderService;

        private Draggable _currentDraggable;
        private DropArea _prevDropArea;
        
        public void Init(ITradeService traderService)
        {
            _traderService = traderService;
            
            _playerInventoryView.Init(traderService.Player.Inventory, true);
            _traderInventoryView.Init(traderService.Trader.Inventory, false);
            _walletView.Init(traderService.Player.Wallet);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var eventData = new PointerEventData(EventSystem.current)
                {
                    position = Input.mousePosition
                };
                var draggable = GetDraggable(eventData);
                if (draggable != null)
                {
                    StartDrag(draggable, eventData);
                }
            }
        }

        public void StartDrag(Draggable draggable, PointerEventData eventData)
        {
            if (_currentDraggable != null)
                return;
            
            _currentDraggable = draggable;
            _currentDraggable.OnStartDrag();
            _prevDropArea = GetDropArea(eventData);
            _currentDraggable.transform.SetParent(transform);
            _currentDraggable.Dragged += ProcessDrag;
            _currentDraggable.DragEnded += EndDrag;
        }

        public void ProcessDrag(PointerEventData evenData)
        {
            _currentDraggable.transform.position = evenData.position;
        }

        public void EndDrag(PointerEventData eventData)
        {
            if (_currentDraggable == null)
                return;

            DropArea currentDropArea = GetDropArea(eventData);
            if (currentDropArea == null || currentDropArea == _prevDropArea)
            {
                _currentDraggable.transform.SetParent(_prevDropArea.ContainerForDrop);
            }
            else
            {
                var inventoryView = currentDropArea.GetComponent<InventoryView>();
                var itemView = _currentDraggable.GetComponent<ItemView>();
                
                if (inventoryView.IsPlayerInventory)
                {
                    _currentDraggable.transform.SetParent(
                        _traderService.TryBuy(itemView.ItemModelDisplayed)
                        ? currentDropArea.ContainerForDrop
                        : _prevDropArea.ContainerForDrop);
                }
                else
                {
                    _currentDraggable.transform.SetParent(
                        _traderService.TrySell(itemView.ItemModelDisplayed)
                        ? currentDropArea.ContainerForDrop
                        : _prevDropArea.ContainerForDrop);
                }
                
                itemView.Refresh();
            }

            _currentDraggable.Dragged -= ProcessDrag;
            _currentDraggable.DragEnded -= EndDrag;
            _currentDraggable = null;
        }
        
        private DropArea GetDropArea(PointerEventData pointerEventData)
        {
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, results);
            
            foreach (var result in results)
            {
                var dropArea = result.gameObject.GetComponent<DropArea>();
                if (dropArea != null)
                {
                    return dropArea;
                }
            }

            return default;
        }

        private Draggable GetDraggable(PointerEventData pointerEventData)
        {
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, results);
            
            foreach (var result in results)
            {
                var droppable = result.gameObject.GetComponent<Draggable>();
                if (droppable != null && droppable.DragEnabled)
                {
                    return droppable;
                }
            }

            return default;
        }
    }
}