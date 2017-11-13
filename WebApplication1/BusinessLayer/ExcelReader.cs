using System.Collections.Generic;
using System.Linq;
//Add refrrences
using MySql.Data.MySqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
namespace KPI_Dashboard.BusinessLayer
{
    public class ExcelReader : IReader
    {
        static private DataTable dataTable = new DataTable();
        static private MySqlConnection connection = new MySqlConnection("SERVER=localhost;PORT=3306;DATABASE=db_kpi_dashboard;UID=root;PWD=password");
        static private Excel.Application excelApp;
        static private Excel.Workbook workBook;
        static private Excel.Worksheet worksheet;
        static private Excel.Range worksheetUsedRange;
        static private List<string> rowItemsList = new List<string>();
        static private List<string> columnsNameList = new List<string>();
        static string tableName;
        public ExcelReader()
        {
            excelApp = new Excel.Application();
            excelApp.Visible = true;
            excelApp.Workbooks.Open(@"C:\Users\Ryma\Desktop\New folder\Hope\WebApplication3\App_Data\test1.xlsx");
            workBook = excelApp.Workbooks["test1.xlsx"];
            worksheet = workBook.Sheets[1];
            worksheetUsedRange = worksheet.UsedRange;
            tableName = worksheet.Name;
            workBook.AfterSave += Workbook_AfterSave;
          
        }
       
        public void ReadExcel()
        {
            int FirstRow = 1;
            int FirstColumn = 1;
            for (int rowLoop = FirstRow; rowLoop <= worksheetUsedRange.Rows.Count; rowLoop++)
            {
                for (int columnLoop = FirstColumn; columnLoop <= worksheetUsedRange.Columns.Count; columnLoop++)
                {
                    if (rowLoop == 1)
                    {
                        if (worksheet.Cells[rowLoop, columnLoop] != null && worksheet.Cells[rowLoop, columnLoop].Value2 != null)
                        {
                           columnsNameList.Add(worksheet.Cells[rowLoop, columnLoop].Value2.ToString());
                        }
                    }
                    else
                    {
                        if (worksheet.Cells[rowLoop, columnLoop] != null && worksheet.Cells[rowLoop, columnLoop].Value2 != null)
                        {
                            rowItemsList.Add(worksheet.Cells[rowLoop, columnLoop].Value2.ToString());
                        }
                    }
                }
            }
        }
        public void FillColumnsInDataTable()
        {
            foreach (var columnName in columnsNameList)
            {
            dataTable.Columns.Add(columnName, typeof(string));
            }
        }
        //public void EnterDataInDatabase()
        //{
        //    EnterProduct();
        //    EnterRelease();
        //    EnterTestCoverage();
        //}
        public void FillRowsInDataTable()
        {
            int totalColumnInDataTable = dataTable.Columns.Count;
            int counter = rowItemsList.Count / totalColumnInDataTable;
            int InnerIiteration = 0;

            for (int i = 1; i <= counter; i++)
            {

             int columns = 0;
             DataRow newrow = dataTable.NewRow();
             while (InnerIiteration < (i * totalColumnInDataTable))
                {
                    string value = rowItemsList[InnerIiteration];
                    newrow[columns] = value;
                    columns++;
                    InnerIiteration++;
                }
             dataTable.Rows.Add(newrow);
            }
        }
        //public void EnterProduct()
        //{   
        //    var indexOfRequiredColumns = GetIndexOfRequiredColumns("product");
        //    string tableName = "product";
        //    InsertQuery(tableName, indexOfRequiredColumns);

        //}
        //public void EnterRelease()
        //{
        //    var indexOfRequiredColumns = GetIndexOfRequiredColumns( "release", "product");
        //    string tableName = "release";
        //    InsertQuery(tableName, indexOfRequiredColumns);

        //}
        //public void InsertQuery(string tableName, List<int> requiredColumnIndex)
        //{
        //    foreach (DataRow dataRow in dataTable.Rows)
        //    {
        //        string enterDataQuerry = "Insert ignore  INTO `" + tableName + "` values  ('";
        //        for (int i = 0; i <= requiredColumnIndex.Count - 1; i++)
        //        {
        //            if (i == requiredColumnIndex.Count - 1)
        //            {
        //                enterDataQuerry += dataRow[i] + "')";
        //            }
        //            else
        //            {
        //                enterDataQuerry += dataRow[i] + "','";
        //            }

        //        }
        //        ExecutetQuery(enterDataQuerry, connection);
        //    }
        //}
        //public void EnterTestCoverage()
        //{
        //    var indexOfRequiredColumns = GetIndexOfRequiredColumns("testcoverage", "product", "release" );
        //    string tableName = "testcoverage";
        //    InsertQuery(tableName, indexOfRequiredColumns);

        //}
        //public List<int> GetIndexOfRequiredColumns(params string[] arrayOfRequiredColumns)
        //{
        //   // int startingIndexOfArray = 0;
        //    var listOfIndexOfRequiredColumns = new List<int>();
        //    for (int i = 0; i <= arrayOfRequiredColumns.Length - 1; i++)
        //    {
        //        foreach (DataColumn column in dataTable.Columns)
        //        {
        //            if (column.ColumnName == arrayOfRequiredColumns[i])
        //            {
        //                int index = dataTable.Columns.IndexOf(column);
        //                listOfIndexOfRequiredColumns.Add(index);
        //            }
        //        }
        //    }
        //   return listOfIndexOfRequiredColumns;
        //}
        //static void ExecutetQuery(string query, MySqlConnection connection)
        //{
        //    var command = new MySqlCommand()
        //    {
        //    CommandType = CommandType.Text,
        //    CommandText = query,
        //    Connection = connection,
        //    };
        //    connection.Open();
        //    command.ExecuteNonQuery();
        //    connection.Close();
        //}
        private void Workbook_AfterSave(bool success)
        {
            rowItemsList = new List<string>();
            columnsNameList = new List<string>();
            dataTable = new DataTable();
            worksheetUsedRange=worksheet.UsedRange;
            Read();
        }

        public object Read()
        {
            ReadExcel();
            FillColumnsInDataTable();
            FillRowsInDataTable();
            return dataTable;
        }
    }
}
