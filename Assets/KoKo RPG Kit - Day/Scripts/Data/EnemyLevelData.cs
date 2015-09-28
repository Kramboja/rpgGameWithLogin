using System.Collections.Generic;

/// <summary>
/// EnemyLevelData script. 
/// This is Used to Convert Excel Data file to Json File.
/// </summary>
[System.Serializable]
public class EnemyLevelData
{
    // Enemy Race Class.
    [System.Serializable]
    public class Race
    {
        // Race Name ( Slime, WildPig, Goblin )
        public string raceName;
        // Array to store all the enemy level data.
        public Attribute[] enemyData;
    }

    // Enemy game level data.
    [System.Serializable]
    public class Attribute
    {
        // level
        public int level;
        // HP 
        public int maxHP;
        // attack
        public float attack;
        // defence
        public float defence;
        // amount of exp when player killed this enemy.
        public int gainEXP;
        // walk speed.
        public float walkSpeed;
        // run speed applied when chasing player.
        public float runSpeed;
        // turn speed.
        public float turnSpeed;
        // attack range distance.
        public float attackRange;
        // amount of gold when player killed this enemy.
        public int gainGold;
    }
}