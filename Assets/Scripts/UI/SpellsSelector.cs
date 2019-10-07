using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellsSelector : MonoBehaviour
{
    // Singleton
    public static SpellsSelector instance;

    // All items' sprites
    public Sprite[] items;

    public Material disableMaterial;

    // The offset where we begin to draw items
    public Vector2 offset;

    [Range(0, 200)]
    public float padding;

    // The template
    public GameObject spellTemplate;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        for (int i = 1; i < items.Length; ++i)
        {
            // Instantiate spell
            var spell = Instantiate(spellTemplate, transform);
            var r = spell.GetComponent<RectTransform>();

            // Set position
            r.localPosition = new Vector3(
                r.localPosition.x + i * padding,
                r.localPosition.y,
                r.localPosition.z
            );

            // Set sprite in Item child
            var img = spell.transform.GetChild(1).GetComponent<Image>();
            img.sprite = items[i];
            img.material = disableMaterial;

            // Disable selector
            spell.transform.GetChild(2).gameObject.SetActive(false);
        }

        // Disable first
        {
            // Set sprite in Item child
            var img = spellTemplate.transform.GetChild(1).GetComponent<Image>();
            img.material = disableMaterial;

            // Disable selector
            spellTemplate.transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    // Moves the light green selector
    // - from : Can be -1
    public void MoveSelector(int from, int to)
    {
        // Deactivate old selector
        if (from != -1)
        {
            var spell = transform.GetChild(1 + from).transform;
            var selector = spell.GetChild(2);

            selector.gameObject.SetActive(false);
        }

        // Just for identifier scope
        {
            // Activate new selector
            var spell = transform.GetChild(1 + to).transform;
            var selector = spell.GetChild(2);

            selector.gameObject.SetActive(true);
        }
    }
}
