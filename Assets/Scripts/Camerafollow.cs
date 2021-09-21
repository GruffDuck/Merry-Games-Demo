using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{

    [SerializeField] Transform target;
    float deltaZ;

    // Start is called before the first frame update
    void Start()
    {
        deltaZ = transform.position.z - target.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z + deltaZ);
    }

    public void SetTarget(Transform targetIn)
    {
        target = targetIn;
    }
}
