using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RealmWarp : MonoBehaviour
{
    private GameObject[] lightRef;
    public Color baseColor;

    public Slider slider;
    public TextMeshProUGUI amountText;

    private bool isWarped = false;

    private float timer = 0.0f;
    private float warpDuration = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        lightRef = GameObject.FindGameObjectsWithTag("Light");
    }

    // Update is called once per frame
    void Update()
    {
        // if GetRealmWarp() returns true from the GameInformation singleton script, activate warp and set the duration of realm warp equal to the warpDuration variable.
        if (GameInformation.instance.GetRealmWarp() && !isWarped)
        {
            ActivateWarp();
            slider.gameObject.SetActive(true);
            slider.maxValue = warpDuration;
            timer = warpDuration;
            isWarped = true;
        }

        // if warp is on, reduce the timer and when the timer reaches 0, deactivate the warp and set the realm warp variable to false in GameInformation.
        if (isWarped)
        {
            timer -= Time.deltaTime;
            slider.value = timer;
            amountText.text = ((int)timer).ToString();
            if (timer < 0.0f)
            {
                DeactivateWarp();
                slider.gameObject.SetActive(false);
                isWarped = false;
                GameInformation.instance.SetRealmWarp(false);
            }
        }

        // For testing purposes:
        /*
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameInformation.instance.SetRealmWarp(true);
        }
        */

    }

    // changes the colour of all point lights to blue and changes the colour of the directional light to magenta.
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

    // resets the colours of all lights to the standard light colour which is defined in the base colour variable.
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
