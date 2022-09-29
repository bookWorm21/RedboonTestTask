using System.Collections.Generic;
using Sources.RedboonTradeTask.Core.Trading;
using Sources.RedboonTradeTask.Core.Trading.Interfaces;
using Sources.RedboonTradeTask.Game.Data;
using Sources.RedboonTradeTask.Game.Factories;
using UnityEngine;

namespace Sources.RedboonTradeTask.Game
{
    public class GameStartup : MonoBehaviour
    {
        [SerializeField] private GameWorldData _gameWorldData;
        [SerializeField] private List<ItemData> _allItemDatas;

        private void Start()
        {
            var itemFactory = new ItemFactory(_allItemDatas);
            var playerFactory = new PlayerFactory(itemFactory);
            var traderFactory = new TraderFactory(itemFactory);

            var player = playerFactory.CreatePlayer(_gameWorldData.PlayerData);
            var trader = traderFactory.CreateTrader(_gameWorldData.TraderData);

            ITradeService tradeService = new TradeService(player, trader);
        }
    }
}