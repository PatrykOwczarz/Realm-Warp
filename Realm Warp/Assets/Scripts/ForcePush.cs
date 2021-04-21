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

    // this script is put on a collider attached to the player.
    private void OnTriggerEnter(Collider other)
    {
        // do not collide with player.
        if (!other.gameObject.CompareTag("Player"))
        {
            // if the model has a rigidbody attached apply a force equal to the mass of the object * forceModifier * direction.
            // the direction is defined as the vector of the colliding object minus the player position.
            if (other.gameObject.GetComponent<Rigidbody>() != null)
            {
                playerPos = player.transform.position;
                var direction = (other.transform.position - playerPos).normalized;

                // if it collides with an enemy, alters the force modifier and activates ragdoll and disables the NavMeshAgent.
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
