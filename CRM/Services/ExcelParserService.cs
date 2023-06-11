using CRM.Shared;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using System.Xml;

namespace CRM.Services
{
    public class ExcelParserService
    {
        public async Task<List<BaseItem>> ParseExcel(MemoryStream stream)
        {
            var items = new List<BaseItem>();

            using (var workbook = new XSSFWorkbook(stream))
            {
                var sheet = workbook.GetSheetAt(0);
                var headerRow = sheet.GetRow(0);

                for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    var dataRow = sheet.GetRow(i);
                    if (dataRow != null)
                    {
                        var item = new BaseItem();

                        item.PhoneNumber = GetColumnIndex(headerRow, "PhoneNumber") >= 0 ? dataRow.GetCell(GetColumnIndex(headerRow, "PhoneNumber"))?.ToString() : null;
                        item.PersonName = GetColumnIndex(headerRow, "PersonName") >= 0 ? dataRow.GetCell(GetColumnIndex(headerRow, "PersonName"))?.ToString() : null;
                        item.City = GetColumnIndex(headerRow, "City") >= 0 ? dataRow.GetCell(GetColumnIndex(headerRow, "City"))?.ToString() : null;
                        item.Region = GetColumnIndex(headerRow, "Region") >= 0 ? dataRow.GetCell(GetColumnIndex(headerRow, "Region"))?.ToString() : null;
                        item.PersonId = GetColumnIndex(headerRow, "PersonId") >= 0 ? dataRow.GetCell(GetColumnIndex(headerRow, "PersonId"))?.ToString() : null;
                        item.Address = GetColumnIndex(headerRow, "Address") >= 0 ? dataRow.GetCell(GetColumnIndex(headerRow, "Address"))?.ToString() : null;
                        item.DateOfBirth = GetColumnIndex(headerRow, "DateOfBirth") >= 0 ? dataRow.GetCell(GetColumnIndex(headerRow, "DateOfBirth"))?.ToString() : null;
                        item.AdditionalInfo = GetAdditionalInfo(dataRow, headerRow);

                        items.Add(item);
                    }
                }
            }

            return items;
        }

        private int GetColumnIndex(IRow headerRow, string columnName)
        {
            for (int i = headerRow.FirstCellNum; i < headerRow.LastCellNum; i++)
            {
                var cell = headerRow.GetCell(i);
                if (cell != null && cell.ToString() == columnName)
                {
                    return i;
                }
            }
            return -1;
        }

        private string GetAdditionalInfo(IRow dataRow, IRow headerRow)
        {
            var additionalInfo = new List<string>();

            for (int i = dataRow.FirstCellNum; i < dataRow.LastCellNum; i++)
            {
                var cell = dataRow.GetCell(i);
                var columnName = headerRow.GetCell(i)?.ToString();

                if (cell != null && columnName != null && !IsBaseItemProperty(columnName))
                {
                    additionalInfo.Add(columnName + ": " + cell.ToString());
                }
                else if (columnName != null && !IsBaseItemProperty(columnName))
                {
                    additionalInfo.Add(null);
                }
            }

            return string.Join(", ", additionalInfo);
        }

        private bool IsBaseItemProperty(string columnName)
        {
            var baseItemProperties = typeof(BaseItem).GetProperties().Select(p => p.Name);
            return baseItemProperties.Contains(columnName);
        }
    }
}
