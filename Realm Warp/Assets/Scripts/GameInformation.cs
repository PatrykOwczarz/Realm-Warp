using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInformation : MonoBehaviour
{
    public static GameInformation instance;

    private GameObject player;
    private bool realmWarp = false;

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

    public GameObject GetPlayer()
    {
        return player;
    }

    public bool GetRealmWarp()
    {
        return realmWarp;
    }

    public void SetRealmWarp(bool realmWarp)
    {
        this.realmWarp = realmWarp;
    }

}
