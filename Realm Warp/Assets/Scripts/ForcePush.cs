using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePush : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameInformation.instance.GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Implement force on the collider from the player position.
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            //other.gameObject.GetComponent<Rigidbody>().AddForce();

        }
    }

}
