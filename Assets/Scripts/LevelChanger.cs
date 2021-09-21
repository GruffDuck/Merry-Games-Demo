using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public GameObject level1, level2;
    public void LevelS()
    {
        level1.SetActive(false);
        level2.SetActive(true);
    }
}
