using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    private GameObject player;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameInformation.instance.GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            if (timer < 0.0f)
            {
                player.GetComponent<Player>().TakeDamage(20);
                timer = 0.5f;
            }
            
        }
    }
}
