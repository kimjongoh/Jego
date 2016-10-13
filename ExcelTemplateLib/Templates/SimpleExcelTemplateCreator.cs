using ExcelTemplateLib.DataModels;
using ExcelTemplateLib.Interfaces;
using ExcelTemplateLib.Managers;
using ExcelTemplateLib.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelTemplateLib.Templates {
    public class SimpleExcelTemplateCreator : BaseExcelTemplate {
        private ExcelCellNode titleNode;
        private List<ExcelCellNode> headerNodes;
        private string dateTimeString;
        private List<ExcelWidth> widths;

        public Excel.Worksheet ws { get; set; }
        private int sheetPage = 1;
        private IProgressTransfer progressTransfer;
        private List<TypeBaseDayModel> typeBaseDayModels;

        public SimpleExcelTemplateCreator(int sheetPage, string excelUrl, DateTime dateTime, List<TypeBaseDayModel> typeBaseDayModels, IProgressTransfer progressTransfer)
            : base(excelUrl) {
            this.progressTransfer = progressTransfer;
            this.typeBaseDayModels = typeBaseDayModels;
            init(dateTime);
        }

        private void init(DateTime dateTime) {
            initTitle();
            this.dateTimeString = dateTime.ToString("yyyy년 MM월dd일");
            initWidths();
            initHeaders();
        }

        private void initWidths() {
            widths = new List<ExcelWidth>();
            widths.Add(new ExcelWidth(1, 5.88));
            widths.Add(new ExcelWidth(2, 14.5));
            widths.Add(new ExcelWidth(3, 5.88));
            widths.Add(new ExcelWidth(4, 17.38));
            widths.Add(new ExcelWidth(5, 9.5));
            widths.Add(new ExcelWidth(6, 9));
            widths.Add(new ExcelWidth(7, 12.38));
            widths.Add(new ExcelWidth(8, 8.5));
            widths.Add(new ExcelWidth(9, 13.25));
            widths.Add(new ExcelWidth(10, 10));
        }

        private void initHeaders() {
            headerNodes = new List<ExcelCellNode>();
            ExcelFont headerFont = new ExcelFont(11, true);

            headerNodes.Add(new ExcelCellNode("연번", new ExcelCellRange(4, 1), headerFont));
            headerNodes.Add(new ExcelCellNode("식품명", new ExcelCellRange(4, 2), headerFont));
            headerNodes.Add(new ExcelCellNode("단위", new ExcelCellRange(4, 3), headerFont));
            headerNodes.Add(new ExcelCellNode("지급일시", new ExcelCellRange(4, 4), headerFont));
            headerNodes.Add(new ExcelCellNode("지급수량", new ExcelCellRange(4, 5), headerFont));
            headerNodes.Add(new ExcelCellNode("재고량", new ExcelCellRange(4, 6), headerFont));
            headerNodes.Add(new ExcelCellNode("보관상태 및\n변질여부", new ExcelCellRange(4, 7), headerFont));
            headerNodes.Add(new ExcelCellNode("유통기한", new ExcelCellRange(4, 8), headerFont));
            headerNodes.Add(new ExcelCellNode("급식담당확인", new ExcelCellRange(4, 9), headerFont));
            headerNodes.Add(new ExcelCellNode("비고", new ExcelCellRange(4, 10), headerFont));
        }

        private void initTitle() {
            this.titleNode = new ExcelCellNode("주,부식 검사 불출 대장",
                new ExcelCellRange(2, 4, 2, 8),
                new ExcelFont(24, true));
        }

        public override void WriteExcelData(Microsoft.Office.Interop.Excel.Workbook wb) {
            try {
                ws = wb.Worksheets.get_Item(sheetPage) as Excel.Worksheet;

                setWidths();
                writeCell(titleNode);

                foreach (ExcelCellNode node in headerNodes) {
                    writeCell(node);
                }

                setDatas(typeBaseDayModels);
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void setDatas(List<TypeBaseDayModel> typeBaseDayModels) {
            int rowCount = 5;
            ExcelFont dataFont = new ExcelFont(11, false, Excel.XlHAlign.xlHAlignCenter);
            ExcelFont numberFont = new ExcelFont(11, false, Excel.XlHAlign.xlHAlignRight);

            List<ExcelCellNode> typeCellNodes = new List<ExcelCellNode>();

            foreach (TypeBaseDayModel typeDayModel in typeBaseDayModels) {
                int startRow = rowCount;
                
                foreach (KeyValuePair<string, DayFoodModel> entry in typeDayModel.dataList) {
                    DayFoodModel dayFoodModel = entry.Value;

                    typeCellNodes.Add(new ExcelCellNode((rowCount-4).ToString(), new ExcelCellRange(rowCount, 1), dataFont));
                    typeCellNodes.Add(new ExcelCellNode(dayFoodModel.food.name, new ExcelCellRange(rowCount, 2), dataFont));
                    typeCellNodes.Add(new ExcelCellNode(dayFoodModel.food.unit, new ExcelCellRange(rowCount, 3), dataFont));
                    typeCellNodes.Add(new ExcelCellNode(dateTimeString, new ExcelCellRange(rowCount, 4), dataFont));

                    if (dayFoodModel.useTrn != null) {
                        typeCellNodes.Add(new ExcelCellNode(string.Format("{0:G}", dayFoodModel.useTrn.amount), new ExcelCellRange(rowCount, 5), dataFont));
                    } else {
                        typeCellNodes.Add(new ExcelCellNode("", new ExcelCellRange(rowCount, 5), dataFont));
                    }

                    if (dayFoodModel.remain != null) {
                        decimal todayRemain = dayFoodModel.remain.amount;
                        if(dayFoodModel.buyTrn != null) {
                            todayRemain += dayFoodModel.buyTrn.amount;
                        }
                        if(dayFoodModel.useTrn != null) {
                            todayRemain -= dayFoodModel.useTrn.amount;
                        }

                        typeCellNodes.Add(new ExcelCellNode(string.Format("{0:G}", todayRemain), new ExcelCellRange(rowCount, 6), dataFont));
                        typeCellNodes.Add(new ExcelCellNode(dayFoodModel.remain.deadline, new ExcelCellRange(rowCount, 8), dataFont));
                    }
                    
                    typeCellNodes.Add(new ExcelCellNode("이상무", new ExcelCellRange(rowCount, 7), dataFont));
                    typeCellNodes.Add(new ExcelCellNode("O", new ExcelCellRange(rowCount, 9), dataFont));

                    rowCount++;
                }
            }

            foreach (ExcelCellNode node in typeCellNodes) {
                writeCell(node);
            }

            drawBound(4, 1, rowCount, 10);
            
        }

        private void setWidths() {
            foreach (ExcelWidth width in widths) {
                ws.Columns[width.loc].ColumnWidth = width.width;
            }
        }

        private void writeCell(ExcelCellNode cellNode) {
            Excel.Range range = null;
            if (cellNode.range.isMerge) {
                range = ws.Range[ws.Cells[cellNode.range.s_row, cellNode.range.s_col], ws.Cells[cellNode.range.e_row, cellNode.range.e_col]];
                if (cellNode.range.s_row != cellNode.range.e_row) {
                    range.Merge(false);
                } else {
                    range.Merge(true);
                }

                range.Cells.Font.Name = cellNode.font.FontName;
                range.Cells.Font.Size = cellNode.font.FontSize;
                range.Cells.Font.Bold = cellNode.font.FontBold;
                range.Cells.HorizontalAlignment = cellNode.font.hAlign;
                if (cellNode.range.color != 16777215)
                    range.Cells.Interior.Color = cellNode.range.color;

                if (cellNode.isNumber) {
                    range.NumberFormat = "#,##0";
                }

                if (!cellNode.isFormula)
                    ws.Cells[cellNode.range.s_row, cellNode.range.s_col] = cellNode.data;
                else
                    ws.Cells[cellNode.range.s_row, cellNode.range.s_col].Formula = cellNode.data;

            } else {
                ws.Cells[cellNode.range.s_row, cellNode.range.s_col].Font.Name = cellNode.font.FontName;
                ws.Cells[cellNode.range.s_row, cellNode.range.s_col].Font.Size = cellNode.font.FontSize;
                ws.Cells[cellNode.range.s_row, cellNode.range.s_col].Font.Bold = cellNode.font.FontBold;
                ws.Cells[cellNode.range.s_row, cellNode.range.s_col].HorizontalAlignment = cellNode.font.hAlign;
                if (cellNode.range.color != 16777215)
                    ws.Cells[cellNode.range.s_row, cellNode.range.s_col].Interior.Color = cellNode.range.color;

                if (cellNode.isNumber) {
                    ws.Cells[cellNode.range.s_row, cellNode.range.s_col].NumberFormat = "#,##0";
                }

                if (!cellNode.isFormula)
                    ws.Cells[cellNode.range.s_row, cellNode.range.s_col] = cellNode.data;
                else
                    ws.Cells[cellNode.range.s_row, cellNode.range.s_col].Formula = cellNode.data;
            }
        }

        private void drawBound(int s_row, int s_col, int e_row, int e_col) {
            Excel.Range range = ws.Range[ws.Cells[s_row, s_col], ws.Cells[e_row, e_col]];
            range.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            range.Borders.Weight = Excel.XlBorderWeight.xlMedium;
            range.BorderAround2(Type.Missing, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing);
        }

        private Color getColor(string color) {
            return ColorTranslator.FromHtml(color);
        }

        public override string GetExcelURL() {
            return excelURL;
        }

        public override void releasSheet() {
            ExcelManager.ReleaseExcel(ws);
        }
    }
}
