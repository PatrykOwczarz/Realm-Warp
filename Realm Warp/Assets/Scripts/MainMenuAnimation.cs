using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimation : MonoBehaviour
{

    private float forceTime;
    private bool impulse;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        forceTime = 0.0f;
        impulse = false;
        rb.AddTorque(new Vector3(15f, 15f, 0f));
    }


    void FixedUpdate()
    {
        forceTime += Time.deltaTime * 2;
        //Debug.Log((int)forceTime % 10);

        if (transform.position.y < 2.0f)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * 2.5f, ForceMode.Impulse);
            
        }

        if((int)forceTime % 10 == 0)
        {
            int x = Random.Range(-10, 10);
            int y = Random.Range(-10, 10);
            int z = Random.Range(-10, 10);

            Vector3 torqueAmount = new Vector3(x,y,z);

            rb.AddTorque(torqueAmount);
        }
        
    }
}
