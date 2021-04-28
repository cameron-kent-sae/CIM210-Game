using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Governing Board Manager", menuName = "Scenario/Governing Board Manager")]

public class GoverningBoardManager : ScriptableObject
{
    public List<CharacterStats> characters;
}
