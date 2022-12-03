using Group8.TrashDash.Score;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AfterGameUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ScoreManager scoreManager;

    [Space]
    [SerializeField] private TMP_Text collectedText;
    [SerializeField] private TMP_Text sortedText;
    [SerializeField] private TMP_Text uncollectedText;
    [SerializeField] private TMP_Text totalScoreText;

    private void OnEnable()
    {
        SetResultUI(scoreManager.ResultData);
    }

    private void SetResultUI(LevelResultData data)
    {
        collectedText.text = data.Collected.ToString();
        sortedText.text = data.Sorted.ToString();
        uncollectedText.text = data.Uncollected.ToString();
        totalScoreText.text = data.TotalScore.ToString();
    }
}
