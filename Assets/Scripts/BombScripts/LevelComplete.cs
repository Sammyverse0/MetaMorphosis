using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public void CompleteLevel()
    {
        int unlocked = PlayerPrefs.GetInt("LevelUnlocked", 1);

        if (unlocked < 2)
        {
            PlayerPrefs.SetInt("LevelUnlocked", 2);
        }

        SceneManager.LoadScene("LevelSelect");
    }
}