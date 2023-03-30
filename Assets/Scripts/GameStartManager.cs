using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    public static bool isGameStarted = false;

    [SerializeField] private LeafSpawner spawner;
    [SerializeField] private GameObject startScreen;


    public void StartGame()
    {
        isGameStarted = true;  
        spawner.StartLeafSpawn();
        startScreen.SetActive(false);
    }
}
