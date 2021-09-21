using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorWall : MonoBehaviour
{
    [SerializeField] Color newColor;

    // Start is called before the first frame update
    void Start()
    {
        Color tempColor = newColor;
        tempColor.a = .5f;
        Renderer rend = transform.GetChild(0).GetComponent<Renderer>();
        rend.material.SetColor("_Color", tempColor);
    }

    public Color GetColor()
    {
        return newColor;
    }

}
