using UnityEngine;
using Group8.TrashDash.Score;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    private ScoreManager scoreManager;

    [SerializeField]
    private TMP_Text scoreText;

    private void Awake()
    {
        scoreManager = GetComponent<ScoreManager>();
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
        scoreText.text = scoreManager.Score.ToString() + " P";
    }
}
