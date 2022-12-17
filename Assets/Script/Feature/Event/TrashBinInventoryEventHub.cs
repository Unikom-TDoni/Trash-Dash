using UnityEngine;
using Group8.TrashDash.Event;
using Group8.TrashDash.TrashBin;
using Group8.TrashDash.Inventory;
using Group8.TrashDash.Score;
using UnityEngine.InputSystem;
using Group8.TrashDash.Core;
using Group8.TrashDash.Item.Audio;
using TMPro;
using System.Collections.Generic;
using System.Linq;

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

        private List<FloatingFadingText> _activeTextStatusObj = new();

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
            _inventoryHandler.RemoveItem(args.TrashContentInfo, args.InventoryLayoutGroupItem);

            var obj = _activeTextStatusObj.FirstOrDefault(item => !item.gameObject.activeInHierarchy);

            if (obj is null)
            {
                obj = Instantiate(correctWrongPrefab).GetComponent<FloatingFadingText>();
                _activeTextStatusObj.Add(obj);
            }
            else
                obj.gameObject.SetActive(true);

            if (!_trashBinHandler.ActiveTrashBinType.Equals(args.TrashContentInfo.TrashBinType))
            {
                scoreManager.UpdateScore(ScoreState.Wrong);
                obj.wrong.SetActive(true);
            }
            else
            {
                scoreManager.UpdateScore(ScoreState.Correct);
                _playerAudioController.PlaySuccessOnDropSfx();
                obj.correct.SetActive(true);
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