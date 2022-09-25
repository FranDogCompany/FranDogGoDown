using System.Collections.Generic;
using UnityEngine;
using OfficeOpenXml;
using System.IO;
using System.Data;
using Newtonsoft.Json;

public class ExcelManager : MonoBehaviour
{
    void Start()
    {
        // //取得Excel路徑  
        // string filePath = Application.streamingAssetsPath + "/test.xlsx";

        // //獲取Excel文件信息
        // FileInfo fileInfo = new FileInfo(filePath);

        // DataSet dataSet = new DataSet("dataSet"); //建立DataSet
        // DataTable table = new DataTable(); //建立DataTable
        // string columnName = ""; //Excel第一列的名稱
        // DataRow newRow = null;
        // string json = "";

        // //通過Excel表格的文件信息，打開excel表格
        // using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
        // {
        //     //取得Excel文件中的第一張表
        //     ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets[1];

        //     //取得表格裡所有資料
        //     var start = workSheet.Dimension.Start;
        //     var end = workSheet.Dimension.End;

        //     for (int col = start.Column; col <= end.Column; col++)
        //     {
        //         columnName = workSheet.Cells[1, col].Value.ToString();
        //         DataColumn itemColumn = new DataColumn(columnName); //建立item欄
        //         table.Columns.Add(itemColumn);  //DataTable加入欄位
        //     }
        //     //DataSet加入Table
        //     dataSet.Tables.Add(table);

        //     for (int row = start.Row + 1; row <= end.Row; row++)
        //     {
        //         newRow = table.NewRow();
        //         for (int col = start.Column; col <= end.Column; col++)
        //         {
        //             newRow[workSheet.Cells[1, col].Value.ToString()] = workSheet.Cells[row, col].Value.ToString();
        //         }
        //         table.Rows.Add(newRow);
        //     }

        //     //轉成JSON格式
        //     json = JsonConvert.SerializeObject(dataSet);
        //     //顯示
        //     Debug.Log(json);
        // }//using作用:當大括號內執行完畢後，釋放小跨號內的資源
    }

    public string ExcelToJson(string ExcelPath)
    {
        //取得Excel路徑  
        string filePath = Application.streamingAssetsPath + "/" + ExcelPath + ".xlsx";

        //獲取Excel文件信息
        FileInfo fileInfo = new FileInfo(filePath);

        DataSet dataSet = new DataSet("dataSet"); //建立DataSet
        DataTable table = new DataTable(); //建立DataTable
        string columnName = ""; //Excel第一列的名稱
        DataRow newRow = null;
        string json = "";

        //通過Excel表格的文件信息，打開excel表格
        using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
        {
            //取得Excel文件中的第一張表
            ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets[1];

            //取得表格裡所有資料
            var start = workSheet.Dimension.Start;
            var end = workSheet.Dimension.End;

            for (int col = start.Column; col <= end.Column; col++)
            {
                columnName = workSheet.Cells[1, col].Value.ToString();
                DataColumn itemColumn = new DataColumn(columnName); //建立key欄位
                table.Columns.Add(itemColumn);  //DataTable加入欄位
            }
            //DataSet加入Table
            dataSet.Tables.Add(table);

            for (int row = start.Row + 1; row <= end.Row; row++)
            {
                newRow = table.NewRow();
                for (int col = start.Column; col <= end.Column; col++)
                {
                    newRow[workSheet.Cells[1, col].Value.ToString()] = workSheet.Cells[row, col].Value.ToString();
                }
                table.Rows.Add(newRow);
            }

            //轉成JSON格式
            json = JsonConvert.SerializeObject(dataSet);
            
            //存成JSON檔
            File.WriteAllText(Application.streamingAssetsPath + "/" + ExcelPath, json);

            //回傳
            return json;
        }//using作用:當大括號內執行完畢後，釋放小跨號內的資源

    }
}
