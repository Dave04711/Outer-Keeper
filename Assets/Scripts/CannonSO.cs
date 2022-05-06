using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cannon", menuName = "ScriptableObjects/Cannon")]
public class CannonSO : ScriptableObject
{
    public GameObject cannonPrefab;
    public int damage;
    public float speed;
    public float range;
    public int ammo;

    public List<CannonSO> upgrades;

    [Space] public GameObject iconPrefab;

    public Statistic damageU;
    public Statistic speedU;
    public Statistic rangeU;
    public Statistic ammoU;
}

[System.Serializable]
public class Statistic
{
    public int currentLvl;
    public LVL[] levels = new LVL[3];

    [System.Serializable]
    public class LVL
    {
        public int price;
        public float newValue;
    }
}

//public class StatLevel
//{
//    public int level;
//    int maxLevel;

//    public StatLevel(int _max)
//    {
//        level = 0;
//        maxLevel = _max;
//    }

//    public bool CheckLevel()
//    {
//        return level < maxLevel;
//    }

//    public override string ToString()
//    {
//        return $"{level}/{maxLevel}";
//    }
//}