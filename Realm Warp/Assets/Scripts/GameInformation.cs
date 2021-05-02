using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInformation : MonoBehaviour
{
    public static GameInformation instance;

    private static GameObject player;
    private bool realmWarp = false;
    private int score = 0;
    private int enemyCount = 0;

    private void Awake()
    {
        MakeSingleton();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // this video for reference on this implementation https://www.youtube.com/watch?v=Y6cKPfUTrsA
    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetPlayer(GameObject p)
    {
        player = p;
    }
    // a reference to the player in the current scene.
    public GameObject GetPlayer()
    {
        return player;
    }

    // a referece which allows to check if realm warp is active.
    public bool GetRealmWarp()
    {
        return realmWarp;
    }

    // a method which sets the state of realm warp.
    public void SetRealmWarp(bool realmWarp)
    {
        this.realmWarp = realmWarp;
    }

    // a reference to the score, used in the wave based survival mode.
    public int GetScore()
    {
        return score;
    }

    // a method to set the score.
    public void SetScore(int score)
    {
        this.score = score;
    }

    // a reference to the current enemy count in the level, only used in the wave based survival mode.
    public int GetEnemyCount()
    {
        return enemyCount;
    }

    // a method to update the enemy count.
    public void SetEnemyCount(int enemyCount)
    {
        this.enemyCount = enemyCount;
    }
}
