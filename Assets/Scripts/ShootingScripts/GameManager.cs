using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject targetPrefab;
    public int numberOfTargets = 3;

    public Transform worldAnchor;
    public GameObject crosshairUI;
    public Shooter shooter;

    public void StartGame()
    {
        // Lock anchor to camera position when game starts
        worldAnchor.position = Camera.main.transform.position +
                               Camera.main.transform.forward * 1.5f;

        worldAnchor.rotation = Quaternion.identity;

        SpawnTargets();

        crosshairUI.SetActive(true);
        shooter.EnableShooting();
    }

    void SpawnTargets()
    {
        for (int i = 0; i < numberOfTargets; i++)
        {
            Vector3 offset = new Vector3(
                Random.Range(-0.6f, 0.6f),
                Random.Range(-0.3f, 0.3f),
                Random.Range(0.8f, 1.2f)
            );

            Vector3 spawnPos = worldAnchor.position + offset;

            Quaternion rotation = Quaternion.LookRotation(
                spawnPos - Camera.main.transform.position
            );

            rotation *= Quaternion.Euler(0f, -90f, 0f);

            Instantiate(targetPrefab, spawnPos, rotation, worldAnchor);
        }
    }
}