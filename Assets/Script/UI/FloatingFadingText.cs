using TMPro;
using UnityEngine;

public class FloatingFadingText : MonoBehaviour
{
    public GameObject correct, wrong;

    private TextMeshProUGUI _textCorrect = default;

    [SerializeField] float duration;

    private float actualDuration = default;

    private string[] text =
    {
        "Nice!",
        "Good!",
        "Great!",
        "Correct!",
        "Awesome!",
        "Nice Job!",
        "Excellent!",
    };

    private void Awake() =>
        _textCorrect = correct.GetComponent<TextMeshProUGUI>();

    private void OnEnable()
    {
        actualDuration = duration;
        _textCorrect.text = text[Random.Range(0, text.Length - 1)];
    }

    void Update()
    {
        if (actualDuration > 0)
            actualDuration -= Time.deltaTime;
        else
        {
            wrong.SetActive(default);
            correct.SetActive(default);
            gameObject.SetActive(default);
        }
    }
}