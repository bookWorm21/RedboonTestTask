using UnityEngine;

namespace Sources.RedboonTradeTask.GUI
{
    public class DropArea : MonoBehaviour
    {
        [SerializeField] private Transform _containerForDrop;

        public Transform ContainerForDrop => _containerForDrop;
    }
}