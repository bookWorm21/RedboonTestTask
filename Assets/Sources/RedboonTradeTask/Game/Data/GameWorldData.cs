using UnityEngine;

namespace Sources.RedboonTradeTask.Game.Data
{
    [CreateAssetMenu(fileName = "new game world data", menuName = "GameData/GameWorldData")]
    public class GameWorldData : ScriptableObject
    {
        [field: SerializeField] public PlayerData PlayerData { get; private set; }
        [field: SerializeField] public TraderData TraderData { get; private set; }
    }
}
