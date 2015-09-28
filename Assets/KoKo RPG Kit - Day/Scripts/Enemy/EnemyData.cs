using UnityEngine;
using System.Collections;

/// <summary>
/// EnemyData script.
/// Enemy's fsm state scripts use this data.
/// </summary>
public class EnemyData : MonoBehaviour
{
    // EnemyType enum to distingush 3 types Enemies.
    public enum EnemyType
    {
        Slime, WildPig, Goblin
    }

    public EnemyType enemyType = EnemyType.Slime;

    // Enemy Level Data.
    public EnemyLevelData.Attribute levelData;
    public int level = 1;
    public float disableTime = 2f;
    public float deadTime = 3f;
    public float respawnTime = 4f;
    public int currentHP = 30;
    
    public Transform waypoint;
    public Transform[] waypoints;

    public PlayerFSMManager playerFSM;

    void Awake()
    {
        // initialize data.
        levelData = DataManager.Instance.GetEnemyCurrentLevelData((int)enemyType, level - 1);
        currentHP = levelData.maxHP;

        // Get Waypoint's Transform Components as array.
        waypoints = waypoint.GetComponentsInChildren<Transform>();

        // Find PlayerFSMManager script reference.
        playerFSM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFSMManager>();
    }

    void OnEnable()
    {
        currentHP = levelData.maxHP;
    }
}