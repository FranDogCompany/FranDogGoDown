using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OfficeOpenXml;
using System.IO;

public class test : MonoBehaviour
{
    List<EnemyStatus> eStatuses = new List<EnemyStatus>();

    void Start()
    {
        //取得Excel路徑  
        string filePath = Application.streamingAssetsPath + "/test.xlsx";

        //獲取Excel文件信息
        FileInfo fileInfo = new FileInfo(filePath);


        //通過Excel表格的文件信息，打開excel表格
        using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
        {
            //取得Excel文件中的第一張表
            ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets[1];

            //取得表中第一行第一列中的數據
            string s = workSheet.Cells[1, 2].Value.ToString();

            Debug.Log(s);

            //TODO 測試
            //Debug.Log(workSheet.Column(1));

            //取得表格裡所有資料
            var start = workSheet.Dimension.Start;
            var end = workSheet.Dimension.End;
            for (int row = start.Row + 1; row <= end.Row; row++)
            {
                for (int col = start.Column; col <= end.Column; col++)
                {
                    object cellValue = workSheet.Cells[row, col].Text;
                    //Debug.Log(cellValue);

                    
                }
            }
        }//using作用:當大括號內執行完畢後，釋放小跨號內的資源


    }

    // public static EnemyStatus WorksheetToTable(ExcelWorksheet worksheet)
    //     {
    //         //获取worksheet的行数
    //         int rows = worksheet.Dimension.End.Row;
    //         //获取worksheet的列数
    //         int cols = worksheet.Dimension.End.Column;

    //         EnemyStatus dt = new EnemyStatus(worksheet.Name);
    //         DataRow dr = null;
    //         for (int i = 1; i <= rows; i++)
    //         {
    //             if (i > 1)
    //                 dr = dt.Rows.Add();

    //             for (int j = 1; j <= cols; j++)
    //             {
    //                 //默认将第一行设置为datatable的标题
    //                 if (i == 1)
    //                     dt.Columns.Add(GetString(worksheet.Cells[i, j].Value));
    //                 //剩下的写入datatable
    //                 else
    //                     dr[j - 1] = GetString(worksheet.Cells[i, j].Value);
    //             }
    //         }
    //         return dt;
    //     }
}
