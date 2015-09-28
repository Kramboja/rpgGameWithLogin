using UnityEngine;
using System.Collections;

/// <summary>
/// Data Manager script Dealing with Game Data (Single-Ton)
/// </summary>
//[ExecuteInEditMode]
public class DataManager : MonoBehaviour
{
    // Single-ton instance.
    private static DataManager _instance;
    public static DataManager Instance { get { return _instance; } }

    // Player / Enemy Level Data Text written in json.
    // All placed in Resource/LevelData folder.
    public TextAsset playerLevelDataJson;
    public TextAsset enemyLevelDataJson;

    // Real Game Data ( Player / Enemy )
    // Need to be parsed from Json text To Get Real game data. 
    public PlayerLevelData.Attribute[] playerLevelData;
    public EnemyLevelData.Race[] enemyLevelData;

    // used to store next scene load.
    public string sceneNameToLoad;

    void Awake()
    {
        // initilization.
        if (_instance == null)
        {
            _instance = this;

            // Load Json File using Resource.Load Funtion.
            playerLevelDataJson = (TextAsset)Resources.Load("LevelData/PlayerLevelData");
            enemyLevelDataJson = (TextAsset)Resources.Load("LevelData/EnemyLevelData");

            // Parse Json Data to Game Data ( Player / Enemy )
            ParseData();

            // Don't Destroy Data Manager during entire game.
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            // Make this object single-ton.
            Destroy(gameObject);
        }
    }

    void ParseData()
    {
        // Parse JSON Data into Game Data array using SimpleJSON parser.
        playerLevelData = SimpleJson.SimpleJson.DeserializeObject<PlayerLevelData.Attribute[]>(playerLevelDataJson.text);
        enemyLevelData = SimpleJson.SimpleJson.DeserializeObject<EnemyLevelData.Race[]>(enemyLevelDataJson.text);
    }

    // get Player level data with player's level index.
    public PlayerLevelData.Attribute GetPlayerCurrentLevelData(int levelIndex)
    {
        return playerLevelData[levelIndex];
    }

    // get enemy level data with enemy's type and enemy's level index.
    public EnemyLevelData.Attribute GetEnemyCurrentLevelData(int enemyIndex, int levelIndex)
    {
        return enemyLevelData[enemyIndex].enemyData[levelIndex];
    }

    // load "LoadScene" scene and then load scene asynchronously.
    public void SceneLoad(string nextScene)
    {
        sceneNameToLoad = nextScene;
        Application.LoadLevel("LoadScene");
    }
}