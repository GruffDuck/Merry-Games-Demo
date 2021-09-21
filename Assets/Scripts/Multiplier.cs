using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    [SerializeField] float multiplierValue;

    [SerializeField] Color multiplierColor;
    [SerializeField] Renderer[] myRends;

    void Start()
    {
        SetColor();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name + " hit");
        if(collision.transform.tag == "Pickup")
        {
            GameController.instance.UpdateMultiplier(multiplierValue);
        }
    }

    void SetColor()
    {
        
        for (int i = 0; i < myRends.Length; i++)
        {
            myRends[i].material.SetColor("_Color", multiplierColor);
        }
    }
}
