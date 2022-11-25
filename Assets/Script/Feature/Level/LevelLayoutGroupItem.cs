using UnityEngine;
using UnityEngine.UI;
using Group8.TrashDash.Core;
using Lnco.Unity.Module.Layout;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Group8.TrashDash.Level
{
    public sealed class LevelLayoutGroupItem : LayoutGroupItem<LevelScriptableObject>, IPointerClickHandler
    {
        [SerializeField]
        private Image[] _imgStars = default;

        [SerializeField]
        private Image _imgBackground = default;

        private CanvasGroup _canvasGroup = default;

        private LevelScriptableObject _levelInfo = default;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button is not PointerEventData.InputButton.Left) return;
            GameManager.Instance.LevelInfo = _levelInfo;
            SceneManager.LoadScene(GameManager.Instance.Scenes.Gameplay);
        }

        public override void UpdateContent(LevelScriptableObject content)
        {
            var levelEntity = GameManager.Instance.GetLevelEntity(content.Level);
            UpdateStarLayout(content.ScoreStarLimit, levelEntity.HighScore);
            UpdateLockedLayout(levelEntity.IsOpened);
            _levelInfo = content;
        }

        private void UpdateStarLayout(float[] scoreStarLimit, float highScore)
        {
            var starCount = 0;
            foreach (var item in scoreStarLimit)
                if (highScore >= item) starCount++;
                else break;
            for (int i = 0; i < starCount; i++)
                _imgStars[i].color = Color.yellow;
        }

        private void UpdateLockedLayout(bool isOpened)
        {
            _canvasGroup.blocksRaycasts = isOpened;
            if (!isOpened) _imgBackground.color = Color.red;
        }
    }
}