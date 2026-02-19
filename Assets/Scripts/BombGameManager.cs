using UnityEngine;
using UnityEngine.UI;

public class BombGameManager : MonoBehaviour
{
    public float timeLeft = 20f;
    public Text timerText;
    public GameObject resultPanel;
    public Text resultText;

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
resultText.text = "Defused!";

    }

    void Explode()
    {
        gameActive = false;
        timerText.text = "BOOM!";
        resultPanel.SetActive(true);

        if (explosionObject != null)
        {
            ParticleSystem[] particles = explosionObject.GetComponentsInChildren<ParticleSystem>();

            foreach (ParticleSystem ps in particles)
            {
                ps.Play();
            }
        }
    }

}
