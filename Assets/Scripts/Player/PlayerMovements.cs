// Allow dev cheats
//#define USE_CHEATS

using UnityEngine;
using UnityEngine.SceneManagement;

// To control the player
public class PlayerMovements : MonoBehaviour
{
    // Singleton
    public static PlayerMovements instance;

    public GameObject texture;
    public GroundSensor groundSensor;

    [Range(0, 1)]
    public float textureSpeed;

    public float acceleration,
        maxSpeed,
        jumpForce;
    
    // To avoid high speed fall
    public float maxVelocityY;

    // No friction when moving
    public PhysicsMaterial2D noFriction,
        fullFriction;

    // Textures
    public Sprite idle,
        run0,
        run1,
        jumpUp,
        jumpDown;

    [Range(.02f, 1)]
    public float animSpeed;

    [HideInInspector]
    public SpriteRenderer sprite;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        sprite = texture.GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        hitBox = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
#if USE_CHEATS
        // Cheat
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey("r"))
                transform.position = new Vector3(0, 0);
            else if (Input.GetKey("space"))
                body.velocity = new Vector2(body.velocity.x, 50);
        }
#endif

        // Left
        if (Input.GetKey("a"))
        {
            hitBox.sharedMaterial = noFriction;

            // Limit
            if (body.velocity.x > -maxSpeed)
                body.velocity = new Vector2(body.velocity.x - acceleration * Time.deltaTime, body.velocity.y);
        }
        // Right
        else if (Input.GetKey("d"))
        {
            hitBox.sharedMaterial = noFriction;

            // Limit
            if (body.velocity.x < maxSpeed)
                body.velocity = new Vector2(body.velocity.x + acceleration * Time.deltaTime, body.velocity.y);
        }
        else
            // Brake
            hitBox.sharedMaterial = fullFriction;

        // To jump when grounded
        if (groundSensor.isOnGround && (Input.GetKeyDown("space") || Input.GetKeyDown("w")))
        {
            AudioManager.Play("small_jump");

            body.velocity = new Vector2(body.velocity.x, jumpForce);
        }

        if (Input.GetKeyDown("r"))
            Lives.instance.ReloadLevel();
        else if (Input.GetKeyDown("k"))
            Kill();
        else if (Input.GetKeyDown("m"))
            SceneManager.LoadScene("Menu");
    }

    void FixedUpdate()
    {
        // Textures
        if (groundSensor.isOnGround)
        {
            if (Mathf.Abs(body.velocity.x) > .1f)
                sprite.sprite = Time.time % animSpeed > animSpeed * .5f ? run0 : run1;
            else
                sprite.sprite = idle;
        }
        else
            sprite.sprite = body.velocity.y > 0 ? jumpUp : jumpDown;

        // Texture flip
        if (body.velocity.x < 0)
            sprite.flipX = true;
        else if (body.velocity.x > 0)
            sprite.flipX = false;

        // Max velocity
        if (body.velocity.y < maxVelocityY)
            body.velocity = new Vector2(body.velocity.x, maxVelocityY);

        // Fall
        if (transform.position.y < -10)
            Kill();

        // Quit
        if (Input.GetKeyDown("escape"))
            Application.Quit();

#if USE_CHEATS
        // Cheat
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey("x"))
            {
                body.gravityScale = 0;
                body.velocity = new Vector2(0, 0);
            }
        }
        else
            body.gravityScale = 1;
#endif
    }

    void LateUpdate()
    {
        // Compute new position
        float newX = Mathf.Lerp(texture.transform.position.x, transform.position.x, textureSpeed),
            newY = Mathf.Lerp(texture.transform.position.y, transform.position.y, textureSpeed);

        // Update position
        texture.transform.position = new Vector3(newX, newY, -1);
    }

    public void Kill()
    {
        AudioManager.Play("death");

        transform.position = new Vector3();
        body.velocity = new Vector2();
        sprite.gameObject.transform.position = new Vector3();

        Lives.instance.DecreaseLives();
    }

    // Make an air jump
    public void AirJump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpForce);
    }

    Rigidbody2D body;
    Collider2D hitBox;
}
