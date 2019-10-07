using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;

    // Followed object
    public GameObject target;

    [Range(0F, 1F)]
    public float speed;

    public bool isBoss;

    [HideInInspector]
    public float maxX;

    void Awake()
    {
        instance = this;

        if (!GetComponent<MenuCamera>())
            Cursor.visible = false;
    }

    void Start()
    {
        width = GetComponent<Camera>().orthographicSize * GetComponent<Camera>().aspect + PlayerMovements.instance.transform.localScale.x;
    }

    void Update()
    {
        // Fullscreen
        if (Input.GetKeyDown("f"))
                if (Screen.fullScreen)
            {
                Screen.fullScreen = false;
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            }
            else
            {
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                Screen.fullScreen = true;
            }
    }

    void LateUpdate()
    {
        // Compute new position
        float newX = Mathf.Lerp(transform.position.x, target.transform.position.x, speed),
            newY = Mathf.Lerp(transform.position.y, target.transform.position.y, speed);

        // Limits
        if (newX < 0F)
            newX = 0F;
        else if (newX > maxX)
            newX = maxX;

        if (newY < 0F)
            newY = 0F;

        // Update position
        transform.position = new Vector3(newX, newY, -10F);
    }

    void FixedUpdate()
    {
        // Check if player is beyond limits
        if (isBoss)
            if (PlayerMovements.instance.transform.position.x < transform.position.x - width ||
                PlayerMovements.instance.transform.position.x > transform.position.x + width)
                PlayerMovements.instance.Kill();
    }

    public void Shake(float force = .5f)
    {
        // Upwards
        float angle = Mathf.PI * .25f + Random.value * Mathf.PI * .5f;

        transform.position += new Vector3(
            Mathf.Cos(angle) * force, 
            Mathf.Sin(angle) * force, 
            -10F
        );
    }

    float width;
}
