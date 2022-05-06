using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class ItemSO : ScriptableObject
{
    public int price = 10;
    public int quantity = 3;
    public GameObject itemPrefab;

    public Sprite icon;
    public float iconSize = 1;

    public float cooldown = 1;
}