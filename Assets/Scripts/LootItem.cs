using UnityEngine;

[System.Serializable]
public class LootItem
{
    public string itemName;
    public float dropProbability;
    public GameObject itemPrefab; // Prefab del power-up
}
