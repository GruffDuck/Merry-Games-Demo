using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField] Text scoreDisplay;
    int score;

    float multiplierValue;

    [SerializeField] int nextSceneIndex;

    private void OnEnable()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void UpdateScore(int valueIn)
    {
        score += valueIn;
        scoreDisplay.text = score.ToString();
    }

    public void UpdateMultiplier(float valueIn)
    {
        if (valueIn <= multiplierValue)
            return;
        multiplierValue = valueIn;
        scoreDisplay.text = (score * multiplierValue).ToString(); 
    }
}
