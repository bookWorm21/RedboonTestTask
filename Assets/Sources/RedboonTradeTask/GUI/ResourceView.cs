using Sources.RedboonTradeTask.Core.Trading.InventoryLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.RedboonTradeTask.GUI
{
    public class ResourceView : MonoBehaviour
    {
        [SerializeField] private Text _countLabel;
        [SerializeField] private Image _icon;

        public void Init(KitItem kitItem)
        {
            _countLabel.text = kitItem.Count.ToString();
            _icon.sprite = kitItem.SourceItem.Icon;
        }
    }
}