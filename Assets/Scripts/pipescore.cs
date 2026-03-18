using UnityEngine;

public class PipeScore : MonoBehaviour
{
    private bool hasScored = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasScored) return;

        if (collision.CompareTag("Player"))
        {
            hasScored = true;
            ScoreManager.instance.AddScore();

            // Disable collider after scoring
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
