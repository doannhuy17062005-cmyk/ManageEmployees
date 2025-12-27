using System;
using System.Data;
using System.Linq;
using ClosedXML.Excel;

namespace HRMApp.Utils
{
    public static class ExcelHelper
    {
        public static DataTable ReadExcel(string filePath)
        {
            DataTable dt = new DataTable();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1); // sheet đầu tiên
                bool firstRow = true;

                foreach (var row in worksheet.RowsUsed())
                {
                    if (firstRow)
                    {
                        // Tạo cột theo header
                        foreach (var cell in row.Cells())
                            dt.Columns.Add(cell.Value.ToString());
                        firstRow = false;
                    }
                    else
                    {
                        dt.Rows.Add();
                        for (int i = 0; i < row.Cells().Count(); i++) // ✅ dùng Count()
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = row.Cell(i + 1).Value.ToString();
                        }
                    }
                }
            }

            return dt;
        }
    }
}
