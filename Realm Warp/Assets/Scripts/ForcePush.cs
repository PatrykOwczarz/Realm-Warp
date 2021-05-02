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
                    forceModifier = 6f;
                }
                else
                {
                    forceModifier = 15f;
                }

                Debug.Log(forceModifier);
                // applies a force in the forward facing direction of the player with an upwards component.
                var temp = Vector3.up * 0.2f;
                other.gameObject.GetComponent<Rigidbody>().AddForce(forceModifier * (direction + temp) * other.attachedRigidbody.mass, ForceMode.Impulse);
            }
        }
    }

}
