using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : PowerUpBehaviour
{
    // The index of the spell in the spell array of PlayerSpells
    public int id;

    protected override void OnPlayerCollect(GameObject player)
    {
        // Enable spell
        var spells = player.GetComponent<PlayerSpells>();
        spells.ActiveSpell(id);

        // Select it
        SpellsSelector.instance.MoveSelector(spells.currentSpell, id);
        spells.currentSpell = id;

        // Remove it
        base.OnPlayerCollect(player);

    }
}
