using UnityEngine;
using UnityEngine.UI;

public class BombGameManager : MonoBehaviour
{
    public float timeLeft = 20f;
    public Text timerText;
    public GameObject resultPanel;
    //public ParticleSystem explosionEffect;

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
    {
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
        timerText.text = "Defused!";
    }

    void Explode()
    {
        gameActive = false;
        //explosionEffect.Play();
        resultPanel.SetActive(true);
        timerText.text = "BOOM!";
    }
}
