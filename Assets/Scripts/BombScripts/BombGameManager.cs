using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BombGameManager : MonoBehaviour
{
    public float timeLeft = 20f;
    public Text timerText;
    public GameObject resultPanel;
    public Text resultText;
    public TMP_Text hintText;

    public GameObject explosionObject;

    public string correctWire = "Wire_Blue";

    private bool gameActive = false;

    void Update()
    {
        if (!gameActive) return;

        timeLeft -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Ceil(timeLeft).ToString();

        if (timeLeft <= 0)
        {
            Explode();
        }
    }

    public void StartGame()
    {   if(hintText != null)
        {
            hintText.gameObject.SetActive(true);
        }
        timeLeft = 20f;
        gameActive = true;
        resultPanel.SetActive(false);
    }

    public void CutWire(string wireName)
    {
        if (!gameActive) return;

        if (wireName == correctWire)
        {
            Defused();
        }
        else
        {
            Explode();
        }
    }

    void Defused()
    {
        gameActive = false;
        resultPanel.SetActive(true);
resultText.text = "Defused!";
        hintText.gameObject.SetActive(false);
        CompleteLevel();

    }

    void Explode()
    {
        gameActive = false;
        hintText.gameObject.SetActive(false);
        resultPanel.SetActive(true);
        resultText.text = "BOOM!";
        

        if (explosionObject != null)
        {
            ParticleSystem[] particles = explosionObject.GetComponentsInChildren<ParticleSystem>();

            foreach (ParticleSystem ps in particles)
            {
                ps.Play();
            }
        }
    }

    public void CompleteLevel()
    {
        int unlocked = PlayerPrefs.GetInt("LevelUnlocked", 1);

        if (unlocked < 2)
        {
            PlayerPrefs.SetInt("LevelUnlocked", 2);
        }

        SceneManager.LoadScene("LevelScene");
    }

}
