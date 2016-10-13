using ExcelTemplateLib.DataModels;
using ExcelTemplateLib.Interfaces;
using ExcelTemplateLib.Managers;
using ExcelTemplateLib.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;


namespace ExcelTemplateLib.Templates {
    public class DetailExcelTemplateCreator : BaseExcelTemplate {
        private ExcelCellNode titleNode;
        private ExcelCellNode dateNode;
        private List<ExcelCellNode> headerNodes;
        private List<ExcelWidth> widths;
        
        public Excel.Worksheet ws { get; set; }
        private int sheetPage = 1;
        private IProgressTransfer progressTransfer;
        private List<TypeBaseDayModel> typeBaseDayModels;

        public DetailExcelTemplateCreator(int sheetPage, string excelUrl, DateTime dateTime)
            : base(excelUrl) {
            init(dateTime);
        }
        public DetailExcelTemplateCreator(int sheetPage, string excelUrl, DateTime dateTime, 
            List<TypeBaseDayModel> typeBaseDayModels, IProgressTransfer progressTransfer)
            : base(excelUrl) {
            this.progressTransfer = progressTransfer;
            this.typeBaseDayModels = typeBaseDayModels;
            init(dateTime);
        }

        private void init(DateTime dateTime) {
            initTitle();
            initDate(dateTime);
            initWidths();
            initHeaders();
            
        }

        private void initTitle() {
            this.titleNode = new ExcelCellNode("주,부식 검사 불출 대장",
                new ExcelCellRange(1, 1, 1, 6),
                new ExcelFont(24, true));
        }

        private void initDate(DateTime dateTime) {
            this.dateNode = new ExcelCellNode(dateTime.ToString("yyyy년 M월 d일 (ddd요일)"),
                new ExcelCellRange(3, 2, 3, 6),
                new ExcelFont(14, true, Excel.XlHAlign.xlHAlignLeft));
        }

        private void initHeaders() {
            headerNodes = new List<ExcelCellNode>();
            ExcelFont headerFont = new ExcelFont(11, true);
            Color yellowColor = getColor("#FFFF99");


            headerNodes.Add(new ExcelCellNode("식품명", new ExcelCellRange(5, 2), headerFont));
            headerNodes.Add(new ExcelCellNode("단위", new ExcelCellRange(5, 3), headerFont));
            headerNodes.Add(new ExcelCellNode("전 일 재 고", new ExcelCellRange(5, 4, 5, 6), headerFont));
            headerNodes.Add(new ExcelCellNode("금 일 구 입", new ExcelCellRange(5, 7, 5, 9), headerFont));
            headerNodes.Add(new ExcelCellNode("금 일 사 용", new ExcelCellRange(5, 10, 5, 12), headerFont));
            headerNodes.Add(new ExcelCellNode("금 일 재 고", new ExcelCellRange(5, 13, 5, 15), headerFont));
            headerNodes.Add(new ExcelCellNode("유통기한", new ExcelCellRange(5, 16), headerFont));

            headerNodes.Add(new ExcelCellNode("수량", new ExcelCellRange(6, 4, yellowColor), headerFont));
            headerNodes.Add(new ExcelCellNode("단가", new ExcelCellRange(6, 5), headerFont));
            headerNodes.Add(new ExcelCellNode("금액", new ExcelCellRange(6, 6), headerFont));
            headerNodes.Add(new ExcelCellNode("수량", new ExcelCellRange(6, 7, yellowColor), headerFont));
            headerNodes.Add(new ExcelCellNode("단가", new ExcelCellRange(6, 8), headerFont));
            headerNodes.Add(new ExcelCellNode("금액", new ExcelCellRange(6, 9), headerFont));
            headerNodes.Add(new ExcelCellNode("수량", new ExcelCellRange(6, 10, yellowColor), headerFont));
            headerNodes.Add(new ExcelCellNode("단가", new ExcelCellRange(6, 11), headerFont));
            headerNodes.Add(new ExcelCellNode("금액", new ExcelCellRange(6, 12), headerFont));
            headerNodes.Add(new ExcelCellNode("수량", new ExcelCellRange(6, 13, yellowColor), headerFont));
            headerNodes.Add(new ExcelCellNode("단가", new ExcelCellRange(6, 14), headerFont));
            headerNodes.Add(new ExcelCellNode("금액", new ExcelCellRange(6, 15), headerFont));
        }

