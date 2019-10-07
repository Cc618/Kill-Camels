using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    public static Lives instance;

    public int lives = 5;

    void Awake()
    {
        instance = this;

        text = GetComponent<Text>();

        text.text = lives.ToString();
    }

    public void DecreaseLives()
    {
        --lives;

        // Reload level
        if (lives == 0)
            ReloadLevel();
        else
            text.text = lives.ToString();
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    Text text;
}
