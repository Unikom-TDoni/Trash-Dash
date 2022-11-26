using UnityEngine;
using Group8.TrashDash.Score;

public class ScoreUI : MonoBehaviour
{
    private ScoreManager scoreManager;

    private void Awake()
    {
        scoreManager = GetComponent<ScoreManager>();
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
        Debug.Log("Current Score : " + scoreManager.Score);
    }
}
