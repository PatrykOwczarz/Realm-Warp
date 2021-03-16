using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimation : MonoBehaviour
{

    private float forceTime;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        forceTime = 10.0f;
        //rb.AddTorque(new Vector3(15f, 15f, 0f));
    }


    void FixedUpdate()
    {
        forceTime += Time.deltaTime * 2;

        if (transform.position.y < 2.0f)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * 2.5f, ForceMode.Impulse);
            
        }

        if (forceTime > 10f)
        {
            int x = Random.Range(-20, 20);
            int y = Random.Range(-20, 20);
            int z = Random.Range(-20, 20);

            Vector3 torqueAmount = new Vector3(x, y, z);

            rb.AddTorque(torqueAmount);

            forceTime = 0f;
        }

    }
}
