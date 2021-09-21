using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bar : MonoBehaviour
{
    [SerializeField] Transform playerpos;
    [SerializeField] Transform endline;
    [SerializeField] Slider slider;
    float maxdistance;
    void Start()
    {
        maxdistance = getDistance();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerpos.position.z <= maxdistance && playerpos.position.z <= endline.position.z)
        {
            float distance = 1 - (getDistance() / maxdistance);
            setProgress(distance);
        }
    }
    float getDistance()
    {
        return Vector3.Distance(playerpos.position, endline.position);
    }
    void setProgress(float prop)
    {
        slider.value = prop;
    }
}
