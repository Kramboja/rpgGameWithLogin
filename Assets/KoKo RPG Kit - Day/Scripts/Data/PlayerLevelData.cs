using System.Collections.Generic;

/// <summary>
/// PlayerLevelData script. 
/// This is Used to Convert Excel Data file to Json File.
/// </summary>
[System.Serializable]
public class PlayerLevelData
{
    // Player game level data
    [System.Serializable]
    public class Attribute
    {
        // player level
        public int level;
        // hp
        public int maxHP;
        // base attack.
        public float baseAttack;
        // required exp to get this level.
        public int reqEXP;
        // move speed.
        public float moveSpeed;
        // turn speed.
        public float turnSpeed;
        // attack range.
        public float attackRange;
        // skill attack range.
        public float skillAttackRange;
        // skill attack.
        public float skillAttack;
    }
}