using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void scan()
    {
        SceneManager.LoadScene("ARScene");
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    
}
