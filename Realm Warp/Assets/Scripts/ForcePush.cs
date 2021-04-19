using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ForcePush : MonoBehaviour
{
    GameObject player;
    Vector3 playerPos;

    float forceModifier;

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
            if (other.gameObject.GetComponent<Rigidbody>() != null)
            {
                playerPos = player.transform.position;
                var direction = (other.transform.position - playerPos).normalized;

                if (other.gameObject.CompareTag("Enemy"))
                {
                    other.gameObject.GetComponentInParent<Ragdoll>().ActivateRagdoll();
                    other.gameObject.GetComponentInParent<NavMeshAgent>().isStopped = true;
                    forceModifier = 8f;
                }
                else
                {
                    forceModifier = 15f;
                }

                other.gameObject.GetComponent<Rigidbody>().AddForce(forceModifier * direction * other.attachedRigidbody.mass, ForceMode.Impulse);
            }
        }
    }

}
