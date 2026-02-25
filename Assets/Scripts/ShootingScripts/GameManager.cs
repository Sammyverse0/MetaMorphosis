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
        for (int i = 0; i < numberOfTargets; i++)
        {
            Vector3 randomPos = Camera.main.transform.position +
                                Camera.main.transform.forward * Random.Range(1.5f, 2.5f) +
                                new Vector3(Random.Range(-0.5f, 0.5f),
                                            Random.Range(-0.5f, 0.5f),
                                            0);

            // Face camera
            Quaternion rotation = Quaternion.LookRotation(
                randomPos - Camera.main.transform.position
            );

            // Apply -90° Y correction
            rotation *= Quaternion.Euler(0f, -90f, 0f);

            Instantiate(targetPrefab, randomPos, rotation);
        }
    }
}