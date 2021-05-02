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

        // updates the UI with the timer and score every frame.
        timerUI.text = "Time Survived: " + ((int)timer).ToString() + "s";
        scoreUI.text = "Score: " + GameInformation.instance.GetScore();

        if (GameInformation.instance.GetPlayer().GetComponent<Player>().GetIsDead())
        {
            defeatTimerUI.text = "Time Survived: " + ((int)timer).ToString() + "s";
            defeatScoreUI.text = "Score: " + GameInformation.instance.GetScore();
        }

        // controls the spawning of enemies when the enemy count is 0, hence every time a wave is completed.
        if (canSpawnEnemies)
        {
            for (int i = 0; i < GameInformation.instance.GetEnemyCount(); i++)
            {
                SpawnEnemy();
            }
            canSpawnEnemies = false;
        }

        // controls the spawning of powerups when the enemy count is 0, hence every time a wave is completed.
        if (canSpawnPowerup)
        {
            SpawnPowerup();
            canSpawnPowerup = false;
        }

        // when the enemy count is 0 or less, increment the enemy count until 10 and toggle the new wave.
        // each wave spawns the amount of enemies defined by the enemyCount variable from the GameInformation singleton and spawns one powerup.
        // The locations of the spawns is random based on the lists defined at the top of the script.
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
       
    }

    // instantiates an enemy and randomizes the spawn location out of the possible spawns.
    private void SpawnEnemy()
    {

        while(previousEnemySpawn == enemySpawn)
        {
            enemySpawn = (int)Random.Range(0, spawnPointsEnemies.Length);
        }

        previousEnemySpawn = enemySpawn;
        Instantiate(enemyPrefab, spawnPointsEnemies[enemySpawn].position, Quaternion.identity);
    }

    // instantiates a powerup in one of the predefined spawn locations.
    private void SpawnPowerup()
    {
        var location = (int)Random.Range(0, spawnPointsPowerups.Length);
        var powerup = (int)Random.Range(0, powerups.Length);
        Instantiate(powerups[powerup], spawnPointsPowerups[location].position, Quaternion.identity);

    }
}