        private void initWidths() {
            widths = new List<ExcelWidth>();
            widths.Add(new ExcelWidth(1, 3.5));
            widths.Add(new ExcelWidth(2, 8.56));
            widths.Add(new ExcelWidth(3, 4.78));
            widths.Add(new ExcelWidth(4, 5.70));
            widths.Add(new ExcelWidth(5, 9.11));
            widths.Add(new ExcelWidth(6, 11.89));
            widths.Add(new ExcelWidth(7, 4.67));
            widths.Add(new ExcelWidth(8, 8.11));
            widths.Add(new ExcelWidth(9, 10.89));
            widths.Add(new ExcelWidth(10, 4.67));
            widths.Add(new ExcelWidth(11, 8.11));
            widths.Add(new ExcelWidth(12, 10.89));
            widths.Add(new ExcelWidth(13, 6.67));
            widths.Add(new ExcelWidth(14, 8.11));
            widths.Add(new ExcelWidth(15, 10.89));
            widths.Add(new ExcelWidth(16, 6.78));
        }

        public override void WriteExcelData(Excel.Workbook wb) {
            try {
                ws = wb.Worksheets.get_Item(sheetPage) as Excel.Worksheet;

                setWidths();
                writeCell(titleNode);
                writeCell(dateNode);

                foreach (ExcelCellNode node in headerNodes) {
                    writeCell(node);
                }

                ws.Range[ws.Cells[5, 1], ws.Cells[5, 16]].Cells.Interior.Color = System.Drawing.ColorTranslator.ToOle(getColor("#C0C0C0"));

                setDatas(typeBaseDayModels);
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        private Color getColor(string color) {
            return ColorTranslator.FromHtml(color);
        }
        private void setDatas(List<TypeBaseDayModel> typeBaseDayModels) {
            int rowCount = 7;
           
            ExcelFont dataFont = new ExcelFont(11, false, Excel.XlHAlign.xlHAlignLeft);
            ExcelFont amountFont = new ExcelFont(11, false, Excel.XlHAlign.xlHAlignRight);
            ExcelFont numberFont = new ExcelFont(11, false, Excel.XlHAlign.xlHAlignRight);
            int maxRows = 0;
            int countRows = 0;
            foreach (TypeBaseDayModel typeDayModel in typeBaseDayModels) {
                foreach (KeyValuePair<string, DayFoodModel> entry in typeDayModel.dataList) {
                    maxRows++;
                }
            }
            List<int> totalRows = new List<int>();
            Color yellowColor = getColor("#FFFF99");
            Color blueColor = getColor("#CCFFFF");
            
            foreach (TypeBaseDayModel typeDayModel in typeBaseDayModels) {
                int startRow = rowCount;
                string type = typeDayModel.type;
                List<ExcelCellNode> typeCellNodes = new List<ExcelCellNode>();
                

                foreach (KeyValuePair<string, DayFoodModel> entry in typeDayModel.dataList) {
                    DayFoodModel dayFoodModel = entry.Value;
                    typeCellNodes.Add(new ExcelCellNode(dayFoodModel.food.name, new ExcelCellRange(rowCount, 2), dataFont));
                    typeCellNodes.Add(new ExcelCellNode(dayFoodModel.food.unit, new ExcelCellRange(rowCount, 3), dataFont));
                    if(dayFoodModel.remain != null){
                        typeCellNodes.Add(new ExcelCellNode(string.Format("{0:G}", dayFoodModel.remain.amount), new ExcelCellRange(rowCount, 4, yellowColor), amountFont));
                    } else {
                        typeCellNodes.Add(new ExcelCellNode(string.Format("{0:G}", 0), new ExcelCellRange(rowCount, 4, yellowColor), amountFont));
                    }
                    typeCellNodes.Add(new ExcelCellNode(string.Format("{0:G0}", dayFoodModel.food.unit_pirce), new ExcelCellRange(rowCount, 5), numberFont, false, true));
                    typeCellNodes.Add(new ExcelCellNode("=PRODUCT(D" + rowCount + ":" + "E" + rowCount + ")", new ExcelCellRange(rowCount, 6), numberFont, true, true));

                    if (dayFoodModel.buyTrn != null) {
                        typeCellNodes.Add(new ExcelCellNode(string.Format("{0:G}", dayFoodModel.buyTrn.amount), new ExcelCellRange(rowCount, 7, yellowColor), amountFont));
                        typeCellNodes.Add(new ExcelCellNode("=E" + rowCount, new ExcelCellRange(rowCount, 8), numberFont, true, true));
                        typeCellNodes.Add(new ExcelCellNode("=PRODUCT(H" + rowCount + ":" + "G" + rowCount + ")", new ExcelCellRange(rowCount, 9), numberFont, true, true));
                    } else {
                        typeCellNodes.Add(new ExcelCellNode("", new ExcelCellRange(rowCount, 7, yellowColor), amountFont));
                        typeCellNodes.Add(new ExcelCellNode("=E" + rowCount, new ExcelCellRange(rowCount, 8), numberFont, true, true));
                        typeCellNodes.Add(new ExcelCellNode("=PRODUCT(H" + rowCount + ":" + "G" + rowCount + ")", new ExcelCellRange(rowCount, 9), numberFont, true, true));
                    }


                    if (dayFoodModel.useTrn != null) {
                        typeCellNodes.Add(new ExcelCellNode(string.Format("{0:G}", dayFoodModel.useTrn.amount), new ExcelCellRange(rowCount, 10, yellowColor), amountFont));
                        typeCellNodes.Add(new ExcelCellNode("=E" + rowCount, new ExcelCellRange(rowCount, 11), numberFont, true, true));
                        typeCellNodes.Add(new ExcelCellNode("=PRODUCT(J" + rowCount + ":" + "K" + rowCount + ")", new ExcelCellRange(rowCount, 12), numberFont, true, true));
                    } else {
                        typeCellNodes.Add(new ExcelCellNode("", new ExcelCellRange(rowCount, 10, yellowColor), amountFont));
                        typeCellNodes.Add(new ExcelCellNode("=E" + rowCount, new ExcelCellRange(rowCount, 11), numberFont, true, true));
                        typeCellNodes.Add(new ExcelCellNode("=PRODUCT(J" + rowCount + ":" + "K" + rowCount + ")", new ExcelCellRange(rowCount, 12), numberFont, true, true));
                    }

                    typeCellNodes.Add(new ExcelCellNode("=D" + rowCount + "+G" + rowCount + "-J" + rowCount, new ExcelCellRange(rowCount, 13, yellowColor), amountFont, true));
                    typeCellNodes.Add(new ExcelCellNode("=E" + rowCount, new ExcelCellRange(rowCount, 14), numberFont, true, true));
                    typeCellNodes.Add(new ExcelCellNode("=PRODUCT(M" + rowCount + ":" + "N" + rowCount + ")", new ExcelCellRange(rowCount, 15), numberFont, true, true));

                    rowCount++;
                    countRows++;
                    int percent = (int)((double)countRows / (double)maxRows * (double)100);
                    progressTransfer.ReportProgress(this, percent);
                }
                typeCellNodes.Add(new ExcelCellNode("=SUM(F" + startRow + ":F" + (rowCount - 1) + ")", new ExcelCellRange(rowCount, 3, rowCount, 6, blueColor), numberFont, true, true));
                typeCellNodes.Add(new ExcelCellNode("=SUM(I" + startRow + ":I" + (rowCount - 1) + ")", new ExcelCellRange(rowCount, 7, rowCount, 9, blueColor), numberFont, true, true));
                typeCellNodes.Add(new ExcelCellNode("=SUM(L" + startRow + ":L" + (rowCount - 1) + ")", new ExcelCellRange(rowCount, 10, rowCount, 12, blueColor), numberFont, true, true));
                typeCellNodes.Add(new ExcelCellNode("=SUM(O" + startRow + ":O" + (rowCount - 1) + ")", new ExcelCellRange(rowCount, 13, rowCount, 15, blueColor), numberFont, true, true));

                typeCellNodes.Add(new ExcelCellNode(type, new ExcelCellRange(startRow, 1, (rowCount - 1), 1, blueColor), dataFont));
                typeCellNodes.Add(new ExcelCellNode("소 계", new ExcelCellRange(rowCount, 1, rowCount, 2, blueColor), dataFont));
                totalRows.Add(rowCount);
                rowCount++;
                foreach (ExcelCellNode node in typeCellNodes) {
                    writeCell(node);
                }
            }

            
            try {
                List<ExcelCellNode> cellNodes = new List<ExcelCellNode>();
                string exJego = "=";
                string buyJego = "=";
                string useJego = "=";
                string todayJego = "=";
                if (totalRows.Count > 1) {
                    for (int i = 1; i < totalRows.Count; i++) {
                        int rowNumber = totalRows[i];
                        if (i != 1) {
                            exJego += "+";
                            buyJego += "+";
                            useJego += "+";
                            todayJego += "+";
                        }
                        exJego += "C" + rowNumber;
                        buyJego += "G" + rowNumber;
                        useJego += "J" + rowNumber;
                        todayJego += "M" + rowNumber;
                    }
                    cellNodes.Add(new ExcelCellNode(exJego, new ExcelCellRange(rowCount, 3, rowCount, 6, blueColor), numberFont, true, true));
                    cellNodes.Add(new ExcelCellNode(buyJego, new ExcelCellRange(rowCount, 7, rowCount, 9, blueColor), numberFont, true, true));
                    cellNodes.Add(new ExcelCellNode(useJego, new ExcelCellRange(rowCount, 10, rowCount, 12, blueColor), numberFont, true, true));
                    cellNodes.Add(new ExcelCellNode(todayJego, new ExcelCellRange(rowCount, 13, rowCount, 15, blueColor), numberFont, true, true));
                    cellNodes.Add(new ExcelCellNode("부식누계", new ExcelCellRange(rowCount, 1, rowCount, 2, blueColor), dataFont));
                    rowCount++;
                } else {
                    cellNodes.Add(new ExcelCellNode("0", new ExcelCellRange(rowCount, 3, rowCount, 6, blueColor), numberFont, false, true));
                    cellNodes.Add(new ExcelCellNode("0", new ExcelCellRange(rowCount, 7, rowCount, 9, blueColor), numberFont, false, true));
                    cellNodes.Add(new ExcelCellNode("0", new ExcelCellRange(rowCount, 10, rowCount, 12, blueColor), numberFont, false, true));
                    cellNodes.Add(new ExcelCellNode("0", new ExcelCellRange(rowCount, 13, rowCount, 15, blueColor), numberFont, false, true));
                    cellNodes.Add(new ExcelCellNode("부식누계", new ExcelCellRange(rowCount, 1, rowCount, 2, blueColor), dataFont));
                    rowCount++;
                }
                int jusikRow = totalRows[0];
                cellNodes.Add(new ExcelCellNode("=C" + jusikRow + "+C" + (rowCount - 1), new ExcelCellRange(rowCount, 3, rowCount, 6, blueColor), numberFont, true, true));
                cellNodes.Add(new ExcelCellNode("=G" + jusikRow + "+G" + (rowCount - 1), new ExcelCellRange(rowCount, 7, rowCount, 9, blueColor), numberFont, true, true));
                cellNodes.Add(new ExcelCellNode("=J" + jusikRow + "+J" + (rowCount - 1), new ExcelCellRange(rowCount, 10, rowCount, 12, blueColor), numberFont, true, true));
                cellNodes.Add(new ExcelCellNode("=M" + jusikRow + "+M" + (rowCount - 1), new ExcelCellRange(rowCount, 13, rowCount, 15, blueColor), numberFont, true, true));

                cellNodes.Add(new ExcelCellNode("합 계", new ExcelCellRange(rowCount, 1, rowCount, 2, blueColor), dataFont));

                foreach (ExcelCellNode node in cellNodes) {
                    writeCell(node);
                }

                drawBound(5, 1, rowCount, 16);
            } catch(Exception e) {}
           
        }

        private void drawBound(int s_row, int s_col, int e_row, int e_col) {
            Excel.Range range = ws.Range[ws.Cells[s_row, s_col], ws.Cells[e_row, e_col]];
            range.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            range.Borders.Weight = Excel.XlBorderWeight.xlMedium;
            range.BorderAround2(Type.Missing, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing);
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

                if(cellNode.isNumber) {
                    range.NumberFormat = "#,##0";
                }

                if(!cellNode.isFormula)
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

        public override string GetExcelURL() {
            return excelURL;
        }

        public override void releasSheet() {
            ExcelManager.ReleaseExcel(ws);
        }
    }
}
