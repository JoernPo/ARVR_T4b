using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SpawnPos spawnPosScript;
    [SerializeField]
    public GameObject[] cuttables = new GameObject[1];
    public float speed = 0.1f;
    public int score = 0;
    public Text text;


    public IEnumerator StartGame(int amountOfCubes, float speedFactor, float spawnIntervall)
    {
        while (amountOfCubes > 0) {
            Debug.Log("Cubes left:" + amountOfCubes);
            spawnCube(0);
            amountOfCubes--;
            yield return new WaitForSeconds(spawnIntervall);
        }
    }

    IEnumerator moveCube(GameObject cube)
    {
        
        Vector3 target = new Vector3(cube.transform.position.x, cube.transform.position.y, -2);
        while (cube != null&& cube.transform.position.z < -2)
        {
            cube.transform.position = Vector3.MoveTowards(cube.transform.position, target, speed *Time.deltaTime);
            yield return null;
        }
        
    }


    void spawnCube(int index)
    {
        GameObject Cube = Instantiate(cuttables[0], spawnPosScript.GetARandomPos(), Quaternion.identity);
        StartCoroutine(moveCube(Cube));
    }

    public void changeScore(int add)
    {
        score += add;
        text.text = "Score:\n" + score;

    }


    public void remoteStart(int amount, float speed, float intervall)
    {
        StartCoroutine(StartGame(20, 1, 1));
    }

}
