using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterBlock : MonoBehaviour
{
    
    public int amountOfCubes = 10;
    public float speedFactor = 1f;
    public float spawnrate = 1f;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public void startLevel()
    {
        gameManager.remoteStart(amountOfCubes, speedFactor, spawnrate);
    }
}
