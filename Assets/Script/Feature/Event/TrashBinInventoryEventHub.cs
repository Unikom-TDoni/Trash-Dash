using UnityEngine;
using Group8.TrashDash.Event;
using Group8.TrashDash.TrashBin;
using Group8.TrashDash.Inventory;
using Group8.TrashDash.Score;
using UnityEngine.InputSystem;
using Group8.TrashDash.Core;

namespace Group8.TrashDash.Coordinator
{
    public sealed class TrashBinInventoryEventHub : MonoBehaviour
    {
        private PlayerAction _playerAction = default;

        [SerializeField]
        private ScoreManager scoreManager;

        [SerializeField]
        private TrashBinHandler _trashBinHandler = default;

        [SerializeField]
        private InventoryHandler _inventoryHandler = default;

        private void Awake()
        {
            GameManager.Instance.LevelHandler.SpawnLevel();
            _trashBinHandler.OnAwake(OnDrop, OnInteract);
        }

        private void Start()
        {
            _playerAction = InputManager.playerAction;
            RegisterInputCallback();
        }

        private void OnDestroy()
        {
            UnregisterInputCallback();
            _trashBinHandler.Unsubscribe(OnDrop, OnInteract);
        }

        private void OnDrop(DropableData args)
        {
            if (!_trashBinHandler.ActiveTrashBinType.Equals(args.TrashContentInfo.TrashBinType))
                scoreManager.UpdateScore(ScoreState.Wrong);
            else
            {
                scoreManager.UpdateScore(ScoreState.Correct);
                _inventoryHandler.RemoveItem(args.TrashContentInfo, args.InventoryLayoutGroupItem);
            }
        }

        private void OnInteract(TrashBinTypes args)
        {
            _inventoryHandler.SetActiveInventory(true);
            scoreManager.ResetCombo();
        }

        private void OnInventory(InputAction.CallbackContext context)
        {
            _inventoryHandler.SetActiveInventory(true);
            _trashBinHandler.SetActiveTrashBinLayout(default);
        }

        private void OnInventoryPanel(InputAction.CallbackContext context)
        {
            _inventoryHandler.SetActiveInventory(default);
        }

        private void RegisterInputCallback()
        {
            if (_playerAction is null) return;
            _playerAction.Gameplay.Inventory.performed += OnInventory;
            _playerAction.Panel.Cancel.performed += OnInventoryPanel;
        }

        private void UnregisterInputCallback()
        {
            if (_playerAction is null) return;
            _playerAction.Gameplay.Inventory.performed -= OnInventory;
            _playerAction.Panel.Cancel.performed -= OnInventoryPanel;
        }
    }
}