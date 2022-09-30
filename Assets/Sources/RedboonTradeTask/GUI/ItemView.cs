using System.Linq;
using Sources.RedboonTradeTask.Core.Trading.InventoryLogic.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.RedboonTradeTask.GUI
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Text _priceLabel;

        public ItemModel ItemModelDisplayed { get; private set; }

        public void Init(ItemModel itemModel)
        {
            ItemModelDisplayed = itemModel;

            _icon.sprite = itemModel.Icon;
            Refresh();
        }

        public void Refresh()
        {
            _priceLabel.text = ItemModelDisplayed.Price.NeedItems.First().Count.ToString();
        }
    }
}