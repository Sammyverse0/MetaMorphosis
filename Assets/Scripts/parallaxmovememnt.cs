using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float scrollSpeed = 1f;

    void Update()
    {
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
    }
}
