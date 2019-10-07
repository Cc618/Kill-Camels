using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score instance;

    //public static void AddScore

    [HideInInspector]
    public uint score
    {
        set
        {
            _score = value;

            // Format with padding
            text.text = _score.ToString("D8");
        }

        get
        {
            return _score;
        }
    }

    void Awake()
    {
        instance = this;

        text = GetComponent<Text>();

        // Update UI
        score = _score;
    }

    static uint _score;
    
    Text text;
}
