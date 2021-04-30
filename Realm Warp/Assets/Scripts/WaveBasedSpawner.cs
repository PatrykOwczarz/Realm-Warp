using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveBasedSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject[] powerups;
    public TextMeshProUGUI timerUI;
    public TextMeshProUGUI scoreUI;

    public TextMeshProUGUI defeatTimerUI;
    public TextMeshProUGUI defeatScoreUI;

    public Transform[] spawnPointsEnemies;
    public Transform[] spawnPointsPowerups;

    private int enemySpawn;
    private int previousEnemySpawn;

    private int currentEnemyCount = 3;
    private float timer;

    private bool canSpawnEnemies;
    private bool canSpawnPowerup;


    // Start is called before the first frame update
    void Start()
    {
        GameInformation.instance.SetEnemyCount(currentEnemyCount);
        canSpawnEnemies = true;
        canSpawnPowerup = true;
        GameInformation.instance.SetScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timerUI.text = "Time Survived: " + ((int)timer).ToString() + "s";
        scoreUI.text = "Score: " + GameInformation.instance.GetScore();

        if (GameInformation.instance.GetPlayer().GetComponent<Player>().GetIsDead())
        {
            defeatTimerUI.text = "Time Survived: " + ((int)timer).ToString() + "s";
            defeatScoreUI.text = "Score: " + GameInformation.instance.GetScore();
        }

        // (int)timer % 10 == 0 &&
        if (canSpawnEnemies)
        {
            for (int i = 0; i < GameInformation.instance.GetEnemyCount(); i++)
            {
                SpawnEnemy();
            }
            canSpawnEnemies = false;
        }

        // (int)timer % 15 == 0 && 
        if (canSpawnPowerup)
        {
            SpawnPowerup();
            canSpawnPowerup = false;
        }

        if (GameInformation.instance.GetEnemyCount() <= 0)
        {
            if (currentEnemyCount < 10)
            {
                currentEnemyCount += 1;
            }
            GameInformation.instance.SetEnemyCount(currentEnemyCount);
            canSpawnEnemies = true;
            canSpawnPowerup = true;
        }
       

        // to implement this, create a variable that instantiates an amount of enemies equal to the enemyCount variable;
        // instantiate one powerup every x seconds in the spawnPointPowerup locations
        // add score for each 15 seconds survived and for each enemy, store this in GameInformation singleton.
        // When player is defeated show time survived and score in the defeat screen and return to menu.
        // add assets to make this level look somewhat interesting,
        // remove current walls. Make it so that the player cant really get on top of anything.
    }

    private void SpawnEnemy()
    {

        while(previousEnemySpawn == enemySpawn)
        {
            enemySpawn = (int)Random.Range(0, spawnPointsEnemies.Length);
        }

        previousEnemySpawn = enemySpawn;
        Instantiate(enemyPrefab, spawnPointsEnemies[enemySpawn].position, Quaternion.identity);
    }

    private void SpawnPowerup()
    {
        var location = (int)Random.Range(0, spawnPointsPowerups.Length);
        var powerup = (int)Random.Range(0, powerups.Length);
        Instantiate(powerups[powerup], spawnPointsPowerups[location].position, Quaternion.identity);

    }
}
