using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OfficeOpenXml;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

public class EnemyStatus : MonoBehaviour
{
    public int id { get; set; }

    public string enemyName { get; set; }

    public int HP { get; set; }

    public int ATK { get; set; }

    public List<T> GetList<T>(ExcelWorksheet sheet)
    {
        List<T> list = new List<T>();
        //第一列是欄位資訊
        // var columnInfo = Enumerable.Range(1, sheet.Dimension.Columns).ToString().Select(n =>
        //     new { Index = n, ColumnName = sheet.Cells[1, n].Value.ToString() }
        // );

        for (int row = 2; row < sheet.Dimension.Rows; row++)
        {
            T obj = (T)Activator.CreateInstance(typeof(T));//generic object
            foreach (var prop in typeof(T).GetProperties())
            {
                // int col = columnInfo.SingleOrDefault(c => c.ColumnName == prop.Name).Index;
                var start = sheet.Dimension.Start;
                int col = start.Column; 

                var val = sheet.Cells[row, col].Value;
                Debug.Log("val = " + val);
                var propType = prop.PropertyType;
                prop.SetValue(obj, Convert.ChangeType(val, propType));
            }
            list.Add(obj);
        }

        return list;
    }
}
