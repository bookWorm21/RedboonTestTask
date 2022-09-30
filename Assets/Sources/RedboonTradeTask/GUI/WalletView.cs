using System.Linq;
using Sources.RedboonTradeTask.Core.Trading.InventoryLogic;
using UnityEngine;

namespace Sources.RedboonTradeTask.GUI
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private ResourceView _resourceView;

        public Wallet Wallet { get; private set; }

        public void Init(Wallet wallet)
        {
            if (Wallet != null)
            {
                Wallet.Changed -= Refresh;
            }

            Wallet = wallet;
            Wallet.Changed += Refresh;
            
            Refresh();
        }

        private void Refresh()
        {
            _resourceView.Init(Wallet.Items.First());
        }
    }
}