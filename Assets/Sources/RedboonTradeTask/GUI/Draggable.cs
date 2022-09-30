using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sources.RedboonTradeTask.GUI
{
    public class Draggable : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        [SerializeField] private bool _dragEnabled = true;
        [SerializeField] private RectTransform _rectTransform;
        
        public bool DragEnabled => _dragEnabled;

        public RectTransform RectTransform => _rectTransform;
        
        public event Action<PointerEventData> Dragged;
        public event Action<PointerEventData> DragEnded;

        public void OnStartDrag()
        {
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            Dragged?.Invoke(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            DragEnded?.Invoke(eventData);
        }
    }
}