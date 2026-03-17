using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    public Button level1Button;
    public Button level2Button;

    void Start()
    {
        int unlockedLevel = PlayerPrefs.GetInt("LevelUnlocked", 1);

        level1Button.interactable = true;
        level2Button.interactable = unlockedLevel >= 2;
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("BombPuzzleLevel");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("ARGunLevel");
    }
}