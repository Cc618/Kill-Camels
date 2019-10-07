using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public string level;

    public void OnClick()
    {
        SceneManager.LoadScene(level);
    }
}
