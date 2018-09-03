using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NetCoreFramework.Application.Dto;
using NetCoreFramework.Domain.Models;

namespace NetCoreFramework.Application.Services
{
    public class BaseService
    {

        public int PageSize { get; set; } = 20;


        public decimal GetTotalPage(int total, int pageSize)
        {
            return Math.Ceiling((decimal)total / pageSize);
        }

        public async Task<string> SaveFile(string path, IFormFile file, string absolutePath)
        {
            var name = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var fname = path + "/" + name + Path.GetExtension(file.FileName);

            using (var stream = new FileStream(fname, FileMode.Create))
                await file.CopyToAsync(stream);

            return absolutePath + name + Path.GetExtension(file.FileName);

        }

        //public async Task<BoolAndMsg> ImportExcel<T>(string header, string path, string importProperty) where T : new()
        //{
        //    var boolAndMsg = new BoolAndMsg();

        //    if (!File.Exists(path))
        //    {
        //        boolAndMsg.IsOK = false;
        //        boolAndMsg.Msg = "文件不存在";
        //        return boolAndMsg;
        //    }

        //    if (!ValidHeader(header, new MemoryStream(System.IO.File.ReadAllBytes(path))))
        //    {
        //        boolAndMsg.IsOK = false;
        //        boolAndMsg.Msg = "上传格式不正确";
        //        return boolAndMsg;
        //    }

        //    var list = ReadStreamToList<T>(new MemoryStream(System.IO.File.ReadAllBytes(path)), header.TrimEnd(new char[] { '|' }).Split("|").Length, importProperty.Split("|"));
        //    boolAndMsg.IsOK = true;
        //    boolAndMsg.Result = list;

        //    return boolAndMsg;
        //}

        public string ExportExcel<T>(List<T> list, string path, Dictionary<string, string> exportProperty, List<string> selectExportProperty = null)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fname = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            string fpath = path + "/" + fname + ".xls";

            FileStream filecreate = new FileStream(fpath, FileMode.Create, FileAccess.ReadWrite);
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            ISheet sheet = hssfworkbook.CreateSheet("Sheet1");

            IRow row = sheet.CreateRow(0);

            var theExportDic = new Dictionary<string, string>();

            if (selectExportProperty != null)
            {
                for (int i = 0; i < selectExportProperty.Count; i++)
                {
                    var theVal = exportProperty[selectExportProperty.ElementAt(i).ToLower()];
                    ICell cell = row.CreateCell(i);
                    cell.CellStyle = GetCellStyle(hssfworkbook);
                    cell.SetCellValue(theVal);
                    theExportDic.Add(selectExportProperty.ElementAt(i), theVal);
                }
            }
            else
            {
                int i = 0;
                foreach (var item in exportProperty)
                {
                    ICell cell = row.CreateCell(i);
                    cell.CellStyle = GetCellStyle(hssfworkbook);
                    cell.SetCellValue(exportProperty[item.Key]);
                    i++;
                }
                theExportDic = exportProperty;
            }

            int r = 1;
            foreach (var item in list)
            {
                IRow newrow = sheet.CreateRow(r);

                int i = 0;
                foreach (var export in theExportDic)
                {
                    var val = typeof(T).GetProperty(export.Key, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase).GetValue(item)?.ToString();
                    newrow.CreateCell(i).SetCellValue(val);
                    i++;
                }

                r++;
            }

            hssfworkbook.Write(filecreate);
            filecreate.Close();


            return fname + ".xls";
        }

        private ICellStyle GetCellStyle(HSSFWorkbook hssfworkbook)
        {
            ICellStyle style = hssfworkbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            style.FillForegroundColor = HSSFColor.Grey25Percent.Index;
            style.FillPattern = FillPattern.SolidForeground;
            IFont font = hssfworkbook.CreateFont();
            font.FontName = "宋体";
            font.IsBold = true;
            font.FontHeightInPoints = 13;
            style.SetFont(font);

            style.BorderBottom = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;

            return style;
        }
        private bool ValidHeader(string head, MemoryStream fileStream)
        {
            using (var stream = fileStream)
            {
                IWorkbook workbook = WorkbookFactory.Create(stream);
                var sheet = workbook.GetSheetAt(0);

                var headerStr = new StringBuilder();
                var headerRow = sheet.GetRow(0);
                if (headerRow != null)
                {
                    for (int i = 0; i < headerRow.Cells.Capacity; i++)
                    {
                        var cell = headerRow.GetCell(i);
                        if (cell != null)
                        {
                            headerStr.Append(GetCellValue(cell) + "|");
                        }
                    }

                    if (headerStr.ToString().Equals(head))
                        return true;
                }
            }
            return false;
        }

        public string GetCellValue(ICell cell)
        {
            string cellValue;
            switch (cell.CellType)
            {
                case CellType.Boolean:
                    cellValue = cell.BooleanCellValue.ToString();
                    break;
                case CellType.Numeric:
                    cellValue = DateUtil.IsCellDateFormatted(cell) ? cell.DateCellValue.ToString("yyyy-MM-dd HH:mm:ss") : cell.NumericCellValue.ToString(CultureInfo.InvariantCulture);
                    break;
                case CellType.Error:
                case CellType.Formula:
                case CellType.String:
                case CellType.Unknown:
                case CellType.Blank:
                default:
                    cellValue = cell.StringCellValue;
                    break;
            }

            return cellValue;
        }

        public List<T> ReadStreamToList<T>(Stream fileStream, int cellcount, string[] properties) where T : new()
        {
            ISheet sheet = null;
            int startRow = 1;

            var list = new List<T>();

            using (var stream = fileStream)
            {
                IWorkbook workbook = WorkbookFactory.Create(stream);
                sheet = workbook.GetSheetAt(0);

                int rowCount = sheet.LastRowNum;
                for (int i = startRow; i <= rowCount; i++)
                {
                    IRow row = sheet.GetRow(i);

                    if (row == null || row.FirstCellNum < 0) continue;

                    T t = new T();

                    for (int j = row.FirstCellNum; j < cellcount; ++j)
                    {
                        if (row.GetCell(j) != null)
                        {
                            var cellValue = GetCellValue(row.GetCell(j));
                            var property = typeof(T).GetProperty(properties[j]);
                            property.SetValue(t, cellValue);
                        }
                    }
                    list.Add(t);

                }


                return list;
            }


        }
        public void CreateErrors(ValidationResult validationResult, int errorCount, int errorMaxCount, int rowIndex, StringBuilder errors)
        {
            var errorMsgs = validationResult.Errors.Aggregate(new StringBuilder(validationResult.Errors.Count), (acc, pair) => acc.AppendFormat("{0} ", pair.ErrorMessage));
            if (errorCount <= errorMaxCount)
            {
                errors.AppendFormat($"第{rowIndex}行:{errorMsgs}");
            }
            else if (errorCount == errorMaxCount)
            {
                errors.Append("...");
            }
        }
        public void CreateErrors(string errorMsg, int errorCount, int errorMaxCount, int rowIndex, StringBuilder errors)
        {
            if (errorCount <= errorMaxCount)
            {
                errors.AppendFormat($"第{rowIndex}行:{errorMsg}\n");
            }
            else if (errorCount == errorMaxCount)
            {
                errors.Append("...");
            }
        }

    }
}
