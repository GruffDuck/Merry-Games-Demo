using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int value;
    [SerializeField]public Color pickUpColor;
    [SerializeField] Rigidbody pickUpRB;
    [SerializeField] Collider pickUpCollider;
    



    private void OnEnable()
    {
        PlayerController.Kick += MyKick;
    }

    private void MyKick(float forceSent)
    {
        transform.parent = null;
        pickUpCollider.enabled = true;
        pickUpRB.isKinematic = false;
        pickUpRB.AddForce(new Vector3(0, 200, forceSent));
    }

    private void OnDisable()
    {
        PlayerController.Kick -= MyKick;
    }
    // Start is called before the first frame update
    void Start()
    {
        Renderer rend = transform.GetChild(0).GetComponent<Renderer>();
        rend.material.SetColor("_Color", pickUpColor);
       
    }
    
    public Color GetColor()
    {
        return pickUpColor;
    }

    
}
