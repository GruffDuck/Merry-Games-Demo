using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Color myColor;
    [SerializeField] Renderer[] myRends;
    public GameObject panel;
    [SerializeField] bool isPlaying;
    [SerializeField] float forwardSpeed;
    
    Rigidbody myRB;

    [SerializeField] float lerpSpeed;

    Transform parentPickup;
    [SerializeField] Transform stackPosition;

    bool atEnd;
    [SerializeField] float forwardForce;
    [SerializeField] float forceAdder;
    [SerializeField] float forceReducer;

        public static Action<float> Kick;

    // Start is called before the first frame update
    void Start()
    {
        SetColor(myColor);

        myRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlaying)
        {
            MoveForward();
        }

        if (atEnd)
        {
            forwardForce -= forceReducer * Time.deltaTime;
            if (forwardForce < 0)
                forwardForce = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (atEnd)
            {
                forwardForce += forceAdder;
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (atEnd)
                return;

            if(isPlaying == false)
            {
                isPlaying = true;
            }
            MoveSideways();
        }
        

        
    }

    void SetColor(Color colorIn)
    {
        myColor = colorIn;
        for(int i = 0; i <myRends.Length; i++)
        {
            myRends[i].material.SetColor("_Color", myColor);
        }
    }


    void MoveForward()
    {
        myRB.velocity = Vector3.forward * forwardSpeed;
    }

    void MoveSideways()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100))
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(hit.point.x, transform.position.y, transform.position.z), lerpSpeed *Time.deltaTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ColorWall")
        {
            SetColor(other.GetComponent<ColorWall>().GetColor());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "FinishingLineStart")
        {
            atEnd = true;
        }
        if(other.tag == "FinishingLineEnd")
        {
            myRB.velocity = Vector3.zero;
            isPlaying = false;
            Debug.Log(forwardForce + " forward force");
            LaunchStack();
            StartCoroutine(LevelC());

        }

        if (atEnd)
            return;
        
        //Debug.Log(other.gameObject);
        if(other.tag == "Pickup")
        {
            Transform otherTransform = other.transform.parent;
            if (myColor == otherTransform.GetComponent<Pickup>().GetColor())
            {
                Debug.Log("sa");
                GameController.instance.UpdateScore(otherTransform.GetComponent<Pickup>().value);
            }
            else
            {
                GameController.instance.UpdateScore(otherTransform.GetComponent<Pickup>().value * -1);
                other.gameObject.SetActive(false);
                if(parentPickup != null)
                {
                    if(parentPickup.childCount > 1)
                    {
                        parentPickup.position -= Vector3.up * parentPickup.GetChild(parentPickup.childCount - 1).localScale.y;
                        Destroy(parentPickup.GetChild(parentPickup.childCount - 1).gameObject);
                    }
                    else
                    {
                        Destroy(parentPickup.gameObject);
                    }
                }
                return;
            }

            Rigidbody otherRB = otherTransform.GetComponent<Rigidbody>();
            otherRB.isKinematic = true;
            otherRB.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            other.enabled = false;
            if(parentPickup == null)
            {
                parentPickup = otherTransform;
                parentPickup.position = stackPosition.position;
                parentPickup.parent = stackPosition;
               
            }
            else
            {
                
                parentPickup.position += Vector3.up * (otherTransform.localScale.y);
                otherTransform.position = stackPosition.position;
                otherTransform.parent = parentPickup;
            }
        }
    }
    private IEnumerator LevelC()
    {
        yield return new WaitForSeconds(8f);
        panel.SetActive(true) ;
    }

    void LaunchStack()
    {
        Camera.main.GetComponent<Camerafollow>().SetTarget(parentPickup);
        Kick(forwardForce);


    }
  
}
