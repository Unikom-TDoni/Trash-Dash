using DG.Tweening;
using Group8.TrashDash.Core;
using Group8.TrashDash.Score;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AfterGameUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private PanelUIManager panelUIManager;

    [Space]
    [SerializeField] private TMP_Text collectedTitle;
    [SerializeField] private TMP_Text sortedTitle;
    [SerializeField] private TMP_Text sortedWrongTitle;
    [SerializeField] private TMP_Text uncollectedTitle;

    [Space]
    [SerializeField] private TMP_Text collectedText;
    [SerializeField] private TMP_Text sortedText;
    [SerializeField] private TMP_Text sortedWrongText;
    [SerializeField] private TMP_Text uncollectedText;
    [SerializeField] private TMP_Text totalScoreText;

    [SerializeField] private Sprite starLitSprite;
    [SerializeField] private AudioClip _starAudioClip;

    [Header("Animated UI")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float fadeTime = 3f;
    [SerializeField] private float panelTime = 1f;
    [Space]
    [SerializeField] private List<GameObject> items = new List<GameObject>();
    [SerializeField] private float itemDelayTime = 2f;
    [SerializeField] private float itemTime = 1.5f;
    [SerializeField] private float nextItemTime = .25f;
    [Header("Buttons")]
    [SerializeField] private CanvasGroup buttonCanvasGroup;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private float buttonDelayTime = 3f;
    [Header("Text Counter")]
    [SerializeField] private int textFPS = 30;
    [SerializeField] private float textDuration = 3f;

    private Vector3 initialPos;
    private float[] scoreLimit;

    private void OnEnable()
    {
        SetResultUI(scoreManager.ResultData);
        initialPos = rectTransform.localPosition;
        PanelFadeIn();

        scoreLimit = GameManager.Instance.LevelHandler.GetStarScoreLimit();

        if (scoreManager.Score < GameManager.Instance.LevelHandler.GetStarScoreLimit()[0])
        {
            nextLevelButton.SetActive(false);
        }
        else
            GameManager.Instance.LevelHandler.SaveNextLevel();
    }

    private void SetResultUI(LevelResultData data)
    {
        collectedTitle.text += " (" + data.Collected + ")";
        sortedTitle.text += " (" + data.Sorted + ")";
        uncollectedTitle.text += " (" + data.Uncollected + ")";

        StartCoroutine(TextCounter(collectedText, 0, data.Collected * scoreManager.baseScore, " P"));
        StartCoroutine(TextCounter(sortedText, 0, data.SortedScore, " P"));
        StartCoroutine(TextCounter(sortedWrongText, 0, data.WrongSorted * scoreManager.wrongScore, " P"));
        StartCoroutine(TextCounter(uncollectedText, 0, data.Uncollected * scoreManager.uncollectedScore, " P"));
        StartCoroutine(TextCounter(totalScoreText, 0, data.TotalScore, " P"));
    }

    #region Animation
    private void PanelFadeIn()
    {
        canvasGroup.alpha = 0f;
        rectTransform.localPosition = new Vector3(0, -1000f, 0f);
        rectTransform.DOAnchorPos(initialPos, panelTime, false).SetEase(Ease.Linear).SetUpdate(true);
        canvasGroup.DOFade(1, fadeTime).SetUpdate(true);
        StartCoroutine(AnimateItems());
        StartCoroutine(ButtonFadeIn());
    }

    private void PanelFadeOut()
    {
        canvasGroup.alpha = 1f;
        rectTransform.localPosition = initialPos;
        rectTransform.DOAnchorPos(new Vector2(0, -1000f), panelTime, false).SetEase(Ease.Linear).SetUpdate(true);
        canvasGroup.DOFade(1, fadeTime).SetUpdate(true);
    }

    private IEnumerator ButtonFadeIn()
    {
        buttonCanvasGroup.alpha = 0f;
        yield return new WaitForSecondsRealtime(buttonDelayTime);
        buttonCanvasGroup.DOFade(1, fadeTime).SetUpdate(true);
    }

    private IEnumerator AnimateItems()
    {
        foreach (var item in items)
        {
            item.transform.localScale = Vector3.zero;
        }

        yield return new WaitForSecondsRealtime(itemDelayTime);

        for(int i = 0; i < items.Count; i++)
        {
            if(scoreManager.Score >= scoreLimit[i])
            {
                items[i].GetComponent<Image>().sprite = starLitSprite;
                AudioSource.PlayClipAtPoint(_starAudioClip, transform.position);
            }

            items[i].transform.DOScale(1, itemTime).SetEase(Ease.OutBounce).SetUpdate(true);
            yield return new WaitForSecondsRealtime(nextItemTime);
        }
    }

    private IEnumerator TextCounter(TMP_Text text, int startValue, int endValue, string additionalText = "")
    {
        bool condition = endValue < startValue;
        float temp = (endValue - startValue) / (textFPS * textDuration);
        int step = condition ? Mathf.FloorToInt(temp) : Mathf.CeilToInt(temp);
        int value = startValue;

        text.text = value.ToString() + additionalText;

        while (condition ? (endValue < value) : (value < endValue))
        {
            value += step;
            text.text = value.ToString() + additionalText;

            yield return new WaitForSecondsRealtime(1f / textFPS);
        }

        value = endValue;
        text.text = value.ToString() + additionalText;
    }
    #endregion
}
