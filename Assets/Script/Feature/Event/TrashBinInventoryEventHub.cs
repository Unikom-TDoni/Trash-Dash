using UnityEngine;
using Group8.TrashDash.Event;
using Group8.TrashDash.TrashBin;
using Group8.TrashDash.Inventory;
using Group8.TrashDash.Score;

namespace Group8.TrashDash.Coordinator
{
    public sealed class TrashBinInventoryEventHub : MonoBehaviour
    {
        [SerializeField]
        private ScoreManager scoreManager;

        [SerializeField]
        private TrashBinHandler _trashBinHandler = default;

        [SerializeField]
        private InventoryHandler _inventoryHandler = default;

        private void Awake()
        {
            _trashBinHandler.Subscribe(OnDrop, OnInteract);
            //Instantiate(GameManager.Instance.LevelInfo.Prefab, Vector3.zero, default);
        }

        private void OnDestroy()
        {
            _trashBinHandler.Unsubscribe(OnDrop, OnInteract);
        }

        public void OnDrop(DropableData args)
        {
            if (!_trashBinHandler.ActiveTrashBinType.Equals(args.TrashContentInfo.TrashBinType))
                scoreManager.UpdateScore(ScoreState.Wrong);
            else
                scoreManager.UpdateScore(ScoreState.Correct);
            _inventoryHandler.RemoveItem(args.TrashContentInfo, args.InventoryLayoutGroupItem);
        }

        public void OnInteract(TrashBinTypes args)
        {
            _inventoryHandler.SetActiveInventory(true);
            scoreManager.ResetCombo();
        }
    }
}