using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject targetPrefab;
    public int numberOfTargets = 3;

    public void StartGame()
    {
        SpawnTargets();
    }

    void SpawnTargets()
    {
        Camera cam = Camera.main;

        for (int i = 0; i < numberOfTargets; i++)
        {
            float randomX = Random.Range(0.3f, 0.7f);
            float randomY = Random.Range(0.3f, 0.7f);
            float distance = 2f;

            Vector3 viewportPos = new Vector3(randomX, randomY, distance);
            Vector3 worldPos = cam.ViewportToWorldPoint(viewportPos);

            Quaternion rotation = Quaternion.LookRotation(
                worldPos - cam.transform.position
            );

            rotation *= Quaternion.Euler(0f, -90f, 0f);

            GameObject target = Instantiate(targetPrefab, worldPos, rotation);

            target.transform.SetParent(null); // make sure it's not parented accidentally
        }
    }
}