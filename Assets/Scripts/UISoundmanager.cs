using UnityEngine;

public class UISoundManager : MonoBehaviour
{
    public AudioSource uiSource;

    public void PlayPop()
    {
        uiSource.Play();
    }
}
