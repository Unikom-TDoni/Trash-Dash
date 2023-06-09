using UnityEngine;
using Group8.TrashDash.Core;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Group8.TrashDash.Coordinator
{
    public sealed class LevelSelectorCoordinator : MonoBehaviour
    {
        private PlayerAction _input;

        private void OnEnable()
        {
            _input = new();
            _input.Enable();
            _input.Panel.Cancel.performed += PerformedEvent;
        }

        private void OnDisable()
        {
            _input.Disable();
            _input.Panel.Cancel.performed -= PerformedEvent;
        }

        private void PerformedEvent(InputAction.CallbackContext context)
        {
            SceneManager.LoadScene(GameManager.Instance.Scenes.MainMenu);
        }
    }
}