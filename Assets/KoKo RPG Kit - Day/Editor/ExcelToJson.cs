using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using NPOI.HPSF;

/// <summary>
/// Create Game Data File from Excel File as Json Format using NPOI Library.
/// Check NPOI and SimpleJson Manaul if you want to understand this script.
/// </summary>
public class ExcelToJson : MonoBehaviour
{
    // excel data file path ( game data ).
    private static string filePath = "Assets/KoKo RPG Kit - Day/Editor/Data/RPGData.xlsx";
    // PlayerLevelData (Json File) Path.
    private static string playerDataPath = "Assets/KoKo RPG Kit - Day/Resources/LevelData/PlayerLevelData.json";
    // EnemyLevelData (Json File) Path.
    private static string enemyDataPath = "Assets/KoKo RPG Kit - Day/Resources/LevelData/EnemyLevelData.json";

    // Create Json Data.
    [MenuItem("Create Game Data/Create Player and Enemy Level Data")]
    static void CreateData()
    {
        // Create Player Level Data from Excel file as Json format.
        CreatePlayerData();
        // Create Enemy Level Data from Excel file as Json format.
        CreateEnemyData();
    }

    // Create Player Level Data from Excel file as Json format.
    static void CreatePlayerData()
    {
        List<PlayerLevelData.Attribute> list = new List<PlayerLevelData.Attribute>();

        // Read Excel File.
        using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        {
            // Parse Player's Data from Excel Sheet using NPOI.
            IWorkbook book = new XSSFWorkbook(stream);
            ISheet sheet = book.GetSheetAt(0);

            for (int ix = 1; ix < sheet.LastRowNum + 1; ++ix)
            {
                IRow row = sheet.GetRow(ix);
                PlayerLevelData.Attribute attr = new PlayerLevelData.Attribute();
                attr.level = (int)row.GetCell(0).NumericCellValue;
                attr.maxHP = (int)row.GetCell(1).NumericCellValue;
                attr.baseAttack = (float)row.GetCell(2).NumericCellValue;
                attr.reqEXP = (int)row.GetCell(3).NumericCellValue;
                attr.moveSpeed = (float)row.GetCell(4).NumericCellValue;
                attr.turnSpeed = (float)row.GetCell(5).NumericCellValue;
                attr.attackRange = (float)row.GetCell(6).NumericCellValue;
                attr.skillAttackRange = (float)row.GetCell(7).NumericCellValue;
                attr.skillAttack = (float)row.GetCell(8).NumericCellValue;

                list.Add(attr);
            }

            stream.Close();

            // Convert PlayerLevelData to Json format using SimpleJson Library.
            string levelJsonData = SimpleJson.SimpleJson.SerializeObject(list);
            // Create PlayerLevelData.json File.
            File.WriteAllText(playerDataPath, levelJsonData, System.Text.Encoding.UTF8);
        }
    }

    // Create Enemy Level Data from Excel file as Json format.
    static void CreateEnemyData()
    {
        List<EnemyLevelData.Race> list = new List<EnemyLevelData.Race>(3);

        // Read Excel File.
        using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        {
            // Parse Player's Data from Excel Sheet using NPOI.
            IWorkbook book = new XSSFWorkbook(stream);

            for (int ix = 1; ix < 4; ++ix)
            {
                ISheet sheet = book.GetSheetAt(ix);
                EnemyLevelData.Race race = new EnemyLevelData.Race();
                race.raceName = sheet.SheetName;

                race.enemyData = new EnemyLevelData.Attribute[sheet.LastRowNum];
                for (int jx = 1; jx < sheet.LastRowNum + 1; ++jx)
                {
                    IRow row = sheet.GetRow(jx);
                    EnemyLevelData.Attribute attr = new EnemyLevelData.Attribute();
                    attr.level = (int)row.GetCell(0).NumericCellValue;
                    attr.maxHP = (int)row.GetCell(1).NumericCellValue;
                    attr.attack = (float)row.GetCell(2).NumericCellValue;
                    attr.defence = (float)row.GetCell(3).NumericCellValue;
                    attr.gainEXP = (int)row.GetCell(4).NumericCellValue;
                    attr.walkSpeed = (float)row.GetCell(5).NumericCellValue;
                    attr.runSpeed = (float)row.GetCell(6).NumericCellValue;
                    attr.turnSpeed = (float)row.GetCell(7).NumericCellValue;
                    attr.attackRange = (float)row.GetCell(8).NumericCellValue;
                    attr.gainGold = (int)row.GetCell(9).NumericCellValue;

                    race.enemyData[jx - 1] = attr;
                }

                list.Add(race);
            }

            stream.Close();

            // Convert EnemyLevelData to Json format using SimpleJson Library.
            string levelJsonData = SimpleJson.SimpleJson.SerializeObject(list);
            // Create EnemyLevelData.json File.
            File.WriteAllText(enemyDataPath, levelJsonData);
        }
    }
}