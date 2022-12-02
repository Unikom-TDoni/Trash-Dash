using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Group8.TrashDash.Core;
using Lnco.Unity.Module.Layout;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Group8.TrashDash.Level
{
    public sealed class LevelLayoutGroupItem : LayoutGroupItem<int>, IPointerClickHandler
    {
        [SerializeField]
        private Image[] _imgStars = default;

        [SerializeField]
        private TMP_Text _txtLevel = default;

        [SerializeField]
        private Image _imgBackground = default;

        [SerializeField]
        private Image _imgLock = default;

        [SerializeField]
        private Sprite _activeStarSprite = default;

        private int _itemLevelId = default;

        private CanvasGroup _canvasGroup = default;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button is not PointerEventData.InputButton.Left) return;
            GameManager.Instance.LevelHandler.SelectLevel(_itemLevelId);
            SceneManager.LoadScene(GameManager.Instance.Scenes.Gameplay);
        }

        public override void UpdateContent(int content)
        {
            _txtLevel.text = content.ToString();
            var levelEntity = GameManager.Instance.LevelHandler.GetLevelEntity(content);
            UpdateStarLayout(GameManager.Instance.LevelHandler.GetStarScoreLimit(content), levelEntity.HighScore);
            UpdateLockedLayout(levelEntity.Level != default);
            _itemLevelId = content;
        }

        private void UpdateStarLayout(float[] scoreStarLimit, float highScore)
        {
            var starCount = 0;
            foreach (var item in scoreStarLimit)
                if (highScore >= item) starCount++;
                else break;
            for (int i = 0; i < starCount; i++)
                _imgStars[i].sprite = _activeStarSprite;
        }

        private void UpdateLockedLayout(bool isOpened)
        {
            _canvasGroup.blocksRaycasts = isOpened;
            if (isOpened) return;
            foreach (var item in _imgStars)
                item.enabled = default;
            _imgLock.enabled = true;
            _txtLevel.enabled = default;
        }
    }
}