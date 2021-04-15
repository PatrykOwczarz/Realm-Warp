using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealmWarp : MonoBehaviour
{
    private GameObject[] lightRef;
    public Color baseColor;

    private bool isWarped = false;

    private float timer = 0.0f;
    private float warpDuration = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        lightRef = GameObject.FindGameObjectsWithTag("Light");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameInformation.instance.GetRealmWarp() && !isWarped)
        {
            ActivateWarp();
            timer = warpDuration;
            isWarped = true;
        }

        if (isWarped)
        {
            timer -= Time.deltaTime;
            if (timer < 0.0f)
            {
                DeactivateWarp();
                isWarped = false;
                GameInformation.instance.SetRealmWarp(false);
            }
        }

        // For testing purposes:
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateWarp();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DeactivateWarp();
        }

    }

    public void ActivateWarp()
    {
        Debug.Log("Activated Warp");

        foreach (var light in lightRef)
        {
            if (light.GetComponent<Light>().type == LightType.Point)
            {
                light.GetComponent<Light>().color = Color.blue;
            }
            else if (light.GetComponent<Light>().type == LightType.Directional)
            {
                light.GetComponent<Light>().color = Color.magenta;
            }
        }
    }

    public void DeactivateWarp()
    {
        Debug.Log("Deactivated Warp");

        foreach (var light in lightRef)
        {
            if (light.GetComponent<Light>().type == LightType.Point)
            {
                light.GetComponent<Light>().color = baseColor;
            }
            else if (light.GetComponent<Light>().type == LightType.Directional)
            {
                light.GetComponent<Light>().color = baseColor;
            }
        }
    }

}
