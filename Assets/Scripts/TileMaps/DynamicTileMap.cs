using System;
using UnityEngine;
using UnityEngine.Tilemaps;

// Serves to spawn entities associated to tiles
public class DynamicTileMap : MonoBehaviour
{
    // To fetch a game object with a tile
    [Serializable]
    public struct Item
    {
        public TileBase tile;
        public GameObject obj;
    }

    // All items to replace
    public Item[] items;

    void Awake()
    {
        var map = GetComponent<Tilemap>();

        // Reset anchor
        transform.position = new Vector3(.5f, .5f);

        // Iterate through each possible position in the map
        foreach (var pos in map.cellBounds.allPositionsWithin)
        {
            // Pick the tile (can be null)
            TileBase tile = map.GetTile(pos);

            if (tile)
            {
                GameObject target = null;

                // Find the object associated with it
                foreach (Item i in items)
                    if (tile == i.tile)
                    {
                        target = i.obj;
                        break;
                    }

                if (target == null)
                    Debug.LogWarning("Tile not found with name " + tile.name);

                // Instantiate target
                Instantiate(target, map.CellToWorld(pos), Quaternion.identity);
            }
        }

        // Remove us
        Destroy(gameObject);
    }
}
