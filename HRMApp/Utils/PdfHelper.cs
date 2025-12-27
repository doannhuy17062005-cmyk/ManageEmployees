using System;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace HRMApp.Utils
{
    public static class PdfHelper
    {
        public static void ExportNhanVien(DataTable dt, string filePath)
        {
            Document doc = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);
            PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
            doc.Open();

            // Tiêu đề
            var title = new Paragraph("BÁO CÁO DANH SÁCH NHÂN VIÊN\n\n",
                new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD));
            title.Alignment = Element.ALIGN_CENTER;
            doc.Add(title);

            // Tạo bảng
            PdfPTable table = new PdfPTable(6);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 15, 15, 10, 20, 20, 20 });

            // Header
            string[] headers = { "Họ tên", "Ngày sinh", "Giới tính", "Email", "Phòng ban" };
            foreach (var h in headers)
            {
                PdfPCell cell = new PdfPCell(new Phrase(h, new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);
            }

            // Nội dung
            foreach (DataRow row in dt.Rows)
            {
                table.AddCell(row["HoTen"].ToString());
                table.AddCell(Convert.ToDateTime(row["NgaySinh"]).ToString("dd/MM/yyyy"));
                table.AddCell((int)row["GioiTinh"] == 1 ? "Nam" : "Nữ");
                table.AddCell(row["Email"].ToString());
                table.AddCell(row["TenPhongBan"].ToString());

     
            }

            doc.Add(table);
            doc.Close();
        }
    }
}
