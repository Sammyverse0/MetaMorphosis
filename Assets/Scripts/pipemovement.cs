using UnityEngine;

public class PipeMove : MonoBehaviour
{
    public float moveSpeed = 3f;

    void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }
}
