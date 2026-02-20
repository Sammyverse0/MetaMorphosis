using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class ARGameStarter : MonoBehaviour
{
    ObserverBehaviour observer;

    void Start()
    {
        observer = GetComponent<ObserverBehaviour>();
        observer.OnTargetStatusChanged += OnStatusChanged;
    }

    void OnStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (status.Status == Status.TRACKED ||
            status.Status == Status.EXTENDED_TRACKED)
        {
            SceneManager.LoadScene("palakflappy");
        }
    }
}
