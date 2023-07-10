using ChickenWarrior.Skill;
using System;
//using NPOI.SS.UserModel;
//using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DataManager
{
    private static Dictionary<int, SkillData> skillDataDict;
    private static Dictionary<int, EffectData> effectDataDict;

    public static void LoadData()
    {
        LoadDataFromExcel();
    }

    private static void LoadDataFromExcel()
    {
        skillDataDict = new Dictionary<int, SkillData>();
        effectDataDict = new Dictionary<int, EffectData>();

        string filePath = Path.Combine(Application.dataPath, "GameData.xlsx"); // 改成你的Excel文件的路径
        //using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        //{
        //    IWorkbook workbook = new XSSFWorkbook(file);

        //    // Load skill data from the "BasicSkill" sheet
        //    ISheet skillSheet = workbook.GetSheet("BasicSkill"); // 这里填写你的工作表名称
        //    for (int row = 1; row <= skillSheet.LastRowNum; row++)
        //    {
        //        SkillData data = new SkillData();
        //        // 这里以你的工作表的具体列为准，根据你的工作表格内容去获取数据，例如：
        //        data.skillID = (int)skillSheet.GetRow(row).GetCell(0).NumericCellValue;
        //        data.name = skillSheet.GetRow(row).GetCell(1).StringCellValue;
        //        // ...

        //        skillDataDict[data.skillID] = data;
        //    }

        //    // Load effect data from the "Impact" sheet
        //    ISheet effectSheet = workbook.GetSheet("Impact"); // 这里填写你的工作表名称
        //    for (int row = 1; row <= effectSheet.LastRowNum; row++)
        //    {
        //        EffectData data = new EffectData();
        //        // 这里以你的工作表的具体列为准，根据你的工作表格内容去获取数据，例如：
        //        data.id = (int)effectSheet.GetRow(row).GetCell(0).NumericCellValue;
        //        data.effectType = effectSheet.GetRow(row).GetCell(1).StringCellValue;
        //        // ...

        //        effectDataDict[data.id] = data;
        //    }
        //}
    }
    //加载技能数据
    public static List<SkillData> LoadAllSkillData()
    {
        throw new NotImplementedException();
    }
    //加载效果数据
    public static List<EffectData> LoadEffectData(int skillID)
    {
        throw new NotImplementedException();
    }

    // 其他代码...
}