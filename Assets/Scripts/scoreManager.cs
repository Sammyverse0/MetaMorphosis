using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score = 0;
    public int maxScore = 30;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject winPanel;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("Assign ScoreText in Inspector!");
            return;
        }

        if (winPanel == null)
        {
            Debug.LogError("Assign WinPanel in Inspector!");
            return;
        }

        scoreText.text = "0";
        winPanel.SetActive(false);
    }

    public void AddScore()
    {
        score++;
        scoreText.text = score.ToString();

        if (score >= maxScore)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        Time.timeScale = 0f;
        winPanel.SetActive(true);
    }
}
