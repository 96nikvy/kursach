using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Organizer.Model.Events;
using System.IO;
using System.Windows;

namespace Organizer.Service.DataBase.Excel.Writing
{
    class WriterExcel : IWriterExcel
    {
        public bool WriteEventsToExcel(IEnumerable<EventModelBase> events, string path = null)
        {
            if (events == null) throw new ArgumentNullException("events");
            if (path == null)
            {
                path = PathFinder.GetPathToExcelDb();
                if (path == null)
                    throw new InvalidOperationException("path is null trying to get path to Excel Db");
            }

            File.Delete(path);

            // аааа сложнаааа дальше

            Microsoft.Office.Interop.Excel.Application oXL;
            Microsoft.Office.Interop.Excel._Workbook oWB;
            Microsoft.Office.Interop.Excel._Worksheet oSheet;
            Microsoft.Office.Interop.Excel.Range oRng;
            object misvalue = System.Reflection.Missing.Value;

            try
            {
                //Start Excel and get Application object.
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = true;

                //Get a new workbook.
                oWB = oXL.Workbooks.Add("");
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                //Add table headers going cell by cell.
                oSheet.Cells[1, 1] = "Имя события";
                oSheet.Cells[1, 2] = "Описание";
                oSheet.Cells[1, 3] = "Дата создания";
                oSheet.Cells[1, 4] = "Запланированная дата";

                //Format A1:D1 as bold, vertical alignment = center.
                oSheet.get_Range("A1", "D1").Font.Bold = true;
                oSheet.get_Range("A1", "D1").VerticalAlignment =
                    Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                // Create an array to multiple values at once.
                string[,] tableEvents = new string[events.Count(), 4];

                int row = 0;

                foreach (var z in events)
                {
                    for (int column = 0; column < 4; ++column)
                    {
                        if (column == 0)
                        {
                            tableEvents[row, column] = z.Name;
                        }
                        else if (column == 1)
                        {
                            tableEvents[row, column] = z.Description;
                        }
                        else if (column == 2)
                        {
                            tableEvents[row, column] = z.DatePlanned.ToShortDateString();
                        }
                        else if (column == 3)
                        {
                            tableEvents[row, column] = z.DatePlanned.ToShortDateString();
                        }
                    }
                    ++row;
                }

                oSheet.get_Range("A2", "D" + (events.Count()+1).ToString()).Value2 = tableEvents;

                oRng = oSheet.get_Range("A1", "D1");
                oRng.EntireColumn.AutoFit();

                oXL.Visible = false;
                oXL.UserControl = false;
                oWB.SaveAs(path, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
            false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                oWB.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return true;
        }
    }
}
