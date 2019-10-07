using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class PlayerSpells : MonoBehaviour
{
    // To change its value
    public Slider manaSlider;

    // If = 1, the mana regenerates in 1 second
    [Range(0, 4)]
    public float manaRegenerationSpeed;

    [Range(0, 100)]
    public float spellJumpForce;

    public Material blackWhiteMaterial;

    public GameObject laser;

    [Range(0, 2)]
    public float wingsTime;

    public GameObject jumpParticles;
    public GameObject wingsParticles;
    public GameObject tpParticles;

    // Current selected spell index
    [HideInInspector]
    public int currentSpell = -1;

    void Awake()
    {
        spells = new bool[4] { false, false, false, false };
        spellCosts = new float[4]{ .5f, .5f, 1, 1 };

        body = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerMovements>();
        tpRayMask = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        // Update mana
        mana += manaRegenerationSpeed * Time.deltaTime;

        if (mana > 1)
            mana = 1;
        
        // If we have at least one spell available
        if (currentSpell != -1)
            // Test inputs
            if (Input.GetKeyDown("up"))
                // Check cost
                if (mana - spellCosts[currentSpell] >= 0)
                    ThrowSpell();

        // Select other spell
        if (Input.GetKeyDown("left"))
            ChangeSpell(false);
        else if (Input.GetKeyDown("right"))
            ChangeSpell(true);

        // Update UI
        manaSlider.value = mana;
    }

    // Active the spell at index i
    public void ActiveSpell(int i)
    {
        spells[i] = true;

        // Get the item of the target spell object in the selector
        var ui = SpellsSelector.instance.transform;
        var spell = ui.GetChild(1 + i).transform;
        var item = spell.GetChild(1);

        // Disable FX
        item.GetComponent<Image>().material = null;
    }

    void ThrowSpell()
    {
        // Return to cancel spell
        switch (currentSpell)
        {
            case SPELL_JUMP:
                // Only on ground
                if (!player.groundSensor.isOnGround)
                    return;

                AudioManager.Play("long_jump");

                body.velocity = new Vector2(body.velocity.x, spellJumpForce);

                // Add particles
                Instantiate(jumpParticles, transform);

                CameraFollow.instance.Shake(.5f);

                break;

            case SPELL_LASER:
                AudioManager.Play("laser");

                if (PlayerMovements.instance.sprite.flipX)
                {
                    var l = Instantiate(laser, transform.position + new Vector3(
                            -1.5f,
                            0,
                            0
                        ), Quaternion.identity)
                            .GetComponent<Laser>();

                    l.speed = -l.speed;
                }
                else
                    Instantiate(laser, transform.position + new Vector3(
                            1.5f,
                            0,
                            0
                        ), Quaternion.identity);

                break;

            case SPELL_WINGS:
                AudioManager.Play("wings");

                StartCoroutine(WingsCoroutine());
                
                // Add particles
                Instantiate(wingsParticles, transform);
                
                break;

            case SPELL_TP:
                AudioManager.Play("tp");

                // Particles
                Instantiate(tpParticles, transform.position, Quaternion.identity);

                RaycastHit2D r;
                
                if (PlayerMovements.instance.sprite.flipX)
                    r = Physics2D.Raycast(transform.position, new Vector2(
                            -1,
                            0
                        ), 4, tpRayMask);  
                else
                    r = Physics2D.Raycast(transform.position, new Vector2(
                            1,
                            0
                        ), 4, tpRayMask);

                // Tp on block
                if (r)
                    transform.position = new Vector3(
                        r.point.x,
                        r.point.y,
                        -1
                    );
                else
                    transform.position = transform.position + new Vector3(
                        PlayerMovements.instance.sprite.flipX ? -4 : 4,
                        0
                    );

                // Particles
                Instantiate(tpParticles, transform.position, Quaternion.identity);

                CameraFollow.instance.Shake(.5f);

                break;

            default:
                Debug.LogWarning("PlayerSpells.ThrowSpell -> WIP spell");
                break;
        }

        // Retrieve mana
        mana -= spellCosts[currentSpell];
    }

    // Move the selection
    void ChangeSpell(bool right)
    {
        // Can't change spell (no spell available)
        if (currentSpell == -1)
            return;

        // The result of the search
        int newSpell = -1;

        if (right)
        {
            // Search for valid spell untill the end of the array
            for (int i = currentSpell + 1; i < spells.Length; ++i)
                // We found a valid spell
                if (spells[i])
                {
                    newSpell = i;
                    break;
                }

            // If not found
            if (newSpell == -1)
                // Continue from the start
                for (int i = 0; i < currentSpell; ++i)
                    // We found a valid spell
                    if (spells[i])
                    {
                        newSpell = i;
                        break;
                    }
        }
        else
        {
            // Search for valid spell untill the end of the array
            for (int i = currentSpell - 1; i >= 0; --i)
                // We found a valid spell
                if (spells[i])
                {
                    newSpell = i;
                    break;
                }

            // If not found
            if (newSpell == -1)
                // Continue from the start
                for (int i = spells.Length - 1; i > currentSpell; --i)
                    // We found a valid spell
                    if (spells[i])
                    {
                        newSpell = i;
                        break;
                    }
        }

        if (newSpell == -1)
            // Can't change spell (only one available)
            return;

        SpellsSelector.instance.MoveSelector(currentSpell, newSpell);

        currentSpell = newSpell;
    }

    IEnumerator WingsCoroutine()
    {
        const float FACTOR = .33f;

        // Reverse gravity
        Physics2D.gravity = new Vector2(
            Physics2D.gravity.x,
            -Physics2D.gravity.y * FACTOR
        );

        yield return new WaitForSeconds(wingsTime);

        // Reverse gravity
        Physics2D.gravity = new Vector2(
            Physics2D.gravity.x,
            -Physics2D.gravity.y * (1f / FACTOR)
        );
    }

    // Spells indices
    const int
        SPELL_JUMP      = 0,
        SPELL_LASER     = 1,
        SPELL_WINGS     = 2,
        SPELL_TP        = 3;

    // The mana ratio
    float mana = 1;

    // All spells availabilities
    bool[] spells;
    
    // All costs for each spell
    float[] spellCosts;

    Rigidbody2D body;
    PlayerMovements player;
    int tpRayMask;
}
