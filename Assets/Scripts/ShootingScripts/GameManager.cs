using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject targetPrefab;
    public int numberOfTargets = 3;

    
    public GameObject crosshairUI;
    public Shooter shooter;

    private int targetsRemaining;
    public Transform imageTarget;

    public Transform targetContainer;
    public TMP_Text instructionText;

    void Start()
    {
        instructionText.text = "Click the gun to start game";
    }



    public void StartGame()
    {
        targetsRemaining = numberOfTargets;

        SpawnTargets();

        crosshairUI.SetActive(true);
        shooter.EnableShooting();

        instructionText.text = "Tap to shoot, shoot all targets";
    }

    void SpawnTargets()
    {
        float minDistance = 0.2f; // minimum distance between targets
        List<Vector3> usedPositions = new List<Vector3>();

        for (int i = 0; i < numberOfTargets; i++)
        {
            Vector3 spawnPos;
            bool validPosition = false;
            int attempts = 0;

            while (!validPosition && attempts < 20)
            {
                Vector3 localOffset = new Vector3(
                    Random.Range(-0.3f, 0.3f),
                    Random.Range(0.1f, 0.3f),
                    Random.Range(0.3f, 0.6f)
                );

                spawnPos = imageTarget.position + imageTarget.TransformDirection(localOffset);

                validPosition = true;

                foreach (Vector3 pos in usedPositions)
                {
                    if (Vector3.Distance(spawnPos, pos) < minDistance)
                    {
                        validPosition = false;
                        break;
                    }
                }

                if (validPosition)
                {
                    usedPositions.Add(spawnPos);

                    Quaternion rotation = Quaternion.LookRotation(
                        spawnPos - Camera.main.transform.position
                    );

                    rotation *= Quaternion.Euler(0f, -90f, 0f);

                    Instantiate(targetPrefab, spawnPos, rotation, targetContainer);
                }

                attempts++;
            }
        }
    }

    public void TargetDestroyed()
    {
        targetsRemaining--;

        if (targetsRemaining <= 0)
        {
            LevelComplete();
        }
    }

    void LevelComplete()
    {
        instructionText.text = "Level Complete!";
        int unlocked = PlayerPrefs.GetInt("LevelUnlocked", 1);

        if (unlocked < 2)
        {
            PlayerPrefs.SetInt("LevelUnlocked", 3);
        }

        Invoke("LoadLevelScene", 2f);
    }

    void LoadLevelScene()
    {
               SceneManager.LoadScene("LevelScene");

    }

}