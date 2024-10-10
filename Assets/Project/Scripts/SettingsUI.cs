using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Button resetScore;
    [SerializeField] private Button resetWorld;
    [SerializeField] private TMP_Text scoreText;

    private void Awake() {
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        resetScore.onClick.AddListener(() => scoreManager.resetScore());
        resetWorld.onClick.AddListener(() => SceneManager.LoadScene(0));

        scoreText.SetText("0");
        scoreManager.OnScoreChanged += (score) => scoreText.SetText(score.ToString());
    }
}
