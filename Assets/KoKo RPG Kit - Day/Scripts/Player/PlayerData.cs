using UnityEngine;
using System.Collections;

/// <summary>
/// PlayerData script.
/// Player's fsm state script use this data.
/// </summary>
public class PlayerData : MonoBehaviour
{
    public Transform moveMarker;
    public Transform attackMarker;

    // Player Level Data.
    public PlayerLevelData.Attribute levelData;

    // Player game data controlled at runtime.
    public int level = -1;
    public int currentHP = 100;
    public int exp = 0;
    public int gold = 0;

    public EnemyFSMManager enemyFSM;

    private int _levelIndex = 0;

    // Initialization.
    void Awake()
    {
        // Level check.
        CheckLevel();
        // get player level data after level check.
        levelData = DataManager.Instance.GetPlayerCurrentLevelData(level - 1);
    }

    void OnEnable()
    {   
        currentHP = levelData.maxHP;
    }

    public void CheckLevel()
    {
        // Store max level data and max level.
        PlayerLevelData.Attribute maxLevelData = DataManager.Instance.playerLevelData[DataManager.Instance.playerLevelData.Length - 1];
        int maxLevel = maxLevelData.level;

        // if current level is equal to max level, then don't process below script.
        if (level == maxLevel)
        {
            exp = maxLevelData.reqEXP;
            return;
        }

        // store current level before process level data.
        int oldLevel = level;

        // get exp data by comparing reqData(required exp data to get certain level) on player level data array.
        int ix = 0;
        for (ix = 0; ix < DataManager.Instance.playerLevelData.Length; ++ix)
        {
            if (exp < DataManager.Instance.playerLevelData[ix].reqEXP)
                break;
        }

        // if level variable has been initialized and _levelIndex is the same as before, 
        // then don't process below script.
        if (level != -1 && _levelIndex == ix - 1)
        {
            return;
        }   

        // Get levelIndex.
        _levelIndex = ix == 0 ? 0 : ix - 1;
        // Get level
        level = DataManager.Instance.playerLevelData[_levelIndex].level;

        // check if level up state.
        if (oldLevel != level && oldLevel != -1)
            LevelUp();

        // if level is reached max level, set exp data with max level exp data.
        if (level == maxLevel)
        {
            exp = maxLevelData.reqEXP;
        }
    }

    // callled when player gets exp (normally when killed enemy).
    public void GainEXP(int gainEXP)
    {
        exp += gainEXP;

        // check if level up.
        CheckLevel();
    }

    // called when player gets gold (normally when killed enemy).
    public void GainGold(int gainGold)
    {
        gold += gainGold;
    }

    // Level up function.
    void LevelUp()
    {
        // Get new level data.
        levelData = DataManager.Instance.GetPlayerCurrentLevelData(_levelIndex);

        // HP data update.
        currentHP = levelData.maxHP;

        // call LevelUp function on PlayerFSMManager to show levelup effect.
        GetComponent<PlayerFSMManager>().LevelUp();
    }
}