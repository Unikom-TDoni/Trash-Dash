using UnityEngine;
using Group8.TrashDash.Event;
using Group8.TrashDash.TrashBin;
using Group8.TrashDash.Inventory;
using Group8.TrashDash.Score;
using UnityEngine.InputSystem;
using Group8.TrashDash.Core;
using Group8.TrashDash.Item.Audio;
using TMPro;

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

        [SerializeField]
        private TMP_Text _inventoryTitle = default;

        [SerializeField]
        private PlayerAudioController _playerAudioController = default;

        [SerializeField]
        private GameObject correctWrongPrefab;

        private void Awake()
        {
            if(GameManager.Instance)
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
            _inventoryHandler.RemoveItem(args.TrashContentInfo, args.InventoryLayoutGroupItem);
            var resultText = Instantiate(correctWrongPrefab);
            if (!_trashBinHandler.ActiveTrashBinType.Equals(args.TrashContentInfo.TrashBinType))
            {
                scoreManager.UpdateScore(ScoreState.Wrong);
                resultText.GetComponent<FloatingFadingText>().wrong.SetActive(true);
            }
            else
            {
                scoreManager.UpdateScore(ScoreState.Correct);
                _playerAudioController.PlaySuccessOnDropSfx();
                resultText.GetComponent<FloatingFadingText>().correct.SetActive(true);
            }
            _playerAudioController.PlayOnDropSfx();
        }

        private void OnInteract(TrashBinTypes args)
        {
            _inventoryTitle.text = "Trash Bin";
            _inventoryHandler.SetActiveInventory(true);
            scoreManager.ResetCombo();
        }

        public void OnInventory(InputAction.CallbackContext context)
        {
            _inventoryTitle.text = "Inventory";
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