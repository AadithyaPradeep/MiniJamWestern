using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    public float rarity;
    public bool isUnique;
    public Rarity rar;
    public CardManager manager;

    private void Start()
    {
        manager = GameObject.Find("CardManager").GetComponent<CardManager>();
    }
    private void Update()
    {
        
        if (rar == Rarity.Common) rarity = manager.comRar;
        if (rar == Rarity.Rare) rarity = manager.rareRar;
        if (rar == Rarity.Epic) rarity = manager.epicRar;
        if (rar == Rarity.Legendary) rarity = manager.legRar;
        if (rar == Rarity.Boss) rarity = manager.bossRar;
       
    }
}
public enum Rarity
{
    Common,
    Rare,
    Epic,
    Legendary,
    Boss
}

