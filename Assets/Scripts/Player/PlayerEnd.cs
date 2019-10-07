using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerEnd : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.name == "Menu Sensor")
            SceneManager.LoadScene("Menu");
    }
}
