using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "Character Stats/Player Stats")]

public class PlayerStats : ScriptableObject
{
    public string characterName = "James";

    public Sprite playerSprite;

    public float influenceLevel = 1;
    [Range(1, 3)] public int influencemultiplier = 1;
    public float conditionLevel = 40;
}
