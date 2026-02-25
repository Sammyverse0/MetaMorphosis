using UnityEngine;
using Vuforia;

public class ARBombTrigger : MonoBehaviour
{
    public GameObject bombGame;
    public BombGameManager bombManager;

    private ObserverBehaviour observer;

    void Start()
    {
        observer = GetComponent<ObserverBehaviour>();
        observer.OnTargetStatusChanged += OnStatusChanged;

        bombGame.SetActive(false);
    }

    private void OnStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (status.Status == Status.TRACKED ||
            status.Status == Status.EXTENDED_TRACKED)
        {
            bombGame.SetActive(true);
            bombManager.StartGame();
        }
        else
        {
            bombGame.SetActive(false);
        }
    }
}
