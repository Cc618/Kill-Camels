using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float time;

    public bool stopped;

    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<Text>();

    }

    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopped)
        {
            time += Time.deltaTime;

            UpdateUI();
        }
    }

    void UpdateUI()
    {
        text.text = string.Format("{0:00000.00}", time);
    }

    Text text;
}
