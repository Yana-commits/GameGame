using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
[CreateAssetMenu(menuName = "Level repository")]
public class LevelRepository : ScriptableObject
{
    public List<Level> LevelList = new List<Level>();
}

[Serializable]
public class Level
{
    public string id;

   public ChipField[] platforms;

   public int enemyNomber;

    public Color levelColor;
}
