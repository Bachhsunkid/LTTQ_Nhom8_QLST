using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using Excel = Microsoft.Office.Interop.Excel;
namespace Nhom8_BTL_QLST
{
    internal class ExportExcel
    {
        public void dataExport(String Startposition, String ColumnsNames, DataTable dataTable, String Path,string title)
        {
            char[] chars = Startposition.ToArray();
            String str = chars[0].ToString();
            String strCount = "";
            for (int i = 1; i < chars.Length; i++)
            {
                strCount += chars[i].ToString();
            }
            int count = Convert.ToInt32(strCount);
            String[] cNames = ColumnsNames.Split(',');

            Excel.Application exApp = new Excel.Application();
            Excel.Workbook exBook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet exSheet = (Excel.Worksheet)exBook.Worksheets[1];
            for (int i = 0; i < cNames.Length; i++)
            {
                //exSheet.get_Range(str + count.ToString()).Font.Bold = true;
                exSheet.get_Range(str + count.ToString()).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                exSheet.get_Range(str + count.ToString()).Value = cNames[i];
                exSheet.get_Range(str + count.ToString()).Font.Bold = true;
                str = NextCharacter(str);
            }
            count++;
            exSheet.get_Range("A1:" + PrevCharacter(str) + "1").Merge();
            exSheet.get_Range("A1").HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            exSheet.get_Range("A1").Value = title;
            exSheet.get_Range("A1").Font.Bold = true;
            exSheet.get_Range("A1").Font.Size = 20;

            //exSheet.get_Range("A2:" + PrevCharacter(str) + dataTable.Rows.Count.ToString()).AutoFit();

            str = chars[0].ToString();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                //STT
                //exSheet.get_Range("A" + (2 + i).ToString()).Value = (i+1).ToString();
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    DataRow dr = dataTable.Rows[i];
                    String s = dr.ItemArray[j].ToString();
                    exSheet.get_Range(str + count).Value = s;
                    str = NextCharacter(str);
                }
                count++;
                str = chars[0].ToString();
            }

            exBook.Activate();
            exBook.SaveAs(Path);
            exApp.Quit();

        }

        private String NextCharacter(String PrevCharacter)
        {
            byte[] a = Encoding.ASCII.GetBytes(PrevCharacter);
            int n = (int)a[0] + 1;
            String result = char.ConvertFromUtf32(n);
            return result;
        }

        private String PrevCharacter(String PrevCharacter)
        {
            byte[] a = Encoding.ASCII.GetBytes(PrevCharacter);
            int n = (int)a[0] - 1;
            String result = char.ConvertFromUtf32(n);
            return result;
        }
    }
}
