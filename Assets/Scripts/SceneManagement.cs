using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{

    public void Levels(string s)
    {
        SceneManager.LoadScene(s);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    
}
