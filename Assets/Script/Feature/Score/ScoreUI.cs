using UnityEngine;
using Group8.TrashDash.Score;
using TMPro;
using System.Collections;
using DG.Tweening;

public class ScoreUI : MonoBehaviour
{
    private ScoreManager scoreManager;

    [SerializeField]
    private TMP_Text scoreText;

    private int prevScore;
    private int textFPS = 30;
    [SerializeField]
    private float textDuration = .5f;

    private void Awake()
    {
        scoreManager = GetComponent<ScoreManager>();
        prevScore = scoreManager.Score;
        UpdateUI();
    }

    private void OnEnable()
    {
        if (scoreManager == null) return;
        scoreManager.OnScoreChange += UpdateUI;
    }

    private void OnDisable()
    {
        if (scoreManager == null) return;
        scoreManager.OnScoreChange -= UpdateUI;
    }

    private void UpdateUI()
    {
        StopAllCoroutines();
        StartCoroutine(TextCounter(scoreText, prevScore, scoreManager.Score, " P"));
        prevScore = scoreManager.Score;
    }

    private IEnumerator TextCounter(TMP_Text text, int startValue, int endValue, string additionalText = "")
    {
        if (startValue == endValue)
        {
            text.text = startValue.ToString() + additionalText;
            yield break;
        }

        bool condition = endValue < startValue;
        float temp = (endValue - startValue) / (textFPS * textDuration);
        int step = condition ? Mathf.FloorToInt(temp) : Mathf.CeilToInt(temp);
        int value = startValue;

        text.text = value.ToString() + additionalText;

        text.DOColor(condition ? Color.red : Color.green, textDuration / 2).SetEase(Ease.InOutFlash).SetLoops(2, LoopType.Yoyo);

        while (condition ? (endValue < value) : (value < endValue))
        {
            value += step;
            text.text = value.ToString() + additionalText;

            yield return new WaitForSecondsRealtime(1f / textFPS);
        }

        value = endValue;
        text.text = value.ToString() + additionalText;
    }
}
